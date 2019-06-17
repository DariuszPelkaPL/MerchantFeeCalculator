using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine.Processor
{
    using MerchantFeeCalculationEngine.Model;

    public interface IProcessedTransactionWriter
    {
        string ConvertTransactionToTextEntry(ProcessedTransaction transactions);
    }
}
