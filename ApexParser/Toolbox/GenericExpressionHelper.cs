using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApexParser.Toolbox
{
    internal static class GenericExpressionHelper
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

        private static Regex SoqlParameterRegex { get; } =
            new Regex(@"\:\s*(?<Parameter>\w[\w\d\.]*)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        public static string[] GetSoqlParameters(string soqlQuery)
        {
            var matches = SoqlParameterRegex.Matches(soqlQuery ?? string.Empty).OfType<Match>();
            return matches.Select(m => m.Groups["Parameter"].Value).ToArray();
        }
    }
}
