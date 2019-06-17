using System;
using MerchantFeeCalculationEngine.Model;

namespace MerchantFeeCalculationEngine.Processor
{
    public class ProcessedTransactionWriter : IProcessedTransactionWriter
    {
        public string ConvertTransactionToTextEntry(ProcessedTransaction transaction)
        {
            return $"{transaction.Fee,5}";
        }
    }
}
