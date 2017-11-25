using System;
using System.Collections.Generic;
using NUnit.Framework;
using SalesForceAPI.Apex;

namespace SalesForceAPI.UnitTest
{
    [TestFixture]
    public class SoqlApiTest
    {
        [Test]
        public void CrudTest()
        {

            Contact contactNew = new Contact() { LastName = "Jay", Email = "jay@jayjayjay.com" };
            Id newId = SoqlApi.Insert(contactNew).Id;

            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id=:ontactNew.Id", contactNew.Id);
            Assert.AreEqual(newId, contacts[0].Id);

            Contact contact = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id=:ontactNew.Id LIMIT 1", contactNew.Id);
            Assert.AreEqual(newId, contact.Id);

            contact = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact LIMIT 1");
            Console.WriteLine(contact);




        }
    }
}
