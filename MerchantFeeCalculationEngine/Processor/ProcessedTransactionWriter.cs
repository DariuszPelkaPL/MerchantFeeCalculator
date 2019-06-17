using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine.Processor
{
    public class ProcessedTransactionWriter : IProcessedTransactionWriter
    {
        public string ConvertTransactionToTextEntry(MerchantFeeCalculationEngine.Model.Transaction transactions)
        {
            return String.Empty;
        }
    }
}
