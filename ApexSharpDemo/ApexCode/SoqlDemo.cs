namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexAttrbutes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    [WithOutSharing]
    public abstract class SoqlDemo
    {
        public List<Contact> ContactList = Soql.Query<Contact>("SELECT Id, Email FROM Contact");


        public abstract void AbstractMethod();

        /**
         * A simple CRUD Example
         */
        public static void CrudExample()
        {
            Contact contactNew = new Contact() { LastName = "Jay", Email = "abc@abc.com" };

            Soql.Insert(contactNew);
            System.Debug(contactNew.Id);
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                System.Debug(c.Email);
                c.Email = "new@new.com";
            }

            Soql.Update(contacts);
            contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                System.Debug(c.Email);
            }

            Soql.Delete(contacts);
            contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            if (contacts.IsEmpty())
            {
                System.Debug("Delete Worked");
            }
        }

        public static void OneVsListDemo()
        {
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact");
            List<Contact> contact = Soql.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 1");
        }

        public static void VariableScope(int x)
        {
            if (x == 5)
            {
                List<Contact> objectList;
                objectList = Soql.Query<Contact>("SELECT Id FROM Contact");
            }
            else
            {
                List<Contact> objectList;
                objectList = Soql.Query<Contact>("SELECT Id FROM Contact");
            }
        }
    }
}
