namespace ApexSharpBase.MetaClass
{
    using System.Collections.Generic;

    public class Property : BaseSyntax
    {
        public readonly List<string> Modifiers = new List<string>();
        public List<string> AttributeLists = new List<string>();

        public Property()
        {
        }

        public string Type { get; set; }
        public string Identifier { get; set; }


    }
}