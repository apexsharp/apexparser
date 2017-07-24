namespace ApexSharpDemo.ApexCode
{
    using SObjects;
    using Apex.System;
    using Apex.ApexSharp.Api;

    public class Demo
    {
        public static void RunContactDemo()
        {
            string newEmail = "Jay@JayOnSoftware.Com";

            List<Contact> listOfContact = Soql.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 1");

            System.Debug(listOfContact[0].Email);

            listOfContact[0].Email = newEmail;

            Soql.Update(listOfContact[0]);

            List<Contact> listOfContactNew = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :newEmail LIMIT 1", new { newEmail });

            System.Debug(listOfContactNew[0].Email);
        }
    }
}