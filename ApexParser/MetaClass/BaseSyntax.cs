using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public abstract class BaseSyntax
    {
        public abstract void Accept(ApexSyntaxVisitor visitor);

        public List<BaseSyntax> ChildNodes { get; set; } = new List<BaseSyntax>();

        public List<string> CodeComments { get; set; } = new List<string>();

        public SyntaxType Kind { get; set; }
    }
}
