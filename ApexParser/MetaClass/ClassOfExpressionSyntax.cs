using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ClassOfExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ClassOfExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitClassOfExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type);

        public TypeSyntax Type { get; set; }
    }
}
