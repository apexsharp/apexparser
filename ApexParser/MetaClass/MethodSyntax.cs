using System.Collections.Generic;
using ApexParser.Lexer;

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

        public string CodeInsideMethod { get; set; }
    }
}