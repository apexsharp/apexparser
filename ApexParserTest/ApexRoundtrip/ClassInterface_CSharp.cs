namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using SObjects;

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
