using System.IO;
using Danskebank.Common;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPITestProject
{
    public class FakeBigFileTransactionHelper : IFileHelper
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
            var text = @"2018-09-01 7-ELEVEN 100                                                                                    
2018-09-04 CIRCLE_K 100                                                                                    
2018-09-07 TELIA    100                                                                                    
2018-09-09 NETTO    100                                                                                    
2018-09-13 CIRCLE_K 100                                                                                    
2018-09-16 TELIA    100                                                                                    
2018-09-19 7-ELEVEN 100                                                                                    
2018-09-22 CIRCLE_K 100                                                                                    
2018-09-25 TELIA    100                                                                                    
2018-09-28 7-ELEVEN 100                                                                                    
2018-09-30 CIRCLE_K 100                                                                                     
";
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
