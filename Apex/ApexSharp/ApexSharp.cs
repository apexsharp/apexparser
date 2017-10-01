using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apex.ApexSharp.ApexToSharp;
using Apex.ApexSharp.SharpToApex;
using Newtonsoft.Json;
using SalesForceAPI;
using SalesForceAPI.Model;

namespace Apex.ApexSharp
{
    public enum LogLevle
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
    }

    public class ApexSharp
    {
        readonly List<string> _errorMessageList = new List<string>();
        private string ConfigFileName { get; set; }

        public ApexSharp()
        {
            ApexSharpConfigSettings = new ApexSharpConfig();
            ConfigFileName = String.Empty;
        }

        public ApexSharpConfig ApexSharpConfigSettings { get; set; }


        public bool Init()
        {
            string projectDirectoryName = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            if (ConfigFileName != String.Empty)
            {
                FileInfo setupFile = new FileInfo(projectDirectoryName + @"\" + ConfigFileName);
                if (setupFile.Exists)
                {
                    string json = File.ReadAllText(projectDirectoryName + @"\" + ConfigFileName);



                    ApexSharpConfigSettings = JsonConvert.DeserializeObject<ApexSharpConfig>(json);

                    //Log.LogMsg("Setup Info", ApexSharpConfigSettings);


                }
                else
                {
                    _errorMessageList.Add("Setup File Not Found");
                    ConfigFileName = String.Empty;
                    return InitOk();
                }
            }

            var connectionUtil = new ConnectionUtil();
            connectionUtil.DebugOn();

            ConnectionDetail connectionDetail = connectionUtil.SalesForceUrl(ApexSharpConfigSettings.SalesForceUrl)
                .WithUserId(ApexSharpConfigSettings.SalesForceUserId)
                .AndPassword(ApexSharpConfigSettings.SalesForcePassword)
                .AndToken(ApexSharpConfigSettings.SalesForcePasswordToken)
                .ConnectToSalesForce();


            if (ApexSharpConfigSettings.ApexFileLocation == "")
            {
                _errorMessageList.Add("Apex Dir Location Missing");
            }

            var process = global::System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            if (process.Contains(".vshost") == false)
            {
                _errorMessageList.Add("The code must be run using Visual Studio");
            }


            List<string> cShaprFileList = Directory.GetFileSystemEntries(projectDirectoryName, "*.csproj").ToList();
            if (cShaprFileList.Any())
            {
                ConnectionUtil.SetProjectLocation(cShaprFileList[0]);
            }

            return InitOk();
        }

        public bool InitOk()
        {
            if (_errorMessageList.Count > 0)
            {
                return false;
            }
            return true;
        }

        public List<string> GetErrorMessage()
        {
            return _errorMessageList;
        }

        public ApexSharp SalesForceUrl(string salesForceUrl)
        {

            salesForceUrl = "https://login.salesforce.com/services/Soap/c/40.0/";
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

        public ApexSharp LoadApexSharpConfig(string configFileName)
        {
            ConfigFileName = configFileName;
            return this;
        }

        public ApexSharp SetLogLevel(LogLevle logLevel)
        {
            return this;
        }


        public ApexSharp SaveApexSharpConfig(string configFileName)
        {
            string json = JsonConvert.SerializeObject(ApexSharpConfigSettings);
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            File.WriteAllText(path + @"\" + configFileName, json);
            return this;
        }


        public void ConvertToApexAndAddToProject(string fileName, bool overWrite)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            path = path + "\\ApexCode\\" + fileName + ".cs";

            FileInfo cSharpFileInfo = new FileInfo(path);
            ConvertToApexAndSave(cSharpFileInfo);
        }

        public void ConvertAllToApex()
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            path = path + "\\ApexCode\\";

            var cShaprFileList = Directory.GetFileSystemEntries(path, "*.cs").ToList();
            foreach (var cSharpFile in cShaprFileList)
            {
                FileInfo cShaFileInfo = new FileInfo(cSharpFile);
                ConvertToApexAndSave(cShaFileInfo);
            }
        }

        private void ConvertToApexAndSave(FileInfo fullFileName)
        {
            var apexFileName = fullFileName.Name.Replace(".cs", "");
            apexFileName = ApexSharpConfigSettings.ApexFileLocation + apexFileName + ".cls";

            Console.WriteLine($"Converting {apexFileName}");

            CSharpParser parser = new CSharpParser();
            var apexClassDeclarationSyntax = parser.ParseCSharpFromFile(fullFileName);

            var convertedApex = apexClassDeclarationSyntax.GetApexCode();


            Console.WriteLine($"Saving {apexFileName}");

            Console.WriteLine();
            Console.WriteLine(String.Join("\n", convertedApex));

            File.WriteAllLines(apexFileName, convertedApex);
        }


        public void ConvertToCSharpAndAddToProject(string apexFileName, bool overWrite)
        {
            apexFileName = ApexSharpConfigSettings.ApexFileLocation + apexFileName + ".cls";
            Console.WriteLine($"Converting APEX File and Saving {apexFileName}");

            FileInfo newFileInfo = new FileInfo(apexFileName);
            List<FileInfo> newFileInfos = new List<FileInfo> { newFileInfo };

            var apexClassDeclarationSyntax = ApexTokenizer.Parse(newFileInfo);
            Console.WriteLine();

            Console.WriteLine(String.Join("\n", apexClassDeclarationSyntax.GetCSharpCode()));
        }


        public void CreateOfflineClasses(string sObjectName)
        {
            ModelGen modelGen = new ModelGen();
            modelGen.CreateOfflineSymbolTable(sObjectName);
        }
    }
}