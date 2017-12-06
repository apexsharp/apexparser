namespace Demo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ApexSharpApi;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Always Initialize your settings when ever you are connecting to SF
            Setup.Init();

            // CreateOffLineClasses();
            ConvertToCSharp();
            // Demo();
            // ConvertToApex();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void Demo()
        {


            // Demo is a CRUD C# code that we will convert APEX. This can be executed now.
            // Take a look at the Demo.cs file in the /CSharpClasses Folder.  
            CSharpClasses.RunAll.TestClassess();
        }

        // Used to generate the offline C# classes, one C# for each SObject
        public static void CreateOffLineClasses()
        {
            try
            {
                ModelGen modelGen = new ModelGen();
                var allObjects = modelGen.GetAllObjectNames();

                List<string> onlyObjects = new List<string>();
                onlyObjects.Add("Contact");
                onlyObjects.Add("Account");
                onlyObjects.Add("User");
                onlyObjects.Add("UserRole");
                onlyObjects.Add("Profile");
                onlyObjects.Add("UserLicense");

                modelGen.CreateOfflineSymbolTable(onlyObjects);
            }
            catch (ApexSharpHttpException exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public static void ConvertToCSharp()
        {
            // Read all the .cls files from the follwoing dir.
            List<FileInfo> apexFileList = new DirectoryInfo(ConnectionUtil.GetSession().SalesForceLocation + @"\classes\").GetFiles("*.cls").ToList();

            foreach (var apexFile in apexFileList)
            {
                Console.WriteLine($"Convertiong {apexFile}");
                // Convert to C#, Make sure to pass the name of the namespace.
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                var nameSpace = ConnectionUtil.GetSession().VsProjectName + ".CSharpClasses";
                var cSharpFile = ApexParser.ApexSharpParser.ConvertApexToCSharp(cSharpCode, nameSpace);

                // Save the converted C# File. 
                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                File.WriteAllText(ConnectionUtil.GetSession().VsProjectLocation + "\\CSharpClasses\\" + cSharpFileName, cSharpFile);
            }
        }

        public static void ConvertToApex()
        {
            var cSharpFileLocation = Path.Combine(ConnectionUtil.GetSession().VsProjectLocation, "CSharpClasses");
            List<FileInfo> cSharpFileList = new DirectoryInfo(cSharpFileLocation).GetFiles("*.cs").ToList();

            foreach (var cSharpFile in cSharpFileList)
            {
                var cSharpCode = File.ReadAllText(cSharpFile.FullName);

                foreach (var colleciton in ApexParser.ApexSharpParser.ConvertToApex(cSharpCode))
                {
                    var cSharpFileName = Path.ChangeExtension(colleciton.Key, ".cls");

                    var apexFile = Path.Combine(ConnectionUtil.GetSession().SalesForceLocation, "classes", cSharpFileName);
                    Console.WriteLine(apexFile);
                    File.WriteAllText(apexFile, colleciton.Value);
                }
            }
        }
    }
}
