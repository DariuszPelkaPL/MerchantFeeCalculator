using System;
using System.Collections.Generic;
using System.Globalization;
using Danskebank.MerchantFeeCalculation.Engine.Model;
using Danskebank.MerchantFeeCalculation.Engine.Processor;
using Xunit;

namespace Danskebank.MerchantFeeCalculation.EngineTests
{
    using System.Linq;

    public class MerchantFeeCalculationEngineTests
    {
        private readonly string netto = "NETTO";
        private readonly string circleK = "CIRCLE_K";
        private readonly string telia = "TELIA";
        private readonly string sevenEleven = "7-ELEVEN";

        [Theory]
        [InlineData("CIRCLE_K", "2018-09-02", 1, 120, 30.2)]
        [InlineData("TELIA", "2018-09-04", 1, 200, 31)]
        [InlineData("CIRCLE_K", "2018-10-22", 1, 300, 32)]
        [InlineData("CIRCLE_K", "2018-10-29", 1, 150, 30.5)]
        public void FeeCalculator_ShouldCalculateFee_WhenOnlyFeeProvided(string merchantName, string stringifiedTransactionDate, decimal percentageFee, decimal transactionAmount, decimal expectedFee)
        {
            // Arrange
            var calculator = new FeeCalculator();
            DateTime transactionDate;
            var transactionDateValid = DateTime.TryParseExact(stringifiedTransactionDate, "yyyy-MM-dd", null, DateTimeStyles.None, out transactionDate);
            var merchant = new Merchant() { Name = merchantName, FeeAsPercentage = percentageFee };
            var transaction = new Transaction() { Owner = merchant, Amount = transactionAmount, DoneOn = transactionDate};

            // Act
            calculator.InitializeFeeCalculation();
            var processedTransaction = calculator.CalculateFee(transaction);

            // Assert
            Assert.True(transactionDateValid);
            Assert.Equal(expectedFee, processedTransaction.Fee);
        }

        [Theory]
        [InlineData("TELIA", "2018-09-02", 1, 120, 10, 30.08)]
        [InlineData("TELIA", "2018-09-04", 1, 200, 10, 30.80)]
        [InlineData("TELIA", "2018-10-22", 1, 300, 10, 31.7)]
        [InlineData("TELIA", "2018-10-29", 1, 150, 10, 30.35)]
        [InlineData("CIRCLE_K", "2018-09-02", 1, 120, 20, 29.96)]
        [InlineData("CIRCLE_K", "2018-09-04", 1, 200, 20, 30.60)]
        [InlineData("CIRCLE_K", "2018-10-22", 1, 300, 20, 31.40)]
        [InlineData("CIRCLE_K", "2018-10-29", 1, 150, 20, 30.20)]
        public void FeeCalculator_ShouldCalculateFee_WhenDiscountProvided(string merchantName, string stringifiedTransactionDate, decimal percentageFee, decimal transactionAmount, decimal discount, decimal expectedFee)
        {
            // Arrange
            var calculator = new FeeCalculator();
            DateTime transactionDate;
            var transactionDateValid = DateTime.TryParseExact(stringifiedTransactionDate, "yyyy-MM-dd", null, DateTimeStyles.None, out transactionDate);
            var merchant = new Merchant() { Name = merchantName, FeeAsPercentage = percentageFee, DiscountPercentage = discount};
            var transaction = new Transaction() { Owner = merchant, Amount = transactionAmount, DoneOn = transactionDate };

            // Act
            calculator.InitializeFeeCalculation();
            var processedTransaction = calculator.CalculateFee(transaction);

            // Assert
            Assert.True(transactionDateValid);
            Assert.Equal(expectedFee, processedTransaction.Fee);
        }

        [Fact]
        public void FeeCalculator_ShouldNotAddMonthlyFee_WhenFeeIsZero()
        {
            // IWe check following requirement-If transaction fee is 0 after applying discounts, InvoiceFee should not be added

           // Arrange
           var merchants = new Dictionary<string, Merchant>();
            merchants.Add(sevenEleven, new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 0 });

            List<Transaction> transactions = new List<Transaction>
                                 {
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 1), Amount = 100, Owner = merchants[sevenEleven] },
                                     new Transaction{ DoneOn = new DateTime(2018, 9, 19), Amount = 100, Owner = merchants[sevenEleven] },
                                 };

            var calculator = new FeeCalculator();

            // Act
            calculator.InitializeFeeCalculation();
            var processedFirstTransactions = calculator.CalculateFee(transactions[0]);
            var processedSecondTransaction = calculator.CalculateFee(transactions[1]);

            // Assert

            // Month #9
            Assert.Equal(0, processedFirstTransactions.Fee);
            Assert.Equal(sevenEleven, processedFirstTransactions.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 1), processedFirstTransactions.RelatedTransaction.DoneOn);

            Assert.Equal(0, processedSecondTransaction.Fee);
            Assert.Equal(sevenEleven, processedSecondTransaction.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 19), processedSecondTransaction.RelatedTransaction.DoneOn);
        }

        [Fact]
        public void FeeCalculator_ShouldAddMonthlyFeeAsFirstEntry_WhenFeeIsNonZero()
        {
            // We check following requirementInvoice Fee should be included in the fee for first transaction of the month

            // Arrange
            var merchants = new Dictionary<string, Merchant>();
            merchants.Add(sevenEleven, new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 1 });

            List<Transaction> transactions = new List<Transaction>
                                                 {
                                                     new Transaction{ DoneOn = new DateTime(2018, 9, 1), Amount = 100, Owner = merchants[sevenEleven] },
                                                     new Transaction{ DoneOn = new DateTime(2018, 9, 19), Amount = 100, Owner = merchants[sevenEleven] },
                                                 };

            var calculator = new FeeCalculator();

            // Act
            calculator.InitializeFeeCalculation();
            var processedFirstTransactions = calculator.CalculateFee(transactions[0]);
            var processedSecondTransactions = calculator.CalculateFee(transactions[1]);

            // Assert

            // Month #9
            Assert.Equal(30M, processedFirstTransactions.Fee);
            Assert.Equal(sevenEleven, processedFirstTransactions.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 1), processedFirstTransactions.RelatedTransaction.DoneOn);

            Assert.Equal(1M, processedSecondTransactions.Fee);
            Assert.Equal(sevenEleven, processedSecondTransactions.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 19), processedSecondTransactions.RelatedTransaction.DoneOn);
        }

        [Fact]
        public void FeeCalculator_ShouldAddMonthlyFee_WhenAtLeastOneTransaction()
        {
            // We check following requirementInvoice Fee should be included in the fee for first transaction of the month

            // Arrange
            var merchants = new Dictionary<string, Merchant>();
            merchants.Add(sevenEleven, new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 1 });
            merchants.Add(circleK, new Merchant() { DiscountPercentage = 0, Name = circleK, FeeAsPercentage = 2 });

            List<Transaction> transactions = new List<Transaction>
                                                 {
                                                     new Transaction{ DoneOn = new DateTime(2018, 9, 1), Amount = 100, Owner = merchants[sevenEleven] },
                                                     new Transaction{ DoneOn = new DateTime(2018, 9, 19), Amount = 100, Owner = merchants[sevenEleven] },
                                                     new Transaction{ DoneOn = new DateTime(2018, 10, 19), Amount = 100, Owner = merchants[circleK] },
                                                 };

            var calculator = new FeeCalculator();

            // Act
            calculator.InitializeFeeCalculation();
            var processedFirstTransaction = calculator.CalculateFee(transactions[0]);
            var processedSecondTransaction = calculator.CalculateFee(transactions[1]);
            var processedThirdTransaction = calculator.CalculateFee(transactions[2]);

            // Assert

            // Month #9
            Assert.Equal(30M, processedFirstTransaction.Fee);
            Assert.Equal(sevenEleven, processedFirstTransaction.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 1), processedFirstTransaction.RelatedTransaction.DoneOn);

            Assert.Equal(1M, processedSecondTransaction.Fee);
            Assert.Equal(sevenEleven, processedSecondTransaction.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 19), processedSecondTransaction.RelatedTransaction.DoneOn);

            // Month #10
            Assert.Equal(31M, processedThirdTransaction.Fee);
            Assert.Equal(circleK, processedThirdTransaction.RelatedTransaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 10, 19), processedThirdTransaction.RelatedTransaction.DoneOn);
        }
    }
}
