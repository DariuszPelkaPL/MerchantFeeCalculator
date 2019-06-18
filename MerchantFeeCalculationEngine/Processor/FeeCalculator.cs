using MerchantFeeCalculationEngine.Model;
using System.Collections.Generic;

namespace MerchantFeeCalculationEngine.Processor
{
    using System.Linq;

    public class FeeCalculator : IFeeCalculator
    {
        private decimal monthlyFee = 29;

        public ProcessedTransaction CalculateFee(Transaction transaction)
        {
            var processedTransaction = new ProcessedTransaction(transaction);
            processedTransaction.Fee = processedTransaction.RelatedTransaction.Amount
                                       * ((1 - (processedTransaction.RelatedTransaction.Owner.DiscountPercentage / 100)) * (processedTransaction.RelatedTransaction.Owner.FeeAsPercentage / 100));
            return processedTransaction;
        }

        public IList<ProcessedTransaction> CalculateMonthlyFees(IList<Transaction> transactions)
        {
            var grouoedTransactions = transactions.GroupBy(m => new { m.Owner.Name, m.DoneOn.Year, m.DoneOn.Month}).Select(m =>
                {
                    return new
                       {
                          Name = m.Key.Name,
                          Year = m.Key.Year,
                          Month = m.Key.Month,
                          FirstDate = m.Min(n => n.DoneOn)
                    };
                });

            IList<ProcessedTransaction> processedTransactions = new List<ProcessedTransaction>();

            foreach (var transaction in transactions)
            {
                var processedTransaction = this.CalculateFee(transaction);
                processedTransactions.Add(processedTransaction);
            }

            foreach (var item in grouoedTransactions)
            {
                var record = processedTransactions.FirstOrDefault(m => m.RelatedTransaction.Owner.Name == item.Name && m.RelatedTransaction.DoneOn == item.FirstDate);
                if (record.Fee > 0)
                {
                    record.Fee += monthlyFee;
                }
            }

            return processedTransactions;
        }
    }
}
