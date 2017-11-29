using System;
using System.Collections.Generic;
using System.Linq;
using ApexParser.Visitors;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexClass = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexEnum = ApexParser.MetaClass.EnumDeclarationSyntax;

namespace ApexParser
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
    }
}
