namespace ApexParser.Lexer
{
    public class TokenDefinition
    {
        public TokenDefinition(string regex, TokenType tokenType)
        {
            Matcher = new RegexMatcher(regex);
            TokenType = tokenType;
        }

        public RegexMatcher Matcher { get; }

        public TokenType TokenType { get; }
    }
}
