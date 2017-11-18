using System.Collections.Generic;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ParameterSyntax : BaseSyntax, IAnnotatedSyntax
    {
        public ParameterSyntax(string type, string identifier)
            : this(new TypeSyntax(type), identifier)
        {
        }

        public ParameterSyntax(TypeSyntax type, string identifier)
        {
            Type = type;
            Identifier = identifier;
        }

        public override SyntaxType Kind => SyntaxType.Parameter;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitParameter(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Type);

        public List<AnnotationSyntax> Annotations { get; set; } = new List<AnnotationSyntax>();

        public List<string> Modifiers { get; set; } = new List<string>();

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }
    }
}