namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using NUnit.Framework;

    [TestFixture]
    public class DemoTest
    {

        [SetUp]
        public static void Setup()
        {
            NoApex.SalesForceLogIn.Login();

            Contact contactNew = new Contact();
            contactNew.LastName = "Jay";
            contactNew.Email = "jay@jay.com";
            Soql.Insert(contactNew);
        }

        [Test]
        public static void UpdatePhoneTestValidEmail()
        {
            Demo.UpdatePhone("jay@jay.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email = 'jay@jay.com'");
            Assert.AreEqual(contacts[0].Phone, "555-1212");
        }

        [Test]
        public static void UpdatePhoneTestNotValidEmail()
        {
            Demo.UpdatePhone("nojay@jay.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email = 'nojay@jay.com'");
            Assert.AreEqual(contacts.Size(), 0);
        }
    }
}
