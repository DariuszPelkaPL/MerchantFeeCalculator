using System;

namespace Danskebank.MerchantFeeCalculation.Engine.Model
{
    public class Transaction
    {
        public Merchant Owner { get; set; }

        public DateTime DoneOn { get; set; }

        public decimal Amount { get; set; }
    }
}
