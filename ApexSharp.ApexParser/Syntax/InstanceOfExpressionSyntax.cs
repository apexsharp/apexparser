using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class InstanceOfExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.InstanceOfExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitInstanceOfExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression, Type);

        public ExpressionSyntax Expression { get; set; }

        public TypeSyntax Type { get; set; }
    }
}