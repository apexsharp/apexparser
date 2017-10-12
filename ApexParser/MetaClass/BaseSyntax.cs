using System.Collections.Generic;

namespace ApexParser.MetaClass
{
    public class BaseSyntax
    {
        public List<BaseSyntax> ChildNodes { get; set; } = new List<BaseSyntax>();

        public List<string> CodeComments { get; set; } = new List<string>();

        public SyntaxType Kind { get; set; }
    }
}
