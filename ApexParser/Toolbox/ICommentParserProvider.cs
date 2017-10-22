using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace ApexParser.Toolbox
{
    public interface ICommentParserProvider
    {
        Parser<string> Comment { get; }
    }
}
