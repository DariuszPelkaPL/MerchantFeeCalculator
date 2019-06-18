using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace DanskeBank.MerchantFeeCalculationEngine.FileReader
{
    public interface IMerchantReader
    {
        IDictionary<string, Merchant> Read(StreamReader stream, IMerchantParser merchantParser);
    }
}
