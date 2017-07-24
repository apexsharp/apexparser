namespace ApexSharpDemo.ApexCode
{

    using Apex.ApexSharp;
    using Apex.ApexSharp.Api;
    using Apex.System;
    using ApexSharpDemo.SObjects;
    using NUnit.Framework;

    [TestFixture]
    public class SoqlAndDml
    {
        [OneTimeSetUp]
        public void Setup()
        {
            ApexSharp apexSharp = new ApexSharp().LoadApexSharpConfig("setup.json");
            apexSharp.Init();
        }

        public static void RunContactDemo()
        {
            string newEmail = "Jay@JayOnSoftware.Com";

            List<Contact> listOfContact = Soql.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 1");

            if (listOfContact.Size() == 1)
            {
                System.Debug(listOfContact[0].Email);

                listOfContact[0].Email = newEmail;

                Soql.Update(listOfContact[0]);

                listOfContact = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :newEmail LIMIT 1", new { newEmail });

                System.Debug(listOfContact[0].Email);
            }
            else
            {
                System.Debug("No Records Found in the Contact Object Matching " + newEmail);
            }
        }

        [Test]
        public void DeleteRecord()
        {
            string eMail = "jay@jayonsoftware.com";
            List<Contact> listOfContact =
                Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :eMail LIMIT 1", new { eMail });
            if (listOfContact.Size() == 1)
            {
                Apex.System.System.Debug(listOfContact[0].Email);

                Soql.Delete(listOfContact[0]);
            }
        }

        [Test]
        public void MasterChildLookup()
        {
            List<Contact> listOfContact = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact LIMIT 1");
            if (listOfContact.Size() == 1)
            {
                Apex.System.System.Debug(listOfContact[0].Email);
            }
        }

        [Test]
        public void PassingSoqlVariabls()
        {
            string eMail = "jay@jayonsoftware.com";
            List<Contact> listOfContact =
                Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :eMail LIMIT 1", new { eMail });
            if (listOfContact.Size() == 1)
            {
                Apex.System.System.Debug(listOfContact[0].Email);
            }
        }

        [Test]
        public void SoqlLookup()
        {
            Contact contact = new Contact();
            contact.LastName = "Jay";
            contact.Email = "jay@jayonsoftware.com";

            Soql.Insert(contact);


            List<Contact> listOfContact =
                Soql.Query<Contact>("SELECT Id, Email, LastName FROM Contact WHERE LastName = 'Jay'");

            Apex.System.System.Debug(listOfContact.Size());

            if (listOfContact.Size() == 1)
            {
                Apex.System.System.Debug(listOfContact[0].Email);
            }

            Assert.AreEqual(listOfContact[0].Email, contact.Email);
        }
    }
}