using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SalesForceAPI.Apex;

namespace SalesForceAPI
{
    [TestFixture]
    public class TempTest
    {
        [SetUp]
        public void Init()
        {
            // Note: paths can be absolute or relative to the current assembly.
            // Project location is specified in the project settings "Build" tab.
            // Current assembly location is represented by the "{0}" macro.
            ConnectionUtil.Session = new ApexSharp().SalesForceUrl("https://login.salesforce.com/")
                .AndSalesForceApiVersion(40)
                .WithUserId("apexsharp@jayonsoftware.com")
                .AndPassword("1v0EGMfR0NTkbmyQ2Jk4082PA")
                .AndToken("LUTAPwQstOZj9ESx7ghiLB1Ww")
                .CacheLocation(@"{0}\..\..\..\PrivateDemo\")
                .SaveConfigAt(@"{0}\..\..\..\PrivateDemo\config.json")
                .CreateSession();
        }

        [Test]
        public void CrudTest()
        {
            Contact contactNew = new Contact() { LastName = "Jay", Email = "jay@jay.com" };
            Id newId = SoqlApi.Insert(contactNew).Id;
            Console.WriteLine(newId);

            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :contactNew.Email && LastName = :contactNew.LastName LIMIT 1", contactNew.Email, contactNew.LastName);
            Console.WriteLine(contacts.Count);

            foreach (var contact in contacts)
            {
                SoqlApi.Delete(contact);
            }

            contacts = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :contactNew.Email && LastName = :contactNew.LastName LIMIT 1", contactNew.Email, contactNew.LastName);
            Console.WriteLine(contacts.Count);
        }

        [Test]
        public void PolymorphicQueryResultDemo()
        {
            // create new contact
            var contactNew = new Contact() { LastName = "Jay", Email = "jay@jay.com" };
            var newId = SoqlApi.Insert(contactNew).Id;
            Console.WriteLine(newId);

            // multiple records
            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :email", "jay@jay.com");
            Assert.AreEqual(1, contacts.Count);
            Console.WriteLine("Count = {0}", contacts.Count);

            // single record
            Contact contact = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :email", "jay@jay.com");
            Assert.True(contact != null);
            Console.WriteLine(contact.Email);

            // query text
            string queryText = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :email", "jay@jay.com");
            Assert.False(string.IsNullOrWhiteSpace(queryText));
            Console.WriteLine("SOQL query text: {0}", queryText);

            // delete record
            SoqlApi.Delete(contact);
        }
    }
}
