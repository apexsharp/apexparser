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

        protected void AppendIndent() =>
            Code.Append(new string(' ', IndentLevel * IndentSize));

        protected IDisposable Indented()
        {
            IndentLevel++;
            return new Disposable(() => IndentLevel--);
        }

        protected void AppendIndented(string format, params string[] args)
        {
            AppendIndent();
            Code.AppendFormat(format, args);
        }

        protected void AppendIndentedLine(string format, params string[] args)
        {
            AppendIndent();
            Code.AppendFormat(format, args);
            Code.AppendLine();
        }

        protected void Append(string format, params string[] args)
        {
            Code.AppendFormat(format, args);
        }

        protected void AppendLine(string format, params string[] args)
        {
            Code.AppendFormat(format, args);
            Code.AppendLine();
        }

        protected void AppendLine() => Code.AppendLine();
    }
}
