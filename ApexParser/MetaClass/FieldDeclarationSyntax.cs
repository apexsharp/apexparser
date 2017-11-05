using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class FieldDeclarationSyntax : MemberDeclarationSyntax
    {
        public FieldDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
        }

        public override SyntaxType Kind => SyntaxType.Field;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitFieldDeclaration(this);

        public override IEnumerable<BaseSyntax> ChildNodes =>
            base.ChildNodes.Concat(GetNodes(Type)).Concat(Fields).Where(n => n != null);

        public override MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            Type = typeAndName.Type;

            var identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier;
            if (!Fields.IsNullOrEmpty())
            {
                Fields[0].Identifier = identifier;
            }

            return this;
        }

        public TypeSyntax Type { get; set; }

        public List<FieldDeclaratorSyntax> Fields { get; set; }
    }
}
