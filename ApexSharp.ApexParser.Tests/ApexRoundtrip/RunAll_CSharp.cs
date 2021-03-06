namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.ApexSharp.NUnit;
    using Apex.System;
    using SObjects;

    [TestFixture]
    [WithSharing]
    public class RunAll
    {
        [Test]
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
            System.Debug(new ClassUnitTestSeeAllData());
            System.Debug(new ClassWithOutSharing());
            System.Debug(new ClassWithSharing());
        }
    }
}
