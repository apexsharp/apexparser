using SalesForceAPI.Apex;

namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class ClassInterface : IClassInterface
    {
        public Id GetId()
        {
            return "";
        }

        public string GetName()
        {
            return "Jay";
        }
    }
}
