namespace ApexSharpBase.MetaClass
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FieldDeclaration : BaseSyntax
    {
        public readonly List<string> AttributeLists = new List<string>();
        public readonly List<string> IdentifierList = new List<string>();
        public readonly List<string> Modifiers = new List<string>();

        public FieldDeclaration()
        {
            Kind = SyntaxType.FieldDeclaration.ToString();
        }

        public string Type { get; set; }
        public string Initializaer { get; set; }
    }
}
