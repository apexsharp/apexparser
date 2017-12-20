using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser.Parser;

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
            new Regex(@"Soql\s*\.\s*[Qq]uery\s*\<[^>]+\>\s*\(\s*\""(?<Query>[^""]*)\""[^\)]*?\)",
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
            new Regex(@"Soql\s*\.\s*(?<Operation>[Ii]nsert|[Uu]pdate|[Dd]elete)\s*\(\s*(?<Expression>[^\)]*)\s*\)",
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

        private static Regex ApexAnnotationParameterRegex { get; } =
            new Regex(@"
            (
                \s*
                (?<NameValuePair>  \w[\w\d_]* \s* \= \s*
                    (
                        [\d][\d\.eE\-\+]* |
                        [\w][\w\d_]* (\s* \( [^\)]* \) )? ( \s* \. \s* [\w][\w\d_]* (\s* \( [^\)]* \) )? )*  |
                        (\' (\\.|[^\'])*  \')
                    )
                ) \s*
            )+",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertApexAnnotationParametersToCSharp(string parameters)
        {
            // replace "name1='value1' name2=value2" with "name1='value1', name2=value2"
            var match = ApexAnnotationParameterRegex.Match(parameters);
            if (match.Success)
            {
                return string.Join(", ",
                    match.Groups["NameValuePair"].Captures
                        .OfType<Capture>().Select(c => c.Value));
            }

            return parameters;
        }

        private static Regex ApexInstanceOfTypeRegex { get; } = new Regex(@"\binstanceof\b",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertApexInstanceOfTypeExpressionToCSharp(string expression)
        {
            return ApexInstanceOfTypeRegex.Replace(expression, "is");
        }

        private static Regex CSharpIsTypeRegex { get; } = new Regex(@"\bis\b",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string ConvertCSharpIsTypeExpressionToApex(string expression)
        {
            return CSharpIsTypeRegex.Replace(expression, "instanceof");
        }

        private static Dictionary<string, string> CSharpTypes { get; } =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ApexKeywords.Boolean, "bool" },
                { ApexKeywords.Byte, "byte" },
                { ApexKeywords.Char, "char" },
                { ApexKeywords.Datetime, "Datetime" },
                { ApexKeywords.Date, "Date" },
                { ApexKeywords.Decimal, "decimal" },
                { ApexKeywords.Double, "double" },
                { ApexKeywords.Exception, nameof(Exception) },
                { ApexKeywords.Float, "float" },
                { ApexKeywords.Int, "int" },
                { ApexKeywords.Integer, "int" },
                { ApexKeywords.Long, "long" },
                { ApexKeywords.Object, "object" },
                { ApexKeywords.Short, "short" },
                { ApexKeywords.String, "string" },
                { ApexKeywords.Time, "Time" },
                { ApexKeywords.Void, "void" },
            };

        private static Dictionary<string, string> ApexTypes { get; } =
            CSharpTypes.Where(p => p.Key != ApexKeywords.Int).ToDictionary(p => p.Value, p => p.Key);

        private static Dictionary<Regex, string> CSharpTypeRegex { get; } =
            CSharpTypes.ToDictionary(p => new Regex($"\\b{p.Key}\\b",
                RegexOptions.IgnoreCase | RegexOptions.Compiled), p => p.Value);

        private static Dictionary<Regex, string> ApexTypeRegex { get; } =
            ApexTypes.ToDictionary(p => new Regex($"\\b{p.Key}\\b",
                RegexOptions.Compiled), p => p.Value);

        public static string ConvertApexTypeToCSharp(string identifier) =>
            CSharpTypes.TryGetValue(identifier, out var result) ? result : identifier;

        public static string ConvertCSharpTypeToApex(string identifier) =>
            ApexTypes.TryGetValue(identifier, out var result) ? result : identifier;

        public static string ConvertApexTypesToCSharp(string expression)
        {
            // replace Apex types with C# types
            foreach (var r in CSharpTypeRegex.Select(p => new { Regex = p.Key, p.Value }))
            {
                expression = r.Regex.Replace(expression, r.Value);
            }

            return expression;
        }

        public static string ConvertCSharpTypesToApex(string expression)
        {
            // replace C# types with Apex types
            foreach (var r in ApexTypeRegex.Select(p => new { Regex = p.Key, p.Value }))
            {
                expression = r.Regex.Replace(expression, r.Value);
            }

            return expression;
        }
    }
}
