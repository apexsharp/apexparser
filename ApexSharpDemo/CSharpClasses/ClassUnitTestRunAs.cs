namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;
    using Apex.NUnit;

    [TestFixture]
    public class ClassUnitTestRunAs
    {
        [Test]
        static void RunAsExample()
        {
            User newUser = Soql.Query<User>("SELECT Id FROM User LIMIT 1");
            using (System.RunAs(newUser))
            {
            }
        }
    }
}
