using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class UpdateStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.UpdateStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitUpdateStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public ExpressionSyntax Expression { get; set; }
    }
}
