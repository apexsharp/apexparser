@isTest
public class ClassRestTest {
    @isTest
    public static void PostTest() {

        RestContext.Request = new RestRequest();
        RestContext.Response = new RestResponse();

        ClassRest.ContactDTO contact = new ClassRest.ContactDTO();
        contact.LastName = 'LastName';

        RestContext.Request.RequestBody = Blob.ValueOf(JSON.Serialize(contact));
        ClassRest.Post();

        System.AssertEquals(200, RestContext.Response.StatusCode);

        List<Contact> contacts = [SELECT Id FROM Contact WHERE LastName = 'LastName'];
        System.AssertEquals(1, contacts.Size());
    }
}