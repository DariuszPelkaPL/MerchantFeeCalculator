using System.IO;
using Danskebank.Common;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPITestProject
{
    public class FakeFileTransactionWithIncorrectData : IFileHelper
    {
        public void CloseFile(StreamReader reader)
        {
        }

        public bool FileExists(string filePath)
        {
            return true;
        }

        public StreamReader OpenFile(string filePath)
        {
            // Incorrect data
            var text = @"201998-09-01 7-ELEVEN 100
2018-09-0114 CIRCLE_K 200";
            var memoryStream = GenerateStreamFromString(text);
            StreamReader reader = new StreamReader(memoryStream);
            return reader;
        }

        private MemoryStream GenerateStreamFromString(string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
