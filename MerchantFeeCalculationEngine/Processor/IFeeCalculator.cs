using System.Collections.Generic;
using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculationEngine.Processor
{

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction);

        IList<ProcessedTransaction> CalculateMonthlyFees(IList<Transaction> transactions);
    }
}
