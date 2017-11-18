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
            System.AssertEquals(5, 5, "Assert Equal");

            Assert.AreEqual(4, 2 + 2);
        }

        [Test]
        public static /* testMethod */ void AssertEqualsTestMethod()
        {
            System.AssertEquals(5, 5, "Assert Equal");
        }

        [Test]
        public static void AssertNotEquals()
        {
            System.AssertNotEquals(5, 0, "Assert Not Equal");
            Assert.AreNotEqual();
        }

        [Test]
        public static /* testMethod */ void AssertNotTestMethod()
        {
            System.AssertNotEquals(5, 0, "Assert Not Equal");
        }

        [Test]
        static /* testMethod */ void StaticTestMethod()
        {
            System.AssertNotEquals(5, 0, "Assert Not Equal");
        }
    }
}
