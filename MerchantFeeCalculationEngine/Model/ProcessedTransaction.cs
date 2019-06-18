using Danskebank.MerchantFeeCalculationEngine.Model;

namespace DankseBank.MerchantFeeCalculationEngine.Model
{
    public class ProcessedTransaction 
    {
        public ProcessedTransaction(Transaction transaction)
        {
            this._trnsaction = transaction;
        }

        private Transaction _trnsaction = null;
        private decimal _fee = 0;

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
