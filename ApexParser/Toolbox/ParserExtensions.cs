using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (lineNumber > 0 && lineNumber < lines.Length)
            {
                var cr = Environment.NewLine;
                message = $"{message}{cr}--- The current line: ---{cr}{lines[lineNumber]}";
            }

            throw new ParseException(message);
        }
    }
}
