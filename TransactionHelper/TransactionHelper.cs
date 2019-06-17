using System;
using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculator
{
    public class TransactionHelper
    {
        private MerchantFeeCalculationEngine.Model.Transaction tr;

        public Transaction ParseTransactionEntry(string stringifiedTransaction)
        {
            var transactionList = new Transaction();
            return transactionList;
        }

        public string ConvertTransactionToTextEntry(MerchantFeeCalculationEngine.Model.Transaction transactions)
        {
            return String.Empty;
        }
    }
}
