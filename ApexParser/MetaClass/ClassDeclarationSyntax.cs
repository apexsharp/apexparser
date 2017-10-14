using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class ClassDeclarationSyntax : MemberDeclarationSyntax
    {
        public ClassDeclarationSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
            Kind = SyntaxType.Class;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitClassDeclaration(this);

        public string Identifier { get; set; }

        public List<MethodDeclarationSyntax> Methods { get; set; } = new List<MethodDeclarationSyntax>();

        public List<FieldDeclarationSyntax> Fields { get; set; } = new List<FieldDeclarationSyntax>();

        public List<PropertyDeclarationSyntax> Properties { get; set; } = new List<PropertyDeclarationSyntax>();

        public List<ClassDeclarationSyntax> InnerClasses { get; set; } = new List<ClassDeclarationSyntax>();
    }
}
