namespace ApexSharpBase.MetaClass
{
    using System.Collections.Generic;

    public class MethodSyntax : BaseSyntax
    {
        public List<string> Attributes = new List<string>();
        public List<string> Modifiers = new List<string>();
        public string ReturnType { get; set; }
        public string Identifier { get; set; }

        public List<Parameter> Parameters = new List<Parameter>();

        public MethodSyntax()
        {
            Kind = SyntaxType.Method.ToString();
        }
    }
}