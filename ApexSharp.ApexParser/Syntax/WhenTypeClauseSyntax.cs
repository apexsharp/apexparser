using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class WhenTypeClauseSyntax : WhenClauseSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhenTypeClause;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhenTypeClauseSyntax(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type, Block);

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }
    }
}
