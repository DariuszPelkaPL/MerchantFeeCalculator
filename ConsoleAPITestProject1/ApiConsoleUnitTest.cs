using Danskebank.MerchantFeeCalculation.ConsoleAPI;
using Xunit;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPITestProject
{

    public class TransactionProcessingUnitTest
    {
        [Fact]
        public void APIProcessing_ShouldProduceOutput_WhenCorrectDataProvided()
        {
            // Arrange
            var transactionProcessor = new TransactionsProcessor();
            var fakeFileHelper = new FakeFileTransactionHelper();
            var consoleHelper = new FakeConsoleHelper();
            transactionProcessor.ConsoleHelperProperty = consoleHelper;
            transactionProcessor.FileHelperProperty = fakeFileHelper;
            var merchantProcessor = new MerchantsProcessor();
            merchantProcessor.ConsoleHelperProperty = new FakeConsoleHelper();
            merchantProcessor.FileHelperProperty = new FakeFileMerchantHelper();
            var merchants = merchantProcessor.ReadMerchants("someFile");

            // Act
            transactionProcessor.ReadTransactions("someFile", merchants);

            // Assert
            var output = consoleHelper.ConsoleOutput.ToString();
            Assert.Equal("2018-09-01 7-ELEVEN 30.00\n2018-09-04 CIRCLE_K 30.60\n", output);
        }

        [Fact]
        public void APIProcessing_ShouldProduceTaskOutput_WhenTaskDataProvided()
        {
            // Arrange
            var transactionProcessor = new TransactionsProcessor();
            var fakeFileHelper = new FakeBigFileTransactionHelper();
            var consoleHelper = new FakeConsoleHelper();
            transactionProcessor.ConsoleHelperProperty = consoleHelper;
            transactionProcessor.FileHelperProperty = fakeFileHelper;
            var merchantProcessor = new MerchantsProcessor();
            merchantProcessor.ConsoleHelperProperty = new FakeConsoleHelper();
            merchantProcessor.FileHelperProperty = new FakeFileMerchantHelper();
            var merchants = merchantProcessor.ReadMerchants("someFile");

            // Act
            transactionProcessor.ReadTransactions("someFile", merchants);

            // Assert
            var output = consoleHelper.ConsoleOutput.ToString();
            var expectedOitput = @"2018-09-01 7-ELEVEN 30.00
2018-09-04 CIRCLE_K 29.80
2018-09-07 TELIA    29.90
2018-09-09 NETTO    30.00
2018-09-13 CIRCLE_K 0.80
2018-09-16 TELIA    0.90
2018-09-19 7-ELEVEN 1.00
2018-09-22 CIRCLE_K 0.80
2018-09-25 TELIA    0.90
2018-09-28 7-ELEVEN 1.00
2018-09-30 CIRCLE_K 0.80
";

            Assert.Equal(expectedOitput, output);
        }

        [Fact]
        public void APIProcessing_ShouldProduceErrorMessage_WhenNoTransactionFileProvided()
        {
            // Arrange
            var transactionProcessor = new TransactionsProcessor();
            var fakeFileHelper = new FakeFileTransactionDataHelperNoFile();
            var consoleHelper = new FakeConsoleHelper();
            transactionProcessor.ConsoleHelperProperty = consoleHelper;
            transactionProcessor.FileHelperProperty = fakeFileHelper;
            var merchantProcessor = new MerchantsProcessor();
            merchantProcessor.ConsoleHelperProperty = new FakeConsoleHelper();
            merchantProcessor.FileHelperProperty = new FakeFileMerchantHelper();
            var merchants = merchantProcessor.ReadMerchants("someFile");

            // Act
            transactionProcessor.ReadTransactions("someFile", merchants);

            // Assert
            var output = consoleHelper.ConsoleOutput.ToString();
            Assert.Equal("No transaction file\nError while processing tramsaction data\n", output);
        }

        [Fact]
        public void APIProcessing_ShouldProduceErrorMessage_WhenIncorrectTransactionDataProvided()
        {
            // Arrange
            var transactionProcessor = new TransactionsProcessor();
            var fakeFileHelper = new FakeFileTransactionWithIncorrectData();
            var consoleHelper = new FakeConsoleHelper();
            transactionProcessor.ConsoleHelperProperty = consoleHelper;
            transactionProcessor.FileHelperProperty = fakeFileHelper;
            var merchantProcessor = new MerchantsProcessor();
            merchantProcessor.ConsoleHelperProperty = new FakeConsoleHelper();
            merchantProcessor.FileHelperProperty = new FakeFileMerchantHelper();
            var merchants = merchantProcessor.ReadMerchants("someFile");

            // Act
            transactionProcessor.ReadTransactions("someFile", merchants);

            // Assert
            var output = consoleHelper.ConsoleOutput.ToString();
            Assert.Equal("Error while processing tramsaction data\n", output);
        }

        [Fact]
        public void APIProcessing_ShouldProduceErrorMessage_WhenNoMerchantFileProvided()
        {
            // Arrange
            var transactionProcessor = new TransactionsProcessor();
            var fakeFileHelper = new FakeFileTransactionHelper();
            var consoleHelper = new FakeConsoleHelper();
            transactionProcessor.ConsoleHelperProperty = consoleHelper;
            transactionProcessor.FileHelperProperty = fakeFileHelper;
            var merchantProcessor = new MerchantsProcessor();
            merchantProcessor.ConsoleHelperProperty = new FakeConsoleHelper();
            merchantProcessor.FileHelperProperty = new FakeFileMerchantDataHelperNoFile();
            var merchants = merchantProcessor.ReadMerchants("someFile");

            // Act
            transactionProcessor.ReadTransactions("someFile", merchants);

            // Assert
            var output = consoleHelper.ConsoleOutput.ToString();
            Assert.Equal("No merchant file\nError while processing tramsaction data\n", output);
        }

        [Fact]
        public void APIProcessing_ShouldProduceErrorMessage_WhenIncorrectMerchantDataProvided()
        {
            // Arrange
            var transactionProcessor = new TransactionsProcessor();
            var fakeFileHelper = new FakeFileTransactionHelper();
            var consoleHelper = new FakeConsoleHelper();
            transactionProcessor.ConsoleHelperProperty = consoleHelper;
            transactionProcessor.FileHelperProperty = fakeFileHelper;
            var merchantProcessor = new MerchantsProcessor();
            merchantProcessor.ConsoleHelperProperty = new FakeConsoleHelper();
            merchantProcessor.FileHelperProperty = new FakeFileMerchantWithIncorrectDataHelper();
            var merchants = merchantProcessor.ReadMerchants("someFile");

            // Act
            transactionProcessor.ReadTransactions("someFile", merchants);

            // Assert
            var output = consoleHelper.ConsoleOutput.ToString();
            Assert.Equal("Error while processing merchant data\nError while processing tramsaction data\n", output);
        }

        // Here more unit tests! covering other scenarios ...
    }
}
