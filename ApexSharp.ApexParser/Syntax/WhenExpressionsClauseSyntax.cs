using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Toolbox;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class WhenExpressionsClauseSyntax : WhenClauseSyntax
    {
        public override SyntaxType Kind => SyntaxType.WhenExpressionsClause;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhenExpressionsClauseSyntax(this);

        public override IEnumerable<BaseSyntax> ChildNodes =>
            Expressions.EmptyIfNull().Where(n => n != null).Concat(GetNodes(Block));

        // note: Apex only allows literals here
        public List<ExpressionSyntax> Expressions { get; set; } = new List<ExpressionSyntax>();
    }
}
