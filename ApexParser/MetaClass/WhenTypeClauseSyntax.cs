using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class WhenTypeClauseSyntax : WhenClauseSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhenTypeClause;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhenTypeClauseSyntax(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type, Statement);

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }
    }
}
