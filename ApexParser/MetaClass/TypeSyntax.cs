using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class TypeSyntax : BaseSyntax
    {
        public TypeSyntax(IEnumerable<string> qualifiedName)
        {
            Kind = SyntaxType.Type;
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
            Kind = SyntaxType.Type;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitType(this);

        public List<string> Namespaces { get; set; }

        public string Identifier { get; set; }

        public List<TypeSyntax> TypeParameters { get; set; }

        // Note: arrays of arrays are not allowed
        // https://developer.salesforce.com/page/Apex_Code:_The_Basics#Arrays
        public bool IsArray { get; set; }
    }
}
