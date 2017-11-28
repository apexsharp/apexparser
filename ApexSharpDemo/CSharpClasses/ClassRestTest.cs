namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using NUnit.Framework;

    [TestFixture]
    public class ClassRestTest
    {
        [Test]
        public static void InsertContactTest()
        {
            ClassRest.ContactDTO contact = new ClassRest.ContactDTO();
            contact.LastName = "LastName";

            //ClassRest.InsertContact(contact);
            // List<Contact> contacts = [SELECT Id FROM Contact WHERE LastName = 'LastName'];
            //  System.AssertEquals(1, contacts.size());
        }

        [Test]
        public static void PostTest()
        {
            RestContext.request = new RestRequest();
            RestContext.response = new RestResponse();
            ClassRest.ContactDTO contact = new ClassRest.ContactDTO();
            contact.LastName = "LastName";
            RestContext.request.requestBody = Blob.valueOf(JSON.serialize(contact));
            ClassRest.Post();
            System.AssertEquals(200, RestContext.response.statusCode);
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id FROM Contact WHERE LastName = 'LastName'");
            System.AssertEquals(1, contacts.size());
        }
    }
}
