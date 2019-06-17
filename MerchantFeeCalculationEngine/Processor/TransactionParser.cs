using MerchantFeeCalculationEngine.Model;
namespace MerchantFeeCalculationEngine.Processor
{
    public class TransactionParser : ITransactionParser
    {
        public Transaction ParseTransactionEntry(string stringifiedTransaction)
        {
            var transactionList = new Transaction();
            return transactionList;
        }
    }
}
