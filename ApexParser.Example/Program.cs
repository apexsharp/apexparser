using ApexSharpDemo.ApexApiAnalyzer;
using ApexSharpDemo.ApexCodeFormat;
using ApexSharpDemo.CaseClean;
using ApexSharpDemo.ListClassesAndMethods;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace ApexSharpDemo
{
    public class Program
    {
        public class Options
        {
            [Option('s', "service", Required = true, HelpText = "What service you need, type --help for a list of services")]
            public string Service { get; set; }

            [Option('a', "apex", Required = false, HelpText = "Apex Class Name Please")]
            public string ApexFileName { get; set; }

            [Option('d', "dir", Required = true, HelpText = "Directory where Apex classes are located")]
            public string ApexFolder { get; set; }
            [Option('o', "outputJson", Required = false, HelpText = "Output the Results as JSON")]
            public bool IsOutputJson { get; set; }
        }

        public static void Main(string[] args)
        {
            var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var cSharpFileInfo = new FileInfo(Path.Combine(dirInfo.Parent.Parent.Parent.FullName, "ListClassesAndMethods", "Demo.cs"));
            if (cSharpFileInfo.Exists)
            {
                Console.WriteLine(cSharpFileInfo.FullName);

                var cSharpFile = File.ReadAllText(cSharpFileInfo.FullName);
                var classNameList = ClassesAndMethodsDemo.GetClassMethodCount(cSharpFile);
                ClassesAndMethodsDemo.PrintDetails(classNameList);
            }




            //new CodeFormatTest();
            //var apexDirLocation = new DirectoryInfo(@"C:\DevSharp\ApexSharpFsb\FSB\ApexClasses\");
            //var apexClassName = new FileInfo(@"C:\DevSharp\ApexSharpFsb\FSB\ApexClasses\FS_AccountSetupController.cls");
            //foreach (var apexFile in FindRelatedClasses.FindRelatedClasses.GetAllRealatedApexFiles(apexDirLocation, apexClassName))
            //{
            //    Console.WriteLine(apexFile.Name);
            //}


            Console.WriteLine();
            Console.WriteLine("Done, Press Any Key To Exit");
            Console.ReadLine();
        }

        public static void Main1(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
            {
                if (o.ApexFolder.Length > 0)
                {
                    try
                    {
                        DirectoryInfo apexDirectory = new DirectoryInfo(o.ApexFolder);

                        switch (o.Service.ToLower())
                        {
                            case "apexcodeformat":
                                FormatApexCode(o.ApexFolder, o.IsOutputJson);
                                break;

                            case "apextestfind":
                                ApexTestFined(o.ApexFolder, o.ApexFileName, o.IsOutputJson);
                                break;

                            case "caseclean":
                                CaseClean(o.ApexFolder);
                                break;

                            default:
                                Console.WriteLine($"Current Arguments: -v {o.Service}");
                                Console.WriteLine("Quick Start Example!");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            });

            Console.WriteLine("Done, Press Any Key To Exit");
            Console.ReadLine();
        }



        private static void ApexTestFined(string apexFolderName, string apexFileName, bool returnJson)
        {
            int count = Directory.GetFiles(apexFolderName, "*.cls", SearchOption.TopDirectoryOnly).Length;

            Console.WriteLine("Searching in " + count + " Salesforce classes, This is going to take some time...");

            List<string> qaClasses = ApexTestFind.ApexTestFinder.GetAllTestClasses(apexFolderName, apexFileName);
            foreach (var qaClass in qaClasses)
            {
                Console.WriteLine("Related Test Classes " + qaClass);
            }
        }

        public static void ApexAnalyzer(Options opts)
        {
            var analyzer = new Analyzer();
            var results = analyzer.AnylyzDir(opts.ApexFolder);
        }

        public static void CaseClean(string apexFolderName)
        {
            CaseCleaner.Clean(apexFolderName);
        }

        public static void FormatApexCode(string apexFolderName, bool returnJson)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(apexFolderName);
            List<FileFormatDto> results = ApexCodeFormater.FormatApexCode(directoryInfo);
            Console.WriteLine(results.Count);
        }
    }
}
