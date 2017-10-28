using System;
using ApexSharpBase;

namespace ApexSharpDemo
{
    public class SimpleDemo
    {
        public static void Main(string[] args)
        {
            var apexSharp = new ApexSharp();

            // Setup connection info
            apexSharp.SalesForceUrl("https://login.salesforce.com")
               .AndHttpProxy("http://yourproxy.com")
               .UseSalesForceApiVersion(40)
               .WithUserId("SalesForce User Id")
               .AndPassword("SalesForce Password")
               .AndToken("SalesForce Token")
               .SetApexFileLocation("Location Where you want your APEX Files to be saved")
               .SetLogLevel(LogLevel.Info)
               .SaveApexSharpConfig("Location")
               .Connect(); // Always Initialize your settings before using it.

            // Create a local C# for Contact object in SF
            apexSharp.CreateOfflineClasses("Contact");

            // SoqlDemo is a CRUD C# code that we will convert APEX. This can be executed now.
            // Take a look at the SoqlDemo.cs file in the /ApexCode Folder.  
            ApexCode.SoqlDemo.CrudExample();

            //Convert the SoqlDemo.cs File to APEX and save it.
            apexSharp.ConvertToApex("SoqlDemo", overWrite: true);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}