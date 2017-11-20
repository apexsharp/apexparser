using System;
using System.IO;
using SalesForceAPI;
using Serilog;

namespace ApexSharpDemo
{
    public class SimpleDemo
    {
        public static void Main(string[] args)
        {
            // Setup logging. We use Serilog. 
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.ColoredConsole()
            .CreateLogger();

            var apexSharp = new ApexSharp();

            // Setup connection info
            apexSharp.SalesForceUrl("https://login.salesforce.com")
               .AddHttpProxy("http://yourproxy.com")
               .AndSalesForceApiVersion(40)
               .WithUserId("SalesForce User Id")
               .AndPassword("SalesForce Password")
               .AndToken("SalesForce Token")
               .SaveApexSharpConfigAs("Configuration Name")
               .Connect(); // Always Initialize your settings before using it.

            // Create a local C# for Contact object in SF
            apexSharp.CreateOfflineClasses("Contact");


            //Convert the SoqlDemo.cs File to APEX and save it.
            var cSharpCode = File.ReadAllText(@"\ApexSharp\ApexSharpDemo\ApexCode\SoqlDemo.cs");
            var apexClasses = ApexParser.CSharpHelper.ToApex(cSharpCode);
            File.WriteAllText(@"\SalesForceApexSharp\src\classes\SoqlDemo.cls", apexClasses[0]);

            // SoqlDemo is a CRUD C# code that we will convert APEX. This can be executed now.
            // Take a look at the SoqlDemo.cs file in the /ApexCode Folder.  
            ApexCode.SoqlDemo.CrudExample();

            // Convert the C# File to APEX
            var apexFile = File.ReadAllText(@"\SalesForceApexSharp\src\classes\SoqlDemo.cls");
            var cSharpFile = ApexParser.ApexParser.ConvertApexToCSharp(apexFile, "ApexSharpDemo.ApexCode");
            File.WriteAllText(@"\ApexSharp\ApexSharpDemo\ApexCode\SoqlDemo.cs", cSharpFile);


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}