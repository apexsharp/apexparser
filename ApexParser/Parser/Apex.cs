using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Toolbox;

namespace ApexParser.Parser
{
    public static class Apex
    {
        private static ApexGrammar ApexGrammar { get; } = new ApexGrammar();

        public static MemberDeclarationSyntax ParseFile(string text)
        {
            return ApexGrammar.CompilationUnit.ParseEx(text);
        }

        public static ClassDeclarationSyntax ParseClass(string text)
        {
            return ApexGrammar.ClassDeclaration.ParseEx(text);
        }

        public static EnumDeclarationSyntax ParseEnum(string text)
        {
            return ApexGrammar.EnumDeclaration.ParseEx(text);
        }
    }
}
