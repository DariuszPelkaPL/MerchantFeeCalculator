using System;
using System.Collections.Generic;
using Danskebank.Common;
using Danskebank.ConsoleAPI;
using Danskebank.MerchantFeeCalculationEngine.Model;

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
            DependencyInjector.Assign(typeof(ILogger), typeof(Logger));
            DependencyInjector.Assign(typeof(IMerchantsProcessor), typeof(MerchantsProcessor));
            DependencyInjector.Assign(typeof(ITransactionsProcessor), typeof(TransactionsProcessor));
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

            var transactionsProcessor = (ITransactionsProcessor)DependencyInjector.CreateInstance(typeof(ITransactionsProcessor));
            transactionsProcessor.InitializeProcessing();
            var merchantProcessor = (IMerchantsProcessor)DependencyInjector.CreateInstance(typeof(IMerchantsProcessor));
            merchants = merchantProcessor.ReadMerchants(merchantFile);
            transactionsProcessor.ReadTransactions(transactionstFile, merchants);

            Console.ReadLine();
        }
    }
}