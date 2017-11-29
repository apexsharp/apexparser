using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApexParser.Toolbox
{
    public static class GenericExpressionHelper
    {
        private static Regex SplitRegex { get; } =
            new Regex(@"
                # SOQL queries
                (\[ \s* (?:select|find) .*? \])|

                # strings
                (\' (\\. | [^\'])* \')|

                # anything else
                (([^\[\']|\[\s*(?!select|find).*?\])*)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public static string[] Split(string expression) => SplitQuery(expression ?? string.Empty).ToArray();

        private static IEnumerable<string> SplitQuery(string expression) =>
            from m in SplitRegex.Matches(expression).OfType<Match>()
            let v = m.Value
            where !string.IsNullOrEmpty(v)
            select v;

        private static Regex SoqlTableRegex { get; } =
            new Regex(@"from\s*(?<Table>\w[\w\d]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string GetSoqlTableName(string soqlQuery)
        {
            var match = SoqlTableRegex.Match(soqlQuery ?? string.Empty);
            if (match.Success)
            {
                return match.Groups["Table"].Value;
            }

            return string.Empty;
        }

        private static Regex SoqlFieldsRegex { get; } =
            new Regex(@"^select \s+ ((?<Field>[^\,\s]*) \s* \, \s*)* (?<Field>[^\,\s]*) \s* from",
                RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        public static string[] GetSoqlFields(string soqlQuery)
        {
            return SoqlFieldsRegex.Match(soqlQuery ?? string.Empty)
                .Groups["Field"].Captures.OfType<Capture>().Select(c => c.Value)
                .Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();
        }

        private static Regex SoqlParameterRegex { get; } =
            new Regex(@"\:\s*(?<Parameter>\w[\w\d\.]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string[] GetSoqlParameters(string soqlQuery)
        {
            var matches = SoqlParameterRegex.Matches(soqlQuery ?? string.Empty).OfType<Match>();
            return matches.Select(m => m.Groups["Parameter"].Value).ToArray();
        }

        private static Regex CSharpSoqlQueryRegex { get; } =
            new Regex(@"Soql\s*\.\s*Query\s*\<[^>]+\>\s*\(\s*\""(?<Query>[^""]*)\""[^\)]*?\)",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string[] ExtractSoqlQueries(string expression)
        {
            return CSharpSoqlQueryRegex.Matches(expression)
                .OfType<Match>().Select(m => m.Groups["Query"].Value).ToArray();
        }

        public static string ConvertSoqlQueriesToApex(string expression)
        {
            return CSharpSoqlQueryRegex.Replace(expression, @"[${Query}]");
        }

        private static Regex CShaspSoqlInsertUpdateDeleteRegex { get; } =
            new Regex(@"Soql\s*\.\s*(?<Operation>Insert|Update|Delete)\s*\(\s*(?<Expression>[^\)]*)\s*\)",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertSoqlStatementsToApex(string expression)
        {
            return CShaspSoqlInsertUpdateDeleteRegex.Replace(expression,
                x => x.Groups["Operation"].Value.ToLower() + " " + x.Groups["Expression"].Value);
        }

        private static Regex ApexTypeofRegex { get; } = new Regex(@"\b(?<Type>\w[\w\d]*)\b\s*\.\s*class",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertTypeofExpressionsToCSharp(string expression)
        {
            // replace string.class with typeof(string)
            return ApexTypeofRegex.Replace(expression, m => $"typeof({m.Groups["Type"].Value})");
        }

        private static Regex CSharpTypeofRegex { get; } = new Regex(@"\btypeof\s*\(\s*(?<Type>[^()]*)\s*\)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertTypeofExpressionsToApex(string expression)
        {
            // replace typeof(string) with string.class
            return CSharpTypeofRegex.Replace(expression, m => $"{m.Groups["Type"].Value}.class");
        }

        private static Regex ApexStringValueofRegex { get; } = new Regex(@"\bstring\s*\.\s*valueOf\s*\((?<Value>[^()]*)\)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static Regex HasSymbolsRegex { get; } = new Regex(@"[\s+\-*/%\$\,\;]");

        public static string ConvertStringValueofToString(string expression)
        {
            string WrapValue(string value) =>
                HasSymbolsRegex.IsMatch(value) ? $"({value})" : value;

            // replace string.valueOf(x) with x.ToString()
            return ApexStringValueofRegex.Replace(expression, m => $"{WrapValue(m.Groups["Value"].Value)}.ToString()");
        }

        private static Regex ApexDateTimeNowRegex { get; } = new Regex(@"\bDateTime\s*\.\s*now\s*\(\s*\)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertApexDateTimeNowToCSharp(string expression)
        {
            // replace Datetime.now() with DateTime.Now
            return ApexDateTimeNowRegex.Replace(expression, "DateTime.Now");
        }

        private static Regex ApexDateTodayRegex { get; } = new Regex(@"\bDate\s*\.\s*today\s*\(\s*\)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertApexDateTodayToCSharp(string expression)
        {
            // replace Date.today() with DateTime.Today
            return ApexDateTodayRegex.Replace(expression, "DateTime.Today");
        }

        private static Regex CSharpDateTimeNowRegex { get; } = new Regex(@"\bDateTime\s*\.\s*Now",
            RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertCSharpDateTimeNowToApex(string expression)
        {
            // replace DateTime.Now with Datetime.now()
            return CSharpDateTimeNowRegex.Replace(expression, "Datetime.now()");
        }

        private static Regex CSharpDateTimeTodayRegex { get; } = new Regex(@"\bDateTime\s*\.\s*Today",
            RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertCSharpDateTimeTodayToApex(string expression)
        {
            // replace DateTime.Today with Date.today()
            return CSharpDateTimeTodayRegex.Replace(expression, "Date.today()");
        }

        private static Regex ApexConstructorInitializerRegex { get; } =
            new Regex(@"
                \b new \s+ (?<ClassName>\w[\w\d_]*) \s*
                \(
                (?<Body>
                    \s*
	                (
	                    (?<PropertyName>\w[\w\d_]*) \s* \= \s* (?<Value> ( [^\,\(\'] | (\' (\\.|[^\'])*  \') | (\([^\)]*\)) )* ) \s* \, \s*
	                )*
	                (?<PropertyName>\w[\w\d_]*) \s* \= \s* (?<Value> ( [^\,\(] | (\'[^\']\') | (\([^\)]*\)) )* ) \s*
                )
                \)",
                RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertApexConstructorInitializerToCSharp(string expression)
        {
            // replace new MyVar(Name = 'X', Value = 10) with new MyVar { Name = 'X', Value = 10 }
            return ApexConstructorInitializerRegex.Replace(expression,
                m => $"new {m.Groups["ClassName"]} {{ {m.Groups["Body"]} }}");
        }
    }
}
