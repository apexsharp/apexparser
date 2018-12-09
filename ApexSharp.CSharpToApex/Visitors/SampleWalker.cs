using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ApexSharp.CSharpToApex.Visitors
{
    public class SampleWalker : CSharpSyntaxWalker
    {
        private int IndentLevel { get; set; } = 0;

        private StringBuilder Builder { get; } = new StringBuilder();

        public override void Visit(SyntaxNode node)
        {
            var indents = new string('\t', IndentLevel);
            Builder.AppendLine(indents + node.Kind());

            IndentLevel++;
            base.Visit(node);
            IndentLevel--;
        }

        public override string ToString() => Builder.ToString();
    }
}
