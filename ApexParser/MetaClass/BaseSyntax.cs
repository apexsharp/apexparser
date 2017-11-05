using System;
using System.Collections.Generic;
using System.Linq;
using ApexParser.Toolbox;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
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

        public override string ToString() => $"{GetType().Name}: {this.ToApex()}";

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
