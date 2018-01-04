namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using SObjects;

    public class ClassNoApex
    {
        // Any classes in NoApex name space will be commented out in Apex and uncommented on c#.
        public static void MethodOne()
        {
            NoApex.Serilog.LogInfo("Jay");
        }

        public static List<Contact> getContacts()
        {
            List<Contact> contacts = Soql.query<Contact>(@"SELECT Id, Email, Phone FROM Contact");

            NoApex.Serilog.LogInfo(contacts.size().ToString());
            return contacts;
        }

        public static void callingNonApexCode()
        {
            NoApex.Serilog.LogInfo("Hi");
        }

        // Any method in NoApex name space will be commented out in Apex and uncommented on c#.
        public static void NoApexMethodTwo()
        {
            NoApex.Serilog.LogInfo("Jay");
        }
    }
}
