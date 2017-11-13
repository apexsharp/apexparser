using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpParser;
using NUnit.Framework;

namespace CSharpParserTest
{
    [TestFixture]
    public class CSharpParserTests
    {
        [Test]
        public void CSharpParserTest()
        {
            Assert.AreEqual(0, CSharpHelper.Sample());
        }
    }
}
