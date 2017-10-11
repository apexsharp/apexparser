namespace ApexSharpBase.MetaClass
{
    using System.Collections.Generic;
    using ApexSharpBase.Converter;

    public class ClassSyntax : BaseSyntax
    {
        public bool IsShareable { get; set; }
        public List<string> Attributes = new List<string>();
        public List<string> Modifiers = new List<string>();
        public string Identifier { get; set; }
        public List<BaseSyntax> Methods = new List<BaseSyntax>();

        public ClassSyntax()
        {
            Kind = SyntaxType.Class.ToString();
        }

        public void Accept(BaseVisitor visitor)
        {
            visitor.VisitClassDeclaration(this);
        }
    }
}
