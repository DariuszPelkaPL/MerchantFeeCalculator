namespace MerchantFeeCalculationEngine.Processor
{
    using MerchantFeeCalculationEngine.Model;
    using System.Collections.Generic;

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction);

        IList<ProcessedTransaction> CalculateMonthlyFees(IList<Transaction> transactions);
    }
}
