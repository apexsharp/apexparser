namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class ClassInitialization
    {
        public List<Contact> contactList;

        public ClassInitialization()
        {
            contactList = Soql.Query<Contact>("SELECT Id FROM Contact LIMIT 1");
        }
    }
}
