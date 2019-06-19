using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DanskeBank.MerchantFeeCalculationEngine.FileReader
{
    public interface ITransactionFileReader
    {
        Transaction ReadSingleEntry(StreamReader stream, IDictionary<string, Merchant> merchants, ITransactionParser transactionParser);
    }
}
