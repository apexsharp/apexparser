using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class VariableDeclaratorSyntax : BaseSyntax
    {
        public override SyntaxType Kind => SyntaxType.VariableDeclarator;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitVariableDeclarator(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;

        public string Identifier { get; set; }

        public string Expression { get; set; }
    }
}
