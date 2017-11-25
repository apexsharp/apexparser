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
            Contact contactNew = new Contact() { LastName = "Jay", Email = "jay@jay.com" };
            Id newId = SoqlApi.Insert(contactNew).Id;
            Console.WriteLine(newId);

            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :contactNew.Email && LastName = :contactNew.LastName LIMIT 1", contactNew.Email, contactNew.LastName);
            Console.WriteLine(contacts[0].Id);


            Contact contact = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :contactNew.Email && LastName = :contactNew.LastName LIMIT 1", contactNew.Email, contactNew.LastName);
            Console.WriteLine(contact.Id);
        }
    }
}
