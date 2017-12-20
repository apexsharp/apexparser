using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.Parser;
using ApexParser.Toolbox;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexAccessorDeclarationSyntax = ApexParser.MetaClass.AccessorDeclarationSyntax;
using ApexAnnotationSyntax = ApexParser.MetaClass.AnnotationSyntax;
using ApexBaseSyntax = ApexParser.MetaClass.BaseSyntax;
using ApexBlockSyntax = ApexParser.MetaClass.BlockSyntax;
using ApexBreakStatementSyntax = ApexParser.MetaClass.BreakStatementSyntax;
using ApexCatchClauseSyntax = ApexParser.MetaClass.CatchClauseSyntax;
using ApexClassDeclarationSyntax = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexClassInitializerSyntax = ApexParser.MetaClass.ClassInitializerSyntax;
using ApexConstructorDeclarationSyntax = ApexParser.MetaClass.ConstructorDeclarationSyntax;
using ApexContinueStatementSyntax = ApexParser.MetaClass.ContinueStatementSyntax;
using ApexDeleteStatementSyntax = ApexParser.MetaClass.DeleteStatementSyntax;
using ApexDoStatementSyntax = ApexParser.MetaClass.DoStatementSyntax;
using ApexEnumDeclarationSyntax = ApexParser.MetaClass.EnumDeclarationSyntax;
using ApexEnumMemberDeclarationSyntax = ApexParser.MetaClass.EnumMemberDeclarationSyntax;
using ApexExpressionSyntax = ApexParser.MetaClass.ExpressionSyntax;
using ApexFieldDeclarationSyntax = ApexParser.MetaClass.FieldDeclarationSyntax;
using ApexFieldDeclaratorSyntax = ApexParser.MetaClass.FieldDeclaratorSyntax;
using ApexFinallyClauseSyntax = ApexParser.MetaClass.FinallyClauseSyntax;
using ApexForEachStatementSyntax = ApexParser.MetaClass.ForEachStatementSyntax;
using ApexForStatementSyntax = ApexParser.MetaClass.ForStatementSyntax;
using ApexIfStatementSyntax = ApexParser.MetaClass.IfStatementSyntax;
using ApexInsertStatementSyntax = ApexParser.MetaClass.InsertStatementSyntax;
using ApexInterfaceDeclarationSyntax = ApexParser.MetaClass.InterfaceDeclarationSyntax;
using ApexMemberDeclarationSyntax = ApexParser.MetaClass.MemberDeclarationSyntax;
using ApexMethodDeclarationSyntax = ApexParser.MetaClass.MethodDeclarationSyntax;
using ApexParameterSyntax = ApexParser.MetaClass.ParameterSyntax;
using ApexPropertyDeclarationSyntax = ApexParser.MetaClass.PropertyDeclarationSyntax;
using ApexReturnStatementSyntax = ApexParser.MetaClass.ReturnStatementSyntax;
using ApexRunAsStatementSyntax = ApexParser.MetaClass.RunAsStatementSyntax;
using ApexStatementSyntax = ApexParser.MetaClass.StatementSyntax;
using ApexSyntaxType = ApexParser.MetaClass.SyntaxType;
using ApexThrowStatementSyntax = ApexParser.MetaClass.ThrowStatementSyntax;
using ApexTryStatementSyntax = ApexParser.MetaClass.TryStatementSyntax;
using ApexTypeSyntax = ApexParser.MetaClass.TypeSyntax;
using ApexUpdateStatementSyntax = ApexParser.MetaClass.UpdateStatementSyntax;
using ApexVariableDeclarationSyntax = ApexParser.MetaClass.VariableDeclarationSyntax;
using ApexVariableDeclaratorSyntax = ApexParser.MetaClass.VariableDeclaratorSyntax;
using ApexWhileStatementSyntax = ApexParser.MetaClass.WhileStatementSyntax;
using IAnnotatedSyntax = ApexParser.MetaClass.IAnnotatedSyntax;

namespace ApexParser.Visitors
{
    public class ApexSyntaxBuilder : CSharpSyntaxWalker
    {
        public const string NoApexSignature = "NoApex";

        public const string NoApexCommentSignature = ":NoApex ";

        public static List<ApexMemberDeclarationSyntax> GetApexSyntaxNodes(CSharpSyntaxNode node)
        {
            var builder = new ApexSyntaxBuilder();
            node?.Accept(builder);
            return builder.ApexClasses;
        }

        public List<ApexMemberDeclarationSyntax> ApexClasses { get; set; } = new List<ApexMemberDeclarationSyntax>();

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                ApexClasses.Add(LastClassMember);
            }
        }

        private class Comments
        {
            private static bool Filter(SyntaxTrivia t) =>
                t.Kind() == SyntaxKind.SingleLineCommentTrivia ||
                t.Kind() == SyntaxKind.SingleLineDocumentationCommentTrivia ||
                t.Kind() == SyntaxKind.MultiLineCommentTrivia ||
                t.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia;

            private static string ExtractText(SyntaxTrivia t)
            {
                if (t.Kind() == SyntaxKind.SingleLineCommentTrivia)
                {
                    return t.ToString().Trim().Substring(2);
                }
                else if (t.Kind() == SyntaxKind.MultiLineDocumentationCommentTrivia)
                {
                    // C# strips the starting sequence /** for these kinds of comments
                    var text = "*" + Environment.NewLine + t.ToString().Trim();
                    return text.Substring(0, text.Length - 4);
                }
                else
                {
                    // multi-line comments
                    var text = t.ToString().Trim();
                    return text.Substring(2, text.Length - 4);
                }
            }

            public static List<string> ToList(SyntaxTriviaList trivias) =>
                trivias.Where(Filter).Select(ExtractText).ToList();

            public static List<string> Leading(CSharpSyntaxNode node) =>
                ToList(node.GetLeadingTrivia());

            public static List<string> Leading(SyntaxToken token) =>
                ToList(token.LeadingTrivia);

            public static List<string> Trailing(CSharpSyntaxNode node) =>
                ToList(node.GetTrailingTrivia());
        }

        private ApexMemberDeclarationSyntax LastClassMember { get; set; }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // get base types
            var baseTypes = (node.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>()).ToList();
            var baseType = ConvertBaseType(baseTypes.FirstOrDefault());
            var interfaces = new List<ApexTypeSyntax>();
            if (baseTypes.Count > 1)
            {
                interfaces = ConvertBaseTypes(baseTypes.Skip(1).ToList());
            }

            // assume that types starting with the capital 'I' are interfaces
            if (baseType?.Identifier?.StartsWith("I") ?? false)
            {
                interfaces.Insert(0, baseType);
                baseType = null;
            }

            // create the class
            var classDeclaration = new ApexClassDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                BaseType = baseType,
                Interfaces = interfaces,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
                LeadingComments = Comments.Leading(node),
                TrailingComments = Comments.Trailing(node),
            };

            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                AddAnnotationOrModifier(classDeclaration, ConvertClassAnnotation(LastAnnotation));
            }

            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                if (LastClassMember != null)
                {
                    classDeclaration.Members.Add(LastClassMember);
                    LastClassMember = null;
                }
            }

            classDeclaration.InnerComments = NoApexComments.Concat(
                Comments.ToList(node.CloseBraceToken.LeadingTrivia)).ToList();
            NoApexComments.Clear();
            LastClassMember = classDeclaration;
        }

        private void AddAnnotationOrModifier(IAnnotatedSyntax member, object converted)
        {
            if (converted is ApexAnnotationSyntax annotation)
            {
                member.Annotations.Add(annotation);
            }
            else if (converted is string modifier)
            {
                if (modifier == ApexKeywords.Global)
                {
                    member.Modifiers.Insert(0, modifier);
                }
                else
                {
                    member.Modifiers.Add(modifier);
                }
            }

            // public and global modifiers shouldn't be mixed
            if (member.Modifiers.Any(m => m == ApexKeywords.Global))
            {
                member.Modifiers.RemoveAll(m => m == ApexKeywords.Public);
            }
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // get base interfaces
            var interfaces = (node.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>()).ToArray();

            // create the interface
            var interfaceDeclaration = new ApexInterfaceDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                BaseType = ConvertBaseType(interfaces.FirstOrDefault()),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                AddAnnotationOrModifier(interfaceDeclaration, ConvertClassAnnotation(LastAnnotation));
            }

            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                if (LastClassMember != null)
                {
                    interfaceDeclaration.Members.Add(LastClassMember);
                    LastClassMember = null;
                }
            }

            interfaceDeclaration.InnerComments = NoApexComments.Concat(
                Comments.ToList(node.CloseBraceToken.LeadingTrivia)).ToList();
            NoApexComments.Clear();
            LastClassMember = interfaceDeclaration;
        }

        private ApexTypeSyntax ConvertBaseType(BaseTypeSyntax csharpType)
        {
            if (csharpType != null)
            {
                return new ApexTypeSyntax(csharpType.ToString());
            }

            return null;
        }

        private List<ApexTypeSyntax> ConvertBaseTypes(IEnumerable<BaseTypeSyntax> csharpTypes) =>
            csharpTypes.EmptyIfNull().Select(ConvertBaseType).Where(t => t != null).ToList();

        private object ConvertClassAnnotation(ApexAnnotationSyntax node)
        {
            // annotations
            if (node.Identifier == "TestFixture")
            {
                return new ApexAnnotationSyntax
                {
                    Identifier = ApexKeywords.IsTest,
                    Parameters = node.Parameters,
                };
            }

            // modifiers
            else if (node.Identifier == "Global")
            {
                return ApexKeywords.Global;
            }
            else if (node.Identifier == "WithSharing")
            {
                return $"{ApexKeywords.With} {ApexKeywords.Sharing}";
            }
            else if (node.Identifier == "WithoutSharing")
            {
                return $"{ApexKeywords.Without} {ApexKeywords.Sharing}";
            }

            return node;
        }

        private object ConvertMethodAnnotation(ApexAnnotationSyntax node)
        {
            // annotations
            if (node.Identifier == "Test")
            {
                return new ApexAnnotationSyntax
                {
                    Identifier = ApexKeywords.IsTest,
                    Parameters = node.Parameters,
                };
            }
            else if (node.Identifier == "SetUp")
            {
                return new ApexAnnotationSyntax
                {
                    Identifier = ApexKeywords.TestSetup,
                    Parameters = node.Parameters,
                };
            }

            // modifiers
            else if (node.Identifier == "WebService")
            {
                return ApexKeywords.WebService;
            }
            else if (node.Identifier == "Global")
            {
                return ApexKeywords.Global;
            }

            return node;
        }

        private object ConvertFieldAnnotation(ApexAnnotationSyntax node)
        {
            // modifiers
            if (node.Identifier == "Transient")
            {
                return ApexKeywords.Transient;
            }

            return node;
        }

        private object ConvertParameterAnnotation(ApexAnnotationSyntax node)
        {
            // modifiers
            if (node.Identifier == "Final")
            {
                return ApexKeywords.Final;
            }

            return node;
        }

        private ApexAnnotationSyntax LastAnnotation { get; set; }

        public override void VisitAttribute(AttributeSyntax node)
        {
            var annotation = new ApexAnnotationSyntax
            {
                Identifier = node.Name.ToString(),
            };

            if (node.ArgumentList != null)
            {
                node.ArgumentList.Accept(this);
                if (!LastAttributeArgumentList.IsNullOrEmpty())
                {
                    annotation.Parameters = string.Join(" ", LastAttributeArgumentList);
                    LastAttributeArgumentList = null;
                }
            }

            LastAnnotation = annotation;
        }

        private List<string> LastAttributeArgumentList { get; set; }

        public override void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            LastAttributeArgumentList = new List<string>();
            foreach (var arg in node.Arguments.EmptyIfNull())
            {
                arg.Accept(this);
                if (!string.IsNullOrWhiteSpace(LastAttributeArgument))
                {
                    LastAttributeArgumentList.Add(LastAttributeArgument);
                    LastAttributeArgument = null;
                }
            }
        }

        private string LastAttributeArgument { get; set; }

        public override void VisitAttributeArgument(AttributeArgumentSyntax node)
        {
            var expr = new StringBuilder();
            if (node.NameColon != null)
            {
                expr.AppendFormat("{0}=", node.NameColon.Name);
            }
            else if (node.NameEquals != null)
            {
                expr.AppendFormat("{0}=", node.NameEquals.Name);
            }

            expr.Append(ConvertExpression(node.Expression).Expression);
            LastAttributeArgument = expr.ToString();
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var enumeration = new ApexEnumDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                enumeration.Members.Add(LastEnumMember);
            }

            enumeration.InnerComments = NoApexComments.Concat(
                Comments.ToList(node.CloseBraceToken.LeadingTrivia)).ToList();
            NoApexComments.Clear();
            LastClassMember = enumeration;
        }

        private ApexEnumMemberDeclarationSyntax LastEnumMember { get; set; }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            LastEnumMember = new ApexEnumMemberDeclarationSyntax
            {
                LeadingComments = NoApexComments.ToList(),
                Identifier = node.Identifier.ValueText,
            };

            NoApexComments.Clear();
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var method = new ApexConstructorDeclarationSyntax
            {
                LeadingComments = NoApexComments.Concat(Comments.Leading(node)).ToList(),
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
                TrailingComments = Comments.Trailing(node),
            };

            NoApexComments.Clear();
            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                AddAnnotationOrModifier(method, ConvertMethodAnnotation(LastAnnotation));
            }

            foreach (var param in node.ParameterList?.Parameters.EmptyIfNull())
            {
                param.Accept(this);
                method.Parameters.Add(LastParameter);
            }

            if (node.Body != null)
            {
                node.Body.Accept(this);
                method.Body = LastStatement as ApexBlockSyntax;
                LastStatement = null;

                if (!method.TrailingComments.IsNullOrEmpty())
                {
                    method.Body.TrailingComments = method.TrailingComments.ToList();
                    method.TrailingComments.Clear();
                }
            }

            if (method.Modifiers.All(m => m != "static"))
            {
                LastClassMember = method;
            }
            else
            {
                // static constructors are converted to the class initializers
                LastClassMember = new ApexClassInitializerSyntax
                {
                    Modifiers = method.Modifiers,
                    Annotations = method.Annotations,
                    Body = method.Body,
                    LeadingComments = method.LeadingComments,
                    TrailingComments = method.TrailingComments,
                };
            }
        }

        private List<string> NoApexComments { get; set; } = new List<string>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // skip methods starting with the signature
            if (node.Identifier.ValueText.StartsWith(NoApexSignature, StringComparison.InvariantCultureIgnoreCase))
            {
                NoApexComments = NoApexComments.Concat(Comments.Leading(node))
                    .Concat(CommentOutNoApexCode(node.ToString())).ToList();
                return;
            }

            var method = new ApexMethodDeclarationSyntax
            {
                LeadingComments = NoApexComments.Concat(Comments.Leading(node)).ToList(),
                Identifier = node.Identifier.ValueText,
                ReturnType = ConvertType(node.ReturnType),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
                TrailingComments = Comments.Trailing(node),
            };

            NoApexComments.Clear();
            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                AddAnnotationOrModifier(method, ConvertMethodAnnotation(LastAnnotation));
            }

            foreach (var param in node.ParameterList?.Parameters.EmptyIfNull())
            {
                param.Accept(this);
                method.Parameters.Add(LastParameter);
            }

            if (node.Body != null)
            {
                node.Body.Accept(this);
                method.Body = LastStatement as ApexBlockSyntax;
                LastStatement = null;

                if (!method.TrailingComments.IsNullOrEmpty())
                {
                    method.Body.TrailingComments = method.TrailingComments.ToList();
                    method.TrailingComments.Clear();
                }
            }

            LastClassMember = method;
        }

        internal List<string> CommentOutNoApexCode(string code)
        {
            var lines = code.Trim().Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            int CalcIndent(string line) =>
                line.Length - line.TrimStart().Length;

            // find out the minimal indent
            var minIndent = 0;
            var indents =
                from line in lines
                where !string.IsNullOrWhiteSpace(line)
                let indent = CalcIndent(line)
                where indent > 0
                select indent;
            if (indents.Any())
            {
                minIndent = indents.Min();
            }

            string TrimMinIndent(string line)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    return string.Empty;
                }

                if (minIndent > 0 && CalcIndent(line) >= minIndent)
                {
                    return line.Substring(minIndent);
                }

                return line;
            }

            // minimize indents and append the signatures
            var processed = lines.Select(line => NoApexCommentSignature + TrimMinIndent(line));
            return processed.ToList();
        }

        private ApexParameterSyntax LastParameter { get; set; }

        public override void VisitParameter(ParameterSyntax node)
        {
            var param = new ApexParameterSyntax(ConvertType(node.Type), node.Identifier.ValueText);

            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                AddAnnotationOrModifier(param, ConvertParameterAnnotation(LastAnnotation));
            }

            LastParameter = param;
        }

        private ApexTypeSyntax ConvertType(TypeSyntax type)
        {
            if (type != null)
            {
                var apexType = GenericExpressionHelper.ConvertCSharpTypesToApex(type.ToString());
                return new ApexTypeSyntax(apexType);
            }

            return null;
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            var field = new ApexFieldDeclarationSyntax
            {
                LeadingComments = NoApexComments.Concat(Comments.Leading(node)).ToList(),
                TrailingComments = Comments.Trailing(node),
                Type = ConvertType(node.Declaration.Type),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            // readonly modifier converted to final
            var index = field.Modifiers.IndexOf("readonly");
            if (index >= 0)
            {
                field.Modifiers[index] = ApexKeywords.Final;
            }

            NoApexComments.Clear();
            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                AddAnnotationOrModifier(field, ConvertFieldAnnotation(LastAnnotation));
            }

            if (node.Declaration != null)
            {
                node.Declaration.Accept(this);
            }

            if (LastVariableDeclaration != null)
            {
                field.Type = LastVariableDeclaration.Type;
                field.Fields = LastVariableDeclaration.Variables.Select(v => new ApexFieldDeclaratorSyntax
                {
                    Identifier = v.Identifier,
                    Expression = v.Expression,
                }).ToList();
            }

            LastClassMember = field;
        }

        private ApexStatementSyntax LastStatement { get; set; }

        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            node.Declaration.Accept(this);
            LastStatement = LastVariableDeclaration;
            LastStatement.LeadingComments = NoApexComments.Concat(Comments.Leading(node)).ToList();
            LastStatement.TrailingComments = Comments.Trailing(node);
        }

        private ApexVariableDeclarationSyntax LastVariableDeclaration { get; set; }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            var variable = new ApexVariableDeclarationSyntax
            {
                Type = ConvertType(node.Type),
            };

            foreach (var var in node.Variables.EmptyIfNull())
            {
                var.Accept(this);
                variable.Variables.Add(LastVariableDeclarator);
            }

            LastVariableDeclaration = variable;
        }

        private ApexVariableDeclaratorSyntax LastVariableDeclarator { get; set; }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            LastVariableDeclarator = new ApexVariableDeclaratorSyntax
            {
                Identifier = node.Identifier.ValueText,
                Expression = ConvertExpression(node.Initializer?.Value),
            };
        }

        private ApexExpressionSyntax ConvertExpression(ExpressionSyntax expression)
        {
            if (expression == null)
            {
                return null;
            }

            var apexExpr = expression.ToString();
            apexExpr = GenericExpressionHelper.ConvertSoqlQueriesToApex(apexExpr);
            apexExpr = GenericExpressionHelper.ConvertSoqlStatementsToApex(apexExpr);
            apexExpr = GenericExpressionHelper.ConvertTypeofExpressionsToApex(apexExpr);
            apexExpr = GenericExpressionHelper.ConvertCSharpIsTypeExpressionToApex(apexExpr);
            apexExpr = GenericExpressionHelper.ConvertCSharpTypesToApex(apexExpr);
            apexExpr = apexExpr.Replace("\"", "'");
            return new ApexExpressionSyntax(apexExpr);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var property = new ApexPropertyDeclarationSyntax
            {
                Type = ConvertType(node.Type),
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            foreach (var accessor in node.AccessorList?.Accessors.EmptyIfNull())
            {
                accessor.Accept(this);
                property.Accessors.Add(LastAccessor);
            }

            LastClassMember = property;
        }

        private ApexAccessorDeclarationSyntax LastAccessor { get; set; }

        public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            var accessor = new ApexAccessorDeclarationSyntax
            {
                IsGetter = node.Kind() == SyntaxKind.GetAccessorDeclaration,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            if (node.Body != null)
            {
                node.Body.Accept(this);
                accessor.Body = LastStatement as ApexBlockSyntax;
                LastStatement = null;
            }

            LastAccessor = accessor;
        }

        public override void VisitBlock(BlockSyntax node)
        {
            var block = new ApexBlockSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
            };

            foreach (var stmt in node.Statements.EmptyIfNull())
            {
                stmt.Accept(this);
                if (LastStatement != null)
                {
                    block.Statements.Add(LastStatement);
                    LastStatement = null;
                }
            }

            block.InnerComments = GetLeadingAndNoApexComments(node.CloseBraceToken );
            NoApexComments.Clear();
            LastStatement = block;
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            var ifStmt = new ApexIfStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Expression = ConvertExpression(node.Condition),
            };

            if (node.Statement != null)
            {
                node.Statement.Accept(this);
                ifStmt.ThenStatement = LastStatement;
            }

            if (node.Else != null)
            {
                node.Else.Accept(this);
                ifStmt.ElseStatement = LastStatement;
            }

            LastStatement = ifStmt;
        }

        private List<string> GetLeadingAndNoApexComments(SyntaxToken token)
        {
            var result = NoApexComments.Concat(Comments.Leading(token)).ToList();
            NoApexComments.Clear();
            return result;
        }

        private List<string> GetLeadingAndNoApexComments(CSharpSyntaxNode node)
        {
            var result = NoApexComments.Concat(Comments.Leading(node)).ToList();
            NoApexComments.Clear();
            return result;
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            // skip stateements starting with the signature
            if (node.ToString().StartsWith(NoApexSignature, StringComparison.InvariantCultureIgnoreCase))
            {
                NoApexComments = CommentOutNoApexCode(node.ToString() + Environment.NewLine);
                return;
            }

            // also handles SOQL insert/update/delete statements
            LastStatement = new ApexStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Body = ConvertExpression(node.Expression).Expression,
            };
        }

        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            LastStatement = new ApexReturnStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Expression = ConvertExpression(node.Expression),
            };
        }

        public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            LastStatement = new ApexThrowStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Expression = ConvertExpression(node.Expression),
            };
        }

        public override void VisitBreakStatement(BreakStatementSyntax node) =>
            LastStatement = new ApexBreakStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
            };

        public override void VisitContinueStatement(ContinueStatementSyntax node) =>
            LastStatement = new ApexContinueStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
            };

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            var doStmt = new ApexDoStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Expression = ConvertExpression(node.Condition),
            };

            if (node.Statement != null)
            {
                node.Statement.Accept(this);
                doStmt.Statement = LastStatement;
            }

            LastStatement = doStmt;
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            var forStmt = new ApexForEachStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Type = ConvertType(node.Type),
                Identifier = node.Identifier.ValueText,
                Expression = ConvertExpression(node.Expression),
            };

            if (node.Statement != null)
            {
                node.Statement.Accept(this);
                forStmt.Statement = LastStatement;
            }

            LastStatement = forStmt;
        }

        public override void VisitForStatement(ForStatementSyntax node)
        {
            var forStmt = new ApexForStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Condition = ConvertExpression(node.Condition),
                Incrementors = node.Incrementors.EmptyIfNull().Select(x => ConvertExpression(x)).ToList(),
            };

            if (node.Declaration != null)
            {
                node.Declaration.Accept(this);
                forStmt.Declaration = LastVariableDeclaration;
            }

            if (node.Statement != null)
            {
                node.Statement.Accept(this);
                forStmt.Statement = LastStatement;
            }

            LastStatement = forStmt;
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            var whileStmt = new ApexWhileStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
                Expression = ConvertExpression(node.Condition),
            };

            if (node.Statement != null)
            {
                node.Statement.Accept(this);
                whileStmt.Statement = LastStatement;
            }

            LastStatement = whileStmt;
        }

        public override void VisitTryStatement(TryStatementSyntax node)
        {
            var tryStatement = new ApexTryStatementSyntax
            {
                LeadingComments = GetLeadingAndNoApexComments(node),
                TrailingComments = Comments.Trailing(node),
            };

            if (node.Block != null)
            {
                node.Block.Accept(this);
                tryStatement.Block = LastStatement as ApexBlockSyntax;
                LastStatement = null;
            }

            foreach (var @catch in node.Catches.EmptyIfNull())
            {
                @catch.Accept(this);
                tryStatement.Catches.Add(LastCatch);
            }

            if (node.Finally != null)
            {
                node.Finally.Accept(this);
                tryStatement.Finally = LastFinally;
            }

            LastStatement = tryStatement;
        }

        private ApexCatchClauseSyntax LastCatch { get; set; }

        public override void VisitCatchClause(CatchClauseSyntax node)
        {
            var catchClause = new ApexCatchClauseSyntax();
            if (node.Declaration != null)
            {
                if (node.Declaration.Type != null)
                {
                    catchClause.Type = ConvertType(node.Declaration.Type);
                }

                if (node.Declaration.Identifier != null)
                {
                    catchClause.Identifier = node.Declaration.Identifier.ValueText;
                }
            }

            node.Block.Accept(this);
            catchClause.Block = LastStatement as ApexBlockSyntax;
            LastStatement = null;
            LastCatch = catchClause;
        }

        private ApexFinallyClauseSyntax LastFinally { get; set; }

        public override void VisitFinallyClause(FinallyClauseSyntax node)
        {
            node.Block.Accept(this);
            LastFinally = new ApexFinallyClauseSyntax
            {
                Block = LastStatement as ApexBlockSyntax,
            };

            LastStatement = null;
        }

        public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            bool Equals(ExpressionSyntax left, string right) =>
                StringComparer.InvariantCultureIgnoreCase.Compare(left.ToString(), right) == 0;

            if (node.Expression is InvocationExpressionSyntax runAs)
            {
                if (Equals(runAs.Expression, "System.runAs"))
                {
                    var argument = runAs.ArgumentList?.Arguments.EmptyIfNull().FirstOrDefault()?.Expression;
                    var runAsNode = new ApexRunAsStatementSyntax
                    {
                        LeadingComments = GetLeadingAndNoApexComments(node),
                        TrailingComments = Comments.Trailing(node),
                        Expression = ConvertExpression(argument),
                    };

                    if (node.Statement != null)
                    {
                        node.Statement.Accept(this);
                        runAsNode.Statement = LastStatement;
                    }

                    LastStatement = runAsNode;
                    return;
                }
            }

            // or perhaps better throw NotSupportedException?
            base.VisitUsingStatement(node);
        }
    }
}
