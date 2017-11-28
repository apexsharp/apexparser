namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    [WithSharing]
    public class MethodComplex
    {
        public static void MethodOne()
        {
            foreach (Account a in Soql.Query<Account>("SELECT Id FROM Account"))
            {
                System.Debug(a.Id);
            }

            for (int i = 0; i<10; i++)
            {
            }
        }

        public Database.QueryLocator QueryLocator(Database.BatchableContext bc)
        {
            return Database.getQueryLocator(Soql.Query<Contact>("SELECT Id FROM Contact"));
        }
    }
}
