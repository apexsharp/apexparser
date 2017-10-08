namespace ApexSharpBase.MetaClass
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AccessorDeclaration : BaseSyntax
    {
        public readonly List<string> Modifiers = new List<string>();
        public List<string> AttributeLists = new List<string>();

        public AccessorDeclaration()
        {

        }

        public string Accessor { get; set; }

        public bool ContainsChildren { get; set; }
    }
}
