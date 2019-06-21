using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculation.Engine.Model;
using Danskebank.MerchantFeeCalculation.Engine.Processor;

namespace Danskebank.MerchantFeeCalculation.Engine.FileReader
{
    public interface ITransactionFileReader
    {
        Transaction ReadSingleEntry(StreamReader stream, IDictionary<string, Merchant> merchants, ITransactionParser transactionParser);
    }
}
