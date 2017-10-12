using System.Collections.Generic;
using ApexParser.Lexer;

namespace ApexParserTest.Lexer
{
    public class LexerTestElement
    {
        public string ApexLine { get; set; }
        public List<TokenType> TokenList = new List<TokenType>();
    }
}