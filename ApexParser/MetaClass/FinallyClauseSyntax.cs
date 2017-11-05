using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class FinallyClauseSyntax : BaseSyntax
    {
        public override SyntaxType Kind => SyntaxType.Finally;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitFinally(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Block);

        public BlockSyntax Block { get; set; }
    }
}
