using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public interface ITransactionParser
    {
        Transaction ParseTransactionEntry(string stringifiedTransaction);
    }
}
