using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public static class SyntaxExtensions
    {
        public static T WithLeadingComments<T>(this T syntax, IEnumerable<string> comments)
            where T : BaseSyntax
        {
            if (comments != null)
            {
                syntax.LeadingComments.AddRange(comments);
            }

            return syntax;
        }

        public static T WithLeadingComment<T>(this T syntax, string comment)
            where T : BaseSyntax
        {
            if (comment != null)
            {
                syntax.LeadingComments.Add(comment);
            }

            return syntax;
        }

        public static T WithTrailingComments<T>(this T syntax, IEnumerable<string> comments)
            where T : BaseSyntax
        {
            if (comments != null)
            {
                syntax.TrailingComments.AddRange(comments);
            }

            return syntax;
        }

        public static T WithTrailingComment<T>(this T syntax, string comment)
            where T : BaseSyntax
        {
            if (comment != null)
            {
                syntax.TrailingComments.Add(comment);
            }

            return syntax;
        }

        public static BlockSyntax WithInnerComments(this BlockSyntax syntax, IEnumerable<string> comments)
        {
            if (comments != null)
            {
                syntax.InnerComments.AddRange(comments);
            }

            return syntax;
        }

        public static BlockSyntax WithInnerComment(this BlockSyntax syntax, params string[] comments) =>
            syntax.WithInnerComments(comments);

        public static T WithProperties<T>(this T syntax, MemberDeclarationSyntax other = null)
            where T : MemberDeclarationSyntax
        {
            if (other != null)
            {
                syntax.LeadingComments = other.LeadingComments;
                syntax.TrailingComments = other.TrailingComments;
                syntax.Annotations = other.Annotations;
                syntax.Modifiers = other.Modifiers;
            }

            return syntax;
        }

        public static bool IsConstructor(this MethodDeclarationSyntax method) =>
            method is ConstructorDeclarationSyntax ||
            method.ReturnType.Identifier == method.Identifier;
    }
}
