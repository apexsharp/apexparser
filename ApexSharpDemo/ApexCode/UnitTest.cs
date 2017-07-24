namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    using NUnit.Framework;

    [TestFixture]
    public class UnitTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            System.Debug("One Time Setup Got Called");
        }

        [Test]
        public void Assert()
        {
            System.Assert(true, "Assert True");
        }


        [Test]
        public void AssertEquals()
        {
            System.AssertEquals(5, 5, "Assert Equal");
        }


        [Test]
        public void AssertNotEquals()
        {
            System.AssertEquals(5, 0, "Assert Not Equal");
        }
    }
}
