using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

            // Did we get the 18 Digit SF Id
            Regex regex = new Regex(@"[a-zA-Z0-9]{18}");
            var match = regex.Match(newId.ToString());
            Assert.IsTrue(match.Success);

            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id=:ontactNew.Id", contactNew.Id);
            Assert.AreEqual(newId, contacts[0].Id);

            Contact contact = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id=:ontactNew.Id LIMIT 1", contactNew.Id);
            Assert.AreEqual(newId, contact.Id);

            contact = SoqlApi.Query<Contact>("SELECT Id, Email, Name FROM Contact LIMIT 1");
        }
    }
}
