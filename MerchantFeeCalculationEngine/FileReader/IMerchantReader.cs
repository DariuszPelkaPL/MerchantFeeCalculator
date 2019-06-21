using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculation.Engine.Model;
using Danskebank.MerchantFeeCalculation.Engine.Processor;

namespace Danskebank.MerchantFeeCalculation.Engine.FileReader
{
    public interface IMerchantReader
    {
        IDictionary<string, Merchant> Read(StreamReader stream, IMerchantParser merchantParser);
    }
}
