namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class Demo
    {
        public List<Contact> GetAllContacts()
        {
            List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Name, Level__c FROM Contact");
            return contacts;
        }
    }
}
