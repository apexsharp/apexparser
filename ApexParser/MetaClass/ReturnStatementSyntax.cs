using System;
using System.Collections.Generic;
using System.Text;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ReturnStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.ReturnStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitReturnStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
