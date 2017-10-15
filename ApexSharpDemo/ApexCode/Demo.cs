namespace ApexSharpDemo.ApexCode
{
    using SObjects;
    using Apex.System;
    using Apex.ApexSharp;
    using Apex.ApexAttrbutes;
    using SalesForceAPI.Apex;

    public class Demo
    {
        public static void RunContactDemo()
        {
            Contact contactNew = new Contact();
            contactNew.LastName = "Jay";
            contactNew.Email = "abc@abc.com";

            SOQL.Insert(contactNew);

            Id contactNewId = contactNew.Id;

            List<Contact> contacts = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });

            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
                contact.Email = "new@new.com";
            }

            SOQL.Update(contacts);


            contacts = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });
            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
            }

            SOQL.Delete(contacts);

            contacts = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });
            if (contacts.IsEmpty())
            {
                System.Debug("Del Worked");
            }
        }
    }
}