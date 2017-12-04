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
            //ConvertToCSharp();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void Demo()
        {
            // SoqlDemo is a CRUD C# code that we will convert APEX. This can be executed now.
            // Take a look at the SoqlDemo.cs file in the /ApexCode Folder.  
            Setup.Init();
            //CSharpClasses.Demo.GetContacts();
        }

        public static void CreateOffLineClasses()
        {
            // Always Initialize your settings when ever you are connecting to SF
            Setup.Init();

            try
            {
                ModelGen modelGen = new ModelGen();
                modelGen.CreateOfflineSymbolTable("ApexSharpDemo.SObjects", modelGen.GetAllObjectNames());
            }
            catch (ApexSharpHttpException exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public static void ConvertToCSharp()
        {
            List<FileInfo> apexFileList = new DirectoryInfo(@"\DevSharp\ApexSharp\ApexSharpDemo\ApexClasses\").GetFiles("*.apex").ToList();

            foreach (var apexFile in apexFileList)
            {
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                //   var cSharpFile = ApexParser.ApexParser.ConvertApexToCSharp(cSharpCode, "ApexSharpDemo.CSharpClasses");

                // var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                //   File.WriteAllText(@"\DevSharp\ApexSharp\ApexSharpDemo\CSharpClasses\" + cSharpFileName, cSharpFile);
            }
        }
    }
}