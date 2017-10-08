using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace ApexSharpBaseTest
{
    [TestFixture]
    public class NUnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            if (path != null)
            {
                var localPath = new Uri(path).LocalPath;
                var dataFile = File.ReadAllText(localPath + @"\TestData\qa.cls");
                Assert.AreEqual("Test", dataFile);
            }

        }
    }
}