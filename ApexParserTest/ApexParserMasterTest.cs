using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using ApexParser.Toolbox;

namespace ApexParserTest
{
    public class ApexParserMasterTest
    {
        public static void TestApexFiles(string apexFileDirLocation)
        {
            DirectoryInfo dInfo = new DirectoryInfo(apexFileDirLocation);
            List<FileInfo> apexFileList = dInfo.GetFiles("*.cls").ToList();

            ApexGrammar Apex = new ApexGrammar();
            StringBuilder errorMessage = new StringBuilder();

            int errorNumber = 0;
            foreach (var apexFileName in apexFileList)
            {
                StringBuilder errorMessageLocal = new StringBuilder();

                var apexCode = File.ReadAllText(apexFileName.FullName);

                try
                {
                    var cd = Apex.ClassDeclaration.ParseEx(apexCode);
                }
                catch (ParseExceptionCustom e)
                {
                    errorNumber++;
                    errorMessageLocal.AppendLine(errorNumber.ToString());
                    errorMessageLocal.AppendLine(apexFileName.FullName);
                    errorMessageLocal.AppendLine(e.Message);
                    errorMessageLocal.AppendLine("-------------------------------------------------------------------");
                    errorMessageLocal.AppendLine($"[{e.LineNumber}>>> " + e.Apexcode[e.LineNumber]);
                    errorMessageLocal.AppendLine("-------------------------------------------------------------------");
                    errorMessageLocal.AppendLine();

                    errorMessage.AppendLine(errorMessageLocal.ToString());
                    Console.WriteLine(errorMessageLocal);
                }
            }
        }
    }
}
