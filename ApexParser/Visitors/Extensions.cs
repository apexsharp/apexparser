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
        public static string ToCSharp(this BaseSyntax node) =>
            CSharpCodeGenerator.Generate(node);
    }
}
