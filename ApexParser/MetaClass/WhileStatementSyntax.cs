using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class WhileStatementSyntax : StatementSyntax
    {
        public WhileStatementSyntax()
        {
            Kind = SyntaxType.WhileStatement;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitWhileStatement(this);

        public string Expression { get; set; }

        public StatementSyntax LoopBody { get; set; }
    }
}
