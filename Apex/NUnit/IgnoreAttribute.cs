using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ignore = NUnit.Framework.IgnoreAttribute;

namespace Apex.NUnit.Framework
{
    public class IgnoreAttribute : Ignore
    {
        public IgnoreAttribute(string msg = "TODO") : base(msg)
        {
        }
    }
}
