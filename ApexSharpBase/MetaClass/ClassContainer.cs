namespace ApexSharpBase.MetaClass
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ClassContainer : BaseSyntax
    {
        // The source code belonging to this container. 
        public string SoureCode { get; set; }

        public ClassContainer()
        {
            Kind = SyntaxType.ClassContainer.ToString();
        }
    }
}
