using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;

namespace Danskebank.MerchantFeeCalculationEngine.FileReader
{
    public interface ITransactionFileReader
    {
        Transaction ReadSingleEntry(StreamReader stream, IDictionary<string, Merchant> merchants, ITransactionParser transactionParser);
    }
}
