using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser;
using ApexSharp.ApexParser.Syntax;
using ApexSharp.ApexToCSharp.Visitors;

namespace ApexSharp.ApexToCSharp
{
    public static class ApexToCSharpHelpers
    {
        // Convert Apex AST to C#
        public static string ConvertToCSharp(BaseSyntax ast, string @namespace = null)
        {
        	return ast.ToCSharp(@namespace: @namespace);
        }

        // Convert Apex Code to C#
        public static string ConvertToCSharp(string apexCode, string @namespace = null)
        {
            return ApexSharpParser.GetApexAst(apexCode).ToCSharp(@namespace: @namespace);
        }

        // Convert Apex Code to C# with custom options
        public static string ConvertToCSharp(string apexCode, ApexSharpParserOptions options)
        {
            return ApexSharpParser.GetApexAst(apexCode).ToCSharp(options);
        }

        public static void ValidateDir(DirectoryInfo dirInfo)
        {
            if (dirInfo.Exists == false)
            {
                throw new DirectoryNotFoundException();
            }
        }

        // Convert Apex files to C#
        public static void ConvertToCSharp(string apexDir, string cSharpDir, string nameSpace)
        {
            var apexDirInfo = new DirectoryInfo(apexDir);
            ValidateDir(apexDirInfo);
            var cSharpDirInfo = new DirectoryInfo(cSharpDir);
            ValidateDir(cSharpDirInfo);

            FileInfo[] apexFileList = apexDirInfo.GetFiles("*.cls");

            foreach (var apexFile in apexFileList)
            {
                Console.WriteLine($"Convertiong {apexFile}");

                // Read and Convert to C#, Make sure to pass the name of the namespace.
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                var cSharpFile = ConvertToCSharp(cSharpCode, nameSpace);

                // Save the converted C# File
                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                var cSharpFileSave = Path.Combine(cSharpDirInfo.FullName, cSharpFileName);

                Console.WriteLine($"Saving {cSharpFileSave}");
                File.WriteAllText(cSharpFileSave, cSharpFile);
            }
        }
    }
}
