namespace PrivateDemo.CSharpClasses
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;
    //  using Apex.NUnit;
    using Apex.ApexSharp.NUnit;

    [TestFixture]
    public class DemoTest
    {
        [SetUp]
        public static void Setup()
        {
            Contact contactNew = new Contact();
            contactNew.LastName = "Jay";
            contactNew.Email = "jay@jay.com";
            Soql.Insert(contactNew);
        }

        [Test]
        public static void UpdatePhoneTestValidEmail()
        {
            Demo.UpdatePhone("jay@jay.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT ID, Email, Phone FROM Contact WHERE Email = 'jay@jay.com'");
            System.AssertEquals(contacts[0].Phone, "555-1212");
        }

        [Test]
        public static void UpdatePhoneTestNotValidEmail()
        {
            Demo.UpdatePhone("nojay@jay.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT ID, Email, Phone FROM Contact WHERE Email = 'nojay@jay.com'");
            System.AssertEquals(contacts.Size(), 0);
        }
    }
}
