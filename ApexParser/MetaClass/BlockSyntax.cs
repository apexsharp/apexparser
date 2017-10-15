using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class BlockSyntax : StatementSyntax
    {
        public BlockSyntax()
        {
            Kind = SyntaxType.Block;
        }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitBlock(this);

        public List<StatementSyntax> Statements { get; set; } = new List<StatementSyntax>();
    }
}
