using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.Lexer
{
    public class ApexLexer
    {
        // Given a APEX source, return Tokens. Remove Space and Return.
        public static List<Token> GetApexTokens(string apexCode)
        {
            List<Token> apexTokenList = new List<Token>();
            Lexer lexer = new Lexer(apexCode);

            while (true)
            {
                Result result = lexer.Next();
                if (result != null)
                {
                    apexTokenList.Add(new Token(result.TokenType, result.TokenContent));
                }
                else
                {
                    break;
                }
            }

            foreach (var apexToken in apexTokenList)
            {
                ////Console.WriteLine($"{apexToken.TokenType}  ::::::::  {apexToken.Content.Trim()}");
            }

            return apexTokenList;
        }
    }
}
