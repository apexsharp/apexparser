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

        public virtual void VisitAccessor(AccessorDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitAnnotation(AnnotationSyntax node) => DefaultVisit(node);

        public virtual void VisitBlock(BlockSyntax node) => DefaultVisit(node);

        public virtual void VisitBreakStatement(BreakStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitCatch(CatchClauseSyntax node) => DefaultVisit(node);

        public virtual void VisitClassDeclaration(ClassDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitClassInitializer(ClassInitializerSyntax node) => DefaultVisit(node);

        public virtual void VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitDeleteStatement(DeleteStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitDoStatement(DoStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitEnum(EnumDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitEnumMember(EnumMemberDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitExpression(ExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitFieldDeclaration(FieldDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitFieldDeclarator(FieldDeclaratorSyntax node) => DefaultVisit(node);

        public virtual void VisitFinally(FinallyClauseSyntax node) => DefaultVisit(node);

        public virtual void VisitForStatement(ForStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitForEachStatement(ForEachStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitIfStatement(IfStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitInsertStatement(InsertStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitMethodDeclaration(MethodDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitParameter(ParameterSyntax node) => DefaultVisit(node);

        public virtual void VisitPropertyDeclaration(PropertyDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitReturnStatement(ReturnStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitRunAsStatement(RunAsStatementSyntax node) => DefaultVisit(node);

        // Temporary method
        public virtual void VisitStatement(StatementSyntax node) => DefaultVisit(node);

        public virtual void VisitTryStatement(TryStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitType(TypeSyntax node) => DefaultVisit(node);

        public virtual void VisitUpdateStatement(UpdateStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitVariableDeclaration(VariableDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitVariableDeclarator(VariableDeclaratorSyntax node) => DefaultVisit(node);

        public virtual void VisitWhileStatement(WhileStatementSyntax node) => DefaultVisit(node);
    }
}
