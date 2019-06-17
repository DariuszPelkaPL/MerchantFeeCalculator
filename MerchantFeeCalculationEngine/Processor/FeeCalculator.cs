using MerchantFeeCalculationEngine.Model;
using System.Collections.Generic;

namespace MerchantFeeCalculationEngine.Processor
{
    using System.Linq;

    public class FeeCalculator : IFeeCalculator
    {
        private decimal monthlyFee = 30;

        public ProcessedTransaction CalculateFee(Transaction transaction, decimal fee)
        {
            var processedTransaction = new ProcessedTransaction(transaction);
            processedTransaction.Fee = processedTransaction.RelatedTransaction.Amount
                                       * ((1 - (processedTransaction.RelatedTransaction.Owner.DiscountPercentage / 100)) * (processedTransaction.RelatedTransaction.Owner.FeeAsPercentage / 100));
            return processedTransaction;
        }

        public IList<ProcessedTransaction> CalculateFees(IList<ProcessedTransaction> processedTransactions)
        {
            var grouoedTransactions = processedTransactions.GroupBy(m => new { m.RelatedTransaction.Owner.Name, m.RelatedTransaction.DoneOn.Year, m.RelatedTransaction.DoneOn.Month}).Select(m =>
                {
                    return new
                       {
                          Name = m.Key.Name,
                          Year = m.Key.Year,
                          Month = m.Key.Month,
                          FirstDate = m.Min(n => n.RelatedTransaction.DoneOn)
                    };
                });

            foreach (var item in grouoedTransactions)
            {
                processedTransactions.FirstOrDefault(m => m.RelatedTransaction.Owner.Name == item.Name).Fee += monthlyFee;
            }

            return processedTransactions;
        }
    }
}
