using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculationEngine.Processor
{
    public class FeeCalculator : IFeeCalculator
    {
        public ProcessedTransaction CalculateFee(Transaction transaction, decimal fee)
        {
            var processedTransaction = new ProcessedTransaction(transaction);
            processedTransaction.Fee = processedTransaction.RelatedTransaction.Amount
                                       * (processedTransaction.RelatedTransaction.Owner.FeeAsPercentage / 100);
            return processedTransaction;
        }
    }
}
