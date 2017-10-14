using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ForStatementSyntax : StatementSyntax
    {
        public ForStatementSyntax()
        {
            Kind = SyntaxType.ForStatement;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitForStatement(this);

        public string Expression { get; set; }

        public StatementSyntax LoopBody { get; set; }
    }
}
