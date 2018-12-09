using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class ThisExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ThisExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitThisExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;
    }
}