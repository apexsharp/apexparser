namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class MethodComplex
    {


        public Database.QueryLocator QueryLocator(Database.BatchableContext bc)
        {
            return Database.GetQueryLocator("SELECT Id FROM Contact");
        }
    }
}
