using System;
using System.Collections.Generic;
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
                var fileHelper =
                    (IFileHelper)DependencyInjector.CreateInstance(typeof(IFileHelper));

                if (!string.IsNullOrEmpty(transactionstFile))
                {
                    if (fileHelper.FileExists(transactionstFile))
                    {

                        var consoleHelper =
                            (IConsoleHelper)DependencyInjector.CreateInstance(typeof(IConsoleHelper));
                        var calculator = (IFeeCalculator)DependencyInjector.CreateInstance(typeof(IFeeCalculator));
                        var transactionParser =
                            (ITransactionParser)DependencyInjector.CreateInstance(typeof(ITransactionParser));
                        var processedTransactionWriter =
                            (IProcessedTransactionWriter)DependencyInjector.CreateInstance(
                                typeof(IProcessedTransactionWriter));
                        var transactionReader =
                            (ITransactionFileReader)DependencyInjector.CreateInstance(typeof(ITransactionFileReader));

                        var file = fileHelper.OpenFile(transactionstFile);
                        var monthNumber = 0;
                        Transaction transaction;
                        calculator.InitializeFeeCalculation();

                        while ((transaction = transactionReader.ReadSingleEntry(file, merchants, transactionParser)) != null)
                        {
                            var processedTransaction = calculator.CalculateFee(transaction);

                            if (monthNumber != 0 && monthNumber != processedTransaction.RelatedTransaction.DoneOn.Month)
                            {
                                consoleHelper.WriteLine("\n");
                            }

                            var stringifiedTransaction =
                                processedTransactionWriter.ConvertTransactionToTextEntry(processedTransaction);
                            consoleHelper.WriteLine(stringifiedTransaction);
                            monthNumber = processedTransaction.RelatedTransaction.DoneOn.Month;
                        }
                        consoleHelper.WriteLine("\n");
                        fileHelper.CloseFile(file);
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
            DependencyInjector.Assign(typeof(IFileHelper), typeof(FileHelper));
            DependencyInjector.Assign(typeof(IConsoleHelper), typeof(ConsoleHelper));
        }
    }
}
