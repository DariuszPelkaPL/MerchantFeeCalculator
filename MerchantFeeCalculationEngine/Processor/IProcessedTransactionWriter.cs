using DankseBank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public interface IProcessedTransactionWriter
    {
        string ConvertTransactionToTextEntry(ProcessedTransaction transactions);
    }
}
