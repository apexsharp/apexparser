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
        public static void PostTest()
        {
            RestContext.request = new RestRequest();
            RestContext.response = new RestResponse();
            ClassRest.ContactDTO contact = new ClassRest.ContactDTO();
            contact.LastName = "LastName";
            RestContext.Request.RequestBody = Blob.ValueOf(JSON.serialize(contact));
            ClassRest.Post();
            System.AssertEquals(200, RestContext.Response.StatusCode);
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id FROM Contact WHERE LastName = 'LastName'");
            System.AssertEquals(1, contacts.size());
        }
    }
}
