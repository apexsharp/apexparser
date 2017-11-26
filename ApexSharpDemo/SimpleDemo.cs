using System;
using System.IO;
using SalesForceAPI;

namespace ApexSharpDemo
{
    public class SimpleDemo
    {
        public static void Main(string[] args)
        {
            // Always Initialize your settings before using it.
            Setup.Init();

            // SoqlDemo is a CRUD C# code that we will convert APEX. This can be executed now.
            // Take a look at the SoqlDemo.cs file in the /ApexCode Folder.  
            ApexCode.SoqlDemo.CrudExample();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void CreateOffLineClasses()
        {
            // Create a local C# for Contact object in SF
            ApexSharp.CreateOfflineClasses("Contact");
        }

        public static void ConvertToApex()
        {
            // Convert the C# File to APEX
            var apexFile = File.ReadAllText(@"\SalesForceApexSharp\src\classes\SoqlDemo.cls");
            var cSharpFile = ApexParser.ApexParser.ConvertApexToCSharp(apexFile, "ApexSharpDemo.ApexCode");
            File.WriteAllText(@"\ApexSharp\ApexSharpDemo\ApexCode\SoqlDemo.cs", cSharpFile);
        }


        public static void ConvertToCSharp()
        {
            //Convert the SoqlDemo.cs File to APEX and save it.
            var cSharpCode = File.ReadAllText(@"\ApexSharp\ApexSharpDemo\ApexCode\SoqlDemo.cs");
            var apexClasses = ApexParser.CSharpHelper.ToApex(cSharpCode);
            File.WriteAllText(@"\SalesForceApexSharp\src\classes\SoqlDemo.cls", apexClasses[0]);
        }
    }
}