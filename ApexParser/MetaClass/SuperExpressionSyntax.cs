using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class SuperExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.SuperExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitSuperExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;
    }
}