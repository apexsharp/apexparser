namespace ApexParserTest.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public interface IClassInterface : IClassInterfaceExt
    {
        string GetName();
    }
}
