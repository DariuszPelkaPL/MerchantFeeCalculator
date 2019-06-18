using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculationEngine.Model;
using DanskeBank.MerchantFeeCalculationEngine.FileReader;
using Danskebank.MerchantFeeCalculationEngine.Processor;
using Xunit;

namespace DanskeBank.MerchantFeeCalculationEngineTests
{
    public class FileReaderTests
    {
        private readonly string netto = "NETTO";
        private readonly string circleK = "CIRCLE_K";
        private readonly string telia = "TELIA";
        private readonly string sevenEleven = "7-ELEVEN";

        [Fact]
        public void TransactionFileReader_ShouldReadTransactions_WhenCorrectDataProvided()
        {
            // Arrange
            var merchants = new Dictionary<string, Merchant>();
            merchants.Add("7-ELEVEN", new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 1 });
            merchants.Add("CIRCLE_K", new Merchant() { DiscountPercentage = 20, Name = circleK, FeeAsPercentage = 1 });
            merchants.Add("TELIA", new Merchant() { DiscountPercentage = 10, Name = telia, FeeAsPercentage = 1 });
            merchants.Add("NETTO", new Merchant() { DiscountPercentage = 0, Name = netto, FeeAsPercentage = 1 });

            var transactionReader = new TransactionFileReader();
            var text = @"2018-09-01 7-ELEVEN 100
2018-09-04 CIRCLE_K 100";
            var memoryStream = GenerateStreamFromString(text);
            StreamReader reader = new StreamReader(memoryStream);

            // Act
            var transactions = transactionReader.Read(reader, merchants, new TransactionParser());

            // Assert
            Assert.Equal(2, transactions.Count);
        }

        private MemoryStream GenerateStreamFromString(string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
