using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class DoStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.DoStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitDoStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression, Statement);

        public ExpressionSyntax Expression { get; set; }

        public StatementSyntax Statement { get; set; }
    }
}
