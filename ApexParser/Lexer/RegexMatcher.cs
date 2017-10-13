using System.Text.RegularExpressions;

namespace ApexParser.Lexer
{
    public class RegexMatcher
    {
        private Regex Regex { get; }

        public RegexMatcher(string regex)
        {
            Regex = new Regex($"^{regex}");
        }

        public int Match(string text)
        {
            var m = Regex.Match(text);
            return m.Success ? m.Length : 0;
        }
    }
}