using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class IfStatementSyntax : StatementSyntax
    {
        public string Expression { get; set; }

        public StatementSyntax ThenStatement { get; set; }

        public StatementSyntax ElseStatement { get; set; }
    }
}
