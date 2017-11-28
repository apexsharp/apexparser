namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class SoqlDemo
    {
        /**
         * A simple CRUD Example
         */
        public static void CrudExample()
        {
            Contact contactNew = new Contact(LastName = "Jay", Email = "abc@abc.com");
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
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 5");
            List<Contact> contact = Soql.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 1");
        }

        public static void VariableScope(int x)
        {
            if (x == 5)
            {
                List<Contact> objectList;
                objectList = Soql.Query<Contact>("SELECT Id FROM Contact LIMIT 5");
            }
            else
            {
                List<Contact> objectList;
                objectList = Soql.Query<Contact>("SELECT Id FROM Contact LIMIT 5");
            }
        }

        public static void InClauseTest()
        {
            Contact[] contactList = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email IN ('rose@edge.com', 'sean@edge.com')");
            string[] emails = new string[]{"rose@edge.com", "sean@edge.com"};
            Contact[] contactListThree = Soql.Query<Contact>("SELECT Id, Email, Phone FROM Contact WHERE Email IN :emails", emails);
            Contact[] contactListOne = Soql.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 2");
            Contact[] contactListTwo = Soql.Query<Contact>("SELECT Id FROM Contact WHERE Id IN :contactListOne", contactListOne);
        }
    }
}
