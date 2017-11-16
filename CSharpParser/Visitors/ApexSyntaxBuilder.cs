using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.MetaClass;
using ApexParser.Toolbox;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexAccessorDeclarationSyntax = ApexParser.MetaClass.AccessorDeclarationSyntax;
using ApexBlockSyntax = ApexParser.MetaClass.BlockSyntax;
using ApexCatchClauseSyntax = ApexParser.MetaClass.CatchClauseSyntax;
using ApexClassDeclarationSyntax = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexConstructorDeclarationSyntax = ApexParser.MetaClass.ConstructorDeclarationSyntax;
using ApexEnumDeclarationSyntax = ApexParser.MetaClass.EnumDeclarationSyntax;
using ApexEnumMemberDeclarationSyntax = ApexParser.MetaClass.EnumMemberDeclarationSyntax;
using ApexExpressionSyntax = ApexParser.MetaClass.ExpressionSyntax;
using ApexFieldDeclarationSyntax = ApexParser.MetaClass.FieldDeclarationSyntax;
using ApexFieldDeclaratorSyntax = ApexParser.MetaClass.FieldDeclaratorSyntax;
using ApexIfStatementSyntax = ApexParser.MetaClass.IfStatementSyntax;
using ApexMemberDeclarationSyntax = ApexParser.MetaClass.MemberDeclarationSyntax;
using ApexMethodDeclarationSyntax = ApexParser.MetaClass.MethodDeclarationSyntax;
using ApexParameterSyntax = ApexParser.MetaClass.ParameterSyntax;
using ApexPropertyDeclarationSyntax = ApexParser.MetaClass.PropertyDeclarationSyntax;
using ApexReturnStatementSyntax = ApexParser.MetaClass.ReturnStatementSyntax;
using ApexStatementSyntax = ApexParser.MetaClass.StatementSyntax;
using ApexTryStatementSyntax = ApexParser.MetaClass.TryStatementSyntax;
using ApexTypeSyntax = ApexParser.MetaClass.TypeSyntax;
using ApexVariableDeclarationSyntax = ApexParser.MetaClass.VariableDeclarationSyntax;
using ApexVariableDeclaratorSyntax = ApexParser.MetaClass.VariableDeclaratorSyntax;
using CSharpAccessorDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax;
using CSharpBlockSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
using CSharpCatchClauseSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CatchClauseSyntax;
using CSharpCatchDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax;
using CSharpClassDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax;
using CSharpConstructorDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax;
using CSharpEnumDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax;
using CSharpEnumMemberDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax;
using CSharpExpressionSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax;
using CSharpFieldDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax;
using CSharpIfStatementSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax;
using CSharpMethodDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax;
using CSharpParameterSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax;
using CSharpPropertyDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax;
using CSharpReturnStatementSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax;
using CSharpSyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using CSharpTryStatementSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.TryStatementSyntax;
using CSharpTypeSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax;
using CSharpVariableDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax;
using CSharpVariableDeclaratorSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax;

namespace CSharpParser.Visitors
{
    public class ApexSyntaxBuilder : CSharpSyntaxWalker
    {
        public static List<BaseSyntax> GetApexSyntaxNodes(CSharpSyntaxNode node)
        {
            var builder = new ApexSyntaxBuilder();
            node?.Accept(builder);
            return builder.ApexClasses;
        }

        public List<BaseSyntax> ApexClasses { get; set; } = new List<BaseSyntax>();

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                ApexClasses.Add(LastClassMember);
            }
        }

        private BaseTypeDeclarationSyntax[] GetTopLevelTypeDeclarations(CompilationUnitSyntax node) =>
            node.DescendantNodes(n => !(n is CSharpClassDeclarationSyntax))
                .OfType<BaseTypeDeclarationSyntax>().ToArray();

        private ApexClassDeclarationSyntax LastClass { get; set; }

        public override void VisitClassDeclaration(CSharpClassDeclarationSyntax node)
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

            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
                classDeclaration.Members.Add(LastClassMember);
            }

            LastClassMember = LastClass = classDeclaration;
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

        private ApexEnumDeclarationSyntax LastEnum { get; set; }

        public override void VisitEnumDeclaration(CSharpEnumDeclarationSyntax node)
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

            LastClassMember = LastEnum = enumeration;
        }

        private ApexEnumMemberDeclarationSyntax LastEnumMember { get; set; }

        public override void VisitEnumMemberDeclaration(CSharpEnumMemberDeclarationSyntax node)
        {
            LastEnumMember = new ApexEnumMemberDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
            };
        }

        private ApexMemberDeclarationSyntax LastClassMember { get; set; }

        public override void VisitConstructorDeclaration(CSharpConstructorDeclarationSyntax node)
        {
            var method = new ApexConstructorDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

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

        public override void VisitMethodDeclaration(CSharpMethodDeclarationSyntax node)
        {
            var method = new ApexMethodDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                ReturnType = ConvertType(node.ReturnType),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

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

        private ApexParameterSyntax LastParameter { get; set; }

        public override void VisitParameter(CSharpParameterSyntax node)
        {
            LastParameter = new ApexParameterSyntax(ConvertType(node.Type), node.Identifier.ValueText);
        }

        private ApexTypeSyntax ConvertType(CSharpTypeSyntax type)
        {
            if (type != null)
            {
                return new ApexTypeSyntax(type.ToString());
            }

            return null;
        }

        public override void VisitFieldDeclaration(CSharpFieldDeclarationSyntax node)
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

            if (LastVariable != null)
            {
                field.Type = LastVariable.Type;
                field.Fields = LastVariable.Variables.Select(v => new ApexFieldDeclaratorSyntax
                {
                    Identifier = v.Identifier,
                    Expression = v.Expression,
                }).ToList();
            }

            LastClassMember = field;
        }

        private ApexStatementSyntax LastStatement { get; set; }

        private ApexVariableDeclarationSyntax LastVariable { get; set; }

        public override void VisitVariableDeclaration(CSharpVariableDeclarationSyntax node)
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

            LastStatement = LastVariable = variable;
        }

        private ApexVariableDeclaratorSyntax LastVariableDeclarator { get; set; }

        public override void VisitVariableDeclarator(CSharpVariableDeclaratorSyntax node)
        {
            LastVariableDeclarator = new ApexVariableDeclaratorSyntax
            {
                Identifier = node.Identifier.ValueText,
                Expression = ConvertExpression(node.Initializer?.Value),
            };
        }

        private ApexExpressionSyntax ConvertExpression(CSharpExpressionSyntax expression)
        {
            if (expression == null)
            {
                return null;
            }

            var apexExpr = expression.ToString();
            apexExpr = GenericExpressionHelper.ConvertSoqlQueriesToApex(apexExpr).Replace("\"", "'");
            return new ApexExpressionSyntax(apexExpr);
        }

        public override void VisitPropertyDeclaration(CSharpPropertyDeclarationSyntax node)
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

        public override void VisitAccessorDeclaration(CSharpAccessorDeclarationSyntax node)
        {
            var accessor = new ApexAccessorDeclarationSyntax
            {
                IsGetter = node.Kind() == CSharpSyntaxKind.GetAccessorDeclaration,
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

        public override void VisitBlock(CSharpBlockSyntax node)
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

        public override void VisitIfStatement(CSharpIfStatementSyntax node)
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
            LastStatement = new ApexStatementSyntax
            {
                Body = ConvertExpression(node.Expression).Expression,
            };
        }

        public override void VisitReturnStatement(CSharpReturnStatementSyntax node)
        {
            LastStatement = new ApexReturnStatementSyntax
            {
                Expression = ConvertExpression(node.Expression),
            };
        }

        public override void VisitTryStatement(CSharpTryStatementSyntax node)
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

            LastStatement = tryStatement;
        }

        private ApexCatchClauseSyntax LastCatch { get; set; }

        public override void VisitCatchClause(CSharpCatchClauseSyntax node)
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
    }
}
