using System;
using System.Collections.Generic;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculation.ConsoleAPI;
using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.ConsoleApp
{
    public class Program
    {
        private static ILogger logger = null;

        public static void Main(string[] args)
        {
            try
            {
                IDictionary<string, Merchant> merchants = new Dictionary<string, Merchant>();
                DependencyInjector.Assign(typeof(ILogger), typeof(Logger));
                DependencyInjector.Assign(typeof(IMerchantsProcessor), typeof(MerchantsProcessor));
                DependencyInjector.Assign(typeof(ITransactionsProcessor), typeof(TransactionsProcessor));
                DependencyInjector.Assign(typeof(IConsoleHelper), typeof(ConsoleHelper));
                DependencyInjector.Assign(typeof(IFileHelper), typeof(FileHelper));

                logger = (ILogger)DependencyInjector.CreateInstance(typeof(ILogger));
                var merchantFile = "merchants.txt";
                var transactionstFile = "transactions.txt";

                var transactionsProcessor = (ITransactionsProcessor)DependencyInjector.CreateInstance(typeof(ITransactionsProcessor));
                transactionsProcessor.FileHelperProperty = (IFileHelper)DependencyInjector.CreateInstance(typeof(IFileHelper));
                transactionsProcessor.ConsoleHelperProperty = (IConsoleHelper)DependencyInjector.CreateInstance(typeof(IConsoleHelper));
                var merchantProcessor = (IMerchantsProcessor)DependencyInjector.CreateInstance(typeof(IMerchantsProcessor));
                merchantProcessor.FileHelperProperty = (IFileHelper)DependencyInjector.CreateInstance(typeof(IFileHelper));
                merchantProcessor.ConsoleHelperProperty = (IConsoleHelper)DependencyInjector.CreateInstance(typeof(IConsoleHelper));
                merchants = merchantProcessor.ReadMerchants(merchantFile);
                transactionsProcessor.ReadTransactions(transactionstFile, merchants);
                Console.ReadLine();
            }
            catch (Exception exc)
            {
                var logger = (ILogger)DependencyInjector.CreateInstance(typeof(ILogger));
                logger.WriteError(exc.ToString());
                Console.WriteLine("Error while processing fees");
            }
        }
    }
}