using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apex.ApexSharp;
using ApexSharpDemo.ApexCode;

namespace ApexSharpDemo
{
    public class SimpleDemoLocal
    {
        public static void Main(string[] args)
        {
            var apexSharp = new ApexSharp().LoadApexSharpConfig("setup.json");

            // Always Initialize your settings before using it.
            if (apexSharp.Init())
            {
                // Create a local C# for Contact object in SF
                //  apexSharp.CreateOfflineClasses("Contact");

                // Demo.cs is a simple C# code that we will convert APEX. This can be executed now.
                // Take a look at the Demo.cs file in the /ApexCode Folder.  
                Demo.GetContacts();

                //Convert the Demo.cs File to APEX
                //apexSharp.ConvertToApexAndAddToProject("Demo", overWrite: true);
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
