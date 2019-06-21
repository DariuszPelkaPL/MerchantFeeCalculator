using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.Engine.Processor
{
    public interface IProcessedTransactionWriter
    {
        string ConvertTransactionToTextEntry(ProcessedTransaction transactions);
    }
}
