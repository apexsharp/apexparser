using System;
using System.Collections.Generic;
using ApexParser.Visitors;
using Sprache;

namespace ApexParser.MetaClass
{
    public class MethodDeclarationSyntax : MemberDeclarationSyntax
    {
        public MethodDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
        }

        public override SyntaxType Kind => SyntaxType.Method;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitMethodDeclaration(this);

        public TypeSyntax ReturnType { get; set; }

        public string Identifier { get; set; }

        public List<ParameterSyntax> Parameters { get; set; } = new List<ParameterSyntax>();

        public BlockSyntax Body { get; set; }

        public bool IsAbstract => Body == null;

        public override MemberDeclarationSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            ReturnType = typeAndName.Type;
            Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier;
            return this;
        }
    }
}