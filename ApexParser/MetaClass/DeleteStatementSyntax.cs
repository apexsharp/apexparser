using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class DeleteStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.DeleteStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitDeleteStatement(this);

        public string Expression { get; set; }
    }
}