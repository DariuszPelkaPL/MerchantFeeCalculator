using System;
using System.Collections.Generic;
using System.IO;
using Danskebank.Common;
using Danskebank.ConsoleAPI;
using Danskebank.MerchantFeeCalculationEngine.FileReader;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace Danskebank.MerchantFeeCalculator
{
    public class Program
    {
        private static ILogger logger = null;

        public static void Main(string[] args)
        {
            var merchantFile = string.Empty;
            var transactionstFile = string.Empty;
            IDictionary<string, Merchant> merchants = new Dictionary<string, Merchant>();
            DependencyInjector.Assign(typeof(ITransactionParser), typeof(TransactionParser));
            DependencyInjector.Assign(typeof(IMerchantParser), typeof(MerchantParser));
            DependencyInjector.Assign(typeof(IFeeCalculator), typeof(FeeCalculator));
            DependencyInjector.Assign(typeof(IProcessedTransactionWriter), typeof(ProcessedTransactionWriter));
            DependencyInjector.Assign(typeof(IMerchantReader), typeof(MerchantReader));
            DependencyInjector.Assign(typeof(ITransactionFileReader), typeof(TransactionFileReader));
            DependencyInjector.Assign(typeof(ILogger), typeof(Logger));
            DependencyInjector.Assign(typeof(IMerchantsProcessor), typeof(MerchantsProcessor));
            logger = (ILogger)DependencyInjector.CreateInstance(typeof(ILogger));

            if (args == null || args.Length == 0)
            {
                merchantFile = "merchants.txt";
                transactionstFile = "transactions.txt";
            }
            else if (args.Length == 2)
            {
                merchantFile = args[0];
                transactionstFile = args[1];
            }
            else
            {
                throw new ArgumentException("Wrong parameters");
            }

            var merchantProcessor = (IMerchantsProcessor)DependencyInjector.CreateInstance(typeof(IMerchantsProcessor));
            merchants = merchantProcessor.ReadMerchants(merchantFile);
            ReadTransactions(transactionstFile, merchants);

            Console.ReadLine();
        }

        private static IDictionary<string, Merchant> ReadMerchants(string merchantFile)
        {
            IDictionary<string, Merchant> merchants = new Dictionary<string, Merchant>();

            if (!string.IsNullOrEmpty(merchantFile))
            {
                try
                {
                    if (File.Exists(merchantFile))
                    {
                        var merchantParser =
                            (IMerchantParser)DependencyInjector.CreateInstance(typeof(IMerchantParser));
                        var merchantReader =
                            (IMerchantReader)DependencyInjector.CreateInstance(typeof(IMerchantReader));

                        StreamReader file = new StreamReader(merchantFile);
                        merchants = merchantReader.Read(file, merchantParser);
                        file.Close();
                    }
                    else
                    {
                        throw new ArgumentException("Merchant file does not exist");
                    }
                }
                catch (Exception exception)
                {
                    string message = $"Error while processing fees: {exception.Message}";
                    logger.WriteError(message);
                    Console.WriteLine("Error while processing input data");
                }
            }

            return merchants;
        }

        private static void ReadTransactions(string transactionstFile, IDictionary<string, Merchant> merchants)
        {
            try
            {
                if (!string.IsNullOrEmpty(transactionstFile))
                {
                    if (File.Exists(transactionstFile))
                    {
                        StreamReader file = new StreamReader(transactionstFile);
                        var calculator = (IFeeCalculator)DependencyInjector.CreateInstance(typeof(IFeeCalculator));
                        var transactionParser =
                            (ITransactionParser)DependencyInjector.CreateInstance(typeof(ITransactionParser));
                        var processedTransactionWriter =
                            (IProcessedTransactionWriter)DependencyInjector.CreateInstance(
                                typeof(IProcessedTransactionWriter));
                        var transactionReader =
                            (ITransactionFileReader)DependencyInjector.CreateInstance(typeof(ITransactionFileReader));

                        var monthNumber = 0;
                        Transaction transaction;
                        calculator.InitializeFeeCalculation();

                        while ((transaction = transactionReader.ReadSingleEntry(file, merchants, transactionParser)) != null)
                        {
                            var processedTransaction = calculator.CalculateFee(transaction);

                            if (monthNumber != 0 && monthNumber != processedTransaction.RelatedTransaction.DoneOn.Month)
                            {
                                Console.WriteLine("\n");
                            }

                            var stringifiedTransaction =
                                processedTransactionWriter.ConvertTransactionToTextEntry(processedTransaction);
                            Console.WriteLine(stringifiedTransaction);
                            monthNumber = processedTransaction.RelatedTransaction.DoneOn.Month;
                        }
                        Console.WriteLine("\n");
                        file.Close();
                    }
                    else
                    {
                        throw new ArgumentException("Transaction file does not exist");
                    }
                }
            }
            catch (Exception exception)
            {
                string message = $"Error while processing fees: {exception.Message}";
                logger.WriteError(message);
                Console.WriteLine("Error while processing input data");
            }
        }
    }

}