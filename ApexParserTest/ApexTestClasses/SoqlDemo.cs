namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;

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

            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew });

            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
                contact.Email = "new@new.com";
            }
            Soql.Update(contacts);


            contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew });
            foreach (Contact contact in contacts)
            {
                System.Debug(contact.Email);
            }
            Soql.Delete(contacts);

            contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNew.Id LIMIT 1", new { contactNew });
            if (contacts.IsEmpty())
            {
                System.Debug("Del Worked");
            }
        }
    }
}
