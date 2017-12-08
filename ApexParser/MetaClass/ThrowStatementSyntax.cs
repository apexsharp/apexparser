using System;
using System.Collections.Generic;
using System.Text;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ThrowStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.ThrowStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitThrowStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
