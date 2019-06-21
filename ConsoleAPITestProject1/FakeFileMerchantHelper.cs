using System.IO;
using Danskebank.Common;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPITestProject
{
    public class FakeFileMerchantHelper : IFileHelper
    {
        public void CloseFile(StreamReader reader)
        {
            ;
        }

        public bool FileExists(string filePath)
        {
            return true;
        }

        public StreamReader OpenFile(string filePath)
        {
            var text = @"7-ELEVEN 1 0 
CIRCLE_K 1 20
TELIA    1 10
NETTO    1 0 "; 
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
