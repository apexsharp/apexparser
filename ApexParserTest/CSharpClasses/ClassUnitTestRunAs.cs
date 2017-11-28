namespace ApexParserTest.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using NUnit.Framework;

    [TestFixture]
    public class ClassUnitTestRunAs
    {
        [Test]
        static void RunAsExample()
        {
            User newUser = new User();
            System.runAs(newUser)
            {
            }
        }
    }
}
