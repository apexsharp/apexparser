using System;
using System.Linq;
using ApexParser.Visitors;
using CSharpParser.Visitors;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpParser
{
    public class CSharpHelper
    {
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
    }
}
