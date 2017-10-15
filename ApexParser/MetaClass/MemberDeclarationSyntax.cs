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
            WithProperties(other);
        }

        public override void Accept(ApexSyntaxVisitor visitor) => throw new InvalidOperationException();

        public List<string> Attributes { get; set; } = new List<string>();

        public List<string> Modifiers { get; set; } = new List<string>();

        public MemberDeclarationSyntax WithProperties(MemberDeclarationSyntax other = null)
        {
            if (other != null)
            {
                CodeComments = other.CodeComments;
                Attributes = other.Attributes;
                Modifiers = other.Modifiers;
            }

            return this;
        }

        public virtual MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            return this;
        }
    }
}
