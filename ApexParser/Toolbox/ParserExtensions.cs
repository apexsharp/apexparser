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

        private class SourceSpan<T> : ISourceSpan<T>
        {
            public Position Start { get; set; }

            public Position End { get; set; }

            public int Length { get; set; }

            public T Value { get; set; }
        }

        public static Parser<ISourceSpan<T>> Span<T>(this Parser<T> parser)
        {
            return i =>
            {
                var r = parser(i);
                if (r.WasSuccessful)
                {
                    var span = new SourceSpan<T>
                    {
                        Value = r.Value,
                        Start = Position.FromInput(i),
                        End = Position.FromInput(r.Remainder),
                        Length = r.Remainder.Position - i.Position,
                    };

                    return Result.Success(span, r.Remainder);
                }

                return Result.Failure<ISourceSpan<T>>(r.Remainder, r.Message, r.Expectations);
            };
        }

        private class Commented<T> : ICommented<T>
        {
            public Commented(T value)
            {
                LeadingComments = TrailingComments = EmptyList;
                Value = value;
            }

            public Commented(IEnumerable<string> leading, T value, IEnumerable<string> trailing)
            {
                LeadingComments = leading ?? EmptyList;
                Value = value;
                TrailingComments = trailing ?? EmptyList;
            }

            private static readonly string[] EmptyList = new string[0];

            public IEnumerable<string> LeadingComments { get; }

            public T Value { get; }

            public IEnumerable<string> TrailingComments { get; }
        }

        public static Parser<ICommented<T>> Token<T>(this Parser<T> parser, ICommentParserProvider provider)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            // the grammar has no support for comments, use the original Token combinator
            if (provider == null)
            {
                return
                    from p in parser.Token()
                    select new Commented<T>(p);
            }

            // add leading and trailing comments to the parser
            return
                from whiteSpace in Parse.WhiteSpace.Many()
                from leadingComments in provider.Comment.Token().Many()
                from value in parser
                from trailingComments in provider.Comment.Token().Many()
                select new Commented<T>(leadingComments, value, trailingComments);
        }
    }
}
