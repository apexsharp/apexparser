using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class WhenElseClauseSyntax : WhenClauseSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhenElseClause;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhenElseClauseSyntax(this);
    }
}
