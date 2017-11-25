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
            // current assembly location is represented by the "{0}" macro
            SalesForceAPI.ConnectionUtil.Session = new SalesForceAPI.ApexSharp().SalesForceUrl("https://login.salesforce.com/")
                .AndSalesForceApiVersion(40)
                .WithUserId("apexsharp@jayonsoftware.com")
                .AndPassword("1v0EGMfR0NTkbmyQ2Jk4082PA")
                .AndToken("LUTAPwQstOZj9ESx7ghiLB1Ww")
                .CacheLocation(@"{0}\..\..\..\PrivateDemo\")
                .SaveConfigAt(@"{0}\..\..\..\PrivateDemo\config.json")
                .CreateSession();

            Contact contactNew = new Contact();
            contactNew.LastName = "Jay";
            contactNew.Email = "jay@jay12.com";
            Soql.Insert(contactNew);
        }

        [Test]
        public static void UpdatePhoneTestValidEmail()
        {
            Demo.UpdatePhone("jay@jay12.com", "555-1212");
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email = 'jay@jay12.com'");
            Assert.True(contacts.Size() > 0);
            Assert.AreEqual("555-1212", contacts[0].Phone);
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
