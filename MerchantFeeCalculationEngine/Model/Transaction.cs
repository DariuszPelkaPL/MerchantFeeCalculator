using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine.Model
{
    public class Transaction
    {
        public Merchant Owner { get; set; }

        public DateTime DoneOn { get; set; }

        public decimal Amount { get; set; }
    }
}
