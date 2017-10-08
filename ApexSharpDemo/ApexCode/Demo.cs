

namespace ApexSharpDemo.ApexCode
{
    using SObjects;
    using Apex.System;
    using Apex.ApexSharp;


    public class Demo
    {
        public static void RunContactDemo()
        {
            Contact contactNew = new Contact();
            contactNew.LastName = "Jay";
            contactNew.Email = "abc@abc.com";

            Soql.Insert(contactNew);

            SalesForceAPI.Apex.Id contactNewId = contactNew.Id;

            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });
            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
                contact.Email = "new@new.com";
            }

            Soql.Update(contacts);


            contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });
            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
            }

            Soql.Delete(contacts);

            contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });
            if (contacts.IsEmpty())
            {
                System.Debug("Del Worked");
            }
        }
    }
}