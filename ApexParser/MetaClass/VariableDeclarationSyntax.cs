using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class VariableDeclarationSyntax : StatementSyntax
    {
        public VariableDeclarationSyntax()
        {
            Kind = SyntaxType.VariableDeclaration;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitVariableDeclaration(this);

        public TypeSyntax Type { get; set; }

        public List<VariableDeclaratorSyntax> Variables { get; set; }
    }
}
