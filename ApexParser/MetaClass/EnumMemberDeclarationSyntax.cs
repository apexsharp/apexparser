using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class EnumMemberDeclarationSyntax : MemberDeclarationSyntax
    {
        public EnumMemberDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
        }

        public override SyntaxType Kind => SyntaxType.EnumMember;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitEnumMember(this);

        public string Identifier { get; set; }
    }
}
