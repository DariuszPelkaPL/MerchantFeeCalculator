using DankseBank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public class ProcessedTransactionWriter : IProcessedTransactionWriter
    {
        public string ConvertTransactionToTextEntry(ProcessedTransaction transaction)
        {
            var stringifiedFee = transaction.Fee.ToString("0.00");
            var stringifiedDate = transaction.RelatedTransaction.DoneOn.ToString("yyyy-MM-dd");
            return $"{stringifiedDate,10} {transaction.RelatedTransaction.Owner.Name,8} {stringifiedFee,3}";
        }
    }
}
