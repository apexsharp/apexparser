using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ApexParser.MetaClass;
using ApexParser.Visitors;

namespace ApexSharpDemo.ApexCodeFormat
{
    public class CustomApexCodeGenerator : ApexCodeGeneratorBase
    {
        public static string FormatApex(string apexCode, Settings settings = null)
        {
            if (string.IsNullOrWhiteSpace(apexCode))
            {
                return string.Empty;
            }

            var apexAst = ApexParser.ApexSharpParser.GetApexAst(apexCode);
            return GenerateApex(apexAst, settings);
        }

        public static string GenerateApex(BaseSyntax ast, Settings settings = null)
        {
            if (ast == null)
            {
                return string.Empty;
            }

            settings = settings ?? new Settings();
            var generator = new CustomApexCodeGenerator
            {
                Settings = settings,
                IndentSize = settings.TabIndentSize
            };

            ast.Accept(generator);
            return generator.Code.ToString();
        }

        private Settings Settings { get; set; }

        protected override void AppendExpressionPart(string part)
        {
            if (Settings.SingleLine)
            {
                part = Regex.Replace(part, @"\s*[\r\n]\s*", " ");
            }

            base.AppendExpressionPart(part);
        }
    }
}
