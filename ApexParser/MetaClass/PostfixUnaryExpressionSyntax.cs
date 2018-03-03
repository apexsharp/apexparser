using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class PostfixUnaryExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.PostfixUnaryExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitPostfixUnaryExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Operand);

        public ExpressionSyntax Operand { get; set; }

        public string Operator { get; set; }
    }
}
