using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class ConstructorSyntax : BaseSyntax
    {
        public ConstructorSyntax()
        {
            Kind = SyntaxType.Constructor;
        }

        public List<string> Attributes { get; set; } = new List<string>();

        public List<string> Modifiers { get; set; } = new List<string>();

        public string Identifier { get; set; }

        public List<ParameterSyntax> Parameters { get; set; } = new List<ParameterSyntax>();

        public string CodeInsideConstructor { get; set; }
    }
}
