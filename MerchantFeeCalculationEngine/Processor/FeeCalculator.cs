using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculationEngine.Processor
{
    public class FeeCalculator : IFeeCalculator
    {
        public ProcessedTransaction CalculateFee(Transaction transaction, decimal fee)
        {
            var processedTransaction = new ProcessedTransaction(transaction);
            processedTransaction.Fee = processedTransaction.RelatedTransaction.Amount
                                       * ((1 - (processedTransaction.RelatedTransaction.Owner.DiscountPercentage / 100)) * (processedTransaction.RelatedTransaction.Owner.FeeAsPercentage / 100));
            return processedTransaction;
        }
    }
}
