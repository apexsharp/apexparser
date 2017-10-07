using System.Collections.Generic;

namespace ApexSharpBase.MetaClass
{
    public class MethodSyntax : BaseSyntax
    {
        public List<string> Attributes = new List<string>();
        public List<string> Modifiers = new List<string>();
        public string ReturnType { get; set; }
        public string Identifier { get; set; }

        public List<ParameterSyntax> Parameters = new List<ParameterSyntax>();

        public MethodSyntax()
        {
            Kind = SyntaxType.Method.ToString();
        }
    }
}