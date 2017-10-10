using System.Collections.Generic;

namespace ApexSharpBase.Lexer
{
    public class ApexLexer
    {
        // Given a APEX source, return Tokens. Remove Space and Return.
        public static List<Token> GetApexTokens(string apexCode)
        {
            List<Token> apexTokenList = new List<Token>();
            ApexSharpBase.Lexer.Lexer lexer = new ApexSharpBase.Lexer.Lexer(apexCode);

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
