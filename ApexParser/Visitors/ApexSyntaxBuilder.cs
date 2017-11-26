using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.Parser;
using ApexParser.Toolbox;
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
using ApexTryStatementSyntax = ApexParser.MetaClass.TryStatementSyntax;
using ApexTypeSyntax = ApexParser.MetaClass.TypeSyntax;
using ApexUpdateStatementSyntax = ApexParser.MetaClass.UpdateStatementSyntax;
using ApexVariableDeclarationSyntax = ApexParser.MetaClass.VariableDeclarationSyntax;
using ApexVariableDeclaratorSyntax = ApexParser.MetaClass.VariableDeclaratorSyntax;
using ApexWhileStatementSyntax = ApexParser.MetaClass.WhileStatementSyntax;

namespace ApexParser.Visitors
{
    public class ApexSyntaxBuilder : CSharpSyntaxWalker
    {
        public const string NoApexSignature = "NoApex";

        public const string NoApexCommentSignature = ":NoApex ";

        public static List<ApexBaseSyntax> GetApexSyntaxNodes(CSharpSyntaxNode node)
        {
            var builder = new ApexSyntaxBuilder();
            node?.Accept(builder);
            return builder.ApexClasses;
        }

        public List<ApexBaseSyntax> ApexClasses { get; set; } = new List<ApexBaseSyntax>();

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                ApexClasses.Add(LastClassMember);
            }
        }

        private ApexMemberDeclarationSyntax LastClassMember { get; set; }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // get base types
            var baseTypes = (node.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>()).ToList();
            var baseType = baseTypes.FirstOrDefault();
            var interfaces = new BaseTypeSyntax[0];
            if (baseTypes.Count > 1)
            {
                interfaces = baseTypes.Skip(1).ToArray();
            }

            // create the class
            var classDeclaration = new ApexClassDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                BaseType = ConvertBaseType(baseType),
                Interfaces = ConvertBaseTypes(interfaces),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                classDeclaration.Annotations.Add(ConvertClassAnnotation(LastAnnotation));
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

            classDeclaration.InnerComments = LastComments.ToList();
            LastComments.Clear();
            LastClassMember = classDeclaration;
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // get base interfaces
            var interfaces = (node.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>()).ToArray();

            // create the interface
            var interfaceDeclaration = new ApexInterfaceDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                Interfaces = ConvertBaseTypes(interfaces),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                interfaceDeclaration.Annotations.Add(ConvertClassAnnotation(LastAnnotation));
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

            interfaceDeclaration.InnerComments = LastComments.ToList();
            LastComments.Clear();
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

        private List<ApexTypeSyntax> ConvertBaseTypes(params BaseTypeSyntax[] csharpTypes) =>
            csharpTypes.EmptyIfNull().Select(ConvertBaseType).Where(t => t != null).ToList();

        private ApexAnnotationSyntax ConvertClassAnnotation(ApexAnnotationSyntax node)
        {
            if (node.Identifier == "TestFixture")
            {
                return new ApexAnnotationSyntax
                {
                    Identifier = ApexKeywords.IsTest,
                    Parameters = node.Parameters,
                };
            }

            return node;
        }

        private ApexAnnotationSyntax ConvertMethodAnnotation(ApexAnnotationSyntax node)
        {
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

            return node;
        }

        private ApexAnnotationSyntax LastAnnotation { get; set; }

        public override void VisitAttribute(AttributeSyntax node)
        {
            LastAnnotation = new ApexAnnotationSyntax
            {
                Identifier = node.Name.ToString(),
            };
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

            enumeration.InnerComments = LastComments.ToList();
            LastComments.Clear();
            LastClassMember = enumeration;
        }

        private ApexEnumMemberDeclarationSyntax LastEnumMember { get; set; }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            LastEnumMember = new ApexEnumMemberDeclarationSyntax
            {
                LeadingComments = LastComments.ToList(),
                Identifier = node.Identifier.ValueText,
            };

            LastComments.Clear();
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var method = new ApexConstructorDeclarationSyntax
            {
                LeadingComments = LastComments.ToList(),
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            LastComments.Clear();
            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                method.Annotations.Add(ConvertMethodAnnotation(LastAnnotation));
            }

            foreach (var param in node.ParameterList?.Parameters.EmptyIfNull())
            {
                param.Accept(this);
                method.Parameters.Add(LastParameter);
            }

            if (node.Body != null)
            {
                node.Body.Accept(this);
                method.Body = LastBlock;
            }

            LastClassMember = method;
        }

        private List<string> LastComments { get; set; } = new List<string>();

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // skip methods starting with the signature
            if (node.Identifier.ValueText.StartsWith(NoApexSignature))
            {
                LastComments = CommentOutNoApexCode(node.ToString() + Environment.NewLine);
                return;
            }

            var method = new ApexMethodDeclarationSyntax
            {
                LeadingComments = LastComments.ToList(),
                Identifier = node.Identifier.ValueText,
                ReturnType = ConvertType(node.ReturnType),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            LastComments.Clear();
            foreach (var attr in node.AttributeLists.EmptyIfNull())
            {
                attr.Accept(this);
                method.Annotations.Add(ConvertMethodAnnotation(LastAnnotation));
            }

            foreach (var param in node.ParameterList?.Parameters.EmptyIfNull())
            {
                param.Accept(this);
                method.Parameters.Add(LastParameter);
            }

            if (node.Body != null)
            {
                node.Body.Accept(this);
                method.Body = LastBlock;
            }

            LastClassMember = method;
        }

        internal List<string> CommentOutNoApexCode(string code)
        {
            var lines = code.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
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
            LastParameter = new ApexParameterSyntax(ConvertType(node.Type), node.Identifier.ValueText);
        }

        private ApexTypeSyntax ConvertType(TypeSyntax type)
        {
            if (type != null)
            {
                return new ApexTypeSyntax(type.ToString());
            }

            return null;
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            var field = new ApexFieldDeclarationSyntax
            {
                Type = ConvertType(node.Declaration.Type),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

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

            LastStatement = LastVariableDeclaration = variable;
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
                accessor.Body = LastBlock;
            }

            LastAccessor = accessor;
        }

        private ApexBlockSyntax LastBlock { get; set; }

        public override void VisitBlock(BlockSyntax node)
        {
            var block = new ApexBlockSyntax();

            foreach (var stmt in node.Statements.EmptyIfNull())
            {
                stmt.Accept(this);
                if (LastStatement != null)
                {
                    block.Statements.Add(LastStatement);
                    LastStatement = null;
                }
            }

            LastStatement = LastBlock = block;
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            var ifStmt = new ApexIfStatementSyntax
            {
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

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            // also handles SOQL insert/update/delete statements
            LastStatement = new ApexStatementSyntax
            {
                Body = ConvertExpression(node.Expression).Expression,
            };
        }

        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            LastStatement = new ApexReturnStatementSyntax
            {
                Expression = ConvertExpression(node.Expression),
            };
        }

        public override void VisitBreakStatement(BreakStatementSyntax node) =>
            LastStatement = new ApexBreakStatementSyntax();

        public override void VisitContinueStatement(ContinueStatementSyntax node) =>
            LastStatement = new ApexContinueStatementSyntax();

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            var doStmt = new ApexDoStatementSyntax
            {
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
            var tryStatement = new ApexTryStatementSyntax();
            if (node.Block != null)
            {
                node.Block.Accept(this);
                tryStatement.Block = LastBlock;
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
            catchClause.Block = LastBlock;
            LastCatch = catchClause;
        }

        private ApexFinallyClauseSyntax LastFinally { get; set; }

        public override void VisitFinallyClause(FinallyClauseSyntax node)
        {
            node.Block.Accept(this);
            LastFinally = new ApexFinallyClauseSyntax
            {
                Block = LastBlock,
            };
        }
    }
}
