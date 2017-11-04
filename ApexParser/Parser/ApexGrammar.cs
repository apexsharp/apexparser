using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Toolbox;
using Sprache;

namespace ApexParser.Parser
{
    public class ApexGrammar : ICommentParserProvider
    {
        // examples: a, Apex, code123
        protected internal virtual Parser<string> Identifier =>
        (
            from identifier in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit.Or(Parse.Char('_')))
            where !ApexKeywords.ReservedWords.Contains(identifier)
            select identifier
        )
        .Token().Named("Identifier");

        // examples: System.debug
        protected internal virtual Parser<IEnumerable<string>> QualifiedIdentifier =>
            Identifier.DelimitedBy(Parse.Char('.').Token())
                .Named("QualifiedIdentifier");

        // examples: /* default settings are OK */ //
        protected internal virtual CommentParser CommentParser { get; } = new CommentParser();

        // used by the Token(this) modifier
        Parser<string> ICommentParserProvider.Comment => CommentParser.AnyComment;

        // example: @isTest, returned as IsTest
        protected internal virtual Parser<AnnotationSyntax> Annotation =>
            from at in Parse.Char('@').Token()
            from name in Identifier.Select(id => id.Normalized())
            from parameters in GenericExpressionInBraces().Optional().Token(this)
            select new AnnotationSyntax
            {
                Identifier = name,
                Parameters = parameters.Value.GetOrDefault(),
            };

        // returns the keyword normalized to its canonic representation
        // examples: void, testMethod
        protected internal virtual Parser<string> Keyword(string text) =>
            Parse.IgnoreCase(text).Return(text);

        // examples: int, void
        protected internal virtual Parser<TypeSyntax> SystemType =>
            Keyword(ApexKeywords.Blob).Or(
            Keyword(ApexKeywords.Boolean)).Or(
            Keyword(ApexKeywords.Byte)).Or(
            Keyword(ApexKeywords.Char)).Or(
            Keyword(ApexKeywords.Decimal)).Or(
            Keyword(ApexKeywords.Double)).Or(
            Keyword(ApexKeywords.Exception)).Or(
            Keyword(ApexKeywords.Float)).Or(
            Keyword(ApexKeywords.Int)).Or(
            Keyword(ApexKeywords.Long)).Or(
            Keyword(ApexKeywords.SetType)).Or(
            Keyword(ApexKeywords.Short)).Or(
            Keyword(ApexKeywords.List)).Or(
            Keyword(ApexKeywords.Map)).Or(
            Keyword(ApexKeywords.Void))
                .Text().Then(n => Parse.Not(Parse.LetterOrDigit.Or(Parse.Char('_'))).Return(n))
                .Token().Select(n => new TypeSyntax(n))
                .Named("SystemType");

        // examples: int, String, System.Collections.Hashtable
        protected internal virtual Parser<TypeSyntax> NonGenericType =>
            SystemType.Or(QualifiedIdentifier.Select(qi => new TypeSyntax(qi)));

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
            from arraySpecifier in Parse.Char('[').Token().Then(_ => Parse.Char(']').Token()).Optional()
            select new TypeSyntax(type)
            {
                TypeParameters = parameters.GetOrElse(Enumerable.Empty<TypeSyntax>()).ToList(),
                IsArray = arraySpecifier.IsDefined,
            };

        // example: string name
        protected internal virtual Parser<ParameterSyntax> ParameterDeclaration =>
            from modifiers in Modifier.Token().Many().Commented(this)
            from type in TypeReference.Commented(this)
            from name in Identifier.Commented(this)
            select new ParameterSyntax(type.Value, name.Value)
            {
                LeadingComments = modifiers.LeadingComments.Concat(type.LeadingComments).ToList(),
                Modifiers = modifiers.Value.ToList(),
                TrailingComments = name.TrailingComments.ToList(),
            };

        // example: int a, Boolean flag
        protected internal virtual Parser<IEnumerable<ParameterSyntax>> ParameterDeclarations =>
            ParameterDeclaration.DelimitedBy(Parse.Char(',').Token());

        // example: (string a, char delimiter)
        protected internal virtual Parser<List<ParameterSyntax>> MethodParameters =>
            from openBrace in Parse.Char('(').Token()
            from param in ParameterDeclarations.Optional()
            from closeBrace in Parse.Char(')').Token()
            select param.GetOrElse(Enumerable.Empty<ParameterSyntax>()).ToList();

        // examples: public, private, with sharing
        protected internal virtual Parser<string> Modifier =>
            Keyword(ApexKeywords.Public).Or(
            Keyword(ApexKeywords.Protected)).Or(
            Keyword(ApexKeywords.Private)).Or(
            Keyword(ApexKeywords.Static)).Or(
            Keyword(ApexKeywords.Abstract)).Or(
            Keyword(ApexKeywords.Final)).Or(
            Keyword(ApexKeywords.Global)).Or(
            Keyword(ApexKeywords.WebService)).Or(
            Keyword(ApexKeywords.Override)).Or(
            Keyword(ApexKeywords.Virtual)).Or(
            Keyword(ApexKeywords.TestMethod)).Or(
            Keyword(ApexKeywords.With).Token().Then(_ => Keyword(ApexKeywords.Sharing)).Return($"{ApexKeywords.With} {ApexKeywords.Sharing}")).Or(
            Keyword(ApexKeywords.Without).Token().Then(_ => Keyword(ApexKeywords.Sharing)).Return($"{ApexKeywords.Without} {ApexKeywords.Sharing}")).Or(
            Keyword(ApexKeywords.Transient))
                .Text().Token().Named("Modifier");

        // examples:
        // @isTest void Test() {}
        // public static void Hello() {}
        protected internal virtual Parser<MethodDeclarationSyntax> MethodDeclaration =>
            from heading in MemberDeclarationHeading
            from typeAndName in TypeAndName
            from methodBody in MethodParametersAndBody
            select new MethodDeclarationSyntax(heading)
            {
                Identifier = typeAndName.Identifier ?? typeAndName.Type.Identifier,
                ReturnType = typeAndName.Type,
                Parameters = methodBody.Parameters,
                Body = methodBody.Body,
            };

        // examples: string Name, void Test
        protected internal virtual Parser<ParameterSyntax> TypeAndName =>
            from type in TypeReference
            from name in Identifier.Optional()
            select new ParameterSyntax(type, name.GetOrDefault());

        // examples:
        // void Test() {}
        // string Hello(string name) {}
        // int Dispose();
        protected internal virtual Parser<MethodDeclarationSyntax> MethodParametersAndBody =>
            from parameters in MethodParameters
            from methodBody in Block.Or(Parse.Char(';').Return(default(BlockSyntax))).Token()
            select new MethodDeclarationSyntax
            {
                Parameters = parameters,
                Body = methodBody,
            };

        // example: @required public String name { get; set; }
        protected internal virtual Parser<PropertyDeclarationSyntax> PropertyDeclaration =>
            from heading in MemberDeclarationHeading
            from typeAndName in TypeAndName
            from accessors in PropertyAccessors
            select new PropertyDeclarationSyntax(heading)
            {
                Type = typeAndName.Type,
                Identifier = typeAndName.Identifier,
                Accessors = accessors.Accessors,
            };

        // example: { get; set; }
        protected internal virtual Parser<PropertyDeclarationSyntax> PropertyAccessors =>
            from openBrace in Parse.Char('{').Token()
            from accessors in PropertyAccessor.Many()
            from closeBrace in Parse.Char('}').Token()
            select new PropertyDeclarationSyntax(accessors);

        // examples: get; private set; get { return 0; }
        protected internal virtual Parser<AccessorDeclarationSyntax> PropertyAccessor =>
            from heading in MemberDeclarationHeading
            from keyword in Parse.IgnoreCase(ApexKeywords.Get).Or(Parse.IgnoreCase(ApexKeywords.Set)).Token().Text()
            from body in Parse.Char(';').Return(default(BlockSyntax)).Or(Block).Commented(this)
            select new AccessorDeclarationSyntax(heading)
            {
                IsGetter = keyword == ApexKeywords.Get,
                Body = body.Value,
                TrailingComments = body.TrailingComments.ToList(),
            };

        // example: private static int x, y, z = 3;
        protected internal virtual Parser<FieldDeclarationSyntax> FieldDeclaration =>
            from heading in MemberDeclarationHeading
            from type in TypeReference
            from declarators in FieldDeclarator.DelimitedBy(Parse.Char(',').Token())
            from semicolon in Parse.Char(';').Token()
            select new FieldDeclarationSyntax(heading)
            {
                Type = type,
                Fields = declarators.ToList(),
            };

        // example: now = DateTime.Now()
        protected internal virtual Parser<FieldDeclaratorSyntax> FieldDeclarator =>
            from identifier in Identifier
            from expression in Parse.Char('=').Token().Then(c => GenericExpression).Optional()
            select new FieldDeclaratorSyntax
            {
                Identifier = identifier,
                Expression = expression.GetOrDefault(),
            };

        // examples: return true; if (false) return; etc.
        protected internal virtual Parser<StatementSyntax> Statement =>
            from statement in Block.Select(s => s as StatementSyntax)
                .Or(IfStatement)
                .Or(DoStatement)
                .Or(ForEachStatement)
                .Or(ForStatement)
                .Or(WhileStatement)
                .Or(BreakStatement)
                .Or(RunAsStatement)
                .Or(TryCatchFinallyStatement)
                .Or(InsertStatement)
                .Or(UpdateStatement)
                .Or(DeleteStatement)
                .Or(VariableDeclaration)
                .Or(UnknownGenericStatement)
                .Commented(this)
            select statement.Value
                .WithLeadingComments(statement.LeadingComments)
                .WithTrailingComments(statement.TrailingComments);

        // examples: {}, { /* inner comments */ }, { int a = 0; return; } // trailing comments
        protected internal virtual Parser<BlockSyntax> Block =>
            from comments in CommentParser.AnyComment.Token().Many()
            from openBrace in Parse.Char('{').Token()
            from statements in Statement.Many()
            from closeBrace in Parse.Char('}').Commented(this)
            select new BlockSyntax
            {
                LeadingComments = comments.ToList(),
                Statements = statements.ToList(),
                InnerComments = closeBrace.LeadingComments.ToList(),
                TrailingComments = closeBrace.TrailingComments.ToList(),
            };

        // example: int x, y, z = 3;
        protected internal virtual Parser<VariableDeclarationSyntax> VariableDeclaration =>
            from type in TypeReference
            from declarators in VariableDeclarator.DelimitedBy(Parse.Char(',').Token())
            from semicolon in Parse.Char(';')
            select new VariableDeclarationSyntax
            {
                Type = type,
                Variables = declarators.ToList(),
            };

        // example: now = DateTime.Now()
        protected internal virtual Parser<VariableDeclaratorSyntax> VariableDeclarator =>
            from identifier in Identifier
            from expression in Parse.Char('=').Token().Then(c => GenericExpression).Optional()
            select new VariableDeclaratorSyntax
            {
                Identifier = identifier,
                Expression = expression.GetOrDefault(),
            };

        // examples: (MyExpr), (MyExpr ex)
        protected internal virtual Parser<CatchClauseSyntax> CatchExpressionTypeName =>
            from openBrace in Parse.Char('(').Token()
            from exceptionType in TypeReference
            from identifier in Identifier.Optional()
            from closeBrace in Parse.Char(')').Token()
            select new CatchClauseSyntax
            {
                Type = exceptionType,
                Identifier = identifier.GetOrDefault(),
            };

        // examples: catch { ... }, catch (MyEx) { ...}, catch (MyEx ex) { ... }
        protected internal virtual Parser<CatchClauseSyntax> CatchClause =>
            from @catch in Parse.IgnoreCase(ApexKeywords.Catch).Commented(this)
            from expr in CatchExpressionTypeName.Commented(this).Optional()
            from block in Block.Commented(this)
            select new CatchClauseSyntax
            {
                LeadingComments = @catch.LeadingComments.ToList(),
                Type = expr.GetOrDefault()?.Value?.Type,
                Identifier = expr.GetOrDefault()?.Value?.Identifier,
                Block = block.Value.WithLeadingComments(block.LeadingComments),
                TrailingComments = block.TrailingComments.ToList(),
            };

        // examples: finally { ... }
        protected internal virtual Parser<FinallyClauseSyntax> FinallyClause =>
            from @finally in Parse.IgnoreCase(ApexKeywords.Finally).Commented(this)
            from block in Block.Commented(this)
            select new FinallyClauseSyntax
            {
                LeadingComments = @finally.LeadingComments.ToList(),
                Block = block.Value.WithLeadingComments(block.LeadingComments),
                TrailingComments = block.TrailingComments.ToList(),
            };

        // example: try { ... } catch (Ex) { ... } finally { }
        protected internal virtual Parser<TryStatementSyntax> TryCatchFinallyStatement =>
            from @try in Parse.IgnoreCase(ApexKeywords.Try).Commented(this)
            from block in Block
            from catchClauses in CatchClause.Many()
            from @finally in FinallyClause.Optional()
            where @finally.IsDefined || catchClauses.Any()
            select new TryStatementSyntax
            {
                LeadingComments = @try.LeadingComments.ToList(),
                Block = block,
                Catches = catchClauses.ToList(),
                Finally = @finally.GetOrDefault(),
            };

        // example: System.runAs(user) { System.debug('Hi there!'); }
        protected internal virtual Parser<RunAsStatementSyntax> RunAsStatement =>
            from system in Parse.IgnoreCase(ApexKeywords.System).Token()
            from dot in Parse.Char('.').Token()
            from runAs in Parse.IgnoreCase(ApexKeywords.RunAs).Token()
            from expression in GenericExpressionInBraces()
            from statement in Statement
            select new RunAsStatementSyntax
            {
                Expression = expression,
                Statement = statement,
            };

        // dummy generic parser for any unknown statement ending with a semicolon
        protected internal virtual Parser<StatementSyntax> UnknownGenericStatement =>
            from contents in GenericExpressionCore(forbidden: ";").Token()
            from semicolon in Parse.Char(';')
            select new StatementSyntax
            {
                Body = contents.Trim(),
            };

        // examples: 'hello', '\'world\'\n'
        protected internal virtual Parser<string> StringLiteral =>
            from leading in Parse.WhiteSpace.Many()
            from openQuote in Parse.Char('\'')
            from fragments in Parse.Char('\\').Then(_ => Parse.AnyChar.Select(c => $"\\{c}"))
                .Or(Parse.CharExcept("\\'").Many().Text()).Many()
            from closeQuote in Parse.Char('\'')
            from trailing in Parse.WhiteSpace.Many()
            select $"'{string.Join(string.Empty, fragments)}'";

        // dummy generic parser for expressions with matching braces
        protected internal virtual Parser<string> GenericExpression =>
            GenericExpressionCore(forbidden: ",;").Select(x => x.Trim());

        // creates dummy generic parser for expressions with matching braces allowing commas and semicolons by default
        protected internal virtual Parser<string> GenericExpressionCore(string forbidden = null) =>
            from subExpressions in
                GenericNewExpression.Select(x => $" {x}")
                .Or(Parse.CharExcept("'/(){}[]" + forbidden).Except(GenericNewExpression).Many().Text().Token())
                .Or(Parse.Char('/').Then(_ => Parse.Not(Parse.Chars('/', '*'))).Once().Return("/"))
                .Or(CommentParser.AnyComment.Return(string.Empty))
                .Or(StringLiteral)
                .Or(GenericExpressionInBraces('(', ')').Select(x => $"({x})"))
                .Or(GenericExpressionInBraces('{', '}').Select(x => $"{{{x}}}"))
                .Or(GenericExpressionInBraces('[', ']').Select(x => $"[{x}]"))
                .Many()
            let expr = string.Join(string.Empty, subExpressions)
            where !string.IsNullOrWhiteSpace(expr)
            select expr;

        // examples: new Map<string, string>
        protected internal virtual Parser<string> GenericNewExpression =>
            from @new in Parse.IgnoreCase(ApexKeywords.New).Then(_ => Parse.Not(Parse.LetterOrDigit)).Token()
            from type in TypeReference
            select $"new {type.AsString()}";

        // creates dummy generic parser for any expressions with matching braces
        protected internal virtual Parser<string> GenericExpressionInBraces(char open = '(', char close = ')') =>
            from openBrace in Parse.Char(open).Token()
            from expression in GenericExpressionCore().Optional()
            from closeBrace in Parse.Char(close).Token()
            select expression.GetOrElse(string.Empty).Trim();

        // example: break;
        protected internal virtual Parser<BreakStatementSyntax> BreakStatement =>
            from @break in Parse.IgnoreCase(ApexKeywords.Break).Token()
            from semicolon in Parse.Char(';')
            select new BreakStatementSyntax();

        // simple if statement without the expressions support
        protected internal virtual Parser<IfStatementSyntax> IfStatement =>
            from ifKeyword in Parse.IgnoreCase(ApexKeywords.If).Token()
            from expression in GenericExpressionInBraces()
            from thenBranch in Statement
            from elseBranch in Parse.IgnoreCase(ApexKeywords.Else).Token(this).Then(_ => Statement).Optional()
            select new IfStatementSyntax
            {
                Expression = expression,
                ThenStatement = thenBranch,
                ElseStatement = elseBranch.GetOrDefault(),
            };

        // simple foreach statement without the expression support
        protected internal virtual Parser<ForEachStatementSyntax> ForEachStatement =>
            from forKeyword in Parse.IgnoreCase(ApexKeywords.For).Token()
            from openBrace in Parse.Char('(').Token()
            from typeReference in TypeReference
            from identifier in Identifier
            from colon in Parse.Char(':').Token()
            from expression in GenericExpression
            from closeBrace in Parse.Char(')').Token()
            from loopBody in Statement
            select new ForEachStatementSyntax
            {
                Type = typeReference,
                Identifier = identifier,
                Expression = expression,
                Statement = loopBody,
            };

        // simple for statement without the expression support
        protected internal virtual Parser<ForStatementSyntax> ForStatement =>
            from forKeyword in Parse.IgnoreCase(ApexKeywords.For).Token()
            from openBrace in Parse.Char('(').Token()
            from declaration in VariableDeclaration.Or(Parse.Char(';').Token().Return(default(VariableDeclarationSyntax)))
            from condition in GenericExpression.Optional()
            from semicolon in Parse.Char(';').Token()
            from incrementors in GenericExpression.DelimitedBy(Parse.Char(',').Token()).Optional()
            from closeBrace in Parse.Char(')').Token()
            from loopBody in Statement
            select new ForStatementSyntax
            {
                Declaration = declaration,
                Condition = condition.GetOrDefault(),
                Incrementors = incrementors.GetOrElse(Enumerable.Empty<string>()).ToList(),
                Statement = loopBody,
            };

        // simple do-while statement without the expression support
        protected internal virtual Parser<DoStatementSyntax> DoStatement =>
            from doKeyword in Parse.IgnoreCase(ApexKeywords.Do).Token()
            from loopBody in Statement
            from whileKeyword in Parse.IgnoreCase(ApexKeywords.While).Token()
            from expression in GenericExpressionInBraces()
            from semicolon in Parse.Char(';')
            select new DoStatementSyntax
            {
                Expression = expression,
                Statement = loopBody,
            };

        // simple while statement without the expression support
        protected internal virtual Parser<WhileStatementSyntax> WhileStatement =>
            from whileKeyword in Parse.IgnoreCase(ApexKeywords.While).Token()
            from expression in GenericExpressionInBraces()
            from loopBody in Statement
            select new WhileStatementSyntax
            {
                Expression = expression,
                Statement = loopBody,
            };

        // examples: return x; insert y; delete z;
        protected internal virtual Parser<string> KeywordExpressionStatement(string keyword) =>
            from key in Parse.IgnoreCase(keyword).Token()
            from expr in GenericExpression.XOptional()
            from semicolon in Parse.Char(';')
            select expr.GetOrDefault();

        // example: insert contact;
        protected internal virtual Parser<InsertStatementSyntax> InsertStatement =>
            from expr in KeywordExpressionStatement(ApexKeywords.Insert)
            where !string.IsNullOrWhiteSpace(expr)
            select new InsertStatementSyntax
            {
                Expression = expr,
            };

        // example: update items;
        protected internal virtual Parser<UpdateStatementSyntax> UpdateStatement =>
            from expr in KeywordExpressionStatement(ApexKeywords.Update)
            where !string.IsNullOrWhiteSpace(expr)
            select new UpdateStatementSyntax
            {
                Expression = expr,
            };

        // example: delete user;
        protected internal virtual Parser<DeleteStatementSyntax> DeleteStatement =>
            from expr in KeywordExpressionStatement(ApexKeywords.Delete)
            where !string.IsNullOrWhiteSpace(expr)
            select new DeleteStatementSyntax
            {
                Expression = expr,
            };

        // examples: /* this is a member */ @isTest public
        protected internal virtual Parser<MemberDeclarationSyntax> MemberDeclarationHeading =>
            from comments in CommentParser.AnyComment.Token().Many()
            from annotations in Annotation.Many()
            from modifiers in Modifier.Many()
            select new MemberDeclarationSyntax
            {
                LeadingComments = comments.ToList(),
                Annotations = annotations.ToList(),
                Modifiers = modifiers.ToList(),
            };

        // example: SomeValue
        protected internal virtual Parser<EnumMemberDeclarationSyntax> EnumMember =>
            from heading in MemberDeclarationHeading
            from identifier in Identifier
            from skippedComments in CommentParser.AnyComment.Token().Many()
            select new EnumMemberDeclarationSyntax(heading)
            {
                Identifier = identifier,
            };

        // example: public enum Weekday { Monday, Thursday }
        protected internal virtual Parser<EnumDeclarationSyntax> EnumDeclaration =>
            from heading in MemberDeclarationHeading
            from @enum in EnumDeclarationBody
            select new EnumDeclarationSyntax(heading)
            {
                Identifier = @enum.Identifier,
                Members = @enum.Members,
            };

        // example: enum Weekday { Monday, Thursday }
        protected internal virtual Parser<EnumDeclarationSyntax> EnumDeclarationBody =>
            from @enum in Parse.IgnoreCase(ApexKeywords.Enum).Token()
            from identifier in Identifier
            from skippedComments in CommentParser.AnyComment.Token().Many()
            from openBrace in Parse.Char('{').Token()
            from members in EnumMember.XDelimitedBy(Parse.Char(',').Token())
            from comment in CommentParser.AnyComment.Optional()
            from closeBrace in Parse.Char('}').Token()
            select new EnumDeclarationSyntax
            {
                Identifier = identifier,
                Members = members.ToList(),
            };

        // example: @TestFixture public static class Program { static void main() {} }
        public virtual Parser<ClassDeclarationSyntax> ClassDeclaration =>
            from heading in MemberDeclarationHeading
            from classBody in ClassDeclarationBody
            select ClassDeclarationSyntax.Create(heading, classBody);

        // example: class Program { void main() {} }
        protected internal virtual Parser<ClassDeclarationSyntax> ClassDeclarationBody =>
            from @class in Parse.IgnoreCase(ApexKeywords.Class).Text().Token().Or(Parse.IgnoreCase(ApexKeywords.Interface).Text().Token())
            from className in Identifier
            from baseType in Parse.IgnoreCase(ApexKeywords.Extends).Token().Then(t => TypeReference).Optional()
            from interfaces in Parse.IgnoreCase(ApexKeywords.Implements).Token().Then(t => TypeReference.DelimitedBy(Parse.Char(',').Token())).Optional()
            from skippedComments in CommentParser.AnyComment.Token().Many()
            from openBrace in Parse.Char('{').Token()
            from members in ClassMemberDeclaration.Many()
            from innerComments in CommentParser.AnyComment.Token().Many()
            from closeBrace in Parse.Char('}').Token()
            from trailingComments in CommentParser.AnyComment.Token().Many()
            select new ClassDeclarationSyntax()
            {
                Identifier = className,
                IsInterface = @class == ApexKeywords.Interface,
                BaseType = baseType.GetOrDefault(),
                Interfaces = interfaces.GetOrElse(Enumerable.Empty<TypeSyntax>()).ToList(),
                Members = ConvertConstructors(members).ToList(),
                InnerComments = innerComments.ToList(),
                TrailingComments = trailingComments.ToList(),
            };

        private IEnumerable<MemberDeclarationSyntax> ConvertConstructors(IEnumerable<MemberDeclarationSyntax> members)
        {
            foreach (var member in members)
            {
                if (member is MethodDeclarationSyntax m && m.IsConstructor())
                {
                    yield return new ConstructorDeclarationSyntax(m);
                    continue;
                }

                yield return member;
            }
        }

        // examples: { instanceProperty = 0; }, static { staticProperty = 0; }
        protected internal virtual Parser<ClassInitializerSyntax> ClassInitializer =>
            from heading in MemberDeclarationHeading
            from initializer in ClassInitializerBody
            select initializer.WithProperties(heading);

        // examples: { a = 0; }
        protected internal virtual Parser<ClassInitializerSyntax> ClassInitializerBody =>
            from body in Block
            select new ClassInitializerSyntax
            {
                Body = body,
            };

        // method or property declaration starting with the type and name
        protected internal virtual Parser<MemberDeclarationSyntax> MethodOrPropertyDeclaration =>
            from typeAndName in TypeAndName
            from member in MethodParametersAndBody.Select(c => c as MemberDeclarationSyntax)
                .XOr(PropertyAccessors)
            select member.WithTypeAndName(typeAndName);

        // class members: methods, classes, properties
        protected internal virtual Parser<MemberDeclarationSyntax> ClassMemberDeclaration =>
            from heading in MemberDeclarationHeading
            from member in ClassInitializerBody.Select(c => c as MemberDeclarationSyntax)
                .Or(EnumDeclarationBody)
                .Or(ClassDeclarationBody)
                .Or(MethodOrPropertyDeclaration)
                .Or(FieldDeclaration)
            select member.WithProperties(heading);

        // top-level declaration: a class or an enum followed by the end of file
        protected internal virtual Parser<MemberDeclarationSyntax> CompilationUnit =>
            ClassDeclaration.Select(c => c as MemberDeclarationSyntax).Or(EnumDeclaration).End();
    }
}
