using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class BreakStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.BreakStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitBreakStatement(this);
    }
}
