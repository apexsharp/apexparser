using System;
using System.Collections.Generic;
using System.Text;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class ThrowStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.ThrowStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitThrowStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
