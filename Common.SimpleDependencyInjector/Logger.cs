using System;
using System.Collections.Generic;
using System.IO;

namespace DanskeBank.Common
{
    public class Logger : ILogger
    {
        private static readonly string LoggerFileName = "log.txt";

        public void WriteError(string message)
        {
            StreamWriter file =
                new StreamWriter(LoggerFileName);
            file.WriteLine(message);
            file.Close();
        }
    }
}
