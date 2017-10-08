namespace ApexSharpDemo.ApexCode
{
    using NUnit.Framework;

    [TestFixture]
    public class UnitTest
    {
        private bool _isTrueSetup;

        [OneTimeSetUp]
        public void Setup()
        {
            _isTrueSetup = true;
        }

        [Test]
        public void AssertIsTrue()
        {
            Assert.IsTrue(_isTrueSetup, "Assert True Test");
        }


        [Test]
        public void AssertEquals()
        {
            Assert.AreEqual(5, 5, "Assert Equal Test");
        }


        [Test]
        public void AssertNotEquals()
        {
            Assert.AreNotEqual(5, 0, "Assert Not Equal Test");
        }
    }
}
