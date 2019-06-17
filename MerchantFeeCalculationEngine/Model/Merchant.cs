using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine.Model
{
    public class Merchant
    {
        public string Name { get; set; }

        public decimal FeeAsPercentage { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
