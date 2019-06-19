using System.Collections.Generic;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.ConsoleAPI
{
    public interface ITransactionsProcessor
    {
        IConsoleHelper ConsoleHelperProperty
        {
            get;
            set;
        }

        IFileHelper FileHelperProperty
        {
            get;
            set;
        }

        void ReadTransactions(string transactionstFile, IDictionary<string, Merchant> merchants);

        void InitializeProcessing();
    }
}
