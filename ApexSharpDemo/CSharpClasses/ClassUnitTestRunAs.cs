namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using Apex.NUnit.Framework;

    [TestFixture]
    public class ClassUnitTestRunAs
    {
        [Test]
        public static void RunAsExample()
        {
            User newUser = new User();
            using (System.RunAs(newUser))
            {
            }
        }
    }
}
