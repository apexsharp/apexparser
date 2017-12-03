namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;

    public class ClassInterface : IClassInterface
    {
        public string GetName(string name)
        {
            return name;
        }

        public string GetName()
        {
            return "Jay";
        }
    }
}
