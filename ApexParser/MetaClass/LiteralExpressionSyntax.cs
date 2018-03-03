using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class LiteralExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.LiteralExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitLiteralExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;

        public string Token { get; set; }
    }
}
