using System;
using System.Globalization;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public class TransactionParser : ITransactionParser
    {
        public Transaction ParseTransactionEntry(string stringifiedTransaction)
        {
            var stringifiedTransactionDate = stringifiedTransaction.Substring(0, 10);
            DateTime transactionDate;
            if (!DateTime.TryParseExact(stringifiedTransactionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out transactionDate))
            {
                throw new ArgumentException("Improper format of transaction date");
            }

            decimal amount;
            var stringifiedTransactionAmount = stringifiedTransaction.Substring(20, 3);
            if (!decimal.TryParse(stringifiedTransactionAmount, out amount))
            {
                throw new ArgumentException("Improper format of transaction amount");
            }
            var merchant = stringifiedTransaction.Substring(11, 8);
            var transaction = new Transaction() { Amount = amount, Owner = new Merchant() { Name = merchant}, DoneOn = transactionDate};
            return transaction;
        }
    }
}
