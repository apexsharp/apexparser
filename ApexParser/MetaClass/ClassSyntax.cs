using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class ClassSyntax : MemberDeclarationSyntax
    {
        public ClassSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Class;
        }

        public string Identifier { get; set; }

        public List<MethodDeclarationSyntax> Methods { get; set; } = new List<MethodDeclarationSyntax>();

        public List<PropertyDeclarationSyntax> Properties { get; set; } = new List<PropertyDeclarationSyntax>();

        public List<ClassSyntax> InnerClasses { get; set; } = new List<ClassSyntax>();
    }
}
