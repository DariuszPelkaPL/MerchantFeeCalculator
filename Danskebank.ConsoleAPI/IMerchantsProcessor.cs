using System.Collections.Generic;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.ConsoleAPI
{
    public interface IMerchantsProcessor
    {
        IDictionary<string, Merchant> ReadMerchants(string merchantFile);
    }
}
