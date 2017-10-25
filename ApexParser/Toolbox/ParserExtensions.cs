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
            public T Value { get; set; }
            public Position Start { get; set; }
            public Position End { get; set; }
            public int Length { get; set; }
        }

        /// <summary>
        /// Constructs a parser that returns the <see cref="ISourceSpan{T}"/> of the parsed value.
        /// </summary>
        /// <typeparam name="T">The result type of the given parser</typeparam>
        /// <param name="parser">The parser to wrap</param>
        /// <returns>A parser for the source span of the given parser.</returns>
        public static Parser<ISourceSpan<T>> Span<T>(this Parser<T> parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

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

        internal abstract class AbstractOption<T> : IOption<T>
        {
            public abstract bool IsEmpty { get; }
            public bool IsDefined => !IsEmpty;
            public T GetOrDefault() => IsEmpty ? default(T) : Get();
            public abstract T Get();
        }

        internal sealed class Some<T> : AbstractOption<T>
        {
            public Some(T value) => Value = value;
            private T Value { get; }
            public override bool IsEmpty => false;
            public override T Get() => Value;
        }

        internal sealed class None<T> : AbstractOption<T>
        {
            public override bool IsEmpty => true;
            public override T Get() => throw new InvalidOperationException("Cannot get value from None.");
        }

        /// <summary>
        /// Constructs a parser that will succeed if the given parser succeeds,
        /// but won't consume any input. It's like a positive look-ahead in regex.
        /// </summary>
        /// <typeparam name="T">The result type of the given parser</typeparam>
        /// <param name="parser">The parser to wrap</param>
        /// <returns>A non-consuming version of the given parser.</returns>
        public static Parser<IOption<T>> Preview<T>(this Parser<T> parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            return i =>
            {
                var result = parser(i);
                if (result.WasSuccessful)
                {
                    return Result.Success(new Some<T>(result.Value), i);
                }

                return Result.Success(new None<T>(), i);
            };
        }

        private class CommentedValue<T> : ICommented<T>
        {
            public CommentedValue(T value)
            {
                LeadingComments = TrailingComments = EmptyList;
                Value = value;
            }

            public CommentedValue(IEnumerable<string> leading, T value, IEnumerable<string> trailing)
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

        /// <summary>
        /// Constructs a parser that consumes a whitespace and all comments
        /// parsed by the provider.Comment parser.
        /// </summary>
        /// <typeparam name="T">The result type of the given parser</typeparam>
        /// <param name="parser">The parser to wrap</param>
        /// <param name="provider">The provider for the Comment parser</param>
        /// <returns>An extended Token() version of the given parser.</returns>
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
                    select new CommentedValue<T>(p);
            }

            // add leading and trailing comments to the parser
            return
                from whiteSpace in Parse.WhiteSpace.Many()
                from leadingComments in provider.Comment.Token().Many()
                from value in parser
                from trailingComments in provider.Comment.Token().Many()
                select new CommentedValue<T>(leadingComments, value, trailingComments);
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
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            // the grammar has no support for comments, use the original Token combinator
            if (provider == null)
            {
                return
                    from p in parser.Token()
                    select new CommentedValue<T>(p);
            }

            // returns true if the second span starts on the first span's last line
            bool IsSameLine(ISourceSpan<T> first, IOption<ISourceSpan<string>> second) =>
                first.End.Line == second.GetOrDefault()?.Start.Line;

            // add leading and trailing comments to the parser
            return
                from leadingWhiteSpace in Parse.WhiteSpace.Many()
                from leadingComments in provider.Comment.Token().Many()
                from valueSpan in parser.Span()
                from trailingWhiteSpace in Parse.WhiteSpace.Many()
                from trailingPreview in provider.Comment.Span().Preview()
                from trailingComments in IsSameLine(valueSpan, trailingPreview) ?
                    provider.Comment.Token().Once() :
                    Parse.WhiteSpace.Many().Text().Return(Enumerable.Empty<string>())
                select new CommentedValue<T>(leadingComments, valueSpan.Value, trailingComments);
        }
    }
}
