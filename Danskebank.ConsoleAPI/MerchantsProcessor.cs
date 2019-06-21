using System;
using System.Collections.Generic;
using System.IO;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculationEngine.FileReader;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace Danskebank.ConsoleAPI
{
    public class MerchantsProcessor : IMerchantsProcessor
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

        public IDictionary<string, Merchant> ReadMerchants(string merchantFile)
        {
            DependencyInjector.Assign(typeof(ITransactionParser), typeof(TransactionParser));
            DependencyInjector.Assign(typeof(IMerchantParser), typeof(MerchantParser));
            DependencyInjector.Assign(typeof(IFeeCalculator), typeof(FeeCalculator));
            DependencyInjector.Assign(typeof(IProcessedTransactionWriter), typeof(ProcessedTransactionWriter));
            DependencyInjector.Assign(typeof(IMerchantReader), typeof(MerchantReader));
            DependencyInjector.Assign(typeof(ITransactionFileReader), typeof(TransactionFileReader));
            DependencyInjector.Assign(typeof(ILogger), typeof(Logger));

            var logger = (ILogger)DependencyInjector.CreateInstance(typeof(ILogger));
            IDictionary<string, Merchant> merchants = new Dictionary<string, Merchant>();

            if (!string.IsNullOrEmpty(merchantFile))
            {
                try
                {
                    if (FileHelperProperty.FileExists(merchantFile))
                    {
                        var merchantParser =
                            (IMerchantParser)DependencyInjector.CreateInstance(typeof(IMerchantParser));
                        var merchantReader =
                            (IMerchantReader)DependencyInjector.CreateInstance(typeof(IMerchantReader));

                        var file = FileHelperProperty.OpenFile(merchantFile);
                        merchants = merchantReader.Read(file, merchantParser);
                        FileHelperProperty.CloseFile(file);
                    }
                    else
                    {
                        ConsoleHelperProperty.WriteLine("No merchant file");
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

    }
}
