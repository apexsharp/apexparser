using NUnit.Framework;

namespace SalesForceAPI.UnitTest
{
    [TestFixture, Ignore("TODO: file access violation")]
    public class UnitTestDataManagerTest
    {
        [Test]
        public void AddIdTest()
        {
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
