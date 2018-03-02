using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ThisExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ThisExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitThisExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;
    }
}