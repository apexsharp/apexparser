using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class SuperExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.SuperExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitSuperExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;
    }
}