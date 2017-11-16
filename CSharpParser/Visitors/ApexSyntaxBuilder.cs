using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.MetaClass;
using ApexParser.Toolbox;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexBlockSyntax = ApexParser.MetaClass.BlockSyntax;
using ApexClassDeclarationSyntax = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexConstructorDeclarationSyntax = ApexParser.MetaClass.ConstructorDeclarationSyntax;
using ApexEnumDeclarationSyntax = ApexParser.MetaClass.EnumDeclarationSyntax;
using ApexEnumMemberDeclarationSyntax = ApexParser.MetaClass.EnumMemberDeclarationSyntax;
using ApexMethodDeclarationSyntax = ApexParser.MetaClass.MethodDeclarationSyntax;
using ApexParameterSyntax = ApexParser.MetaClass.ParameterSyntax;
using ApexTypeSyntax = ApexParser.MetaClass.TypeSyntax;
using CSharpClassDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax;
using CSharpConstructorDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax;
using CSharpEnumDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax;
using CSharpEnumMemberDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax;
using CSharpMethodDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax;
using CSharpParameterSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax;
using CSharpTypeSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax;

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

            var types = node.ChildNodes().OfType<BaseTypeDeclarationSyntax>().ToArray();
            foreach (var type in types)
            {
                if (ConvertedNodes.TryGetValue(type, out var apexType))
                {
                    ApexClasses.Add(apexType);
                }
            }
        }

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
            ConvertedNodes[node] = CurrentClass = new ApexClassDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                BaseType = ConvertBaseType(baseType),
                Interfaces = ConvertBaseTypes(interfaces),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            base.VisitClassDeclaration(node);
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

            base.VisitEnumDeclaration(node);
        }

        public override void VisitEnumMemberDeclaration(CSharpEnumMemberDeclarationSyntax node)
        {
            ConvertedNodes[node] = new ApexEnumMemberDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
            };

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
                Body = CurrentBlock = new ApexBlockSyntax(),
            };

            CurrentClass.Members.Add(CurrentMethod);
            ConvertedNodes[node.Body] = CurrentBlock;

            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(CSharpMethodDeclarationSyntax node)
        {
            ConvertedNodes[node] = CurrentMethod = new ApexMethodDeclarationSyntax
            {
                Identifier = node.Identifier.ValueText,
                ReturnType = ConvertType(node.ReturnType),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
                Body = CurrentBlock = new ApexBlockSyntax(),
            };

            CurrentClass.Members.Add(CurrentMethod);
            ConvertedNodes[node.Body] = CurrentBlock;

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
    }
}
