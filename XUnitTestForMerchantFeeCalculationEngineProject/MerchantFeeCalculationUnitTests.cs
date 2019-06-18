using System;
using System.Collections.Generic;
using System.Globalization;
using MerchantFeeCalculationEngine.Model;
using MerchantFeeCalculationEngine.Processor;
using Xunit;

namespace XUnitTestForMerchantFeeCalculationEngineProject
{
    public class MerchantFeeCalculationUnitTests
    {
        private readonly string netto = "NETTO";
        private readonly string circleK = "CIRCLE_K";
        private readonly string telia = "TELIA";
        private readonly string sevenEleven = "7-ELEVEN";

        [Theory]
        [InlineData("CIRCLE_K", "2018-09-02", 1, 120, 1.2)]
        [InlineData("TELIA", "2018-09-04", 1, 200, 2)]
        [InlineData("CIRCLE_K", "2018-10-22", 1, 300, 3)]
        [InlineData("CIRCLE_K", "2018-10-29", 1, 150, 1.5)]
        public void FeeCalculator_ShouldCalculateFee_WhenOnlyFeeProvided(string merchantName, string stringifiedTransactionDate, decimal percentageFee, decimal transactionAmount, decimal expectedFee)
        {
            // Arrange
            var calculator = new FeeCalculator();
            DateTime transactionDate;
            var transactionDateValid = DateTime.TryParseExact(stringifiedTransactionDate, "yyyy-MM-dd", null, DateTimeStyles.None, out transactionDate);
            var merchant = new Merchant() { Name = merchantName, FeeAsPercentage = percentageFee };
            var transaction = new Transaction() { Owner = merchant, Amount = transactionAmount, DoneOn = transactionDate};

            // Act
            var processedTransaction = calculator.CalculateFee(transaction);

            // Assert
            Assert.True(transactionDateValid);
            Assert.Equal(expectedFee, processedTransaction.Fee);
        }

        [Theory]
        [InlineData("TELIA", "2018-09-02", 1, 120, 10, 1.08)]
        [InlineData("TELIA", "2018-09-04", 1, 200, 10, 1.80)]
        [InlineData("TELIA", "2018-10-22", 1, 300, 10, 2.7)]
        [InlineData("TELIA", "2018-10-29", 1, 150, 10, 1.35)]
        [InlineData("CIRCLE_K", "2018-09-02", 1, 120, 20, 0.96)]
        [InlineData("CIRCLE_K", "2018-09-04", 1, 200, 20, 1.60)]
        [InlineData("CIRCLE_K", "2018-10-22", 1, 300, 20, 2.40)]
        [InlineData("CIRCLE_K", "2018-10-29", 1, 150, 20, 1.20)]
        public void FeeCalculator_ShouldCalculateFee_WhenDiscountProvided(string merchantName, string stringifiedTransactionDate, decimal percentageFee, decimal transactionAmount, decimal discount, decimal expectedFee)
        {
            // Arrange
            var calculator = new FeeCalculator();
            DateTime transactionDate;
            var transactionDateValid = DateTime.TryParseExact(stringifiedTransactionDate, "yyyy-MM-dd", null, DateTimeStyles.None, out transactionDate);
            var merchant = new Merchant() { Name = merchantName, FeeAsPercentage = percentageFee, DiscountPercentage = discount};
            var transaction = new Transaction() { Owner = merchant, Amount = transactionAmount, DoneOn = transactionDate };

            // Act
            var processedTransaction = calculator.CalculateFee(transaction);

            // Assert
            Assert.True(transactionDateValid);
            Assert.Equal(expectedFee, processedTransaction.Fee);
        }

        [Fact]
        public void FeeCalculator_ShouldCalculateMonthlyFees_WhenOnlyFeeProvided()
        {
            // Arrange
            var merchants = new Dictionary<string, Merchant>();
            merchants.Add("7-ELEVEN", new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 1 });
            merchants.Add("CIRCLE_K", new Merchant() { DiscountPercentage = 20, Name = circleK, FeeAsPercentage = 1 });
            merchants.Add("TELIA", new Merchant() { DiscountPercentage = 10, Name = telia, FeeAsPercentage = 1 });
            merchants.Add("NETTO", new Merchant() { DiscountPercentage = 0, Name = netto, FeeAsPercentage = 1 });

            List<Transaction> transactions = new List<Transaction>
                                 {
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 1), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 4), Amount = 100, Owner = merchants[circleK] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 7), Amount = 100, Owner = merchants[telia] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 9), Amount = 100, Owner = merchants[netto] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 13), Amount = 100, Owner = merchants[circleK] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 16), Amount = 100, Owner = merchants[telia] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 19), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 22), Amount = 100, Owner = merchants[circleK] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 25), Amount = 100, Owner = merchants[telia] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 28), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 30), Amount = 100, Owner = merchants[circleK] },

                                     new Transaction{ DoneOn = new DateTime(2018, 10, 1), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 4), Amount = 100, Owner = merchants[circleK] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 7), Amount = 100, Owner = merchants[telia] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 10), Amount = 100, Owner = merchants[netto] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 13), Amount = 100, Owner = merchants[circleK] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 16), Amount = 100, Owner = merchants[telia] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 19), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 22), Amount = 100, Owner = merchants[circleK] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 25), Amount = 100, Owner = merchants[telia] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 28), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 10, 30), Amount = 100, Owner = merchants[circleK] },
                                 };

            var calculator = new FeeCalculator();

            // Act
            var processedTransactions = calculator.CalculateMonthlyFees(transactions);

            // Assert
            Assert.Equal(transactions.Count, processedTransactions.Count);

            // Month #9
            Assert.Equal(30, processedTransactions[0].Fee);
            Assert.Equal(sevenEleven, processedTransactions[0].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 1), processedTransactions[0].RelatedTransaction.DoneOn);

            Assert.Equal(29.8M, processedTransactions[1].Fee);
            Assert.Equal(circleK, processedTransactions[1].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 4), processedTransactions[1].RelatedTransaction.DoneOn);

            Assert.Equal(29.9M, processedTransactions[2].Fee);
            Assert.Equal(telia, processedTransactions[2].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 7), processedTransactions[2].RelatedTransaction.DoneOn);

            Assert.Equal(30, processedTransactions[3].Fee);
            Assert.Equal(netto, processedTransactions[3].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 9), processedTransactions[3].RelatedTransaction.DoneOn);

            Assert.Equal(0.8M, processedTransactions[4].Fee);
            Assert.Equal(circleK, processedTransactions[4].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 13), processedTransactions[4].RelatedTransaction.DoneOn);

            Assert.Equal(0.9M, processedTransactions[5].Fee);
            Assert.Equal(telia, processedTransactions[5].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 16), processedTransactions[5].RelatedTransaction.DoneOn);

            Assert.Equal(1M, processedTransactions[6].Fee);
            Assert.Equal(sevenEleven, processedTransactions[6].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 19), processedTransactions[6].RelatedTransaction.DoneOn);

            Assert.Equal(0.8M, processedTransactions[7].Fee);
            Assert.Equal(circleK, processedTransactions[7].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 22), processedTransactions[7].RelatedTransaction.DoneOn);

            // Month #10
            Assert.Equal(30, processedTransactions[11].Fee);
            Assert.Equal(sevenEleven, processedTransactions[11].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 1), processedTransactions[11].RelatedTransaction.DoneOn);

            Assert.Equal(29.8M, processedTransactions[12].Fee);
            Assert.Equal(circleK, processedTransactions[12].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 4), processedTransactions[12].RelatedTransaction.DoneOn);

            Assert.Equal(29.9M, processedTransactions[13].Fee);
            Assert.Equal(telia, processedTransactions[13].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 7), processedTransactions[13].RelatedTransaction.DoneOn);

            Assert.Equal(30, processedTransactions[14].Fee);
            Assert.Equal(netto, processedTransactions[14].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 10), processedTransactions[14].RelatedTransaction.DoneOn);

            Assert.Equal(0.8M, processedTransactions[15].Fee);
            Assert.Equal(circleK, processedTransactions[15].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 13), processedTransactions[15].RelatedTransaction.DoneOn);

            Assert.Equal(0.9M, processedTransactions[16].Fee);
            Assert.Equal(telia, processedTransactions[16].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 16), processedTransactions[16].RelatedTransaction.DoneOn);

            Assert.Equal(1M, processedTransactions[17].Fee);
            Assert.Equal(sevenEleven, processedTransactions[17].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 19), processedTransactions[17].RelatedTransaction.DoneOn);

            Assert.Equal(0.8M, processedTransactions[18].Fee);
            Assert.Equal(circleK, processedTransactions[18].RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 22), processedTransactions[18].RelatedTransaction.DoneOn);

        }
    }
}
