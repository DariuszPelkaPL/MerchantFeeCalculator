using System.IO;

namespace Danskebank.Common
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
