using System;
using Apex.ApexSharp;
using ApexSharpDemo.ApexCode;

namespace ApexSharpDemo
{
    public class SimpleDemo
    {
        public static void Main(string[] args)
        {
            ApexSharp apexSharp = new ApexSharp();

            // Setup connection info
            apexSharp.SalesForceUrl("https://login.salesforce.com")
               .AndHttpProxy("http://yourproxy.com")
               .UseSalesForceApiVersion(40)
               .WithUserId("SalesForce User Id")
               .AndPassword("SalesForce Password")
               .AndToken("SalesForce Token")
               .SetApexFileLocation("Location Where you want your APEX Files to be saved")
               .SetLogLevel(LogLevle.Info)
               .SaveApexSharpConfig("Save this setup under this file Name");

            // Always Initialize your settings before using it.
            if (apexSharp.Init())
            {
                // Create a local C# for Contact object in SF
                apexSharp.CreateOfflineClasses("Contact");

                // Demo.cs is a simple C# code that we will convert APEX. This can be executed now.
                // Take a look at the Demo.cs file in the /ApexCode Folder.  
                Demo.RunContactDemo();

                // Convert the Demo.cs File to APEX
                apexSharp.ConvertToApexAndAddToProject("Demo", overWrite: true);
            }
            else
            {
                // Printout any errors
                Console.WriteLine(String.Join("\n", apexSharp.GetErrorMessage()));
            }


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}