using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class BlockStatementSyntax : StatementSyntax
    {
        public List<StatementSyntax> Statements { get; set; } = new List<StatementSyntax>();
    }
}
