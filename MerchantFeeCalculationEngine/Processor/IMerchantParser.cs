using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.Engine.Processor
{
    public interface IMerchantParser
    {
        Merchant ParseMerchantEntry(string stringifiedMerchant);
    }
}
