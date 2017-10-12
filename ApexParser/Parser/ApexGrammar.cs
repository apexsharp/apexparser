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
                TypeParameters = parameters.GetOrElse(Enumerable.Empty<TypeSyntax>()).ToList()
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
            from methodBody in MethodDeclarationBody
            select new MethodSyntax(heading)
            {
                Identifier = methodBody.Identifier,
                ReturnType = methodBody.ReturnType,
                MethodParameters = methodBody.MethodParameters,
                CodeInsideMethod = methodBody.CodeInsideMethod
            };

        // examples:
        // @isTest void Test() {}
        // public static void Hello() {}
        protected internal virtual Parser<MethodSyntax> MethodDeclarationBody =>
            from returnType in TypeReference
            from methodName in Identifier.Optional()
            from parameters in MethodParameters
            from methodBody in Block.Token()
            select new MethodSyntax
            {
                Identifier = methodName.GetOrElse(returnType.Identifier),
                ReturnType = returnType,
                MethodParameters = parameters,
                CodeInsideMethod = StripOuterBlockBraces(methodBody)
            };

        // strips the outer curly braces from the method block
        private string StripOuterBlockBraces(string blockBody)
        {
            var result = blockBody.Trim();
            if (result.StartsWith("{") && result.EndsWith("}"))
            {
                result = result.Substring(1, result.Length - 2).Trim();
            }

            return result;
        }

        // example: @required public String name { get; set; }
        protected internal virtual Parser<PropertySyntax> PropertyDeclaration =>
            from heading in ClassMemberHeading
            from propertyBody in PropertyDeclarationBody
            select new PropertySyntax(heading)
            {
                Type = propertyBody.Type,
                Identifier = propertyBody.Identifier,
                GetterCode = propertyBody.GetterCode,
                SetterCode = propertyBody.SetterCode
            };

        // example: String name { get; set; }
        protected internal virtual Parser<PropertySyntax> PropertyDeclarationBody =>
            from propertyType in TypeReference
            from propertyName in Identifier
            from openBrace in Parse.Char('{').Token()
            from getterOrSetter in GetterOrSetter.Many()
            from closeBrace in Parse.Char('}').Token()
            select new PropertySyntax(getterOrSetter)
            {
                Type = propertyType,
                Identifier = propertyName
            };

        // examples: get; set; get { ... }
        protected internal virtual Parser<Tuple<string, string>> GetterOrSetter =>
            from getOrSet in Parse.String("get").Or(Parse.String("set")).Token().Text()
            from block in Parse.String(";").Token().Text().Or(Block)
            select Tuple.Create(getOrSet, StripOuterBlockBraces(block));

        // dummy parser for the block with curly brace matching support
        protected internal virtual Parser<string> Block =>
            from openBrace in Parse.Char('{')
            from contents in Parse.CharExcept("{}").Many().Text().Or(Block).Many()
            from closeBrace in Parse.Char('}')
            select "{" + string.Join(string.Empty, contents) + "}";

        // examples: /* this is a member */ @isTest public
        protected internal virtual Parser<ClassMemberSyntax> ClassMemberHeading =>
            from comments in CommentParser.AnyComment.Token().Many()
            from annotations in Annotation.Many()
            from modifiers in Modifier.Many()
            select new ClassMemberSyntax
            {
                CodeComments = comments.ToList(),
                Attributes = annotations.ToList(),
                Modifiers = modifiers.ToList()
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
                InnerClasses = classBody.InnerClasses
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
                InnerClasses = members.OfType<ClassSyntax>().ToList()
            };

        // class members: methods, classes, properties
        protected internal virtual Parser<ClassMemberSyntax> ClassMemberDeclaration =>
            from heading in ClassMemberHeading
            from member in ClassDeclarationBody.Select(c => c as ClassMemberSyntax)
                .Or(MethodDeclarationBody).Or(PropertyDeclarationBody)
            select member.CopyProperties(heading);
    }
}
