namespace ApexParserTest.CSharpClasses
{
    using Apex.ApexAttributes;
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
            Contact newContact = (Contact)JSON.Deserialize(jsonString, typeof(string));
        }
    }
}
