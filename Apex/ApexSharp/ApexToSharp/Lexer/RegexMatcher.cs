using System.Text.RegularExpressions;

namespace Apex.ApexSharp.ApexToSharp.Lexer
{
    public class RegexMatcher
    {
        private readonly Regex _regex;

        public RegexMatcher(string regex)
        {
            _regex = new Regex($"^{regex}");
        }


        public int Match(string text)
        {
            var m = _regex.Match(text);
            return m.Success ? m.Length : 0;
        }
    }
}