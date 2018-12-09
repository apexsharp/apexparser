using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class InterfaceDeclarationSyntax : ClassDeclarationSyntax
    {
        public InterfaceDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
        }

        public InterfaceDeclarationSyntax(MemberDeclarationSyntax heading, ClassDeclarationSyntax classBody)
            : base(heading, classBody)
        {
        }

        public override SyntaxType Kind => SyntaxType.Interface;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitInterfaceDeclaration(this);

        public override bool IsInterface => true;
    }
}
