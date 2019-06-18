﻿using System;
using Common.SimpleDependencyInjector;
using MerchantFeeCalculationEngine.Model;
using MerchantFeeCalculationEngine.Processor;

namespace MerchantFeeCalculator
{
    using System.Collections.Generic;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            var merchantFile = string.Empty;
            var transactionstFile = string.Empty;
            var merchants = new Dictionary<string, Merchant>();

            if (args == null || args.Length == 0)
            {
                merchantFile = "SampleMerchants.txt";
                transactionstFile = "SampleTestTransactions.txt";
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

            DependencyInjector.Assign(typeof(ITransactionParser), typeof(TransactionParser));
            DependencyInjector.Assign(typeof(IMerchantParser), typeof(MerchantParser));
            DependencyInjector.Assign(typeof(IFeeCalculator), typeof(FeeCalculator));
            DependencyInjector.Assign(typeof(IProcessedTransactionWriter), typeof(ProcessedTransactionWriter));

            if (!string.IsNullOrEmpty(merchantFile))
            {
                if (File.Exists(merchantFile))
                {
                    var merchantParser = (IMerchantParser)DependencyInjector.CreateInstance(typeof(IMerchantParser));
                    string line;

                    // Read the file and display it line by line.  
                    StreamReader file =
                        new StreamReader(merchantFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        var merchant = merchantParser.ParseMerchantEntry(line);
                        merchants.Add(merchant.Name, merchant);
                    }

                    file.Close();
                }
                else
                {
                    throw new ArgumentException("Merchant file does not exist");
                }
            }

            if (!string.IsNullOrEmpty(transactionstFile))
            {
                if (File.Exists(merchantFile))
                {
                    var calculator = (IFeeCalculator)DependencyInjector.CreateInstance(typeof(IFeeCalculator));
                    var transactionParser = (ITransactionParser)DependencyInjector.CreateInstance(typeof(ITransactionParser));
                    var processedTransactionWriter = (IProcessedTransactionWriter)DependencyInjector.CreateInstance(typeof(IProcessedTransactionWriter));
                    string line;

                    // Read the file and display it line by line.  
                    StreamReader file =
                        new StreamReader(transactionstFile);
                    while ((line = file.ReadLine()) != null)
                    {
                        var transaction = transactionParser.ParseTransactionEntry(line);
                        transaction.Owner = merchants[transaction.Owner.Name];

                        if (calculator != null)
                        {
                            var processedTransaction = calculator.CalculateFee(transaction);
                            var stringifiedProcessedTransaction = processedTransactionWriter.ConvertTransactionToTextEntry(processedTransaction);
                            Console.WriteLine(stringifiedProcessedTransaction);
                        }
                    }
                    file.Close();
                }
                else
                {
                    throw new ArgumentException("Transaction file does not exist");
                }
            }
        }
    }
}
