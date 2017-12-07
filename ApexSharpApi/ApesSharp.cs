using ApexParser;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApexSharpApi
{
    public class ApexSharp
    {
        public static void ValidateDir(DirectoryInfo dirInfo)
        {
            if (dirInfo.Exists == false) { throw new DirectoryNotFoundException(); }
        }

        public static void ConvertToCSharp(string apexDir, string cSharpDir)
        {
            var apexDirInfo = new DirectoryInfo(apexDir);
            ValidateDir(apexDirInfo);
            var cSharpDirInfo = new DirectoryInfo(cSharpDir);
            ValidateDir(cSharpDirInfo);

            FileInfo[] apexFileList = apexDirInfo.GetFiles("*.cls");

            foreach (var apexFile in apexFileList)
            {
                Console.WriteLine($"Convertiong {apexFile}");

                // Read and Convert to C#, Make sure to pass the name of the namespace.
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                var nameSpace = ConnectionUtil.GetSession().VsProjectName + ".CSharpClasses";
                var cSharpFile = ApexSharpParser.ConvertApexToCSharp(cSharpCode, nameSpace);

                // Save the converted C# File
                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                var cSharpFileSave = Path.Combine(cSharpDirInfo.FullName, cSharpFileName);

                Console.WriteLine($"Saving {cSharpFileSave}");

                File.WriteAllText(cSharpFileSave, cSharpFile);
            }

        }

        public static void ConvertToApex(string cSharpDir, string apexDir)
        {
            var apexDirInfo = new DirectoryInfo(apexDir);
            ValidateDir(apexDirInfo);
            var cSharpDirInfo = new DirectoryInfo(cSharpDir);
            ValidateDir(cSharpDirInfo);

            FileInfo[] cSharpFileList = cSharpDirInfo.GetFiles("*.cs");
            
            foreach (var cSharpFile in cSharpFileList)
            {
                var cSharpCode = File.ReadAllText(cSharpFile.FullName);

                foreach (var colleciton in ApexSharpParser.ConvertToApex(cSharpCode))
                {
                    var cSharpFileName = Path.ChangeExtension(colleciton.Key, ".cls");

                    var apexFile = Path.Combine(apexDirInfo.FullName, cSharpFileName);

                    Console.WriteLine(apexFile);

                    File.WriteAllText(apexFile, colleciton.Value);
                }
            }
        }

        private readonly ApexSharpConfig _apexSharpConfigSettings = new ApexSharpConfig();

        public static object ApexParser { get; private set; }

        // Double Check For All These Values
        public ApexSharpConfig CreateSession()
        {
            DirectoryInfo vsProjectLocation = new DirectoryInfo(_apexSharpConfigSettings.VsProjectLocation);
            Console.WriteLine(vsProjectLocation.Exists);

            DirectoryInfo salesForceLocation = new DirectoryInfo(_apexSharpConfigSettings.SalesForceLocation);
            Console.WriteLine(salesForceLocation.Exists);

            FileInfo configLocation = new FileInfo(_apexSharpConfigSettings.ConfigLocation);
            DirectoryInfo configDirectory = configLocation.Directory;
            Console.WriteLine(configDirectory.Exists);

            Directory.CreateDirectory(_apexSharpConfigSettings.VsProjectLocation + "CSharpClasses");
            Directory.CreateDirectory(_apexSharpConfigSettings.VsProjectLocation + "NoApex");
            Directory.CreateDirectory(_apexSharpConfigSettings.VsProjectLocation + "Cache");
            Directory.CreateDirectory(_apexSharpConfigSettings.VsProjectLocation + "SObjects");
            return ConnectionUtil.CreateSession(_apexSharpConfigSettings);
        }

        public ApexSharp SalesForceUrl(string salesForceUrl)
        {
            _apexSharpConfigSettings.SalesForceUrl = salesForceUrl;
            return this;
        }

        public ApexSharp WithUserId(string salesForceUserId)
        {
            _apexSharpConfigSettings.SalesForceUserId = salesForceUserId;
            return this;
        }

        public ApexSharp AndPassword(string salesForcePassword)
        {
            _apexSharpConfigSettings.SalesForcePassword = salesForcePassword;
            return this;
        }

        public ApexSharp AndToken(string salesForcePasswordToken)
        {
            _apexSharpConfigSettings.SalesForcePasswordToken = salesForcePasswordToken;
            return this;
        }
        public ApexSharp AndSalesForceApiVersion(int apiVersion)
        {
            _apexSharpConfigSettings.SalesForceApiVersion = apiVersion;
            return this;
        }
        public ApexSharp AddHttpProxy(string httpProxy)
        {
            _apexSharpConfigSettings.HttpProxy = httpProxy;
            return this;
        }

        public ApexSharp VsProjectLocation(string dirLocation)
        {
            _apexSharpConfigSettings.VsProjectLocation = dirLocation;
            return this;
        }

        public ApexSharp SalesForceLocation(string dirLocation)
        {
            _apexSharpConfigSettings.SalesForceLocation = dirLocation;
            return this;
        }

        public ApexSharp SetVsProjectName(string projectName)
        {
            _apexSharpConfigSettings.VsProjectName = projectName;
            return this;
        }
        public ApexSharp SaveConfigAt(string configFile)
        {
            _apexSharpConfigSettings.ConfigLocation = configFile;
            return this;
        }
    }
}