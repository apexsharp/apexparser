using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Toolbox;

namespace ApexParser.Parser
{
    internal static class Apex
    {
        private static ApexGrammar ApexGrammar { get; } = new ApexGrammar();

        public static MemberDeclarationSyntax ParseFile(string text) =>
            ApexGrammar.CompilationUnit.ParseEx(text);

        public static ClassDeclarationSyntax ParseClass(string text) =>
            ApexGrammar.ClassDeclaration.ParseEx(text);

        public static EnumDeclarationSyntax ParseEnum(string text) =>
            ApexGrammar.EnumDeclaration.ParseEx(text);
    }
}
