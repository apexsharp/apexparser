using System.Collections.Generic;
using System.Linq;
using ApexSharp.ApexParser.Toolbox;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class InvocationExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.InvocationExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitInvocationExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression).Concat(Arguments.EmptyIfNull());

        public ExpressionSyntax Expression { get; set; }

        public List<ExpressionSyntax> Arguments { get; set; } = new List<ExpressionSyntax>();
    }
}
