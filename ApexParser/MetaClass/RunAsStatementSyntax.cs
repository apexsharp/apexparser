using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class RunAsStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.RunAsStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitRunAsStatement(this);

        public string Expression { get; set; }

        public StatementSyntax Statement { get; set; }
    }
}
