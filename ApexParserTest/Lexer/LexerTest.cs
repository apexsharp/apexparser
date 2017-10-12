using System;
using System.Collections.Generic;
using ApexParser.Lexer;

namespace ApexParserTest.Lexer
{
    public class LexerTest
    {
        public static void Main()
        {
            Test();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
        public static void Test()
        {
            var methodTestData = LexerTestData.GetMethods();

            foreach (var lexerTestElement in methodTestData)
            {
                var apexTokens = ApexLexer.GetApexTokens(lexerTestElement.ApexLine);
                var apexTokensTest = lexerTestElement.TokenList;

                using (List<Token>.Enumerator apexTokEnumerator = apexTokens.GetEnumerator())
                {
                    using (List<TokenType>.Enumerator apexTestTokenEnumerator = apexTokensTest.GetEnumerator())
                    {
                        Console.WriteLine(lexerTestElement.ApexLine);
                        Console.WriteLine();
                        while (apexTestTokenEnumerator.MoveNext() && apexTokEnumerator.MoveNext())
                        {
                            Console.WriteLine(apexTestTokenEnumerator.Current + "  " + apexTokEnumerator.Current.TokenType);
                        }
                        Console.WriteLine();
                    }
                }
            }

        }
    }
}
