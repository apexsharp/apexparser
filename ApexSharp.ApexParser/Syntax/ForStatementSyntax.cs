using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class ForStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.ForStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitForStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Declaration, Statement);

        public VariableDeclarationSyntax Declaration { get; set; }

        public ExpressionSyntax Condition { get; set; }

        public List<ExpressionSyntax> Incrementors { get; set; }

        public StatementSyntax Statement { get; set; }
    }
}
