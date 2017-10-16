namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    /**
    * A simple CRUD Example
    */
    public static class SoqlDemo
    {
        public static void CrudExample()
        {
            Contact contactNew = new Contact() { LastName = "Jay", Email = "abc@abc.com" };
            SOQL.Insert(contactNew);

            System.Debug(contactNew.Id);

            List<Contact> contacts = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew.Id });

            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
                contact.Email = "new@new.com";
            }
            SOQL.Update(contacts);


            contacts = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew.Id });
            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
            }
            SOQL.Delete(contacts);

            contacts = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew.Id });
            if (contacts.IsEmpty())
            {
                System.Debug("Del Worked");
            }
        }
    }
}

