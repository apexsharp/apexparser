namespace Demo
{
    using System;
    using System.Collections.Generic;
    using ApexSharpApi;
    using ApexParser;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Always Initialize your settings when ever you are connecting to SF
            Setup.Init();

            CreateOffLineClasses();
            ApexSharpParser.ConvertToCSharp(@"C:\DevSharp\ApexSharp\SalesForce\src\classes", @"C:\DevSharp\ApexSharp\Demo\CSharpClasses\", "Demo.CSharpClasses");

            // CSharpClasses.RunAll.TestClassess();

            ApexSharpParser.ConvertToApex(@"C:\DevSharp\ApexSharp\Demo\CSharpClasses\", @"C:\DevSharp\ApexSharp\SalesForce\src\classes");
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        // Used to generate the offline C# classes, one C# for each SObject
        public static void CreateOffLineClasses()
        {
            try
            {
                ModelGen modelGen = new ModelGen();
                var allObjects = modelGen.GetAllObjectNames();

                List<string> onlyObjects = new List<string>
                {
                    "Contact",
                    "Account",
                    "User",
                    "UserRole",
                    "Profile",
                    "UserLicense",
                };

                modelGen.CreateOfflineSymbolTable(onlyObjects);
            }
            catch (ApexSharpHttpException exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}
