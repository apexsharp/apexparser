namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
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
        public static void Asserts()
        {
            Assert.IsTrue(true, "Assert is Not True");
        }

        [Test]
        public static void AssertTestMethod()
        {
            Assert.IsTrue(true, "Assert is Not True");
        }


        [Test]
        public static void AssertEquals()
        {
            Assert.AreEqual(5, 5, "Assert Equal");
        }

        [Test]
        public static void AssertEqualsTestMethod()
        {
            Assert.AreEqual(5, 5, "Assert Equal");
        }

        [Test]
        public static void AssertNotEquals()
        {
            Assert.AreNotEqual(5, 0, "Assert Were Equal");
        }

        [Test]
        public static void AssertNotTestMethod()
        {
            Assert.AreNotEqual(5, 0, "Assert Not Equal");
        }

        [Test]
        static void StaticTestMethod()
        {
            Assert.AreNotEqual(5, 0, "Assert Not Equal");
        }
    }
}
