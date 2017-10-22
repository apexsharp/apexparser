using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using Sprache;

namespace ApexParser.Toolbox
{
    public static class ParserExtensions
    {
        public static T ParseEx<T>(this Parser<T> parser, string input)
        {
            var result = parser.TryParse(input);
            if (result.WasSuccessful)
            {
                return result.Value;
            }

            var message = result.ToString();

            // append the whole current line text
            var lines = (input ?? string.Empty).Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var lineNumber = result.Remainder.Line - 1;
            throw new ParseExceptionCustom(message, lineNumber, lines);
        }

        public static Parser<T> Token<T>(this Parser<T> parser, ICommentParserProvider provider)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            // the grammar has no support for comments, use the original Token combinator
            if (provider == null)
            {
                return parser.Token();
            }

            // add leading and trailing comments to the parser
            var parserEx =
                from leading in Parse.WhiteSpace.Many()
                from leadingComments in provider.Comment.Token().Many()
                from parseResult in parser
                from trailingComments in provider.Comment.Token().Many()
                from trailing in Parse.WhiteSpace.Many()
                select new
                {
                    leadingComments,
                    parseResult,
                    trailingComments,
                };

            // if the parser returns BaseSyntax, populate the comments
            return i =>
            {
                var r = parserEx(i);
                if (r.WasSuccessful)
                {
                    var syntax = r.Value.parseResult as BaseSyntax;
                    if (syntax != null)
                    {
                        var leading = r.Value.leadingComments.ToList();
                        var trailing = r.Value.trailingComments.ToList();
                        syntax = syntax.WithLeadingComments(leading)
                            .WithTrailingComments(trailing);

                        return Result.Success((T)(object)syntax, r.Remainder);
                    }

                    return Result.Success(r.Value.parseResult, r.Remainder);
                }

                return Result.Failure<T>(r.Remainder, r.Message, r.Expectations);
            };
        }
    }
}
