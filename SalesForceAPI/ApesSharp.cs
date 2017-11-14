using System;
using System.Collections.Generic;
using System.IO;
using SalesForceAPI.Model;
using Serilog;

namespace SalesForceAPI
{
    public class ApexSharp
    {
        public ApexSharpConfig ApexSharpConfigSettings = new ApexSharpConfig();

        public void Connect(string configLocation)
        {
            ConnectionUtil.Connect(configLocation);
        }

        public void Connect()
        {
            ConnectionUtil.Connect(ApexSharpConfigSettings);
        }

        public List<string> GetAllObjects()
        {
            return new List<string>();
        }


        public void CreateOfflineClasses(string sObjectName)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            ModelGen modelGen = new ModelGen();
            modelGen.CreateOfflineSymbolTable(sObjectName, path);
        }


        public void ConvertCSharpToApex(FileInfo cSharpFile)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            path = path + "\\ApexCode\\" + cSharpFile.Name + ".cs";

            FileInfo cSharpFileInfo = new FileInfo(path);
            var apexFileName = cSharpFileInfo.Name.Replace(".cs", "");
            apexFileName = ApexSharpConfigSettings.ApexFileLocation + apexFileName + ".cls";

            Console.WriteLine($"Converting {apexFileName}");

            //  CSharpParser parser = new CSharpParser();
            //  ApexClassDeclarationSyntax apexClassDeclarationSyntax = parser.ParseCSharpFromFile(cSharpFileInfo);

            // Console.WriteLine(String.Join("\n", convertedApex));
            Console.WriteLine($"Saving {apexFileName}");
            //File.WriteAllLines(apexFileName, convertedApex);
        }


        public ApexSharp SaveApexSharpConfig(string dirLocationAndFileName)
        {
            ApexSharpConfigSettings.DirLocationAndFileName = dirLocationAndFileName;
            return this;
        }

        public ApexSharp SalesForceUrl(string salesForceUrl)
        {
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
        public ApexSharp AndSalesForceApiVersion(int apiVersion)
        {
            ApexSharpConfigSettings.SalesForceApiVersion = apiVersion;
            return this;
        }
        public ApexSharp AddHttpProxy(string httpProxy)
        {
            ApexSharpConfigSettings.HttpProxy = httpProxy;
            return this;
        }
        public ApexSharp SetApexFileLocation(string directory)
        {
            ApexSharpConfigSettings.ApexFileLocation = directory;
            return this;
        }

        public ApexSharp SetVisualStudioProjectLocation(string dir)
        {
            ApexSharpConfigSettings.VisualStudioProjectFile = dir;
            return this;
        }

        public ApexSharp SetLogLevel(LogLevel logLevel)
        {
            Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.ColoredConsole()
            .CreateLogger();

            ApexSharpConfigSettings.LogLevel = logLevel;
            return this;
        }
    }
}