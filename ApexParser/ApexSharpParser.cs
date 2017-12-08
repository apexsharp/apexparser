using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApexParser.Parser;
using ApexParser.Toolbox;
using ApexParser.Visitors;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexClass = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexEnum = ApexParser.MetaClass.EnumDeclarationSyntax;
using MemberDeclarationSyntax = ApexParser.MetaClass.MemberDeclarationSyntax;

namespace ApexParser
{
    public class ApexSharpParser
    {
        private static ApexGrammar ApexGrammar { get; } = new ApexGrammar();

        // Convert Apex Code to C#
        public static string ConvertApexToCSharp(string apexCode, string @namespace = null)
        {
            return GetApexAst(apexCode).ToCSharp(@namespace: @namespace);
        }

        // Get the AST for a given APEX File
        public static MemberDeclarationSyntax GetApexAst(string apexCode)
        {
            return ApexGrammar.CompilationUnit.ParseEx(apexCode);
        }

        // Convert a given Apex Ast to C#
        public static string ConvertApexAstToCSharp(MemberDeclarationSyntax astSyntax, string @namespace = null)
        {
            return astSyntax.ToCSharp(@namespace: @namespace);
        }

        // Format APEX Code so each statement is in its own line
        public static string FormatApex(string apexCode)
        {
            return GetApexAst(apexCode).ToApex(tabSize: 0);
        }

        // Indent APEX code, Pass the Tab Size. If Tab size is set to 0, no indentions
        public static string IndentApex(string apexCode, int tabSize = 4)
        {
            return GetApexAst(apexCode).ToApex(tabSize);
        }

        public static CompilationUnitSyntax ParseText(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            return tree.GetRoot() as CompilationUnitSyntax;
        }

        public static string ToCSharp(CompilationUnitSyntax syntax)
        {
            return syntax.ToFullString();
        }

        public static string[] ToApex(string csharp)
        {
            var csharpTree = ParseText(csharp);
            var apexTrees = ApexSyntaxBuilder.GetApexSyntaxNodes(csharpTree);
            var apexClasses = apexTrees.Select(cd => cd.ToApex());
            return apexClasses.ToArray();
        }

        public static Dictionary<string, string> ConvertToApex(string csharp)
        {
            var csharpTree = ParseText(csharp);
            var apexTrees = ApexSyntaxBuilder.GetApexSyntaxNodes(csharpTree);
            var result = new Dictionary<string, string>();

            foreach (var apexNode in apexTrees)
            {
                if (apexNode is ApexClass cd)
                {
                    result[cd.Identifier ?? string.Empty] = cd.ToApex();
                }
                else if (apexNode is ApexEnum ed)
                {
                    result[ed.Identifier ?? string.Empty] = ed.ToApex();
                }
                else
                {
                    var apex = apexNode.ToApex();
                    result[apex] = apex;
                }
            }

            return result;
        }

        public static void ValidateDir(DirectoryInfo dirInfo)
        {
            if (dirInfo.Exists == false)
            {
                throw new DirectoryNotFoundException();
            }
        }

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
                var cSharpFile = ApexSharpParser.ConvertApexToCSharp(cSharpCode, nameSpace);

                // Save the converted C# File
                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                var cSharpFileSave = Path.Combine(cSharpDirInfo.FullName, cSharpFileName);

                Console.WriteLine($"Saving {cSharpFileSave}");

                File.WriteAllText(cSharpFileSave, cSharpFile);
            }
        }

        public static void ConvertToApex(string cSharpDir, string apexDir)
        {
            var apexDirInfo = new DirectoryInfo(apexDir);
            ValidateDir(apexDirInfo);
            var cSharpDirInfo = new DirectoryInfo(cSharpDir);
            ValidateDir(cSharpDirInfo);

            FileInfo[] cSharpFileList = cSharpDirInfo.GetFiles("*.cs");

            foreach (var cSharpFile in cSharpFileList)
            {
                var cSharpCode = File.ReadAllText(cSharpFile.FullName);

                foreach (var colleciton in ApexSharpParser.ConvertToApex(cSharpCode))
                {
                    var cSharpFileName = Path.ChangeExtension(colleciton.Key, ".cls");

                    var apexFile = Path.Combine(apexDirInfo.FullName, cSharpFileName);

                    Console.WriteLine(apexFile);

                    File.WriteAllText(apexFile, colleciton.Value);
                }
            }
        }
    }
}
