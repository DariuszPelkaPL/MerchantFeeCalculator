using System.Collections.Generic;
using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.Engine.FileReader
{
    using System.ComponentModel;
    using System.IO;

    using Danskebank.MerchantFeeCalculation.Engine.Processor;

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
