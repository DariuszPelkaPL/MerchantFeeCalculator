using MerchantFeeCalculationEngine;
using System;
using Xunit;

namespace XUnitTestForMerchantFeeCalculationEngineProject
{
    using MerchantFeeCalculationEngine.Model;
    using System.Globalization;

    public class UnitTest
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
    }
}
