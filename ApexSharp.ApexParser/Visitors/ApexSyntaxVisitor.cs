using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Syntax;

namespace ApexSharp.ApexParser.Visitors
{
    public abstract class ApexSyntaxVisitor
    {
        public virtual void DefaultVisit(BaseSyntax node)
        {
        }

        public virtual void VisitAccessor(AccessorDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitAnnotation(AnnotationSyntax node) => DefaultVisit(node);

        public virtual void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitAssignmentExpression(AssignmentExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitBinaryExpression(BinaryExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitBlock(BlockSyntax node) => DefaultVisit(node);

        public virtual void VisitBreakStatement(BreakStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitCastExpression(CastExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitCatch(CatchClauseSyntax node) => DefaultVisit(node);

        public virtual void VisitClassDeclaration(ClassDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitClassInitializer(ClassInitializerSyntax node) => DefaultVisit(node);

        public virtual void VisitClassOfExpression(ClassOfExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitConditionalExpression(ConditionalExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitContinueStatement(ContinueStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitDeleteStatement(DeleteStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitDoStatement(DoStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitElementAccessExpression(ElementAccessExpressionSyntax node) => DefaultVisit(node);

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

        public virtual void VisitInstanceOfExpression(InstanceOfExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitInvocationExpression(InvocationExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitLiteralExpression(LiteralExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitMemberAccessExpression(MemberAccessExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitMethodDeclaration(MethodDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitParameter(ParameterSyntax node) => DefaultVisit(node);

        public virtual void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitPropertyDeclaration(PropertyDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitReturnStatement(ReturnStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitRunAsStatement(RunAsStatementSyntax node) => DefaultVisit(node);

        // Temporary method
        public virtual void VisitStatement(StatementSyntax node) => DefaultVisit(node);

        public virtual void VisitSuperExpression(SuperExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitSwitchStatement(SwitchStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitThisExpression(ThisExpressionSyntax node) => DefaultVisit(node);

        public virtual void VisitThrowStatement(ThrowStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitTryStatement(TryStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitType(TypeSyntax node) => DefaultVisit(node);

        public virtual void VisitUpdateStatement(UpdateStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitUpsertStatement(UpsertStatementSyntax node) => DefaultVisit(node);

        public virtual void VisitVariableDeclaration(VariableDeclarationSyntax node) => DefaultVisit(node);

        public virtual void VisitVariableDeclarator(VariableDeclaratorSyntax node) => DefaultVisit(node);

        public virtual void VisitWhenElseClauseSyntax(WhenElseClauseSyntax node) => DefaultVisit(node);

        public virtual void VisitWhenExpressionsClauseSyntax(WhenExpressionsClauseSyntax node) => DefaultVisit(node);

        public virtual void VisitWhenTypeClauseSyntax(WhenTypeClauseSyntax node) => DefaultVisit(node);

        public virtual void VisitWhileStatement(WhileStatementSyntax node) => DefaultVisit(node);
    }
}
