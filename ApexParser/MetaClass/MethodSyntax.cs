using System;
using System.Collections.Generic;
using ApexParser.Lexer;
using Sprache;

namespace ApexParser.MetaClass
{
    public class MethodSyntax : ClassMemberSyntax
    {
        public MethodSyntax(ClassMemberSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Method;
        }

        public TypeSyntax ReturnType { get; set; }

        public string Identifier { get; set; }

        public List<ParameterSyntax> MethodParameters { get; set; } = new List<ParameterSyntax>();

        public StatementSyntax Statement { get; set; }

        public override ClassMemberSyntax WithTypeAndName(Tuple<TypeSyntax, IOption<string>> typeAndName)
        {
            ReturnType = typeAndName.Item1;
            Identifier = typeAndName.Item2.GetOrElse(typeAndName.Item1.Identifier);
            return this;
        }
    }
}