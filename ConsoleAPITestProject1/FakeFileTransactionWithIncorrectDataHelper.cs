using System.IO;
using Danskebank.Common;

namespace Danskebank.ConsoleAPITestProject1
{
    public class FakeFileTransactionWithIncorrectData : IFileHelper
    {
        public void CloseFile(StreamReader reader)
        {
            ;
        }

        public bool FileExists(string filePath)
        {
            return false;
        }

        public StreamReader OpenFile(string filePath)
        {
            var text = @"2018-09-01 7-ELEVEN 100
2018-09-04 CIRCLE_K 200";
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
