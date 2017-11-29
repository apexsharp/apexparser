namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;

    public class ClassInterface : IClassInterface
    {
        public ID GetId()
        {
            return "";
        }

        public string GetName()
        {
            return "Jay";
        }
    }
}
