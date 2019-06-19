using System.Collections.Generic;
using DankseBank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction);
    }
}
