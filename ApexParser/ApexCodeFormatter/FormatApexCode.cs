using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser.Lexer;

namespace ApexParser.ApexCodeFormatter
{
    public class FormatApexCode
    {
        public const int IndentSize = 5;

        private static TokenType[] ValidCommentPositions { get; } = new[]
        {
            TokenType.OpenCurlyBrackets,
            TokenType.CloseCurlyBrackets,
            TokenType.StatementTerminator,
            TokenType.AccessModifier,
            TokenType.KwVoid
        };

        public static string GetFormattedApexCode(string apexCode)
        {
            var formatedApexCode = FormatApexCodeNoIndent(apexCode);
            var indentedApexCode = IndentApexCode(formatedApexCode);
            return indentedApexCode;
        }

        public static List<string> FormatApexCodeNoIndent(string apexCode)
        {
            List<string> apexCodeList = new List<string>();

            var apexTokens = ApexLexer.GetApexTokens(apexCode);
            var multiLineCommentLevel = 0;
            var bracketNestingLevel = 0;
            var lastValidCommentPosition = 0;
            var lastTokenType = TokenType.Empty;

            string apexLine = string.Empty;
            foreach (var apexToken in apexTokens)
            {
                // Reposition the comment at the start of the last statement
                if (apexToken.TokenType == TokenType.CommentLine && multiLineCommentLevel == 0)
                {
                    apexCodeList.Insert(lastValidCommentPosition, apexToken.Content.Trim());
                    apexLine = apexLine.TrimEnd();
                    lastValidCommentPosition++;
                }

                // Replace the original newlines with a single space unless it's a multi-line comment
                else if (apexToken.TokenType == TokenType.Return)
                {
                    if (multiLineCommentLevel == 0 && lastTokenType != TokenType.CommentEnd)
                    {
                        if (lastTokenType != TokenType.Space)
                        {
                            apexLine += " ";
                        }

                        lastTokenType = TokenType.Space;
                        continue;
                    }

                    apexCodeList.Add(apexLine.Trim());
                    apexLine = string.Empty;
                }

                // Ignore duplicate spaces unless it's a multi-line comment
                else if (apexToken.TokenType == TokenType.Space && multiLineCommentLevel == 0)
                {
                    // Replace multiple spaces with a single one
                    if (lastTokenType != TokenType.Space)
                    {
                        apexLine += " ";
                    }

                    lastTokenType = apexToken.TokenType;
                    continue;
                }

                // If we have a ';' then next line should be new
                else if (apexToken.TokenType == TokenType.StatementTerminator && bracketNestingLevel == 0)
                {
                    apexLine = apexLine + apexToken.Content;

                    if (lastTokenType != TokenType.KwGetSet)
                    {
                        apexCodeList.Add(apexLine.Trim());
                        apexLine = string.Empty;
                    }
                }

                // '{' and "}" should be on its own line
                else if (apexToken.TokenType == TokenType.OpenCurlyBrackets || apexToken.TokenType == TokenType.CloseCurlyBrackets)
                {
                    apexCodeList.Add(apexLine.Trim());
                    apexCodeList.Add(apexToken.Content);
                    apexLine = string.Empty;
                }
                else
                {
                    apexLine = apexLine + apexToken.Content;
                }

                // Save the last statement starting position
                if (ValidCommentPositions.Contains(apexToken.TokenType))
                {
                    lastValidCommentPosition = apexCodeList.Count;
                }

                // Compute the multi-line comment nesting level
                else if (apexToken.TokenType == TokenType.CommentStart)
                {
                    multiLineCommentLevel++;
                }
                else if (apexToken.TokenType == TokenType.CommentEnd)
                {
                    multiLineCommentLevel--;
                }

                // Compute the bracket nesting level
                else if (apexToken.TokenType == TokenType.OpenBrackets)
                {
                    bracketNestingLevel++;
                }
                else if (apexToken.TokenType == TokenType.CloseBrackets)
                {
                    bracketNestingLevel--;
                }

                lastTokenType = apexToken.TokenType;
            }

            // Make sure that the last line isn't lost if it's not empty
            if (!string.IsNullOrWhiteSpace(apexLine))
            {
                apexCodeList.Add(apexLine.Trim());
            }

            // Ignore empty lines and collapse empty getters/setters
            var newApexCodeList = new List<string>();
            for (var index = 0; index < apexCodeList.Count; index++)
            {
                var apexCodeLine = apexCodeList[index];
                if (apexCodeLine == "{" &&
                    index < apexCodeList.Count - 2 &&
                    apexCodeList[index + 2] == "}" &&
                    IsEmptyGetterOrSetter(apexCodeList[index + 1]))
                {
                    apexCodeLine = $"{{ {apexCodeList[index + 1]} }}";
                    index += 2;

                    if (newApexCodeList.Count > 0)
                    {
                        newApexCodeList[newApexCodeList.Count - 1] += " " + apexCodeLine;
                        continue;
                    }
                }

                if (apexCodeLine.Length != 0)
                {
                    newApexCodeList.Add(apexCodeLine);
                }
            }

            return newApexCodeList;
        }

        // Detects empty getters and setters in any order
        private static bool IsEmptyGetterOrSetter(string line) =>
            Regex.IsMatch(line, @"^((get|set)\s*\;\s*)+$");

        public static string IndentApexCode(List<string> apexCodeList)
        {
            var sb = new StringBuilder();
            var needExtraLine = false;
            var padding = 0;

            foreach (var apexCode in apexCodeList)
            {
                if (apexCode.Trim() == "}")
                {
                    padding = padding - IndentSize;
                    needExtraLine = true;
                }
                else if (apexCode.Trim().EndsWith("}"))
                {
                    needExtraLine = true;
                }
                else if (needExtraLine)
                {
                    sb.AppendLine();
                    needExtraLine = false;
                }

                sb.AppendLine(new string(' ', padding) + apexCode);

                if (apexCode.Trim() == "{")
                {
                    padding = padding + IndentSize;
                }
            }

            return sb.ToString();
        }
    }
}
