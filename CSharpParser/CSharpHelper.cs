using System;
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
    }
}
