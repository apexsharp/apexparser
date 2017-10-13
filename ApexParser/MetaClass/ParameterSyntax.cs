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
            Kind = SyntaxType.MethodParameter;
        }

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }
    }
}