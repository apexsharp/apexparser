using System.Collections.Generic;
using ApexSharpBase.Lexer;

namespace ApexSharpBaseTest.Lexer
{
    public class LexerTestElement
    {
        public string ApexLine { get; set; }
        public List<TokenType> TokenList = new List<TokenType>();
    }
}