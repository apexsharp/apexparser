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

        internal static Parser<T> TokenEx<T>(this Parser<T> parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            // if the grammar class provides comments, use them
            if (parser.Target is ICommentParserProvider provider)
            {
                // add leading and trailing comments to the parser
                var parserEx =
                    from leadingComments in provider.Comment.Token().Many()
                    from parseResult in parser
                    from trailingComments in provider.Comment.Token().Many()
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

            // the grammar has no support for comments, use the original Token combinator
            return parser.Token();
        }
    }
}
