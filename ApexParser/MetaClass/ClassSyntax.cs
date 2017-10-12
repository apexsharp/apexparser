using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class ClassSyntax : ClassMemberSyntax
    {
        public ClassSyntax(ClassMemberSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Class;
        }

        public string Identifier { get; set; }

        public List<MethodSyntax> Methods { get; set; } = new List<MethodSyntax>();

        public List<PropertySyntax> Properties { get; set; } = new List<PropertySyntax>();

        public List<ClassSyntax> InnerClasses { get; set; } = new List<ClassSyntax>();
    }
}
