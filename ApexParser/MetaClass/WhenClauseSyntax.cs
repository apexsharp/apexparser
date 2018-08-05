using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public abstract class WhenClauseSyntax : StatementSyntax
    {
        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Statement);

        public StatementSyntax Statement { get; set; }
    }
}
