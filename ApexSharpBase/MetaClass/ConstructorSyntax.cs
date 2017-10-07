using System.Collections.Generic;

namespace ApexSharpBase.MetaClass
{
    public class ConstructorSyntax : BaseSyntax
    {
        public List<string> Attributes = new List<string>();
        public List<string> Modifiers = new List<string>();
        public string Identifier { get; set; }
        public List<ParameterSyntax> Parameters = new List<ParameterSyntax>();

        public ConstructorSyntax()
        {
            Kind = SyntaxType.Constructor.ToString();
        }
    }
}
