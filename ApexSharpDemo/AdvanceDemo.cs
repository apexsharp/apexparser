using System;
using Apex.ApexSharp;
using ApexSharpDemo.ApexCode;

namespace ApexSharpDemo
{
    public class AdvanceDemo
    {
        public static void StartDemo()
        {
            // If you had the settings saved in advance.
            ApexSharp apexSharp = new ApexSharp().LoadApexSharpConfig("setup.json");

            // Always Initialize your settings before using it.
            if (apexSharp.Init())
            {
                //apexSharp.CreateOfflineClasses("Contact");
                //ApexCode.Demo.RunContactDemo();
                //apexSharp.ConvertToApexAndAddToProject("Demo", overWrite: true);
                //apexSharp.ConvertToCSharpAndAddToProject("Demo", overWrite: true);

                apexSharp.ConvertToApexAndAddToProject("UnitTest", overWrite: true);
            }
            else
            {
                Console.WriteLine(String.Join("\n", apexSharp.GetErrorMessage()));
            }
        }
    }
}