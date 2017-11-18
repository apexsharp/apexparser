namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class JsonExample
    {
        public void JsonExampleMethod()
        {
            Contact contact = new Contact();
            contact.LastName = "Jay";
            contact.Email = "jay@jay.com";

            string jsonString = JSON.Serialize(contact);

            Contact newContact = JSON.Deserialize<Contact>(jsonString);
        }
    }
}
