using System;
using System.Collections.Generic;
using System.Text;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ContinueStatementSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.ContinueStatement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitContinueStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;
    }
}
