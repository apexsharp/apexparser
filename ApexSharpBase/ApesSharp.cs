using System;
using System.IO;
using Newtonsoft.Json;
using SalesForceAPI;

namespace ApexSharpBase
{
    public enum LogLevel
    {
        Info
    }

    public class ApexSharpConfig
    {
        public string ApexFileLocation { get; set; }
        public string SalesForceUrl { get; set; }
        public string HttpProxy { get; set; }
        public string SalesForceUserId { get; set; }
        public string SalesForcePassword { get; set; }
        public string SalesForcePasswordToken { get; set; }
        public int SalesForceApiVersion { get; set; }
        public string VisualStudioProjectFile { get; set; }
        public LogLevel LogLevel { get; set; }
    }

    public class ApexSharp
    {
        public ApexSharpConfig ApexSharpConfigSettings = new ApexSharpConfig();
        private string ConfigFileName { get; set; }

        public ApexSharp()
        {
            ConfigFileName = String.Empty;
        }

        public void Connect()
        {
            ConnectionUtil.Connect(ApexSharpConfigSettings.SalesForceUrl, ApexSharpConfigSettings.SalesForceUserId, ApexSharpConfigSettings.SalesForcePassword, ApexSharpConfigSettings.SalesForcePasswordToken, ApexSharpConfigSettings.VisualStudioProjectFile);
        }

        public ApexSharp SalesForceUrl(string salesForceUrl)
        {

            salesForceUrl = salesForceUrl + "services/Soap/c/40.0/";
            ApexSharpConfigSettings.SalesForceUrl = salesForceUrl;
            return this;
        }

        public ApexSharp WithUserId(string salesForceUserId)
        {
            ApexSharpConfigSettings.SalesForceUserId = salesForceUserId;
            return this;
        }

        public ApexSharp AndPassword(string salesForcePassword)
        {
            ApexSharpConfigSettings.SalesForcePassword = salesForcePassword;
            return this;
        }

        public ApexSharp AndToken(string salesForcePasswordToken)
        {
            ApexSharpConfigSettings.SalesForcePasswordToken = salesForcePasswordToken;
            return this;
        }

        public ApexSharp UseSalesForceApiVersion(int apiVersion)
        {
            ApexSharpConfigSettings.SalesForceApiVersion = apiVersion;
            return this;
        }

        public ApexSharp AndHttpProxy(string httpProxy)
        {
            ApexSharpConfigSettings.HttpProxy = httpProxy;
            return this;
        }

        public ApexSharp SetApexFileLocation(string directory)
        {
            ApexSharpConfigSettings.ApexFileLocation = directory;
            return this;
        }

        public ApexSharp LoadApexSharpConfig()
        {
            ConfigFileName = "ApexSharpProjectSetup.json";
            return this;
        }

        public ApexSharp SetVisualStudioProjectLocation(string dir)
        {
            ApexSharpConfigSettings.VisualStudioProjectFile = dir;
            return this;
        }

        public ApexSharp SetLogLevel(LogLevel logLevel)
        {
            ApexSharpConfigSettings.LogLevel = logLevel;
            return this;
        }

        public ApexSharp SaveApexSharpConfig(string dirLocationAndFileName)
        {
            string json = JsonConvert.SerializeObject(ApexSharpConfigSettings);
            File.WriteAllText(dirLocationAndFileName, json);
            return this;
        }


        public void CreateOfflineClasses(string sObjectName)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            ModelGen modelGen = new ModelGen();
            modelGen.CreateOfflineSymbolTable(sObjectName, path);
        }


        public void ConvertToApex(string fileName, bool overWrite)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            path = path + "\\ApexCode\\" + fileName + ".cs";

            FileInfo cSharpFileInfo = new FileInfo(path);


            var apexFileName = cSharpFileInfo.Name.Replace(".cs", "");
            apexFileName = ApexSharpConfigSettings.ApexFileLocation + apexFileName + ".cls";

            Console.WriteLine($"Converting {apexFileName}");




          //  CSharpParser parser = new CSharpParser();
          //  ApexClassDeclarationSyntax apexClassDeclarationSyntax = parser.ParseCSharpFromFile(cSharpFileInfo);


            //Console.WriteLine(ConvertToApex.GetApexCode(apexClassDeclarationSyntax));


         //   List<string> convertedApex = apexClassDeclarationSyntax.GetApexCode();
            Console.WriteLine();
           // Console.WriteLine(String.Join("\n", convertedApex));
            Console.WriteLine($"Saving {apexFileName}");
            //File.WriteAllLines(apexFileName, convertedApex);
        }




    }
}