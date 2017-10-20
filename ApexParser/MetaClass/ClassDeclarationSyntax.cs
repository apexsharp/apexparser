using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;
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

        public List<MemberDeclarationSyntax> Members { get; set; } = new List<MemberDeclarationSyntax>();

        // the following members are kept for the unit testing purposes only
        public List<ConstructorDeclarationSyntax> Constructors => Members.OfType<ConstructorDeclarationSyntax>().ToList();

        public List<MethodDeclarationSyntax> Methods => Members.OfExactType<MethodDeclarationSyntax>().ToList();

        public List<FieldDeclarationSyntax> Fields => Members.OfType<FieldDeclarationSyntax>().ToList();

        public List<PropertyDeclarationSyntax> Properties => Members.OfType<PropertyDeclarationSyntax>().ToList();

        public List<EnumDeclarationSyntax> Enums => Members.OfType<EnumDeclarationSyntax>().ToList();

        public List<ClassDeclarationSyntax> InnerClasses => Members.OfType<ClassDeclarationSyntax>().ToList();
    }
}
