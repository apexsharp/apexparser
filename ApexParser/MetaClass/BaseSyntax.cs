using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public abstract class BaseSyntax
    {
        public abstract SyntaxType Kind { get; }

        public abstract void Accept(ApexSyntaxVisitor visitor);

        public List<BaseSyntax> ChildNodes { get; set; } = new List<BaseSyntax>();

        public List<string> LeadingComments { get; set; } = new List<string>();

        public List<string> TrailingComments { get; set; } = new List<string>();
    }
}
