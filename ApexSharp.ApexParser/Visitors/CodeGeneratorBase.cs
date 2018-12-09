using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexSharp.ApexParser.Syntax;
using ApexSharp.ApexParser.Toolbox;

namespace ApexSharp.ApexParser.Visitors
{
    public class CodeGeneratorBase : ApexSyntaxVisitor
    {
        protected internal StringBuilder Code { get; } = new StringBuilder();

        protected int IndentLevel { get; private set; }

        public int IndentSize { get; set; } = 4;

        private int SkipIndentCounter { get; set; } = 0;

        protected void SkipIndent(int count = 1) => SkipIndentCounter += count;

        protected void AppendIndent()
        {
            if (SkipIndentCounter > 0)
            {
                SkipIndentCounter--;
            }
            else if (SkipNewLinesLevel == 0)
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

        protected MemberDeclarationSyntax CurrentMember { get; private set; }

        private int MemberDeclarationLevel { get; set; }

        protected bool IsTopLevelDeclaration => MemberDeclarationLevel <= 1;

        protected IDisposable SetCurrentMember(MemberDeclarationSyntax node)
        {
            var oldMember = CurrentMember;
            CurrentMember = node;
            MemberDeclarationLevel++;

            return new Disposable(() =>
            {
                CurrentMember = oldMember;
                MemberDeclarationLevel--;
            });
        }
    }
}
