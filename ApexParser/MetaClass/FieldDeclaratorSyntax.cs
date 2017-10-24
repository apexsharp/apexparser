using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class FieldDeclaratorSyntax : BaseSyntax
    {
        public override SyntaxType Kind => SyntaxType.FieldDeclarator;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitFieldDeclarator(this);

        public string Identifier { get; set; }

        public string Expression { get; set; }
    }
}
