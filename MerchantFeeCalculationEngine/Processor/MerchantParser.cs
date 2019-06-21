using System;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public class MerchantParser : IMerchantParser
    {
        private readonly int merchantEntryLength = 13;

        public Merchant ParseMerchantEntry(string stringifiedMerchant)
        {
            decimal discount;
            if (stringifiedMerchant.Length != merchantEntryLength)
            {
                throw new ArgumentException("Improper format of merchant data");
            }
            var stringifiedDiscount = stringifiedMerchant.Substring(11, 2);
            if (!decimal.TryParse(stringifiedDiscount, out discount))
            {
                throw new ArgumentException("Improper format of merchant data");
            }

            decimal fee;
            var stringifiedFee = stringifiedMerchant.Substring(9, 1);
            if (!decimal.TryParse(stringifiedFee, out fee))
            {
                throw new ArgumentException("Improper format of merchant data");
            }

            var name = stringifiedMerchant.Substring(0, 8);
            var merchant = new Merchant() { Name = name, DiscountPercentage = discount, FeeAsPercentage = fee};

            return merchant;
        }
    }
}
