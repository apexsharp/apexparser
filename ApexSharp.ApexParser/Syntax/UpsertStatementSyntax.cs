using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class UpsertStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.UpsertStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitUpsertStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
