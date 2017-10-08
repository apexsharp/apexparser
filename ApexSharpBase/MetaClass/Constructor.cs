namespace ApexSharpBase.MetaClass
{
    using System.Collections.Generic;

    public class Constructor : BaseSyntax
    {
        public List<string> Attributes = new List<string>();
        public List<string> Modifiers = new List<string>();
        public string Identifier { get; set; }
        public List<Parameter> Parameters = new List<Parameter>();

        public Constructor()
        {
            Kind = SyntaxType.Constructor.ToString();
        }
    }
}
