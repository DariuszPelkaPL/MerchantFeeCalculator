using System.Collections.Generic;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculation.Engine.Model;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPI
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
    }
}
