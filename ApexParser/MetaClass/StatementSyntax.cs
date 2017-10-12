using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.MetaClass
{
    public class StatementSyntax : BaseSyntax
    {
        public StatementSyntax()
        {
            Kind = SyntaxType.Statement;
        }

        public StatementSyntax(string body) : this()
        {
            StatementBody = body;
        }

        public string StatementBody { get; set; }
    }
}
