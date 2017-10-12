namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    using NUnit.Framework;

    [TestFixture]
    public class ClassUnitTest
    {
        [SetUp]
        public static void Setup()
        {
            System.Debug("One Time Setup Got Called");
        }

        [Test]
        public static void AssertTrue()
        {
            Assert.IsTrue(true, "Assert True");
        }
        [Test]
        public static void AssertEquals()
        {
            Assert.AreEqual(5, 5, "Assert Equal");
        }
        [Test]
        public static void AssertNotEquals()
        {
            Assert.AreNotEqual(5, 0, "Assert Not Equal");
        }
        [Test]
        public static void AssertNew()
        {
            Assert.IsTrue(true, "Assert True");
        }
        [Test]
        public static void AssertEqualsNew()
        {
            Assert.AreEqual(5, 5, "Assert Equal");
        }
        [Test]
        public static void AssertNotEqualsNew()
        {
            Assert.AreNotEqual(5, 0, "Assert Not Equal");
        }
    }
}
