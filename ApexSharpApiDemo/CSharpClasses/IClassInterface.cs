namespace ApexSharpApiDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using ApexSharpApi.ApexApi;

    public interface IClassInterface : IClassInterfaceExt
    {
        string GetName();
    }
}
