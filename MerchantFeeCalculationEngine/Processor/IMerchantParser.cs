using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public interface IMerchantParser
    {
        Merchant ParseMerchantEntry(string stringifiedMerchant);
    }
}
