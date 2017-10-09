using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharpBase.Converter.CSharp;
using NUnit.Framework;

namespace ApexSharpBaseTest
{
    [TestFixture]
    public class CSharpLineConveterTest
    {
        [Test]
        public void SoqlLineTest()
        {
            var reply = CSharpLineConverter.GetApexLine(@"List<Contact> contacts = Soql.Query<Contact>(""SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1"", new { contactNewId })");
            Assert.AreEqual("List<Contact> contacts = [SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1]", reply);
        }

        [Test]
        public void SoqlUpdateTest()
        {
            var reply = CSharpLineConverter.GetApexLine(@"Soql.Update(accountList)");
            Assert.AreEqual(@"update accountList", reply);
        }

        [Test]
        public void SoqlUpsert()
        {
            var reply = CSharpLineConverter.GetApexLine("Soql.Upsert(accountList)");
            Assert.AreEqual(@"upsert accountList", reply);
        }

        [Test]
        public void SoqlInsertTest()
        {
            var reply = CSharpLineConverter.GetApexLine("Soql.Insert(accountList)");
            Assert.AreEqual("insert accountList", reply);
        }

        [Test]
        public void SoqlDeleteTest()
        {
            var reply = CSharpLineConverter.GetApexLine(@"Soql.Delete(accountList)");
            Assert.AreEqual("delete accountList", reply);
        }

        [Test]
        public void SoqlUnDeleteTest()
        {
            var reply = CSharpLineConverter.GetApexLine(@"Soql.UnDelete(accountList);");
            Assert.AreEqual(@"undelete accountList", reply);
        }

        [Test]
        public void JsonDeSerializeTest()
        {
            var reply = CSharpLineConverter.GetApexLine(@"List<Account> accounts = JSON.deserialize<List<Account>>(objectToDeserialize)");
            Assert.AreEqual(@"List<Account> accounts = (List<Account>)JSON.deserialize(objectToDeserialize,List<Account>.class)", reply);
        }

        // string json = JSON.serialize(objectToDeserialize)
    }
}
