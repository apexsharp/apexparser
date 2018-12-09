using ApexSharp.ApexParser.Parser;
using ApexSharp.ApexParser.Toolbox;
using ApexSharp.ApexParser.Visitors;
using MemberDeclarationSyntax = ApexSharp.ApexParser.Syntax.MemberDeclarationSyntax;

namespace ApexSharp.ApexParser
{
    public class ApexSharpParser
    {
        private static ApexGrammar ApexGrammar { get; } = new ApexGrammar();

        // Get the AST for a given APEX File
        public static MemberDeclarationSyntax GetApexAst(string apexCode)
        {
            return ApexGrammar.CompilationUnit.ParseEx(apexCode);
        }

        // Format APEX Code so each statement is in its own line
        public static string FormatApex(string apexCode)
        {
            return GetApexAst(apexCode).ToApex(tabSize: 0);
        }

        // Indent APEX code, Pass the Tab Size. If Tab size is set to 0, no indentions
        public static string IndentApex(string apexCode, int tabSize = 4)
        {
            return GetApexAst(apexCode).ToApex(tabSize);
        }
    }
}
