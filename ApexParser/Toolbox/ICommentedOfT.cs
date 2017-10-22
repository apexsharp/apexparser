using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser.Toolbox
{
    public interface ICommented<T>
    {
        IEnumerable<string> LeadingComments { get; }

        T Value { get; }

        IEnumerable<string> TrailingComments { get; }
    }
}
