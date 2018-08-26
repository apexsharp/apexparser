﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Parser;
using ApexParser.Toolbox;

namespace ApexParser.Visitors
{
    public class CSharpCodeGenerator : ApexCodeGeneratorBase
    {
        public string Namespace { get; set; } = "ApexSharpDemo.ApexCode";

        private bool HasRootNamespace => !string.IsNullOrWhiteSpace(Namespace);

        public const string Soql = "Soql";

        public List<string> Usings { get; set; } = new List<string>
        {
            "Apex.ApexSharp",
            "Apex.ApexSharp.ApexAttributes",
            "Apex.System",
            "SObjects",
        };

        public List<string> UnitTestUsings { get; set; } = new List<string>
        {
            "Apex.ApexSharp.NUnit",
        };

        public static string GenerateCSharp(BaseSyntax ast, int tabSize = 4, string @namespace = null)
        {
            var generator = new CSharpCodeGenerator { IndentSize = tabSize };
            if (!string.IsNullOrWhiteSpace(@namespace))
            {
                generator.Namespace = @namespace;
            }

            ast.Accept(generator);
            return generator.Code.ToString();
        }

        protected override void AppendClassDeclaration(ClassDeclarationSyntax node, string classOrInterface = "class")
        {
            AppendNamespacesForTopLevelDeclaration(node, () =>
            {
                AppendCommentsAttributesAndModifiers(node);
                Append("{0} {1}", classOrInterface, node.Identifier);

                // base type and all interfaces are merged into one comma-separated list
                var baseTypes = Enumerable.Repeat(node.BaseType, 1).Concat(node.Interfaces.EmptyIfNull());
                foreach (var baseType in baseTypes.Where(t => t != null).AsSmart())
                {
                    if (baseType.IsFirst)
                    {
                        Append(" : ");
                    }

                    baseType.Value.Accept(this);
                    if (!baseType.IsLast)
                    {
                        Append(", ");
                    }
                }

                AppendLine();
                AppendClassMembers(node);
            });
        }

        protected override void AppendEnumDeclaration(EnumDeclarationSyntax node)
        {
            AppendNamespacesForTopLevelDeclaration(node, () => base.AppendEnumDeclaration(node));
        }

        private void AppendNamespacesForTopLevelDeclaration(MemberDeclarationSyntax node, Action generateCode)
        {
            var optionalIndent = default(IDisposable);
            var appendNamespace = IsTopLevelDeclaration && HasRootNamespace;
            if (appendNamespace)
            {
                AppendIndentedLine("namespace {0}", Namespace);
                AppendIndentedLine("{{");
                optionalIndent = Indented();
            }

            using (optionalIndent)
            {
                if (IsTopLevelDeclaration)
                {
                    foreach (var ns in GetUsings(node as ClassDeclarationSyntax).OrderBy(s => s).AsSmart())
                    {
                        AppendIndentedLine("using {0};", ns.Value);
                        if (ns.IsLast)
                        {
                            AppendLine();
                        }
                    }
                }

                generateCode();
            }

            if (appendNamespace)
            {
                AppendIndentedLine("}}");
            }
        }

        private IEnumerable<string> GetUsings(ClassDeclarationSyntax node)
        {
            if (node != null)
            {
                var isTest = node.Annotations.EmptyIfNull().Any(a => a?.IsTest ?? false);
                if (!isTest)
                {
                    var testMethods =
                        from method in node.Members.EmptyIfNull().OfType<MethodDeclarationSyntax>()
                        from ann in method.Annotations.EmptyIfNull()
                        where ann?.IsTest ?? false
                        select method;

                    isTest = testMethods.Any();
                }

                if (isTest)
                {
                    return Usings.Concat(UnitTestUsings);
                }
            }

            return Usings;
        }

        private class AnnotatedSyntax : IAnnotatedSyntax
        {
            public List<AnnotationSyntax> Annotations { get; set; } = new List<AnnotationSyntax>();
            public List<string> Modifiers { get; set; } = new List<string>();
        }

        protected override IAnnotatedSyntax ConvertModifiersAndAnnotations(IAnnotatedSyntax ownerNode)
        {
            var result = new AnnotatedSyntax
            {
                Annotations = (ownerNode?.Annotations).EmptyIfNull().ToList(),
            };

            var isGlobal = false;
            var isPublic = false;

            // convert unsupported modifiers into annotations
            foreach (var modifier in ownerNode.Modifiers)
            {
                if (modifier == ApexKeywords.Final)
                {
                    if (ownerNode is ClassDeclarationSyntax || ownerNode is MethodDeclarationSyntax)
                    {
                        result.Modifiers.Add("sealed");
                    }
                    else if (ownerNode is FieldDeclarationSyntax)
                    {
                        result.Modifiers.Add("readonly");
                    }
                    else
                    {
                        result.Annotations.Add(new AnnotationSyntax("Final"));
                    }
                }
                else if (modifier == ApexKeywords.Global)
                {
                    result.Annotations.Add(new AnnotationSyntax("Global"));
                    isGlobal = true;
                }
                else if (modifier.StartsWith(ApexKeywords.Without))
                {
                    result.Annotations.Add(new AnnotationSyntax("WithoutSharing"));
                }
                else if (modifier.StartsWith(ApexKeywords.With))
                {
                    result.Annotations.Add(new AnnotationSyntax("WithSharing"));
                }
                else if (modifier == ApexKeywords.TestMethod)
                {
                    result.Annotations.Add(new AnnotationSyntax("IsTest"));
                }
                else if (modifier == ApexKeywords.Transient)
                {
                    result.Annotations.Add(new AnnotationSyntax("Transient"));
                }
                else if (modifier == ApexKeywords.WebService)
                {
                    result.Annotations.Add(new AnnotationSyntax("WebService"));
                }
                else
                {
                    result.Modifiers.Add(modifier);
                    if (modifier == ApexKeywords.Public)
                    {
                        isPublic = true;
                    }
                }
            }

            // enforce public modifier for the global symbols
            if (isGlobal && !isPublic)
            {
                result.Modifiers.Insert(0, ApexKeywords.Public);
            }

            return result;
        }

        private HashSet<ClassDeclarationSyntax> GeneratedInitializers { get; } =
            new HashSet<ClassDeclarationSyntax>();

        public override void VisitClassInitializer(ClassInitializerSyntax node)
        {
            // generate class initializers once per class
            var currentClass = CurrentMember as ClassDeclarationSyntax;
            if (!GeneratedInitializers.Add(currentClass))
            {
                return;
            }

            // TODO: generate instance initializer
            var initializers = currentClass?.Members?.EmptyIfNull().OfType<ClassInitializerSyntax>();
            GenerateStaticConstructor(currentClass, initializers.Where(init => init.IsStatic));
        }

        private void GenerateStaticConstructor(ClassDeclarationSyntax @class, IEnumerable<ClassInitializerSyntax> initializers)
        {
            if (initializers.IsNullOrEmpty())
            {
                return;
            }

            var nodes = initializers.ToArray();
            var node = initializers.First();
            AppendCommentsAttributesAndModifiers(node);
            AppendLine("{0}()", @class.Identifier);
            if (nodes.Length == 1)
            {
                node.Body.Accept(this);
                return;
            }

            // merge all bodies into one block
            var statements =
                from n in nodes
                let block = new BlockSyntax
                {
                    LeadingComments = n.LeadingComments,
                    Statements = n.Body.Statements,
                    TrailingComments = n.Body.TrailingComments,
                }
                select block as StatementSyntax;

            new BlockSyntax(statements).Accept(this);
        }

        public const string SpecialCommentSignature = ":NoApex ";

        protected override bool IsSpecialComment(string comment) =>
            (comment ?? string.Empty).TrimStart().StartsWith(SpecialCommentSignature, StringComparison.InvariantCultureIgnoreCase);

        protected override string ProcessSpecialComment(string comment)
        {
            comment = (comment ?? string.Empty).TrimStart();
            if (comment.StartsWith(SpecialCommentSignature, StringComparison.InvariantCultureIgnoreCase))
            {
                comment = comment.Substring(SpecialCommentSignature.Length);
            }

            return comment;
        }

        public override void VisitAnnotation(AnnotationSyntax node)
        {
            var attribute = ConvertUnitTestAnnotation(node, CurrentMember);
            if (string.IsNullOrWhiteSpace(attribute.Parameters))
            {
                AppendIndentedLine("[{0}]", attribute.Identifier);
            }
            else
            {
                // make sure parameters are comma-separated
                var parameters = GenericExpressionHelper.ConvertApexAnnotationParametersToCSharp(attribute.Parameters);
                parameters = parameters.Replace("\'", "\"");
                AppendIndentedLine("[{0}({1})]", attribute.Identifier, parameters);
            }
        }

        internal static AnnotationSyntax ConvertUnitTestAnnotation(AnnotationSyntax node, BaseSyntax parentNode)
        {
            // TODO: refactor this method out of CSharpCodeGenerator
            if (!node.IsTest)
            {
                return node;
            }

            if (parentNode is ClassDeclarationSyntax)
            {
                // IsTest => TestFixture
                if (string.Equals(node.Identifier, ApexKeywords.IsTest, StringComparison.OrdinalIgnoreCase))
                {
                    return new AnnotationSyntax
                    {
                        LeadingComments = node.LeadingComments,
                        Identifier = "TestFixture",
                        Parameters = node.Parameters,
                        TrailingComments = node.TrailingComments,
                    };
                }
            }
            else if (parentNode is MethodDeclarationSyntax)
            {
                // IsTest => Test
                if (string.Equals(node.Identifier, ApexKeywords.IsTest, StringComparison.OrdinalIgnoreCase))
                {
                    return new AnnotationSyntax
                    {
                        LeadingComments = node.LeadingComments,
                        Identifier = "Test",
                        Parameters = node.Parameters,
                        TrailingComments = node.TrailingComments,
                    };
                }

                // TestSetup => SetUp
                else if (string.Equals(node.Identifier, ApexKeywords.TestSetup, StringComparison.OrdinalIgnoreCase))
                {
                    return new AnnotationSyntax
                    {
                        LeadingComments = node.LeadingComments,
                        Identifier = "SetUp",
                        Parameters = node.Parameters,
                        TrailingComments = node.TrailingComments,
                    };
                }
            }

            return node;
        }

        protected override void AppendMethodParametersAndBody(MethodDeclarationSyntax node)
        {
            // C# version skips the parameter modifiers
            foreach (var p in node.Parameters.AsSmart())
            {
                p.Value.Accept(this);
                if (!p.IsLast)
                {
                    Append(", ");
                }
            }

            Append(")");

            if (node.Body != null)
            {
                // non-abstract method
                AppendLine();
                node.Body.Accept(this);
            }
            else
            {
                // abstract method
                Append(";");
                AppendTrailingComments(node);
            }
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("foreach (");
            using (SkipNewLines())
            {
                if (node.Type != null)
                {
                    node.Type.Accept(this);
                }

                if (!string.IsNullOrWhiteSpace(node.Identifier))
                {
                    Append(" {0} in ", node.Identifier);
                }

                node.Expression.Accept(this);
                Append(")");
            }

            AppendLine();
            AppendStatementWithOptionalIndent(node.Statement);
        }

        public override void VisitInsertStatement(InsertStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.insert(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        public override void VisitUpdateStatement(UpdateStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.update(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        public override void VisitUpsertStatement(UpsertStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.upsert(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        public override void VisitDeleteStatement(DeleteStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.delete(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        public override void VisitRunAsStatement(RunAsStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("using (System.runAs(");
            node.Expression.Accept(this);
            AppendLine("))");
            AppendStatementWithOptionalIndent(node.Statement);
        }

        public override void VisitExpression(ExpressionSyntax node)
        {
            // replace Apex-style constructor initializers:
            // new Class(Prop1=Value1, Prop2=Value2) => new Class { Prop1=Value1, Prop2=Value2 }
            var expr = GenericExpressionHelper.ConvertApexConstructorInitializerToCSharp(node.ExpressionString);

            // split into portions and process one by one
            base.VisitExpression(new ExpressionSyntax(expr));
        }

        protected override void AppendStringLiteral(string literal) =>
            Append("\"{0}\"", literal.Substring(1, literal.Length - 2));

        protected override void AppendSoqlQuery(string soqlQuery)
        {
            var queryText = soqlQuery.Substring(1, soqlQuery.Length - 2);
            var tableName = GenericExpressionHelper.GetSoqlTableName(queryText);
            var parameters = GenericExpressionHelper.GetSoqlParameters(queryText);
            var paramList = string.Empty;
            if (parameters.Any())
            {
                paramList = ", " + string.Join(", ", parameters);
            }

            Append("Soql.query<{0}>(@\"{1}\"{2})", tableName, queryText, paramList);
        }

        protected override void AppendExpressionPart(string part)
        {
            // replace string.class => typeof(string), string.valueOf(x) => x.ToString(), etc
            part = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(part);
            part = GenericExpressionHelper.ConvertStringValueofToString(part);
            part = GenericExpressionHelper.ConvertApexInstanceOfTypeExpressionToCSharp(part);
            part = GenericExpressionHelper.ConvertApexTypesToCSharp(part);

            base.AppendExpressionPart(part);
        }

        public override string NormalizeTypeName(string identifier) =>
            GenericExpressionHelper.ConvertApexTypeToCSharp(identifier);

        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("switch (");
            node.Expression?.Accept(this);
            AppendLine(")");

            AppendIndentedLine("{{");
            using (Indented())
            {
                foreach (var whenClause in node.WhenClauses.AsSmart())
                {
                    whenClause.Value.Accept(this);
                    if (!whenClause.IsLast)
                    {
                        AppendLine();
                    }
                }
            }

            AppendIndented("}}");
            AppendTrailingComments(node);
        }

        public override void VisitWhenExpressionsClauseSyntax(WhenExpressionsClauseSyntax node)
        {
            AppendLeadingComments(node);

            foreach (var expression in node.Expressions.EmptyIfNull())
            {
                AppendIndented("case ");
                expression.Accept(this);
                AppendLine(":");
            }

            GenerateCaseBlock(node.Block);
        }

        public override void VisitWhenTypeClauseSyntax(WhenTypeClauseSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("case ");
            node.Type.Accept(this);
            AppendLine(" {0}:", node.Identifier);
            GenerateCaseBlock(node.Block);
        }

        public override void VisitWhenElseClauseSyntax(WhenElseClauseSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndentedLine("default:");
            GenerateCaseBlock(node.Block);
        }

        private void GenerateCaseBlock(BlockSyntax block)
        {
            using (Indented())
            {
                GenerateStatements(block.Statements);

                // check the last statement
                var lastStatement = block.Statements.EmptyIfNull().LastOrDefault();
                if (lastStatement is ReturnStatementSyntax || lastStatement is ThrowStatementSyntax)
                {
                    return;
                }

                // emit "break" unless the last statement was either "return" or "throw"
                AppendIndented("break;");
                AppendTrailingComments(block);
            }
        }
    }
}
