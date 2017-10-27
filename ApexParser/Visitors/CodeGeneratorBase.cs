using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.Toolbox;

namespace ApexParser.Visitors
{
    public class CodeGeneratorBase : ApexSyntaxVisitor
    {
        protected StringBuilder Code { get; } = new StringBuilder();

        protected int IndentLevel { get; private set; }

        public int IndentSize { get; set; } = 4;

        protected void AppendIndent()
        {
            if (SkipNewLinesLevel == 0)
            {
                Code.Append(new string(' ', IndentLevel * IndentSize));
            }
        }

        protected void AppendLine()
        {
            if (SkipNewLinesLevel == 0)
            {
                Code.AppendLine();
            }
            else if (ReplaceNewLineWithSpace)
            {
                Code.Append(" ");
            }
        }

        protected IDisposable Indented()
        {
            IndentLevel++;
            return new Disposable(() => IndentLevel--);
        }

        private int SkipNewLinesLevel { get; set; }

        private bool ReplaceNewLineWithSpace { get; set; }

        protected IDisposable SkipNewLines(bool replaceWithSpace = true)
        {
            var oldReplace = ReplaceNewLineWithSpace;
            ReplaceNewLineWithSpace = replaceWithSpace;
            SkipNewLinesLevel++;

            return new Disposable(() =>
            {
                SkipNewLinesLevel--;
                ReplaceNewLineWithSpace = oldReplace;
            });
        }

        protected void AppendIndented(string format, params string[] args)
        {
            AppendIndent();
            Append(format, args);
        }

        protected void AppendIndentedLine(string format, params string[] args)
        {
            AppendIndent();
            Append(format, args);
            AppendLine();
        }

        protected void Append(string format, params string[] args) =>
            Code.AppendFormat(format, args);

        protected void AppendLine(string format, params string[] args)
        {
            Code.AppendFormat(format, args);
            AppendLine();
        }
    }
}
