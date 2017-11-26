using System;
using System.IO;
using NUnit.Framework;

namespace SalesForceAPI.UnitTest
{
    [TestFixture]
    public class UnitTestDataManagerTest
    {
        [Test]
        public void AddIdTest123()
        {
            UnitTestDataManager.RemoveAllIds();
            UnitTestDataManager.AddId("123");
            Assert.AreEqual(1, UnitTestDataManager.IdCount());
        }

        [Test]
        public void RemoveIdTest()
        {
            UnitTestDataManager.RemoveAllIds();
            UnitTestDataManager.AddId("123");
            UnitTestDataManager.RemoveId("123");
            Assert.AreEqual(0, UnitTestDataManager.IdCount());
        }
    }
}
