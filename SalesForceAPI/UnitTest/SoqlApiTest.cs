using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SalesForceAPI.ApexApi;

namespace SalesForceAPI.UnitTest
{
    [TestFixture]
    public class SoqlApiTest
    {
        [Test]
        public void CrudTest()
        {
            Contact contactNew = new Contact() { LastName = "Jay", Email = "jay@jay.com" };
            ID newId = SoqlApi.Insert(contactNew).Id;

            // Did we get the 18 Digit SF ID
            Regex regex = new Regex(@"[a-zA-Z0-9]{18}");
            var match = regex.Match(newId.ToString());
            Assert.IsTrue(match.Success);

            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT ID, Email, Name FROM Contact WHERE ID=:ontactNew.Id", contactNew.Id);
            Assert.AreEqual(newId, contacts[0].Id);

            Contact contact = SoqlApi.Query<Contact>("SELECT ID, Email, Name FROM Contact WHERE ID=:ontactNew.ID LIMIT 1", contactNew.Id);
            Assert.AreEqual(newId, contact.Id);

            contact = SoqlApi.Query<Contact>("SELECT ID, Email, Name FROM Contact LIMIT 1");
        }
    }
}
