using System;
using System.Collections.Generic;
using System.IO;
using FlatFileReader;

namespace FlatFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string flatFile = @"..\..\..\ANK_DAILY_CLAIM_EXTRACT_8-16-2018 (1).txt";
            string xmlFile = @"..\..\..\John_Hopkins_837.xml";
            Exception exception = null;

            try
            {
                JohnHopkinsFlatFile837ClaimReader.Read(flatFile, "JOHN HOPKINS").ToFile(xmlFile);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }     
    }
}
