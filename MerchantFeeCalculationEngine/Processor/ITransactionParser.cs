using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.Engine.Processor
{
    public interface ITransactionParser
    {
        Transaction ParseTransactionEntry(string stringifiedTransaction);
    }
}
