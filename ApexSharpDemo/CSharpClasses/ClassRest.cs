namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    [RestResourceurlMapping='/api/RestDemo']
    [Global]
    class ClassRest
    {
        public class ContactDTO
        {
            public string LastName { get; set; }
        }

        [HttpPost]
        [Global]
        static void Post()
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
        static string Get()
        {
            return "Jay";
        }

        [HttpPatch]
        [Global]
        static void Patch()
        {
        }

        [HttpPut]
        [Global]
        static void Put()
        {
        }

        [HttpDelete]
        [Global]
        static void DoDelete()
        {
        }
    }
}
