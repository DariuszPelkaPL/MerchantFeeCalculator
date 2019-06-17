namespace MerchantFeeCalculationEngine.Processor
{
    using MerchantFeeCalculationEngine.Model;
    using System.Collections.Generic;

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction, decimal fee);

        IList<ProcessedTransaction> CalculateFees(IList<ProcessedTransaction> processedTransactions);
    }
}
