using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class StatementSyntax : BaseSyntax
    {
        public StatementSyntax(string body = null)
        {
            Body = body;
        }

        public override SyntaxType Kind => SyntaxType.Statement;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitStatement(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;

        public bool IsEmpty => string.IsNullOrWhiteSpace(Body);

        public string Body { get; set; }
    }
}
