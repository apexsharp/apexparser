using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser;
using ApexSharp.ApexParser.Syntax;

namespace ApexSharp.ApexToCSharp.Visitors
{
    public static class Extensions
    {
        public static string ToCSharp(this BaseSyntax node, int tabSize = 4, string @namespace = null) =>
            CSharpCodeGenerator.GenerateCSharp(node, tabSize, @namespace);

        public static string ToCSharp(this BaseSyntax node, ApexSharpParserOptions options) =>
            CSharpCodeGenerator.GenerateCSharp(node, options);
    }
}
