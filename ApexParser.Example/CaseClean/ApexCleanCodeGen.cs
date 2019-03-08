using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ApexParser.MetaClass;
using ApexParser.Visitors;

namespace ApexSharpDemo.CaseClean
{
    public class ApexCleanCodeGen : ApexCodeGeneratorBase
    {
        public static string NormalizeCode(string apexCode)
        {
            var apexAst = ApexParser.ApexSharpParser.GetApexAst(apexCode);
            return GenerateApex(apexAst);
        }

        public static string GenerateApex(BaseSyntax ast, int tabSize = 4)
        {
            var generator = new ApexCleanCodeGen { IndentSize = tabSize };
            ast.Accept(generator);
            return generator.Code.ToString();
        }

        internal static string NormalizeWords(string part)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                return part;
            }

            return Regex.Replace(part, @"([A-Za-z]\w+)", m =>
            {
                if (CaseCleaner.SalesForceNames.TryGetValue(m.Value, out var actualValue))
                {
                    return actualValue;
                }

                return m.Value;
            });
        }

        protected override void AppendExpressionPart(string part)
        {
            base.AppendExpressionPart(NormalizeWords(part));
        }
    }
}
