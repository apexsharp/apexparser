using ApexSharp.ApexParser;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApexSharpDemo.ApexCodeFormat
{
    public class FileFormatDto
    {
        public string ApexFileName { get; set; }
        public string ApexFileBeforeFormat { get; set; }
        public string ApexFileAfterFormat { get; set; }
    }

    public class ApexCodeFormater
    {
        public static List<FileFormatDto> FormatApexCode(DirectoryInfo apexFolderName)
        {
            List<FileFormatDto> dtoList = new List<FileFormatDto>();

            var files = Directory.GetFiles(apexFolderName.FullName, "*.cls", SearchOption.TopDirectoryOnly);

            foreach (var sourceFile in files)
            {
                FileInfo apexFileInfo = new FileInfo(sourceFile);

                Console.WriteLine($"Formatting file: {sourceFile}...");
                var backupFile = sourceFile + ".bak";

                File.Delete(backupFile);
                File.Move(sourceFile, backupFile);

                var apexCode = File.ReadAllText(backupFile);
                var formatted = ApexSharpParser.IndentApex(apexCode);
                File.WriteAllText(sourceFile, formatted);
                File.Delete(backupFile);

                FileFormatDto dto = new FileFormatDto
                {
                    ApexFileName = apexFileInfo.FullName,
                    ApexFileBeforeFormat = apexCode,
                    ApexFileAfterFormat = formatted
                };
                dtoList.Add(dto);
            }

            Console.WriteLine($"Done. Formatted {dtoList.Count} files.");
            return dtoList;
        }
    }
}
