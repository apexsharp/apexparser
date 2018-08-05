using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class WhenElseClauseSyntax : WhenClauseSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhenElseClause;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhenElseClauseSyntax(this);
    }
}
