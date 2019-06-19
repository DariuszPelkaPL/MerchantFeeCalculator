namespace Danskebank.Common
{
    using System.IO;

    public interface IFileHelper
    {
        StreamReader OpenFile(string filePath);

        bool FileExists(string filePath);

        void CloseFile(StreamReader reader);
    }
}
