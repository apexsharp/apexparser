using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;

namespace ApexParser.Visitors
{
    public abstract class ApexSyntaxVisitor
    {
        public virtual void DefaultVisit(BaseSyntax node)
        {
        }

        public virtual void VisitBlock(BlockSyntax node) => DefaultVisit(node);

        public virtual void VisitClassDeclaration(ClassDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitDoStatement(DoStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitFieldDeclaration(FieldDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitForStatement(ForStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitIfStatement(IfStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitMethodDeclaration(MethodDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitParameter(ParameterSyntax node) => DefaultVisit(node);

        public virtual void VisitPropertyDeclaration(PropertyDeclarationSyntax node) => DefaultVisit(node);

        // Temporary method
        public virtual void VisitStatement(StatementSyntax node) => DefaultVisit(node);

        public virtual void VisitType(TypeSyntax node) => DefaultVisit(node);

        public virtual void VisitWhileStatement(WhileStatementSyntax node) => DefaultVisit(node);
    }
}
