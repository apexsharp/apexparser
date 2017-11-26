using NUnit.Framework;

namespace SalesForceAPI.UnitTest
{
    [SetUpFixture]
    public class OneTimeSetUp
    {
        [OneTimeSetUp]
        public static void Init()
        {
            // Always Initialize your settings before using it.
            var status = Setup.Init();
            Assert.IsTrue(status);

            UnitTestDataManager.UnitTestDataManagerOn();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            UnitTestDataManager.UnitTestDataManagerOff();
        }
    }
}
