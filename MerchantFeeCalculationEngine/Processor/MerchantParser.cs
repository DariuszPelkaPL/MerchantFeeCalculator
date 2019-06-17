using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculationEngine.Processor
{
    public class MerchantParser : IMerchantParser
    {
        public Merchant ParseMerchantEntry(string stringifiedMerchant)
        {
            return new Merchant();
        }
    }
}
