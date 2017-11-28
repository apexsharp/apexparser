@RestResource(urlMapping='/api/RestDemo')
global class ClassRest
{
    public class ContactDTO {
        public String LastName {get;set;}
    }

    @httpPost
    global static void Post() {

        try {
            ContactDTO contact = (ContactDTO) JSON.Deserialize(RestContext.Request.RequestBody.ToString(), ContactDTO.class);
            InsertContact(contact);
            RestContext.Response.StatusCode = 200;
        } catch (Exception e) {
            RestContext.Response.StatusCode = 500;
        }
    }

    public static void InsertContact(ContactDTO contactToSave) {
        Contact contact = new Contact();
        contact.LastName = contactToSave.LastName;
        insert contact;
    }

    @httpGet
    global static string Get() {
        return 'Jay';
    }

    @httpPatch
    global static void Patch() {
    }

    @httpPut
    global static void Put() {
    }

    @httpDelete
    global static void DoDelete() {
    }

}