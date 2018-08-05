using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class WhenExpressionsClauseSyntax : WhenClauseSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhenExpressionsClause;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhenExpressionsClauseSyntax(this);

        public override IEnumerable<BaseSyntax> ChildNodes =>
            Expressions.EmptyIfNull().Where(n => n != null).Concat(GetNodes(Block));

        // note: Apex only allows literals here
        public List<ExpressionSyntax> Expressions { get; set; }
    }
}
