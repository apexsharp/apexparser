namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using ApexSharpApi.ApexApi;
    using SObjects;

    public class JsonExample
    {
        public void JsonExampleMethod()
        {
            Contact contact = new Contact();
            contact.LastName = "Jay";
            contact.Email = "jay@jay.com";
            string jsonString = JSON.Serialize(contact);
            Contact newContact = (Contact)JSON.Deserialize(jsonString, typeof(string));
        }
    }
}
