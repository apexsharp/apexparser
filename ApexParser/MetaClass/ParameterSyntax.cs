using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ParameterSyntax : BaseSyntax
    {
        public ParameterSyntax(string type, string identifier)
            : this(new TypeSyntax(type), identifier)
        {
        }

        public ParameterSyntax(TypeSyntax type, string identifier)
        {
            Type = type;
            Identifier = identifier;
            Kind = SyntaxType.Parameter;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitParameter(this);

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }
    }
}