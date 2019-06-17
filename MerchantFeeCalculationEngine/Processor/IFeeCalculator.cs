namespace MerchantFeeCalculationEngine.Processor
{
    using MerchantFeeCalculationEngine.Model;

    public interface IFeeCalculator
    {
        ProcessedTransaction CalculateFee(Transaction transaction, decimal fee);
    }
}
