using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ApexSharp.ApexParser;
using ApexSharp.ApexParser.Syntax;
using ApexSharp.ApexParser.Toolbox;
using ApexSharp.ApexParser.Visitors;
using Sprache;

namespace ApexSharpDemo.FindRelatedClasses
{
    /// <summary>
    /// Helper class to check whether two classes are related.
    /// Note that it's still may have false positives because we don't have the semantic part.
    /// </summary>
    public class RelatedClassHelper : ApexSyntaxVisitor
    {
        public static bool IsRelated(string apexText, string apexClassName)
        {
            if (string.IsNullOrWhiteSpace(apexText) || string.IsNullOrWhiteSpace(apexClassName))
            {
                return false;
            }

            try
            {
                // try to parse and analyze the file
                var ast = ApexSharpParser.GetApexAst(apexText);

                // ignore the class itself
                if (ast is ClassDeclarationSyntax @class && Comparer.Equals(@class.Identifier, apexClassName))
                {
                    return false;
                }
                else if (ast is EnumDeclarationSyntax @enum && Comparer.Equals(@enum.Identifier, apexClassName))
                {
                    return false;
                }

                // inspect the code structure
                var visitor = new RelatedClassHelper(apexClassName);
                ast.Accept(visitor);
                return visitor.ClassIsRelated;
            }
            catch (ParseException)
            {
                // fall back to simple string comparison
                return apexText.Contains(apexClassName, StringComparison.CurrentCultureIgnoreCase);
            }
        }

        public RelatedClassHelper(string apexClassName)
        {
            ApexClassName = apexClassName;
            ApexClassNameRegex = new Regex($"\\b{Regex.Escape(apexClassName)}\\b",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private string ApexClassName { get; }

        private Regex ApexClassNameRegex { get; }

        private bool ClassIsRelated { get; set; }

        private static StringComparer Comparer => StringComparer.OrdinalIgnoreCase;

        public override void DefaultVisit(BaseSyntax node)
        {
            foreach (var child in node.ChildNodes)
            {
                if (ClassIsRelated)
                {
                    return;
                }

                child.Accept(this);
            }

            base.DefaultVisit(node);
        }

        public override void VisitAnnotation(AnnotationSyntax node)
        {
            if (ClassIsRelated)
            {
                return;
            }

            ClassIsRelated = Comparer.Equals(node.Identifier, ApexClassName);
            base.VisitAnnotation(node);
        }

        public override void VisitType(TypeSyntax node)
        {
            if (ClassIsRelated)
            {
                return;
            }

            ClassIsRelated = Comparer.Equals(node.Identifier, ApexClassName);
            base.VisitType(node);
        }

        public override void VisitExpression(ExpressionSyntax node)
        {
            if (ClassIsRelated)
            {
                return;
            }

            ClassIsRelated = ExpressionContainsApexClassName(node.ExpressionString);
            base.VisitExpression(node);
        }

        public override void VisitBlock(BlockSyntax node)
        {
            if (ClassIsRelated)
            {
                return;
            }

            ClassIsRelated = node.Statements.OfType<StatementSyntax>().Any(s => ExpressionContainsApexClassName(s.Body));
            base.VisitBlock(node);
        }

        internal bool ExpressionContainsApexClassName(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
            {
                return false;
            }

            var parts = GenericExpressionHelper.Split(expr);
            foreach (var part in parts)
            {
                if (part.StartsWith("'"))
                {
                    // ignore string literals
                    continue;
                }
                else if (part.StartsWith("["))
                {
                    // ignore SOQL queries
                    continue;
                }
                else
                {
                    // compare the whole word considering the word boundaries
                    if (ApexClassNameRegex.IsMatch(part))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
