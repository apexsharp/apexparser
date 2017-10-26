using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class InsertStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.InsertStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitInsertStatement(this);

        public string Expression { get; set; }
    }
}
