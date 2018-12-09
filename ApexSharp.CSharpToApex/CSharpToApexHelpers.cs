using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ApexSharp.ApexParser.Syntax;
using ApexSharp.ApexParser.Visitors;
using ApexSharp.CSharpToApex.Visitors;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexClass = ApexSharp.ApexParser.Syntax.ClassDeclarationSyntax;
using ApexEnum = ApexSharp.ApexParser.Syntax.EnumDeclarationSyntax;

namespace ApexSharp.CSharpToApex
{
    public static class CSharpToApexHelpers
    {
        public static CompilationUnitSyntax ParseText(string csharpText)
        {
            var tree = CSharpSyntaxTree.ParseText(csharpText);
            return tree.GetRoot() as CompilationUnitSyntax;
        }

        internal static string ConvertToCSharp(CompilationUnitSyntax syntax)
        {
            return syntax.ToFullString();
        }

        internal static string[] ConvertToApex(string csharp)
        {
            var csharpTree = ParseText(csharp);
            var apexTrees = ApexSyntaxBuilder.GetApexSyntaxNodes(csharpTree);
            var apexClasses = apexTrees.Select(cd => cd.ToApex());
            return apexClasses.ToArray();
        }

        public static Dictionary<string, BaseSyntax> ConvertToApexAst(string csharpText)
        {
            return ConvertToApexAst(ParseText(csharpText));
        }

        public static Dictionary<string, BaseSyntax> ConvertToApexAst(CompilationUnitSyntax csharpTree)
        {
            var apexTrees = ApexSyntaxBuilder.GetApexSyntaxNodes(csharpTree);
            var result = new Dictionary<string, BaseSyntax>();

            foreach (var apexNode in apexTrees)
            {
                if (apexNode is ApexClass cd)
                {
                    result[cd.Identifier ?? string.Empty] = cd;
                }
                else if (apexNode is ApexEnum ed)
                {
                    result[ed.Identifier ?? string.Empty] = ed;
                }
                else
                {
                    var apex = apexNode.ToApex();
                    result[apex] = apexNode;
                }
            }

            return result;
        }

        public static Dictionary<string, string> ConvertToApexCode(CompilationUnitSyntax csharpTree)
        {
            var result = ConvertToApexAst(csharpTree);
            return result.ToDictionary(p => p.Key, p => p.Value.ToApex());
        }

        public static Dictionary<string, string> ConvertToApexCode(string csharpText)
        {
            return ConvertToApexCode(ParseText(csharpText));
        }

        public static void ValidateDir(DirectoryInfo dirInfo)
        {
            if (dirInfo.Exists == false)
            {
                throw new DirectoryNotFoundException();
            }
        }

        public static void ConvertToApex(string cSharpDir, string apexDir, int salesForceVersion)
        {
            var apexDirInfo = new DirectoryInfo(apexDir);
            ValidateDir(apexDirInfo);
            var cSharpDirInfo = new DirectoryInfo(cSharpDir);
            ValidateDir(cSharpDirInfo);

            FileInfo[] cSharpFileList = cSharpDirInfo.GetFiles("*.cs");

            foreach (var cSharpFile in cSharpFileList)
            {
                var cSharpCode = File.ReadAllText(cSharpFile.FullName);

                foreach (var collection in ConvertToApexCode(cSharpCode))
                {
                    var apexFileName = Path.ChangeExtension(collection.Key, ".cls");
                    var apexFile = Path.Combine(apexDirInfo.FullName, apexFileName);
                    File.WriteAllText(apexFile, collection.Value);

                    var metaFileName = Path.ChangeExtension(apexFile, ".cls-meta.xml");
                    var metaFile = new StringBuilder();

                    metaFile.AppendLine("<?xml version = \"1.0\" encoding = \"UTF-8\"?>");
                    metaFile.AppendLine("<ApexClass xmlns = \"http://soap.sforce.com/2006/04/metadata\">");
                    metaFile.AppendLine($"<apiVersion>{salesForceVersion}.0</apiVersion>");
                    metaFile.AppendLine("<status>Active</status>");
                    metaFile.AppendLine("</ApexClass>");

                    File.WriteAllText(metaFileName, metaFile.ToString());

                    Console.WriteLine(metaFileName);
                }
            }
        }
    }
}
