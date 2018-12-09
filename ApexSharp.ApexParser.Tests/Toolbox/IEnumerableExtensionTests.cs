using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Toolbox;
using NUnit.Framework;

namespace ApexSharp.ApexParser.Tests.Toolbox
{
    [TestFixture]
    public class IEnumerableExtensionTests
    {
        [Test]
        public void EmptyIfNullReturnsEmptyEnumerableInsteadOfNull()
        {
            int[] x = null;
            var enumerable = x.EmptyIfNull();
            Assert.NotNull(enumerable);
            Assert.False(enumerable.Any());
        }

        [Test]
        public void AsSmartExtensionMethodReturnsSmartEnumerableForNullEnumerables()
        {
            int[] x = null;
            foreach (var t in x.AsSmart())
            {
                Console.WriteLine("Hello!");
            }
        }
    }
}
