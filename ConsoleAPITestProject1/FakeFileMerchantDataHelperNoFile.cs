﻿using System.IO;
using Danskebank.Common;

namespace Danskebank.MerchantFeeCalculation.ConsoleAPITestProject
{
    public class FakeFileMerchantDataHelperNoFile : IFileHelper
    {
        public void CloseFile(StreamReader reader)
        {
        }

        public bool FileExists(string filePath)
        {
            return false;
        }

        public StreamReader OpenFile(string filePath)
        {
            var text = @"7-ELEVEN 1 0 
CIRCLE_K 1 20"; 
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
