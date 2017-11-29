namespace ApexSharpDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using SalesForceAPI.ApexApi;

    public interface IClassInterface : IClassInterfaceExt
    {
        string GetName();
    }
}
