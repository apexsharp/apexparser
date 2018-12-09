using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Syntax;

namespace ApexSharp.ApexParser.Visitors
{
    public static class Extensions
    {
        public static string ToApex(this BaseSyntax node, int tabSize = 4) =>
            ApexCodeGenerator.GenerateApex(node, tabSize);

        public static string GetCodeInsideMethod(this MethodDeclarationSyntax node, int tabSize = 4) =>
            ApexMethodBodyGenerator.GenerateApex(node, tabSize);
    }
}
