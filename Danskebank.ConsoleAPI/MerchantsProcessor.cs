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
        public IDictionary<string, Merchant> ReadMerchants(string merchantFile)
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
                   // logger.WriteError(message);
                    Console.WriteLine("Error while processing input data");
                }
            }

            return merchants;
        }

    }
}
