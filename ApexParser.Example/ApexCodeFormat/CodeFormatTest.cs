using ApexSharp.ApexParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApexSharpDemo.ApexCodeFormat
{
    public class CodeFormatTest
    {
        public CodeFormatTest()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Dev\codeformat\src\classes\");
            //DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\DevSharp\ApexSharpFsb\FSB\ApexClasses\");
            List<FileFormatDto> results = ApexCodeFormater.FormatApexCode(directoryInfo);

            foreach (var result in results)
            {
                Console.WriteLine(result.ApexFileName);
                var apexAst = ApexSharpParser.GetApexAst(result.ApexFileAfterFormat);
            }
        }
    }
}
