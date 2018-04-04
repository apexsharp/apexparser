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

        /// <summary>
        /// Constructs a parser that consumes a whitespace and all comments
        /// parsed by the provider.Comment parser.
        /// </summary>
        /// <typeparam name="T">The result type of the given parser</typeparam>
        /// <param name="parser">The parser to wrap</param>
        /// <param name="provider">The provider for the Comment parser</param>
        /// <returns>An extended Token() version of the given parser.</returns>
        public static Parser<T> Token<T>(this Parser<T> parser, ICommentParserProvider provider)
        {
            // if comment provider is not specified, act like normal Token()
            var trailingCommentParser =
                provider?.CommentParser?.AnyComment?.Token() ??
                Parse.WhiteSpace.Many().Text();

            // parse the value and as many trailing comments as possible
            return
                from value in parser.Commented(provider).Token()
                from comment in trailingCommentParser.Many()
                select value.Value;
        }

        /// <summary>
        /// Constructs a parser that consumes a whitespace and all comments
        /// parsed by the provider.Comment parser, but parses only one trailing
        /// comment that starts exactly on the last line of the parsed value.
        /// </summary>
        /// <typeparam name="T">The result type of the given parser</typeparam>
        /// <param name="parser">The parser to wrap</param>
        /// <param name="provider">The provider for the Comment parser</param>
        /// <returns>An extended Token() version of the given parser.</returns>
        public static Parser<ICommented<T>> Commented<T>(this Parser<T> parser, ICommentParserProvider provider)
        {
            return parser.Commented(provider?.CommentParser);
        }
    }
}
