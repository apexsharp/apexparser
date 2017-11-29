using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFixture = NUnit.Framework.TestFixtureAttribute;

namespace Apex.NUnit
{
    public class TestFixtureAttribute : TestFixture
    {
        public TestFixtureAttribute()
        {
        }

        public bool SeeAllData { get; set; }
    }
}
