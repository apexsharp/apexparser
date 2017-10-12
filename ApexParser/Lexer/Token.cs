namespace ApexParser.Lexer
{
    public class Token
    {
        public Token(TokenType tokenType, string content)
        {
            TokenType = tokenType;
            Content = content;
        }

        public TokenType TokenType { get; set; }
        public string Content { get; set; }
        public override string ToString() => TokenType.ToString().PadRight(25, ' ') + Content.Trim();
    }
}