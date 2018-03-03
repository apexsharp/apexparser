using System.Collections.Generic;
using System.Linq;
using ApexParser.Toolbox;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ObjectCreationExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ObjectCreationExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitObjectCreationExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type).Concat(Arguments.EmptyIfNull());

        public TypeSyntax Type { get; set; }

        public List<ExpressionSyntax> Arguments { get; set; }
    }
}
