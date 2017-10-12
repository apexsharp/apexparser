using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class PropertySyntax : ClassMemberSyntax
    {
        public PropertySyntax(ClassMemberSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Property;
        }

        public PropertySyntax(IEnumerable<Tuple<string, string>> gettersOrSetters, ClassMemberSyntax heading = null)
            : this(heading)
        {
            foreach (var item in gettersOrSetters)
            {
                switch (item.Item1)
                {
                    case "get":
                        GetterCode = item.Item2;
                        continue;

                    case "set":
                        SetterCode = item.Item2;
                        continue;
                }
            }
        }

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }

        public string GetterCode { get; set; }

        public string SetterCode { get; set; }
    }
}
