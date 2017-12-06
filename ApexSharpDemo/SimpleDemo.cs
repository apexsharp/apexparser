namespace ApexSharpDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ApexSharpApi;

    public class SimpleDemo
    {
        public static void Main(string[] args)
        {
            // CreateOffLineClasses()
            // Demo();
            // ConvertToCSharp();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void Demo()
        {
            // Always Initialize your settings when ever you are connecting to SF
            Setup.Init();

            // Demo is a CRUD C# code that we will convert APEX. This can be executed now.
            // Take a look at the Demo.cs file in the /CSharpClasses Folder.  
            Setup.Init();
            // CSharpClasses.Demo.RunDemo();
        }

        // Used to generate the offline C# classes, one C# for each SObject
        public static void CreateOffLineClasses()
        {
            // Always Initialize your settings when ever you are connecting to SF
            Setup.Init();

            try
            {
                ModelGen modelGen = new ModelGen();
                modelGen.CreateOfflineSymbolTable(modelGen.GetAllObjectNames());
            }
            catch (ApexSharpHttpException exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public static void ConvertToCSharp()
        {
            // Read all the .cls files from the follwoing dir.
            List<FileInfo> apexFileList = new DirectoryInfo(@"\DevSharp\ApexSharp\src\classes\").GetFiles("*.cls").ToList();

            foreach (var apexFile in apexFileList)
            {
                // Convert to C#, Make sure to pass the name of the namespace.
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                var cSharpFile = ApexParser.ApexSharpParser.ConvertApexToCSharp(cSharpCode, "ApexSharpDemo.CSharpClasses");

                // Save the converted C# File. 
                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                File.WriteAllText(@"\DevSharp\ApexSharp\ApexSharpDemo\CSharpClasses\" + cSharpFileName, cSharpFile);
            }
        }
    }
}