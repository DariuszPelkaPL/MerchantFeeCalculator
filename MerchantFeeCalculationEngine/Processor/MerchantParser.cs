using MerchantFeeCalculationEngine.Model;
using System;

namespace MerchantFeeCalculationEngine.Processor
{
    public class MerchantParser : IMerchantParser
    {
        public Merchant ParseMerchantEntry(string stringifiedMerchant)
        {
            decimal discount;
            var stringifiedDiscount = stringifiedMerchant.Substring(11, 2);
            if (!decimal.TryParse(stringifiedDiscount, out discount))
            {
                throw new ArgumentException("Improper format of discount");
            }

            decimal fee;
            var stringifiedFee = stringifiedMerchant.Substring(9, 1);
            if (!decimal.TryParse(stringifiedFee, out fee))
            {
                throw new ArgumentException("Improper format of fee");
            }

            var name = stringifiedMerchant.Substring(0, 8);
            var merchant = new Merchant() { Name = name, DiscountPercentage = discount, FeeAsPercentage = fee};

            return merchant;
        }
    }
}
