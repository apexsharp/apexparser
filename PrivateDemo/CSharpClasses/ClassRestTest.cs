namespace PrivateDemo.CSharpClasses
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;
    //  using Apex.NUnit;
    using Apex.ApexSharp.NUnit;

    [TestFixture]
    public class ClassRestTest
    {
        [Test]
        public static void PostTest()
        {
            RestContext.Request = new RestRequest();
            RestContext.Response = new RestResponse();
            ClassRest.ContactDTO contact = new ClassRest.ContactDTO();
            contact.LastName = "LastName";
            RestContext.Request.RequestBody = Blob.ValueOf(JSON.Serialize(contact));
            ClassRest.Post();
            System.AssertEquals(200, RestContext.Response.StatusCode);
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id FROM Contact WHERE LastName = 'LastName'");
            System.AssertEquals(1, contacts.Size());
        }
    }
}
