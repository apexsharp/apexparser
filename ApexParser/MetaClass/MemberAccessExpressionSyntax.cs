using System.Collections.Generic;
using System.Linq;
using ApexParser.Toolbox;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class MemberAccessExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.MemberAccessExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitMemberAccessExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }

        public string Name { get; set; }
    }
}
