using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;
using Sprache;

namespace ApexParser.MetaClass
{
    public class MemberDeclarationSyntax : BaseSyntax
    {
        public MemberDeclarationSyntax(MemberDeclarationSyntax other = null)
        {
            this.WithProperties(other);
        }

        public override SyntaxType Kind => SyntaxType.ClassMember;

        public override void Accept(ApexSyntaxVisitor visitor) => throw new InvalidOperationException();

        public List<AnnotationSyntax> Annotations { get; set; } = new List<AnnotationSyntax>();

        public List<string> Modifiers { get; set; } = new List<string>();

        public virtual MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            return this;
        }
    }
}
