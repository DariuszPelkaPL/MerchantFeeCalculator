namespace Danskebank.MerchantFeeCalculationEngine.Model
{
    public class ProcessedTransaction 
    {
        private Transaction _trnsaction = null;

        public ProcessedTransaction(Transaction transaction)
        {
            this._trnsaction = transaction;
        }

        public Transaction RelatedTransaction
        {
            get
            {
                return _trnsaction;
            }
        }

        public decimal Fee
        {
            get; set;
        }
    }
}
