namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using NUnit.Framework;

    [TestFixture, Ignore("Until we find how to store connect.json secure")]
    public class DemoTest
    {
        [OneTimeSetUp]
        public void NoApexSetup()
        {
            new ApexSharpBase.ApexSharp().Connect("C:\\DevSharp\\connect.json");
        }

        [SetUp]
        static void Setup()
        {
            Contact contactNew = new Contact();
            contactNew.LastName = "Jay";
            contactNew.Email = "jay@jay.com";
            Soql.Insert(contactNew);
        }

        [Test]
        static void UpdatePhoneTestValidEmail()
        {
            Demo.UpdatePhone("jay@jay.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email = 'jay@jay.com'");
            System.AssertEquals(contacts[0].Phone, "555-1212");
        }

        [Test]
        static void UpdatePhoneTestNotValidEmail()
        {
            Demo.UpdatePhone("nojay@jay.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email = 'nojay@jay.com'");
            System.AssertEquals(contacts.Size(), 0);
        }
    }
}
