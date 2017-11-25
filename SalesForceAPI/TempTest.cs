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
            ConnectionUtil.Session = ConnectionUtil.LoadSession(@"C:\DevSharp\ApexSharp\PrivateDemo\config.json");
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
