namespace Apex.ApexSharp.ApexToSharp.Lexer
{
    public class TokenDefinition
    {
        public readonly RegexMatcher Matcher;
        public readonly TockenType Token;

        public TokenDefinition(string regex, TockenType token)
        {
            Matcher = new RegexMatcher(regex);
            Token = token;
        }
    }
}