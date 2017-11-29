using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test = NUnit.Framework.TestAttribute;

namespace Apex.NUnit
{
    public class TestAttribute : Test
    {
        public TestAttribute()
        {
        }

        public bool SeeAllData { get; set; }
    }
}
