using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using Sprache;

namespace ApexParser.Parser
{
    public class ApexGrammar
    {
        // examples: a, Apex, code123
        protected internal virtual Parser<string> Identifier =>
        (
            from identifier in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit)
            where !ApexKeywords.All.Contains(identifier)
            select identifier
        )
        .Token().Named("Identifier");

        // examples: System.debug
        protected internal virtual Parser<IEnumerable<string>> QualifiedIdentifier =>
            Identifier.DelimitedBy(Parse.Char('.').Token())
                .Named("QualifiedIdentifier");

        // examples: /* default settings are OK */ //
        protected internal virtual CommentParser CommentParser { get; } = new CommentParser();

        // example: @isTest
        protected internal virtual Parser<string> Annotation =>
            from at in Parse.Char('@').Token()
            from identifier in Identifier
            select identifier;

        // examples: int, void
        protected internal virtual Parser<TypeSyntax> PrimitiveType =>
            Parse.String(ApexKeywords.Boolean).Or(
            Parse.String(ApexKeywords.Byte)).Or(
            Parse.String(ApexKeywords.Char)).Or(
            Parse.String(ApexKeywords.Double)).Or(
            Parse.String(ApexKeywords.Float)).Or(
            Parse.String(ApexKeywords.Int)).Or(
            Parse.String(ApexKeywords.Long)).Or(
            Parse.String(ApexKeywords.Short)).Or(
            Parse.String(ApexKeywords.Void))
                .Token().Text().Select(n => new TypeSyntax(n))
                .Named("PrimitiveType");

        // examples: int, String, System.Collections.Hashtable
        protected internal virtual Parser<TypeSyntax> NonGenericType =>
            PrimitiveType.Or(QualifiedIdentifier.Select(qi => new TypeSyntax(qi)));

        // examples: string, int, char
        protected internal virtual Parser<IEnumerable<TypeSyntax>> TypeParameters =>
            from open in Parse.Char('<').Token()
            from types in TypeReference.DelimitedBy(Parse.Char(',').Token())
            from close in Parse.Char('>').Token()
            select types;

        // example: string, List<string>, Map<string, List<boolean>>
        protected internal virtual Parser<TypeSyntax> TypeReference =>
            from type in NonGenericType
            from parameters in TypeParameters.Optional()
            select new TypeSyntax(type)
            {
                TypeParameters = parameters.GetOrElse(Enumerable.Empty<TypeSyntax>()).ToList(),
            };

        // example: string name
        protected internal virtual Parser<ParameterSyntax> ParameterDeclaration =>
            from type in TypeReference
            from name in Identifier
            select new ParameterSyntax(type, name);

        // example: int a, Boolean flag
        protected internal virtual Parser<List<ParameterSyntax>> ParameterDeclarations =>
            from first in ParameterDeclaration.Once()
            from rest in Parse.Char(',').Then(_ => ParameterDeclaration).Many()
            select first.Concat(rest).ToList();

        // example: (string a, char delimiter)
        protected internal virtual Parser<List<ParameterSyntax>> MethodParameters =>
            from openBrace in Parse.Char('(').Token()
            from param in ParameterDeclarations.Optional()
            from closeBrace in Parse.Char(')').Token()
            select param.GetOrElse(new List<ParameterSyntax>());

        // examples: public, private, with sharing
        protected internal virtual Parser<string> Modifier =>
            Parse.String(ApexKeywords.Public).Or(
            Parse.String(ApexKeywords.Protected)).Or(
            Parse.String(ApexKeywords.Private)).Or(
            Parse.String(ApexKeywords.Static)).Or(
            Parse.String(ApexKeywords.Abstract)).Or(
            Parse.String(ApexKeywords.Final)).Or(
            Parse.String(ApexKeywords.Global)).Or(
            Parse.String(ApexKeywords.WebService)).Or(
            Parse.String(ApexKeywords.Override)).Or(
            Parse.String(ApexKeywords.Virtual)).Or(
            Parse.String(ApexKeywords.TestMethod)).Or(
            Parse.String(ApexKeywords.With).Token().Then(_ => Parse.String(ApexKeywords.Sharing)).Return("with_sharing")).Or(
            Parse.String(ApexKeywords.Without).Token().Then(_ => Parse.String(ApexKeywords.Sharing)).Return("without_sharing")).Or(
            Parse.String("todo?"))
                .Text().Token().Named("Modifier");

        // examples:
        // @isTest void Test() {}
        // public static void Hello() {}
        protected internal virtual Parser<MethodSyntax> MethodDeclaration =>
            from heading in ClassMemberHeading
            from typeAndName in TypeAndName
            from methodBody in MethodParametersAndBody
            select new MethodSyntax(heading)
            {
                Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier,
                ReturnType = typeAndName.Type,
                MethodParameters = methodBody.MethodParameters,
                Statement = methodBody.Statement,
            };

        // examples: string Name, void Test
        protected internal virtual Parser<ParameterSyntax> TypeAndName =>
            from type in TypeReference
            from name in Identifier.Optional()
            select new ParameterSyntax(type, name.GetOrDefault());

        // examples:
        // void Test() {}
        // string Hello(string name) {}
        protected internal virtual Parser<MethodSyntax> MethodParametersAndBody =>
            from parameters in MethodParameters
            from methodBody in Block.Token()
            select new MethodSyntax
            {
                MethodParameters = parameters,
                Statement = methodBody,
            };

        // example: @required public String name { get; set; }
        protected internal virtual Parser<PropertySyntax> PropertyDeclaration =>
            from heading in ClassMemberHeading
            from typeAndName in TypeAndName
            from propertyBody in PropertyGetterAndSetter
            select new PropertySyntax(heading)
            {
                Type = typeAndName.Type,
                Identifier = typeAndName.Identifier,
                GetterStatement = propertyBody.GetterStatement,
                SetterStatement = propertyBody.SetterStatement,
            };

        // example: { get; set; }
        protected internal virtual Parser<PropertySyntax> PropertyGetterAndSetter =>
            from openBrace in Parse.Char('{').Token()
            from getterOrSetter in GetterOrSetter.Many()
            from closeBrace in Parse.Char('}').Token()
            select new PropertySyntax(getterOrSetter);

        // examples: get; set; get { ... }
        protected internal virtual Parser<Tuple<string, StatementSyntax>> GetterOrSetter =>
            from getOrSet in Parse.String("get").Or(Parse.String("set")).Token().Text()
            from block in Parse.String(";").Token().Text().Return(new StatementSyntax()).Or(Block)
            select Tuple.Create(getOrSet, block);

        // examples: return true; if (false) return; etc.
        protected internal virtual Parser<StatementSyntax> Statement =>
            from comments in CommentParser.AnyComment.Token().Many()
            from statement in IfStatement.Select(s => s as StatementSyntax)
                .Or(ForStatement)
                .Or(DoWhileStatement)
                .Or(WhileStatement)
                .Or(Block)
                .Or(UnknownGenericStatement)
            select statement.WithComments(comments);

        // dummy parser for the block with curly brace matching support
        protected internal virtual Parser<BlockStatementSyntax> Block =>
            from openBrace in Parse.Char('{').Token()
            from statements in Statement.Many()
            from trailingComment in CommentParser.AnyComment.Token().Many()
            from closeBrace in Parse.Char('}').Token()
            select new BlockStatementSyntax
            {
                Statements = statements.ToList(),
                CodeComments = trailingComment.ToList(),
            };

        // dummy generic parser for any unknown statement
        protected internal virtual Parser<StatementSyntax> UnknownGenericStatement =>
            from contents in Parse.CharExcept("{};").Many().Text().Token()
            from semicolon in Parse.Char(';').Token()
            select new StatementSyntax
            {
                Body = contents,
            };

        // dummy generic parser for any expressions with matching braces
        protected internal virtual Parser<string> GenericExpressionInBraces =>
            from openBrace in Parse.Char('(').Token()
            from subExpressions in Parse.CharExcept("()").Many().Text()
                .Or(GenericExpressionInBraces.Select(x => $"({x})")).Many()
            from closeBrace in Parse.Char(')').Token()
            select string.Join(string.Empty, subExpressions);

        // simple if statement without the expressions support
        protected internal virtual Parser<IfStatementSyntax> IfStatement =>
            from ifKeyword in Parse.String(ApexKeywords.If).Token()
            from expression in GenericExpressionInBraces
            from thenBranch in Statement
            from elseBranch in Parse.String(ApexKeywords.Else).Token().Then(_ => Statement).Optional()
            select new IfStatementSyntax
            {
                Expression = expression,
                ThenStatement = thenBranch,
                ElseStatement = elseBranch.GetOrDefault(),
            };

        // simple for statement without the expression support
        protected internal virtual Parser<ForStatementSyntax> ForStatement =>
            from forKeyword in Parse.String(ApexKeywords.For).Token()
            from expression in GenericExpressionInBraces
            from loopBody in Statement
            select new ForStatementSyntax
            {
                Expression = expression,
                LoopBody = loopBody,
            };

        // simple do-while statement without the expression support
        protected internal virtual Parser<DoWhileStatementSyntax> DoWhileStatement =>
            from doKeyword in Parse.String(ApexKeywords.Do).Token()
            from loopBody in Statement
            from whileKeyword in Parse.String(ApexKeywords.While).Token()
            from expression in GenericExpressionInBraces
            from semicolon in Parse.Char(';').Token()
            select new DoWhileStatementSyntax
            {
                Expression = expression,
                LoopBody = loopBody,
            };

        // simple while statement without the expression support
        protected internal virtual Parser<WhileStatementSyntax> WhileStatement =>
            from whileKeyword in Parse.String(ApexKeywords.While).Token()
            from expression in GenericExpressionInBraces
            from loopBody in Statement
            select new WhileStatementSyntax
            {
                Expression = expression,
                LoopBody = loopBody,
            };

        // examples: /* this is a member */ @isTest public
        protected internal virtual Parser<ClassMemberSyntax> ClassMemberHeading =>
            from comments in CommentParser.AnyComment.Token().Many()
            from annotations in Annotation.Many()
            from modifiers in Modifier.Many()
            select new ClassMemberSyntax
            {
                CodeComments = comments.ToList(),
                Attributes = annotations.ToList(),
                Modifiers = modifiers.ToList(),
            };

        // example: @TestFixture public static class Program { static void main() {} }
        protected internal virtual Parser<ClassSyntax> ClassDeclaration =>
            from heading in ClassMemberHeading
            from classBody in ClassDeclarationBody
            select new ClassSyntax(heading)
            {
                Identifier = classBody.Identifier,
                Methods = classBody.Methods,
                Properties = classBody.Properties,
                InnerClasses = classBody.InnerClasses,
            };

        // example: class Program { void main() {} }
        protected internal virtual Parser<ClassSyntax> ClassDeclarationBody =>
            from @class in Parse.String(ApexKeywords.Class).Token()
            from className in Identifier
            from openBrace in Parse.Char('{').Token()
            from members in ClassMemberDeclaration.Many()
            from closeBrace in Parse.Char('}').Token()
            select new ClassSyntax()
            {
                Identifier = className,
                Methods = members.OfType<MethodSyntax>().ToList(),
                Properties = members.OfType<PropertySyntax>().ToList(),
                InnerClasses = members.OfType<ClassSyntax>().ToList(),
            };

        // method or property declaration starting with the type and name
        protected internal virtual Parser<ClassMemberSyntax> MethodOrPropertyDeclaration =>
            from typeAndName in TypeAndName
            from member in MethodParametersAndBody.Select(m => m as ClassMemberSyntax).XOr(PropertyGetterAndSetter)
            select member.WithTypeAndName(typeAndName);

        // class members: methods, classes, properties
        protected internal virtual Parser<ClassMemberSyntax> ClassMemberDeclaration =>
            from heading in ClassMemberHeading
            from member in ClassDeclarationBody.Select(c => c as ClassMemberSyntax).Or(MethodOrPropertyDeclaration)
            select member.WithProperties(heading);
    }
}
