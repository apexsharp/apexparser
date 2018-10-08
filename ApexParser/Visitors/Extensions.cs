using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;

namespace ApexParser.Visitors
{
    public static class Extensions
    {
        public static string ToCSharp(this BaseSyntax node, int tabSize = 4, string @namespace = null) =>
            CSharpCodeGenerator.GenerateCSharp(node, tabSize, @namespace);

        public static string ToCSharp(this BaseSyntax node, ApexSharpParserOptions options) =>
            CSharpCodeGenerator.GenerateCSharp(node, options);

        public static string ToApex(this BaseSyntax node, int tabSize = 4) =>
            ApexCodeGenerator.GenerateApex(node, tabSize);

        public static string GetCodeInsideMethod(this MethodDeclarationSyntax node, int tabSize = 4) =>
            ApexMethodBodyGenerator.GenerateApex(node, tabSize);
    }
}
