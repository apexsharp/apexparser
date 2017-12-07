@RestResource(urlMapping='/api/RestDemo')
global class ClassRest
{
    public class ContactDTO
    {
        public String LastName { get; set; }
    }

    @HttpPost
    global static void Post()
    {
        try
        {
            ContactDTO contact = (ContactDTO)JSON.Deserialize(RestContext.Request.RequestBody.ToString(), ContactDTO.class);
            InsertContact(contact);
            RestContext.Response.StatusCode = 200;
        }
        catch (Exception e)
        {
            RestContext.Response.StatusCode = 500;
        }
    }

    public static void InsertContact(ContactDTO contactToSave)
    {
        Contact contact = new Contact();
        contact.LastName = contactToSave.LastName;
        insert contact;
    }

    @HttpGet
    global static String Get()
    {
        return 'Jay';
    }

    @HttpPatch
    global static void Patch()
    {
    }

    @HttpPut
    global static void Put()
    {
    }

    @HttpDelete
    global static void DoDelete()
    {
    }
}
