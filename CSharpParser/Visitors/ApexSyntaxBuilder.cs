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
using ApexClassDeclarationSyntax = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexConstructorDeclarationSyntax = ApexParser.MetaClass.ConstructorDeclarationSyntax;
using ApexEnumDeclarationSyntax = ApexParser.MetaClass.EnumDeclarationSyntax;
using ApexEnumMemberDeclarationSyntax = ApexParser.MetaClass.EnumMemberDeclarationSyntax;
using ApexExpressionSyntax = ApexParser.MetaClass.ExpressionSyntax;
using ApexFieldDeclarationSyntax = ApexParser.MetaClass.FieldDeclarationSyntax;
using ApexFieldDeclaratorSyntax = ApexParser.MetaClass.FieldDeclaratorSyntax;
using ApexMethodDeclarationSyntax = ApexParser.MetaClass.MethodDeclarationSyntax;
using ApexParameterSyntax = ApexParser.MetaClass.ParameterSyntax;
using ApexPropertyDeclarationSyntax = ApexParser.MetaClass.PropertyDeclarationSyntax;
using ApexSyntaxType = ApexParser.MetaClass.SyntaxType;
using ApexTypeSyntax = ApexParser.MetaClass.TypeSyntax;
using ApexVariableDeclarationSyntax = ApexParser.MetaClass.VariableDeclarationSyntax;
using ApexVariableDeclaratorSyntax = ApexParser.MetaClass.VariableDeclaratorSyntax;
using CSharpAccessorDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax;
using CSharpBlockSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.BlockSyntax;
using CSharpClassDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax;
using CSharpConstructorDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax;
using CSharpEnumDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax;
using CSharpEnumMemberDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax;
using CSharpExpressionSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax;
using CSharpFieldDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.FieldDeclarationSyntax;
using CSharpMethodDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax;
using CSharpParameterSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax;
using CSharpPropertyDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax;
using CSharpSyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
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

        public Dictionary<CSharpSyntaxNode, BaseSyntax> ConvertedNodes { get; } =
            new Dictionary<CSharpSyntaxNode, BaseSyntax>();

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            base.VisitCompilationUnit(node);

            var types = GetTopLevelTypeDeclarations(node);
            foreach (var type in types)
            {
                if (ConvertedNodes.TryGetValue(type, out var apexType))
                {
                    ApexClasses.Add(apexType);
                }
            }
        }

        private BaseTypeDeclarationSyntax[] GetTopLevelTypeDeclarations(CompilationUnitSyntax node) =>
            node.DescendantNodes(n => !(n is CSharpClassDeclarationSyntax))
                .OfType<BaseTypeDeclarationSyntax>().ToArray();

        private ApexClassDeclarationSyntax CurrentClass { get; set; }

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
            var oldCurrentClass = CurrentClass;
            ConvertedNodes[node] = CurrentClass = new ApexClassDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                BaseType = ConvertBaseType(baseType),
                Interfaces = ConvertBaseTypes(interfaces),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            if (oldCurrentClass != null)
            {
                oldCurrentClass.Members.Add(CurrentClass);
            }

            base.VisitClassDeclaration(node);
            CurrentClass = oldCurrentClass;
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

        private ApexEnumDeclarationSyntax CurrentEnum { get; set; }

        public override void VisitEnumDeclaration(CSharpEnumDeclarationSyntax node)
        {
            // create the class
            ConvertedNodes[node] = CurrentEnum = new ApexEnumDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            if (CurrentClass != null)
            {
                CurrentClass.Members.Add(CurrentEnum);
            }

            base.VisitEnumDeclaration(node);
        }

        public override void VisitEnumMemberDeclaration(CSharpEnumMemberDeclarationSyntax node)
        {
            var member = new ApexEnumMemberDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
            };

            ConvertedNodes[node] = member;
            CurrentEnum.Members.Add(member);
            base.VisitEnumMemberDeclaration(node);
        }

        private ApexMethodDeclarationSyntax CurrentMethod { get; set; }

        private ApexBlockSyntax CurrentBlock { get; set; }

        public override void VisitConstructorDeclaration(CSharpConstructorDeclarationSyntax node)
        {
            ConvertedNodes[node] = CurrentMethod = new ApexConstructorDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                ReturnType = new ApexTypeSyntax(CurrentClass.Identifier),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            CurrentClass.Members.Add(CurrentMethod);
            if (node.Body != null)
            {
                ConvertedNodes[node.Body] = CurrentBlock = CurrentMethod.Body = new ApexBlockSyntax();
            }

            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(CSharpMethodDeclarationSyntax node)
        {
            ConvertedNodes[node] = CurrentMethod = new ApexMethodDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                ReturnType = ConvertType(node.ReturnType),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            CurrentClass.Members.Add(CurrentMethod);
            if (node.Body != null)
            {
                ConvertedNodes[node.Body] = CurrentBlock = CurrentMethod.Body = new ApexBlockSyntax();
            }

            base.VisitMethodDeclaration(node);
        }

        public override void VisitParameter(CSharpParameterSyntax node)
        {
            var param = new ApexParameterSyntax(ConvertType(node.Type), node.Identifier.ValueText);
            ConvertedNodes[node] = param;
            CurrentMethod.Parameters.Add(param);
            base.VisitParameter(node);
        }

        private ApexTypeSyntax ConvertType(CSharpTypeSyntax type)
        {
            if (type != null)
            {
                return new ApexTypeSyntax(type.ToString());
            }

            return null;
        }

        private ApexFieldDeclarationSyntax CurrentField { get; set; }

        public override void VisitFieldDeclaration(CSharpFieldDeclarationSyntax node)
        {
            ConvertedNodes[node] = CurrentField = new ApexFieldDeclarationSyntax
            {
                Type = ConvertType(node.Declaration.Type),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            CurrentClass.Members.Add(CurrentField);
            base.VisitFieldDeclaration(node);
            if (CurrentVariable != null)
            {
                CurrentField.Type = CurrentVariable.Type;
                CurrentField.Fields = CurrentVariable.Variables.Select(v => new ApexFieldDeclaratorSyntax
                {
                    Identifier = v.Identifier,
                    Expression = v.Expression,
                }).ToList();
            }

            CurrentField = null;
        }

        private ApexVariableDeclarationSyntax CurrentVariable { get; set; }

        public override void VisitVariableDeclaration(CSharpVariableDeclarationSyntax node)
        {
            ConvertedNodes[node] = CurrentVariable = new ApexVariableDeclarationSyntax
            {
                Type = ConvertType(node.Type),
            };

            if (CurrentField == null && CurrentBlock != null)
            {
                CurrentBlock.Statements.Add(CurrentVariable);
            }

            base.VisitVariableDeclaration(node);
        }

        public override void VisitVariableDeclarator(CSharpVariableDeclaratorSyntax node)
        {
            CurrentVariable.Variables.Add(new ApexVariableDeclaratorSyntax
            {
                Identifier = node.Identifier.ValueText,
                Expression = ConvertExpression(node.Initializer?.Value),
            });

            base.VisitVariableDeclarator(node);
        }

        private ApexExpressionSyntax ConvertExpression(CSharpExpressionSyntax expression)
        {
            if (expression == null)
            {
                return null;
            }

            return new ApexExpressionSyntax(expression.ToString().Replace("\"", "'"));
        }

        private ApexPropertyDeclarationSyntax CurrentProperty { get; set; }

        public override void VisitPropertyDeclaration(CSharpPropertyDeclarationSyntax node)
        {
            ConvertedNodes[node] = CurrentProperty = new ApexPropertyDeclarationSyntax
            {
                Type = ConvertType(node.Type),
                Identifier = node.Identifier.ValueText,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            CurrentClass.Members.Add(CurrentProperty);
            base.VisitPropertyDeclaration(node);
        }

        public override void VisitAccessorDeclaration(CSharpAccessorDeclarationSyntax node)
        {
            var accessor = new ApexAccessorDeclarationSyntax
            {
                IsGetter = node.Kind() == CSharpSyntaxKind.GetAccessorDeclaration,
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            CurrentProperty.Accessors.Add(accessor);
            if (node.Body != null)
            {
                ConvertedNodes[node.Body] = CurrentBlock = accessor.Body = new ApexBlockSyntax();
            }

            base.VisitAccessorDeclaration(node);
        }

        public override void VisitBlock(CSharpBlockSyntax node)
        {
            var block = default(ApexBlockSyntax);
            if (ConvertedNodes.TryGetValue(node, out var apexNode))
            {
                block = (ApexBlockSyntax)apexNode;
            }
            else
            {
                block = new ApexBlockSyntax();
                CurrentBlock.Statements.Add(block);
            }

            // handle nested blocks
            var oldCurrentBlock = CurrentBlock;
            CurrentBlock = block;
            base.VisitBlock(node);
            CurrentBlock = oldCurrentBlock;
        }
    }
}
