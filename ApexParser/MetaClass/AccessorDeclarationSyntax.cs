using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class AccessorDeclarationSyntax : MemberDeclarationSyntax
    {
        public AccessorDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
        }

        public override SyntaxType Kind => SyntaxType.Accessor;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitAccessor(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Body);

        public bool IsGetter { get; set; }

        public bool IsSetter => !IsGetter;

        public BlockSyntax Body { get; set; }

        public bool IsEmpty => Body == null;
    }
}
