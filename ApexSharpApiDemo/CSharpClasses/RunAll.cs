namespace ApexSharpApiDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using ApexSharpApi.ApexApi;
    using Apex.NUnit;

    [WithSharing]
    public class RunAll
    {

        public static void TestClassess()
        {
            System.Debug(ClassEnum.America);
            System.Debug(new ClassException());
            System.Debug(new ClassGlobal());
            ClassInitialization newClassInitialization = new ClassInitialization();
            System.Debug(ClassInitialization.colorMap);
            System.Debug(newClassInitialization.contactList);
            ClassInterface classInterface = new ClassInterface();
            System.Debug(classInterface.GetName());
            System.Debug(new ClassInternal.InternalClassOne());
            System.Debug(new ClassInternal.InternalClassTwo());
            System.Debug(ClassNoApex.MethodOne());
            System.Debug(new ClassUnitTestSeeAllData());
            System.Debug(new ClassWithOutSharing());
            System.Debug(new ClassWithSharing());
        }
    }
}
