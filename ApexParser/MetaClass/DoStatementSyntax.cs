using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class DoStatementSyntax : StatementSyntax
    {
        public DoStatementSyntax()
        {
            Kind = SyntaxType.DoStatement;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitDoStatement(this);

        public string Expression { get; set; }

        public StatementSyntax LoopBody { get; set; }
    }
}
