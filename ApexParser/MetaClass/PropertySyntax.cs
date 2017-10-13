using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace ApexParser.MetaClass
{
    public class PropertySyntax : ClassMemberSyntax
    {
        public PropertySyntax(ClassMemberSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Property;
        }

        public PropertySyntax(IEnumerable<Tuple<string, StatementSyntax>> gettersOrSetters, ClassMemberSyntax heading = null)
            : this(heading)
        {
            foreach (var item in gettersOrSetters)
            {
                switch (item.Item1)
                {
                    case "get":
                        GetterStatement = item.Item2;
                        continue;

                    case "set":
                        SetterStatement = item.Item2;
                        continue;
                }
            }
        }

        public TypeSyntax Type { get; set; }

        public string Identifier { get; set; }

        public StatementSyntax GetterStatement { get; set; }

        public StatementSyntax SetterStatement { get; set; }

        public override ClassMemberSyntax WithTypeAndName(ParameterSyntax typeAndName)
        {
            Type = typeAndName.Type;
            Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier;
            return this;
        }
    }
}
