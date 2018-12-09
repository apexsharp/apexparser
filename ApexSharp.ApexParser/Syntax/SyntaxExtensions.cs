using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Toolbox;

namespace ApexSharp.ApexParser.Syntax
{
    public static class SyntaxExtensions
    {
        public static T WithLeadingComments<T>(this T syntax, IEnumerable<string> comments)
            where T : BaseSyntax
        {
            if (comments != null && syntax != null)
            {
                syntax.LeadingComments.AddRange(comments);
            }

            return syntax;
        }

        public static T WithLeadingComment<T>(this T syntax, string comment)
            where T : BaseSyntax
        {
            if (comment != null && syntax != null)
            {
                syntax.LeadingComments.Add(comment);
            }

            return syntax;
        }

        public static T WithTrailingComments<T>(this T syntax, IEnumerable<string> comments)
            where T : BaseSyntax
        {
            if (comments != null && syntax != null)
            {
                syntax.TrailingComments.AddRange(comments);
            }

            return syntax;
        }

        public static T WithTrailingComment<T>(this T syntax, string comment)
            where T : BaseSyntax
        {
            if (comment != null && syntax != null)
            {
                syntax.TrailingComments.Add(comment);
            }

            return syntax;
        }

        public static BlockSyntax WithInnerComments(this BlockSyntax syntax, IEnumerable<string> comments)
        {
            if (comments != null && syntax != null)
            {
                syntax.InnerComments.AddRange(comments);
            }

            return syntax;
        }

        public static BlockSyntax WithInnerComment(this BlockSyntax syntax, params string[] comments) =>
            syntax.WithInnerComments(comments);

        public static ClassDeclarationSyntax WithInnerComments(this ClassDeclarationSyntax syntax, IEnumerable<string> comments)
        {
            if (comments != null && syntax != null)
            {
                syntax.InnerComments.AddRange(comments);
            }

            return syntax;
        }

        public static ClassDeclarationSyntax WithInnerComment(this ClassDeclarationSyntax syntax, params string[] comments) =>
            syntax.WithInnerComments(comments);

        public static T WithProperties<T>(this T syntax, MemberDeclarationSyntax other = null)
            where T : MemberDeclarationSyntax
        {
            if (other != null && syntax != null)
            {
                syntax.LeadingComments = Concat(syntax.LeadingComments, other.LeadingComments);
                syntax.TrailingComments = Concat(syntax.TrailingComments, other.TrailingComments);
                syntax.Annotations = Concat(syntax.Annotations, other.Annotations);
                syntax.Modifiers = Concat(syntax.Modifiers, other.Modifiers);
            }

            return syntax;
        }

        private static List<T> Concat<T>(List<T> first, List<T> second) =>
            first.EmptyIfNull().Concat(second.EmptyIfNull()).ToList();

        public static bool IsConstructor(this MethodDeclarationSyntax method, string className) =>
            method is ConstructorDeclarationSyntax ||
            method.Identifier == className;
    }
}
