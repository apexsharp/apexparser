using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ApexSharpDemo.ApexCode
{
    [TestFixture]
    public class JasonExampleTest
    {
        [Test]
        public void JsonExampleMethodTest()
        {
            JsonExample jsonExample = new JsonExample();
            Assert.AreEqual("Hello World", jsonExample.JsonExampleMethod());
        }
    }
}
