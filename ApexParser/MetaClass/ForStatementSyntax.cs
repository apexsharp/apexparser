using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class ForStatementSyntax : StatementSyntax
    {
        public string Expression { get; set; }

        public StatementSyntax LoopBody { get; set; }
    }
}
