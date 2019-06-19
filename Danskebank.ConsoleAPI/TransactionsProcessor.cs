﻿using System;
using System.Collections.Generic;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculationEngine.FileReader;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace Danskebank.ConsoleAPI
{
    public class TransactionsProcessor : ITransactionsProcessor
    {
        public IConsoleHelper ConsoleHelperProperty
        {
            get;
            set;
        }

        public IFileHelper FileHelperProperty
        {
            get;
            set;
        }

        public void ReadTransactions(string transactionstFile, IDictionary<string, Merchant> merchants)
        {
            try
            {
                if (!string.IsNullOrEmpty(transactionstFile))
                {
                    if (FileHelperProperty.FileExists(transactionstFile))
                    {
                        var calculator = (IFeeCalculator)DependencyInjector.CreateInstance(typeof(IFeeCalculator));
                        var transactionParser =
                            (ITransactionParser)DependencyInjector.CreateInstance(typeof(ITransactionParser));
                        var processedTransactionWriter =
                            (IProcessedTransactionWriter)DependencyInjector.CreateInstance(
                                typeof(IProcessedTransactionWriter));
                        var transactionReader =
                            (ITransactionFileReader)DependencyInjector.CreateInstance(typeof(ITransactionFileReader));

                        var file = FileHelperProperty.OpenFile(transactionstFile);
                        var monthNumber = 0;
                        Transaction transaction;
                        calculator.InitializeFeeCalculation();

                        while ((transaction = transactionReader.ReadSingleEntry(file, merchants, transactionParser)) != null)
                        {
                            var processedTransaction = calculator.CalculateFee(transaction);

                            if (monthNumber != 0 && monthNumber != processedTransaction.RelatedTransaction.DoneOn.Month)
                            {
                                ConsoleHelperProperty.WriteLine("\n");
                            }

                            var stringifiedTransaction =
                                processedTransactionWriter.ConvertTransactionToTextEntry(processedTransaction);
                            ConsoleHelperProperty.WriteLine(stringifiedTransaction);
                            monthNumber = processedTransaction.RelatedTransaction.DoneOn.Month;
                        }
                        ConsoleHelperProperty.WriteLine("\n");
                        FileHelperProperty.CloseFile(file);
                    }
                    else
                    {
                        ConsoleHelperProperty.WriteLine("No transaction file");
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
