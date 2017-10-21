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

        public static T WithTrailingComments<T>(this T syntax, IEnumerable<string> comments)
            where T : BaseSyntax
        {
            if (comments != null)
            {
                syntax.TrailingComments.AddRange(comments);
            }

            return syntax;
        }

        public static T WithProperties<T>(this T syntax, T other = null)
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
