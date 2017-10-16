using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ForEachStatementSyntax : StatementSyntax
    {
        public ForEachStatementSyntax()
        {
            Kind = SyntaxType.ForEachStatement;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitForEachStatement(this);

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }

        public string Expression { get; set; }

        public StatementSyntax Statement { get; set; }
    }
}
