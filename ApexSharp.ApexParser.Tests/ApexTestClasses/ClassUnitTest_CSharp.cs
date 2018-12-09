namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.ApexSharp.NUnit;
    using Apex.System;
    using SObjects;

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
            System.Assert(true, "Assert True");
        }

        [Test]
        public static void AssertEquals()
        {
            System.AssertEquals(5, 5, "Assert Equal");
        }

        [Test]
        public static void AssertNotEquals()
        {
            System.AssertNotEquals(5, 0, "Assert Not Equal");
        }

        [Test]
        public static void AssertNew()
        {
            System.Assert(true, "Assert True");
        }

        [Test]
        static public void AssertEqualsNew()
        {
            System.AssertEquals(5, 5, "Assert Equal");
        }

        [Test]
        static public void AssertNotEqualsNew()
        {
            System.AssertNotEquals(5, 0, "Assert Not Equal");
        }
    }
}
