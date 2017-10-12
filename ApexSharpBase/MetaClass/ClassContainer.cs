namespace ApexSharpBase.MetaClass
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ClassContainer : BaseSyntax
    {
        // What Lang does this container contains , APEX or C#
        public string ContainerLang { get; set; }

        public ClassContainer()
        {
            Kind = SyntaxType.ClassContainer.ToString();
        }
    }
}
