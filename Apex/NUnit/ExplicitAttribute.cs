using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explicit = NUnit.Framework.ExplicitAttribute;

namespace Apex.NUnit.Framework
{
    public class ExplicitAttribute : Explicit
    {
        public ExplicitAttribute()
        {
        }

        public ExplicitAttribute(string reason) : base(reason)
        {
        }
    }
}
