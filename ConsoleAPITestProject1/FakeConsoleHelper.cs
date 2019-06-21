using System.Text;
using Danskebank.Common;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPITestProject
{
    public class FakeConsoleHelper : IConsoleHelper
    {
        private static StringBuilder console = new StringBuilder();

        public FakeConsoleHelper()
        {
            console = new StringBuilder();
        }

        public void WriteLine(string message)
        {
            console.Append(message);
            console.Append("\n");
        }

        public string ConsoleOutput
        {
            get
            {
                return console.ToString();
            }
        }
    }
}
