namespace Apex.ApexSharp.ApexToSharp.Lexer
{
    public class Result
    {
        public bool IsGood { get; set; }
        public TockenType TokenType { get; set; }
        public string TokenContents { get; set; }
    }
}