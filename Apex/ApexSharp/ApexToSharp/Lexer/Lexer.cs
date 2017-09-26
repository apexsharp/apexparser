using System;
using System.IO;

namespace Apex.ApexSharp.ApexToSharp.Lexer
{
    public class Lexer
    {
        private readonly string _fileName;
        private readonly TokenDefinition[] _tokenDefinitions;
        private string _lineRemaining;

        public Lexer(string fileName, string code, TokenDefinition[] tokenDefinitions)
        {
            _tokenDefinitions = tokenDefinitions;
            _lineRemaining = code;
            _fileName = fileName;
        }

        public Lexer(FileInfo apexFileInfo, TokenDefinition[] tokenDefinitions)
        {
            var apexSourceCode = File.ReadAllText(apexFileInfo.FullName);
            _tokenDefinitions = tokenDefinitions;
            _lineRemaining = apexSourceCode;
            _fileName = apexFileInfo.Name;
        }


        public Result Next()
        {
            if (_lineRemaining.Length == 0) return new Result { IsGood = false };


            foreach (var def in _tokenDefinitions)
            {
                var matched = def.Matcher.Match(_lineRemaining);
                if (matched > 0)
                {
                    var newResult = new Result
                    {
                        TokenType = def.Token,
                        IsGood = true,
                        TokenContents = _lineRemaining.Substring(0, matched)
                    };

                    _lineRemaining = _lineRemaining.Substring(matched);
                    return newResult;
                }
            }

            var lenth = _lineRemaining.Length;

            if (lenth > 50)
            {
                PrintErrorMessage(_fileName, _lineRemaining.Substring(0, 1), _lineRemaining.Substring(0, 50));
            }
            else
            {
                PrintErrorMessage(_fileName, _lineRemaining.Substring(0, 1), _lineRemaining.Substring(0));
            }

            _lineRemaining = _lineRemaining.Substring(1);


            Console.ReadLine();
            return new Result { IsGood = true };
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