using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class BlockSyntax : StatementSyntax, IEnumerable, IEnumerable<StatementSyntax>
    {
        public override SyntaxType Kind => SyntaxType.Block;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitBlock(this);

        public List<StatementSyntax> Statements { get; set; } = new List<StatementSyntax>();

        public void Add(StatementSyntax statement) => Statements.Add(statement);

        public List<string> TrailingComments { get; set; } = new List<string>();

        public IEnumerator GetEnumerator() => ((IEnumerable)Statements).GetEnumerator();

        IEnumerator<StatementSyntax> IEnumerable<StatementSyntax>.GetEnumerator() => Statements.GetEnumerator();
    }
}
