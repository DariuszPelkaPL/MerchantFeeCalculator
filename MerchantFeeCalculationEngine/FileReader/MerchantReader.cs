using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace Danskebank.MerchantFeeCalculationEngine.FileReader
{
    public class MerchantReader : IMerchantReader
    {
        public IDictionary<string, Merchant> Read(StreamReader stream, IMerchantParser merchantParser)
        {
            string line;
            var merchants = new Dictionary<string, Merchant>();

            while ((line = stream.ReadLine()) != null)
            {
                var merchant = merchantParser.ParseMerchantEntry(line);
                merchants.Add(merchant.Name, merchant);
            }

            return merchants;
        }
    }
}
