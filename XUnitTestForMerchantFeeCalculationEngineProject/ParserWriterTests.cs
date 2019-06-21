using System;
using Danskebank.MerchantFeeCalculation.Engine.Model;
using Danskebank.MerchantFeeCalculation.Engine.Processor;
using Xunit;

namespace Danskebank.MerchantFeeCalculation.EngineTests
{
    public class ParserWriterTests
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

        [Fact]
        public void TransactionWriter_ShouldWriteTransaction_WhenCorrectDataProvided()
        {
            // Arrange
            var writer = new ProcessedTransactionWriter();
            ProcessedTransaction processedTransaction = new ProcessedTransaction(new Transaction() { Amount = 100, Owner = new Merchant() { Name = "7-ELEVEN" }, DoneOn = new DateTime(2018, 9, 1) });
            var transaction = processedTransaction;
            transaction.Fee = 30;

            // Act
            var stringifiedTransaction = writer.ConvertTransactionToTextEntry(transaction);

            // Assert
            Assert.Equal("2018-09-01 7-ELEVEN 30.00", stringifiedTransaction);
        }
        
    }
}
