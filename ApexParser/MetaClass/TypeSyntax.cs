using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class TypeSyntax : BaseSyntax
    {
        public TypeSyntax(IEnumerable<string> qualifiedName)
        {
            Namespaces = qualifiedName.ToList();

            if (Namespaces.Count > 0)
            {
                var lastItem = Namespaces.Count - 1;
                Identifier = Namespaces[lastItem];
                Namespaces.RemoveAt(lastItem);
            }
        }

        public TypeSyntax(params string[] qualifiedName)
            : this(qualifiedName.AsEnumerable())
        {
        }

        public TypeSyntax(TypeSyntax template)
        {
            Namespaces = template.Namespaces;
            Identifier = template.Identifier;
            TypeParameters = template.TypeParameters;
        }

        public List<string> Namespaces { get; set; }

        public string Identifier { get; set; }

        public List<TypeSyntax> TypeParameters { get; set; }
    }
}
