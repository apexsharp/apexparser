namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using SalesForceAPI.Apex;
    using Apex.System;
    using SObjects;

    public class Collection
    {
        public static void MapDemo()
        {
            //Map<Id, Contact> m = new Map<Id, Contact>([SELECT Id, Name FROM Contact LIMIT 10]);
            Map<Id, Contact> m = new Map<Id, Contact>(Soql.Query<Contact>("SELECT Id FROM Jay__c"));

            foreach (Id idKey in m.keySet())
            {
                Contact contact = m.get(idKey);
            }

        }
    }
}
