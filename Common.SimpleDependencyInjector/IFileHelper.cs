using System.IO;

namespace Danskebank.Common
{
    public interface IFileHelper
    {
        StreamReader OpenFile(string filePath);

        bool FileExists(string filePath);

        void CloseFile(StreamReader reader);
    }
}
