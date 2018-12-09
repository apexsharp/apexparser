namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.System;
    using SObjects;

    [WithSharing]
    public class SoqlDemo
    {
        /**
         * A simple CRUD Example
         */
        public static void CrudExample()
        {
            Contact contactNew = new Contact { LastName = "Jay", EMail = "abc@abc.com" };
            Soql.insert(contactNew);
            System.debug(contactNew.Id);
            List<Contact> contacts = Soql.query<Contact>(@"SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                System.debug(c.Email);
                c.Email = "new@new.com";
            }

            Soql.update(contacts);
            contacts = Soql.query<Contact>(@"SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                System.debug(c.Email);
            }

            Soql.delete(contacts);
            contacts = Soql.query<Contact>(@"SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            if (contacts.isEmpty())
            {
                System.debug("Delete Worked");
            }
        }
    }
}
