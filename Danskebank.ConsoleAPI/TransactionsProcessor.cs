using System;
using System.Collections.Generic;
using System.IO;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculationEngine.FileReader;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace Danskebank.ConsoleAPI
{
    public class TransactionsProcessor : ITransactionsProcessor
    {
        public void ReadTransactions(string transactionstFile, IDictionary<string, Merchant> merchants)
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
                //logger.WriteError(message);
                Console.WriteLine("Error while processing input data");
            }
        }

        public void InitializeProcessing()
        {
            DependencyInjector.Assign(typeof(ITransactionParser), typeof(TransactionParser));
            DependencyInjector.Assign(typeof(IMerchantParser), typeof(MerchantParser));
            DependencyInjector.Assign(typeof(IFeeCalculator), typeof(FeeCalculator));
            DependencyInjector.Assign(typeof(IProcessedTransactionWriter), typeof(ProcessedTransactionWriter));
            DependencyInjector.Assign(typeof(IMerchantReader), typeof(MerchantReader));
            DependencyInjector.Assign(typeof(ITransactionFileReader), typeof(TransactionFileReader));
        }
    }
}
