using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class PrefixUnaryExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxType Kind => SyntaxType.PrefixUnaryExpression;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitPrefixUnaryExpression(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Operand);

        public ExpressionSyntax Operand { get; set; }

        public string Operator { get; set; }
    }
}
