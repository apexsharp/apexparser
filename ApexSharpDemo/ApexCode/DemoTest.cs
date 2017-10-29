using NUnit.Framework;

namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    using SObjects;

    [TestFixture]
    public class DemoTest
    {
       // [Test]
        public void GetAllContactsTest()
        {
            Demo demo = new Demo();
            List<Contact> contacts = demo.GetAllContacts();
        }
    }
}
