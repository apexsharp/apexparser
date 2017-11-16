using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.MetaClass;
using ApexParser.Toolbox;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApexClassDeclarationSyntax = ApexParser.MetaClass.ClassDeclarationSyntax;
using ApexTypeSyntax = ApexParser.MetaClass.TypeSyntax;
using CSharpClassDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax;

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
                BaseType = ConvertType(baseType),
                Interfaces = ConvertTypes(interfaces),
                Modifiers = node.Modifiers.Select(m => m.ToString()).ToList(),
            };

            base.VisitClassDeclaration(node);
        }

        private ApexTypeSyntax ConvertType(BaseTypeSyntax csharpType)
        {
            if (csharpType != null)
            {
                return new ApexTypeSyntax(csharpType.ToString());
            }

            return null;
        }

        private List<ApexTypeSyntax> ConvertTypes(params BaseTypeSyntax[] csharpTypes) =>
            csharpTypes.EmptyIfNull().Select(ConvertType).Where(t => t != null).ToList();
    }
}
