using System;

namespace ApexParser.Lexer
{
    public class Lexer
    {
        public Lexer(string apexSourceCode)
        {
            TokenDefinitions = ApexTokenRegEx.GetTokenDefinitions();
            LineRemaining = apexSourceCode;
            FileName = "UnitTest";
        }

        private string FileName { get; }

        private TokenDefinition[] TokenDefinitions { get; }

        private string LineRemaining { get; set; }

        public Result Next()
        {
            if (LineRemaining.Length == 0)
            {
                return null;
            }

            foreach (var def in TokenDefinitions)
            {
                var matched = def.Matcher.Match(LineRemaining);
                if (matched > 0)
                {
                    var newResult = new Result
                    {
                        TokenType = def.TokenType,
                        TokenContent = LineRemaining.Substring(0, matched)
                    };

                    LineRemaining = LineRemaining.Substring(matched);
                    return newResult;
                }
            }

            var lenth = LineRemaining.Length;

            if (lenth > 50)
            {
                PrintErrorMessage(FileName, LineRemaining.Substring(0, 1), LineRemaining.Substring(0, 50));
            }
            else
            {
                PrintErrorMessage(FileName, LineRemaining.Substring(0, 1), LineRemaining.Substring(0));
            }

            LineRemaining = LineRemaining.Substring(1);

            Console.ReadLine();
            return null;
        }

        private void PrintErrorMessage(string fileName, string issueCharctor, string remainingLine)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("File Name : {0}", fileName);
            Console.WriteLine("Issue Charactor : {0}", issueCharctor);
            Console.WriteLine("Remaining Line: {0}", remainingLine);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}