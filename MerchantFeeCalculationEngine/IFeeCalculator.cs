using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine
{
    using MerchantFeeCalculationEngine.Model;

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction, decimal fee);
    }
}
