using System.Collections.Generic;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace DanskeBank.MerchantFeeCalculationEngine.FileReader
{
    using System.ComponentModel;
    using System.IO;

    using Danskebank.MerchantFeeCalculationEngine.Processor;

    public class TransactionFileReader : ITransactionFileReader
    {
        public Transaction ReadSingleEntry(StreamReader stream, IDictionary<string, Merchant> merchants, ITransactionParser transactionParser)
        {
            string line;

            if ((line = stream.ReadLine()) != null)
            {
                var transaction = transactionParser.ParseTransactionEntry(line);
                transaction.Owner = merchants[transaction.Owner.Name];
                return transaction;
            }
            else
            {
                return default(Transaction);
            }
        }
    }
}
