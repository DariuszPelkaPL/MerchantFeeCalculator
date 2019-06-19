using System.Collections.Generic;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.ConsoleAPI
{
    public interface ITransactionsProcessor
    {
        void ReadTransactions(string transactionstFile, IDictionary<string, Merchant> merchants);

        void InitializeProcessing();
    }
}
