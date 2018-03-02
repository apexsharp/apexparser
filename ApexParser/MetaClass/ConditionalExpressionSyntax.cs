using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ConditionalExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ConditionalExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitConditionalExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Condition, WhenTrue, WhenFalse);

        public ExpressionSyntax Condition { get; set; }

        public ExpressionSyntax WhenTrue { get; set; }

        public ExpressionSyntax WhenFalse { get; set; }
    }
}
