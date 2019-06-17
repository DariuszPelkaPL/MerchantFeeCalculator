using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine.Processor
{
    public interface IProcessedTransactionWriter
    {
        string ConvertTransactionToTextEntry(MerchantFeeCalculationEngine.Model.Transaction transactions);
    }
}
