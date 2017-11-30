namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;

    [RestResource(urlMapping="/api/RestDemo")]
    [Global]
    public class ClassRest
    {
        public class ContactDTO
        {
            public string LastName { get; set; }
        }

        [HttpPost]
        [Global]
        public static void Post()
        {
            try
            {
                ContactDTO contact = (ContactDTO)JSON.Deserialize(RestContext.Request.RequestBody.ToString(), typeof(ContactDTO));
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
            Soql.Insert(contact);
        }

        [HttpGet]
        [Global]
        public static string Get()
        {
            return "Jay";
        }

        [HttpPatch]
        [Global]
        public static void Patch()
        {
        }

        [HttpPut]
        [Global]
        public static void Put()
        {
        }

        [HttpDelete]
        [Global]
        public static void DoDelete()
        {
        }
    }
}
