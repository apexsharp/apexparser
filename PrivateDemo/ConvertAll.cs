using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateDemo
{
    public class ConvertAll
    {
        public static void ConvertFile(string apexFileName)
        {
            foreach (var apexFileInfo in GetAllFiles(@"\DevSharp\SalesForceApexSharp\src\classes\", apexFileName))
            {
                var apexFile = File.ReadAllText(apexFileInfo.FullName);

                var newFileName = Path.ChangeExtension(apexFileInfo.Name, ".cs");

                var cSharpFile = ApexParser.ApexSharpParser.ConvertApexToCSharp(apexFile, "ApexSharpDemo.ApexCode");

                File.WriteAllText(@"\DevSharp\ApexSharp\ApexSharpDemo\ApexCode\" + newFileName, cSharpFile);

            }
        }

        public static List<FileInfo> GetAllFiles(string dirLocation, string ext)
        {
            DirectoryInfo dInfo = new DirectoryInfo(dirLocation);
            List<FileInfo> apexFileList = dInfo.GetFiles(ext).ToList();
            return apexFileList;
        }
    }
}
