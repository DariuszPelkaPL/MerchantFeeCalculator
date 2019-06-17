using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculationEngine.Processor
{
    public interface IMerchantParser
    {
        Merchant ParseMerchantEntry(string stringifiedMerchant);
    }
}
