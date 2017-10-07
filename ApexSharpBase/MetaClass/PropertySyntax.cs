using System.Collections.Generic;

namespace ApexSharpBase.MetaClass
{
    public class PropertySyntax : BaseSyntax
    {
        public readonly List<string> Modifiers = new List<string>();
        public List<string> AttributeLists = new List<string>();

        public PropertySyntax()
        {
        }

        public string Type { get; set; }
        public string Identifier { get; set; }


    }
}