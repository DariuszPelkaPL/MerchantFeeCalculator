using MerchantFeeCalculationEngine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantFeeCalculationEngine.Processor
{
    public interface ITransactionParser
    {
        Transaction ParseTransactionEntry(string stringifiedTransaction);
    }
}
