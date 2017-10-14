using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ConstructorDeclarationSyntax : MethodDeclarationSyntax
    {
        public ConstructorDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Constructor;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitConstructorDeclaration(this);

        public override MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier;
            return this;
        }
    }
}
