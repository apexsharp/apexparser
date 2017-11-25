using System;
using System.Collections.Generic;
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
            ConnectionUtil.Session = new ApexSharp().SalesForceUrl("https://login.salesforce.com/")
                .AndSalesForceApiVersion(40)
                .WithUserId("apexsharp@jayonsoftware.com")
                .AndPassword("1v0EGMfR0NTkbmyQ2Jk4082PA")
                .AndToken("LUTAPwQstOZj9ESx7ghiLB1Ww")
                .CacheLocation(@"C:\DevSharp\ApexSharp\PrivateDemo\")
                .SaveConfigAt(@"C:\DevSharp\ApexSharp\PrivateDemo\config.json")
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
    }
}
