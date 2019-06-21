using System.Collections.Generic;
using System.IO;
using Danskebank.MerchantFeeCalculationEngine.FileReader;
using Danskebank.MerchantFeeCalculationEngine.Model;
using Danskebank.MerchantFeeCalculationEngine.Processor;
using Xunit;

namespace DanskeBank.MerchantFeeCalculationEngineTests
{
    using System;

    public class FileReaderTests
    {
        private readonly string netto = "NETTO";
        private readonly string circleK = "CIRCLE_K";
        private readonly string telia = "TELIA";
        private readonly string sevenEleven = "7-ELEVEN";

        [Fact]
        public void TransactionFileReader_ShouldReadTransaction_WhenCorrectDataProvided()
        {
            // Arrange
            var merchants = new Dictionary<string, Merchant>();
            merchants.Add("7-ELEVEN", new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 1 });
            merchants.Add("CIRCLE_K", new Merchant() { DiscountPercentage = 20, Name = circleK, FeeAsPercentage = 1 });
            merchants.Add("TELIA", new Merchant() { DiscountPercentage = 10, Name = telia, FeeAsPercentage = 1 });
            merchants.Add("NETTO", new Merchant() { DiscountPercentage = 0, Name = netto, FeeAsPercentage = 1 });

            var transactionReader = new TransactionFileReader();
            var text = @"2018-09-01 7-ELEVEN 100
2018-09-04 CIRCLE_K 200";
            var memoryStream = GenerateStreamFromString(text);
            StreamReader reader = new StreamReader(memoryStream);

            // Act
            var transaction = transactionReader.ReadSingleEntry(reader, merchants, new TransactionParser());

            // Assert
            Assert.Equal(sevenEleven, transaction.Owner.Name);
            Assert.Equal(new DateTime(2018, 9, 1), transaction.DoneOn);
            Assert.Equal(100M, transaction.Amount);
        }

        [Theory]
        [InlineData("201A-14-01 7-ELEVEN 100")]
        [InlineData("201A-09-1 7-ELEVEN 100")]
        [InlineData("201A-09-01 7-ELEVENWWW 100")]
        [InlineData("201A-09-01 7-ELEVEN 1A0")]
        [InlineData("201A-09-01 7-ELEVEN   100")]
        [InlineData("XXXX-09-01 7-ELEVEN   100")]
        public void TransactionFileReader_ShouldReadTransaction_WhenIncorrectDataProvided(string entry)
        {
            // Arrange
            var merchants = new Dictionary<string, Merchant>();
            merchants.Add("7-ELEVEN", new Merchant() { DiscountPercentage = 0, Name = sevenEleven, FeeAsPercentage = 1 });
            var exceptionTrhown = false;
            var transactionReader = new TransactionFileReader();

            // Incorrect date format
            var memoryStream = GenerateStreamFromString(entry);
            StreamReader reader = new StreamReader(memoryStream);

            // Act
            try
            {
                var transaction = transactionReader.ReadSingleEntry(reader, merchants, new TransactionParser());
            }
            catch (Exception exception)
            {
                exceptionTrhown = true;
                Assert.Equal("Improper format of transaction date", exception.Message);
            }

            // Assert
            Assert.True(exceptionTrhown);
        }

        [Theory]
        [InlineData("7-ELEVEN A 0")]
        [InlineData("7-ELEVENAAA 1 0")]
        [InlineData("7-ELEVEN  1 0")]
        public void MerchantFileReader_ShouldGenerateError_WhenIncorrectDataProvided(string entry)
        {
            // Arrange
            var merchantReader = new MerchantReader();
            var memoryStream = GenerateStreamFromString(entry);
            StreamReader reader = new StreamReader(memoryStream);
            var exceptionTrhown = false;

            // Act
            try
            {
                var merrhants = merchantReader.Read(reader, new MerchantParser());
            }
            catch (Exception exception)
            {
                exceptionTrhown = true;
                Assert.Equal("Improper format of merchant data", exception.Message);
            }

            // Assert
            Assert.True(exceptionTrhown);
        }

        [Fact]
        public void MerchantFileReader_ShouldReadMerchants_WhenCorrectDataProvided()
        {
            // Arrange
            var merchantReader = new MerchantReader();
            var text = @"7-ELEVEN 1 0 
CIRCLE_K 1 20";
            var memoryStream = GenerateStreamFromString(text);
            StreamReader reader = new StreamReader(memoryStream);

            // Act
            var merrhants = merchantReader.Read(reader, new MerchantParser());

            // Assert
            Assert.Equal(2, merrhants.Count);

            Assert.Equal(sevenEleven, merrhants[sevenEleven].Name);
            Assert.Equal(0, merrhants[sevenEleven].DiscountPercentage);
            Assert.Equal(1, merrhants[sevenEleven].FeeAsPercentage);

            Assert.Equal(circleK, merrhants[circleK].Name);
            Assert.Equal(20, merrhants[circleK].DiscountPercentage);
            Assert.Equal(1, merrhants[circleK].FeeAsPercentage);
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
