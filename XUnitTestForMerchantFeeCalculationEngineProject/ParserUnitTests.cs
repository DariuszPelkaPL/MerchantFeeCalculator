using System;
using System.Collections.Generic;
using System.Globalization;
using MerchantFeeCalculationEngine.Model;
using MerchantFeeCalculationEngine.Processor;
using Xunit;

namespace MerchantFeeCalculationEngineTestProject
{
    public class ParserUnitTests
    {
        [Fact]
        public void TransactionParser_ShouldParseTransaction_WhenCorrectDataProvided()
        {
            // Arrange
            var parser = new TransactionParser();

            // Act
            var transaction = parser.ParseTransactionEntry("2018-09-01 7-ELEVEN 100");

            // Assert
            Assert.Equal("7-ELEVEN", transaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 1), transaction.DoneOn);
            Assert.Equal(100M, transaction.Amount);
        }

        [Fact]
        public void MerchantParser_ShouldParseMerchant_WhenCorrectDataProvided()
        {
            // Arrange
            var parser = new MerchantParser();

            // Act
            var merchant = parser.ParseMerchantEntry("7-ELEVEN 1 10");

            // Assert
            Assert.Equal("7-ELEVEN", merchant.Name);
            Assert.Equal(10, merchant.DiscountPercentage);
            Assert.Equal(1, merchant.FeeAsPercentage);
        }
    }
}
