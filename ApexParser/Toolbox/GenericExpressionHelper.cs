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
                RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        public static string[] Split(string expression) => SplitQuery(expression ?? string.Empty).ToArray();

        private static IEnumerable<string> SplitQuery(string expression) =>
            from m in SplitRegex.Matches(expression).OfType<Match>()
            let v = m.Value
            where !string.IsNullOrEmpty(v)
            select v;
    }
}
