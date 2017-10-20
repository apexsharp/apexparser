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
        }

        public override SyntaxType Kind => SyntaxType.Class;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitClassDeclaration(this);

        public string Identifier { get; set; }

        public TypeSyntax BaseType { get; set; }

        public bool IsInterface { get; set; }

        public List<TypeSyntax> Interfaces { get; set; } = new List<TypeSyntax>();

        public List<ConstructorDeclarationSyntax> Constructors { get; set; } = new List<ConstructorDeclarationSyntax>();

        public List<MethodDeclarationSyntax> Methods { get; set; } = new List<MethodDeclarationSyntax>();

        public List<FieldDeclarationSyntax> Fields { get; set; } = new List<FieldDeclarationSyntax>();

        public List<PropertyDeclarationSyntax> Properties { get; set; } = new List<PropertyDeclarationSyntax>();

        public List<ClassDeclarationSyntax> InnerClasses { get; set; } = new List<ClassDeclarationSyntax>();
    }
}
