﻿namespace Danskebank.MerchantFeeCalculation.Engine.Model
{
    public class Merchant
    {
        public string Name { get; set; }

        public decimal FeeAsPercentage { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
