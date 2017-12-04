using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apex.ApexSharp;
using ApexSharpApi;
using ApexSharpApi.Model.RestApi;
using ApexSharpApiDemo.CSharpClasses;
using ApexSharpApiDemo.SObjects;
using Serilog;

namespace ApexSharpApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Init();
            var limit = Limits.GetRemainingApiLimit(LimitType.DailyApiRequests);
            Console.WriteLine(limit);
            try
            {
                //Select();
                //CrudExample();
                //ConvertToCSharp();
                //RunAll.TestClassess();
                //   ModelGen modelGen = new ModelGen();
                //   modelGen.CreateOfflineSymbolTable("ApexSharpApiDemo.SObjects", modelGen.GetAllObjectNames());

            }
            catch (ApexSharpHttpException exp)
            {
                Console.WriteLine(exp.Message);
            }
            limit = Limits.GetRemainingApiLimit(LimitType.DailyApiRequests);
            Console.WriteLine(limit);

           // Console.ReadLine();
        }

        public static void ConvertToCSharp()
        {
            List<FileInfo> apexFileList = new DirectoryInfo(@"C:\DevSharp\SalesForceApexSharp\src\classes\").GetFiles("*.cls").ToList();

            foreach (var apexFile in apexFileList)
            {
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                var cSharpFile = ApexParser.ApexParser.ConvertApexToCSharp(cSharpCode, "ApexSharpApiDemo.CSharpClasses");

                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                File.WriteAllText(@"C:\DevSharp\ApexSharpApi\ApexSharpApiDemo\CSharpClasses\" + cSharpFileName, cSharpFile);
            }
        }

        public static void Select()
        {
            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 1");
            foreach (Contact c in contacts)
            {
                Console.WriteLine(c.Email);
            }
        }

        public static void CrudExample()
        {
            Contact contactNew = new Contact
            {
                LastName = "Jay",
                Email = "jay@jay.com"
            };
            Soql.Insert(contactNew);
            Console.WriteLine(contactNew.Id);
            List<Contact> contacts = SoqlApi.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                Console.WriteLine(c.Email);
                c.Email = "new@new.com";
            }

            SoqlApi.Update<Contact>(contacts);
            contacts = SoqlApi.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            foreach (Contact c in contacts)
            {
                Console.WriteLine(c.Email);
            }

            SoqlApi.Delete<Contact>(contacts);
            contacts = SoqlApi.Query<Contact>("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id", contactNew.Id);
            if (contacts.Any())
            {
                Console.WriteLine("Delete Worked");
            }
        }

        public static bool Init()
        {
            //Verbose - tracing information and debugging minutiae; generally only switched on in unusual situations
            //Debug - internal control flow and diagnostic state dumps to facilitate pinpointing of recognized problems
            //Information - events of interest or that have relevance to outside observers; the default enabled minimum logging level
            //Warning - indicators of possible issues or service/functionality degradation
            //Error - indicating a failure within the application or connected system
            //Fatal - critical errors causing complete failure of the application

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] [{SourceContext}] {Message} {NewLine}")
                .MinimumLevel.Debug()
                //.WriteTo.Seq("http://localhost:9999") // If you are using https://getseq.net
                .CreateLogger();

            File.Delete(@"\DevSharp\config.json");
            try
            {
                // See if we have an existing connection
                ConnectionUtil.Session = ConnectionUtil.GetSession(@"\DevSharp\config.json");

            }
            catch (SalesForceNoFileFoundException)
            {
                try
                {
                    // Else Create a new session 
                    ConnectionUtil.Session = new ApexSharp().SalesForceUrl("https://login.salesforce.com/")
                        .AndSalesForceApiVersion(40)
                        .WithUserId("apexsharp@jayonsoftware.com")
                        .AndPassword("1v0EGMfR0NTkbmyQ2Jk4082PA")
                        .AndToken("LUTAPwQstOZj9ESx7ghiLB1Ww")
                        .SalesForceLocation(@"\DevSharp\SalesForceApexSharp\src")
                        .VsProjectLocation(@"\DevSharp\ApexSharpApi\ApexSharpApiDemo\")
                        .SaveConfigAt(@"\DevSharp\config.json")
                        .CreateSession();
                }
                catch (SalesForceInvalidLoginException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
