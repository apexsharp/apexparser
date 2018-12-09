using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class ClassOfExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ClassOfExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitClassOfExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type);

        public TypeSyntax Type { get; set; }
    }
}
