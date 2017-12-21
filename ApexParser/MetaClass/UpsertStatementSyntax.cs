using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class UpsertStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.UpsertStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitUpsertStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
