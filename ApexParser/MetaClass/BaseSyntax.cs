using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using ApexParser.Toolbox;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    [DebuggerTypeProxy(typeof(BaseSyntaxDebuggerProxy))]
    public abstract class BaseSyntax
    {
        public abstract SyntaxType Kind { get; }

        public abstract void Accept(ApexSyntaxVisitor visitor);

        public abstract IEnumerable<BaseSyntax> ChildNodes { get; }

        protected IEnumerable<BaseSyntax> GetNodes(params BaseSyntax[] nodes) =>
            nodes.EmptyIfNull().Where(n => n != null);

        protected IEnumerable<BaseSyntax> NoChildren => Enumerable.Empty<BaseSyntax>();

        public List<string> LeadingComments { get; set; } = new List<string>();

        public List<string> TrailingComments { get; set; } = new List<string>();

        private static Regex WhitespaceRegex { get; } = new Regex(@"\s+", RegexOptions.Compiled);

        private string CompactApex => WhitespaceRegex.Replace(this.ToApex(), " ");

        public override string ToString() => $"{GetType().Name}: {CompactApex}";

        public IEnumerable<BaseSyntax> DescendantNodes(Func<BaseSyntax, bool> descendIntoChildren = null) =>
            DescendantNodesAndSelf(descendIntoChildren).Skip(1);

        public IEnumerable<BaseSyntax> DescendantNodesAndSelf(Func<BaseSyntax, bool> descendIntoChildren = null)
        {
            yield return this;

            if (descendIntoChildren == null || descendIntoChildren(this))
            {
                foreach (var child in ChildNodes)
                {
                    foreach (var desc in child.DescendantNodesAndSelf(descendIntoChildren))
                    {
                        yield return desc;
                    }
                }
            }
        }
    }
}
