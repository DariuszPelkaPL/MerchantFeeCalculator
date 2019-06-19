using System.IO;

namespace Danskebank.Common
{
    public class FileHelper : IFileHelper
    {
        public StreamReader OpenFile(string filePath)
        {
            return new StreamReader(filePath);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void CloseFile(StreamReader reader)
        {
            reader.Close();
        }
    }
}
