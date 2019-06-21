using System.Collections.Generic;
using Danskebank.MerchantFeeCalculation.Engine.Model;
using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.Engine.Processor
{

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction);

        void InitializeFeeCalculation();
    }
}
