using System.Collections.Generic;
using Danskebank.Common;
using Danskebank.MerchantFeeCalculationEngine.Model;

namespace Danskebank.ConsoleAPI
{
    public interface IMerchantsProcessor
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

        IDictionary<string, Merchant> ReadMerchants(string merchantFile);
    }
}
