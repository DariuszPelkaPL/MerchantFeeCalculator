using Danskebank.ConsoleAPI;
using System;
using Xunit;

namespace ConsoleAPITestProject1
{
    public class TransactionProcessingUnitTest
    {
        [Fact]
        public void Test1()
        {
            var transactionProcessor = new TransactionsProcessor();
            transactionProcessor.InitializeProcessing();
            var merchantProcessor = new MerchantsProcessor();
            var merchants = merchantProcessor.ReadMerchants("someFile");
            transactionProcessor.ReadTransactions("someFile", merchants);
        }
    }
}
