using ApexSharpBase;
using NUnit.Framework;

namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    using SObjects;

    [TestFixture]
    public class DemoTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var apexSharp = new ApexSharp();
            apexSharp.Connect("C:\\DevSharp\\connect.json");
        }


     //   [Test]
        public void GetAllContactsTest()
        {
            Demo demo = new Demo();
            List<Contact> contacts = demo.GetAllContacts();
            Assert.IsTrue(contacts.Count > 0);
        }
    }
}
