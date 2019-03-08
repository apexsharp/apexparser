using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ApexSharpDemo.ApexCodeFormat;

namespace ApexSharpDemo.CaseClean
{
    public partial class CaseCleaner
    {
        public static List<FileFormatDto> Clean(string apexFolderName)
        {
            var dtoList = new List<FileFormatDto>();
            var files = Directory.GetFiles(apexFolderName, "*.cls", SearchOption.TopDirectoryOnly).ToArray();

            foreach (var sourceFile in files)
            {
                var apexFileInfo = new FileInfo(sourceFile);

                Console.WriteLine($"Normalizing file: {sourceFile}...");
                var backupFile = sourceFile + ".bak";

                File.Delete(backupFile);
                File.Move(sourceFile, backupFile);

                var apexCode = File.ReadAllText(backupFile);
                var normalized = ApexCleanCodeGen.NormalizeCode(apexCode);
                File.WriteAllText(sourceFile, normalized);
                File.Delete(backupFile);

                dtoList.Add(new FileFormatDto
                {
                    ApexFileName = apexFileInfo.Name,
                    ApexFileBeforeFormat = apexCode,
                    ApexFileAfterFormat = normalized
                });
            }

            Console.WriteLine($"Done. Normalized {dtoList.Count} files.");
            return dtoList;
        }
    }
}
