using System;
using System.Collections.Generic;
using System.Linq;
using DankseBank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.MerchantFeeCalculationEngine.Processor
{
    public class FeeCalculator : IFeeCalculator
    {
        private static IDictionary<string, IList<string>> monthlyFeePaid = new Dictionary<string, IList<string>>();
        private decimal monthlyFee = 29;

        public ProcessedTransaction CalculateFee(Transaction transaction)
        {
            bool monthlyFeeWasPaid = true;

            var processedTransaction = new ProcessedTransaction(transaction);
            processedTransaction.Fee = processedTransaction.RelatedTransaction.Amount
                                       * ((1 - (processedTransaction.RelatedTransaction.Owner.DiscountPercentage / 100)) * (processedTransaction.RelatedTransaction.Owner.FeeAsPercentage / 100));
            string stringifiedMonthAndYear = processedTransaction.RelatedTransaction.DoneOn.ToString("YYYYMM");

            if (!monthlyFeePaid.ContainsKey(stringifiedMonthAndYear))
            {
                monthlyFeePaid[stringifiedMonthAndYear] = new List<string>();
                monthlyFeePaid[stringifiedMonthAndYear].Add(processedTransaction.RelatedTransaction.Owner.Name);
                monthlyFeeWasPaid = false;
            }
            else
            {
                var merchantListInGivenMonth = monthlyFeePaid[stringifiedMonthAndYear];
                if (!merchantListInGivenMonth.Contains(processedTransaction.RelatedTransaction.Owner.Name))
                {
                    monthlyFeePaid[stringifiedMonthAndYear].Add(processedTransaction.RelatedTransaction.Owner.Name);
                    monthlyFeeWasPaid = false;
                }
            }

            if (!monthlyFeeWasPaid && processedTransaction.Fee > 0)
            {
                processedTransaction.Fee += monthlyFee;
            }

            return processedTransaction;
        }
    }
}
