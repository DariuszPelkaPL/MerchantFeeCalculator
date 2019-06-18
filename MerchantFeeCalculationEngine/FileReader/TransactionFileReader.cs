using System.Collections.Generic;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace DanskeBank.MerchantFeeCalculationEngine.FileReader
{
    using System.IO;

    using Danskebank.MerchantFeeCalculationEngine.Processor;

    public class TransactionFileReader : ITransactionFileReader
    {
        public IList<Transaction> Read(StreamReader stream, IDictionary<string, Merchant> merchants, ITransactionParser transactionParser)
        {
            string line;
            var transactions = new List<Transaction>();
            while ((line = stream.ReadLine()) != null)
            {
                var transaction = transactionParser.ParseTransactionEntry(line);
                transaction.Owner = merchants[transaction.Owner.Name];
                transactions.Add(transaction);
            }

            return transactions;
        }
    }
}
