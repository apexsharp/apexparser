using System.Collections.Generic;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class DeleteStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.DeleteStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitDeleteStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}