namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;

    [WithSharing]
    public class SoqlDemo
    {
        /**
         * A simple CRUD Example
         */
        public static void CrudExample()
        {
            Contact contactNew = new Contact { LastName = "Jay", EMail = "abc@abc.com" };
            Soql.Insert(contactNew);
            System.debug(contactNew.Id);
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                System.debug(c.Email);
                c.Email = "new@new.com";
            }

            Soql.Update(contacts);
            contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                System.debug(c.Email);
            }

            Soql.Delete(contacts);
            contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            if (contacts.isEmpty())
            {
                System.debug("Delete Worked");
            }
        }
    }
}
