using System;
using Apex.ApexSharp;

namespace ApexSharpDemo
{
    public class SimpleDemo
    {
        public static void StartDemo()
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

                // Run the method on the Demo object
                ApexCode.Demo.RunContactDemo();

                // Convert the Demo.cs File to APEX
                apexSharp.ConvertToApexAndAddToProject("Demo", overWrite: true);

                // Convert the Demo.cls APEX to C#
                apexSharp.ConvertToCSharpAndAddToProject("Demo", overWrite: true);
            }
            else
            {
                // Printout any errors
                Console.WriteLine(String.Join("\n", apexSharp.GetErrorMessage()));
            }
        }
    }
}