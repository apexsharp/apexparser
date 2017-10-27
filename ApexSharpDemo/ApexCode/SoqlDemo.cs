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
            Soql.Insert(contactNew);

            System.Debug(contactNew.Id);

            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew.Id });

            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
                contact.Email = "new@new.com";
            }
            Soql.Update(contacts);


            contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew.Id });
            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
            }
            Soql.Delete(contacts);

            contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew.Id });
            if (contacts.IsEmpty())
            {
                System.Debug("Del Worked");
            }
        }
    }
}

