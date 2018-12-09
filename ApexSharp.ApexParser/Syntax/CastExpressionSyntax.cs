using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class CastExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.CastExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitCastExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type, Expression);

        public TypeSyntax Type { get; set; }

        public ExpressionSyntax Expression { get; set; }
    }
}