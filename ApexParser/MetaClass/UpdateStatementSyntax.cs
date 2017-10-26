using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class UpdateStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.UpdateStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitUpdateStatement(this);

        public string Expression { get; set; }
    }
}
