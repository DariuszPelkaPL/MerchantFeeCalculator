using System;

namespace Danskebank.Common
{
    public class ConsoleHelper : IConsoleHelper
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
