using System;
using System.Collections.Generic;
using Sprache;

namespace ApexParser.MetaClass
{
    public class MethodDeclarationSyntax : MemberDeclarationSyntax
    {
        public MethodDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Method;
        }

        public TypeSyntax ReturnType { get; set; }

        public string Identifier { get; set; }

        public List<ParameterSyntax> MethodParameters { get; set; } = new List<ParameterSyntax>();

        public BlockStatementSyntax Block { get; set; }

        public override MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            ReturnType = typeAndName.Type;
            Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier;
            return this;
        }
    }
}