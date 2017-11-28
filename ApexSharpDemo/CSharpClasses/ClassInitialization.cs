namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
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

        public static Map<string, string> colorMap = new Map<string, string>();

        static ClassInitialization()
        {
            colorMap.put("red", "255, 0, 0");
            colorMap.put("cyan", "0, 255, 255");
            colorMap.put("magenta", "255, 0, 255");
        }
    }
}
