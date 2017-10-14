using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class FieldDeclarationSyntax : MemberDeclarationSyntax
    {
        public FieldDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Field;
        }

        public override MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            Type = typeAndName.Type;
            Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier;
            return this;
        }

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }

        public string Expression { get; set; }
    }
}
