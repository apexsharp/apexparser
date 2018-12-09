using System.Collections.Generic;
using System.Linq;
using ApexSharp.ApexParser.Toolbox;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class ElementAccessExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.ElementAccessExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitElementAccessExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression).Concat(Arguments.EmptyIfNull());

        public ExpressionSyntax Expression { get; set; }

        public List<ExpressionSyntax> Arguments { get; set; } = new List<ExpressionSyntax>();
    }
}
