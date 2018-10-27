namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.ApexSharp.NUnit;
    using Apex.System;
    using SObjects;

    [TestFixture]
    public class DemoControllerTests
    {
        [Test]
        static void testConstructor()
        {
            //  	Contact contact = new Contact(LastName='Smith');
            //  	insert contact;
            //  	Test.setCurrentPage(Page.DemoPage);
            //	ApexPages.currentPage().getParameters().put('lastName', 'Smith');
            //  	DemoController demo = new DemoController(new ApexPages.StandardController(contact));
            //	System.assertEquals(contact.Id, ApexPages.currentPage().getParameters().get('id'));
        }

        [Test]
        static void testGetAppVersion()
        {
            DemoController demo = new DemoController();

            //  System.assertEquals(demo.getAppVersion(), '1.0.0');
        }
    }
}
