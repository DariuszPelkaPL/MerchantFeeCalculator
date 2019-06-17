using System;
using System.Globalization;
using MerchantFeeCalculationEngine.Model;
using MerchantFeeCalculationEngine.Processor;
using Xunit;

namespace XUnitTestForMerchantFeeCalculationEngineProject
{
    public class MerchantFeeCalculationUnitTests
    {
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
            var processedTransaction = calculator.CalculateFee(transaction, 1);

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
            var processedTransaction = calculator.CalculateFee(transaction, 1);

            // Assert
            Assert.True(transactionDateValid);
            Assert.Equal(expectedFee, processedTransaction.Fee);
        }

        [Theory]
        [InlineData("CIRCLE_K", "2018-09-02", 1, 120, 1.2)]
        [InlineData("TELIA", "2018-09-04", 1, 200, 2)]
        [InlineData("CIRCLE_K", "2018-10-22", 1, 300, 3)]
        [InlineData("CIRCLE_K", "2018-10-29", 1, 150, 1.5)]
        public void FeeCalculator_ShouldCalculateMonthlyFees_WhenOnlyFeeProvided(string merchantName, string stringifiedTransactionDate, decimal percentageFee, decimal transactionAmount, decimal expectedFee)
        {
            // Arrange
            var calculator = new FeeCalculator();
            DateTime transactionDate;
            var transactionDateValid = DateTime.TryParseExact(stringifiedTransactionDate, "yyyy-MM-dd", null, DateTimeStyles.None, out transactionDate);
            var merchant = new Merchant() { Name = merchantName, FeeAsPercentage = percentageFee };
            var transaction = new Transaction() { Owner = merchant, Amount = transactionAmount, DoneOn = transactionDate };

            // Act
            var processedTransaction = calculator.CalculateFee(transaction, 1);

            // Assert
            Assert.True(transactionDateValid);
            Assert.Equal(expectedFee, processedTransaction.Fee);
        }
    }
}
