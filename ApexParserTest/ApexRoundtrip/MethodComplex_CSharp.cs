namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.System;
    using SObjects;

    [WithSharing]
    public class MethodComplex
    {
        public static void MethodOne()
        {
            foreach (Account a in Soql.query<Account>(@"SELECT Id FROM Account"))
            {
                System.Debug(a.Id);
            }

            for (int i = 0; i<10; i++)
            {
            }
        }

        public Database.QueryLocator QueryLocator(Database.BatchableContext bc)
        {
            return Database.GetQueryLocator(Soql.query<Contact>(@"SELECT Id FROM Contact"));
        }
    }
}
