using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class InsertStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.InsertStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitInsertStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
