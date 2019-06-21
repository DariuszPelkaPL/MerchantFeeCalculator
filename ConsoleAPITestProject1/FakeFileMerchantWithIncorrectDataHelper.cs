using System.IO;
using Danskebank.Common;

namespace Danskebank.ConsoleAPITestProject1
{
    public class FakeFileMerchantWithIncorrectDataHelper : IFileHelper
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
            var text = @"7-ELEVEN A 0 
CIRCLE_K 1 2dd0"; 
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
