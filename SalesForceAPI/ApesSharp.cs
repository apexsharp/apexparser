using System;
using System.IO;

namespace SalesForceAPI
{
    public class ApexSharp
    {
        private readonly ApexSharpConfig _apexSharpConfigSettings = new ApexSharpConfig();

        public static void CreateOfflineClasses(string sObjectName)
        {
            throw new NotImplementedException();
        }

        // Double Check For All These Values
        public ApexSharpConfig CreateSession()
        {
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

        public ApexSharp CacheLocation(string dirLocation)
        {
            _apexSharpConfigSettings.CatchLocation = new DirectoryInfo(dirLocation);
            return this;
        }

        public ApexSharp SaveConfigAt(string configFile)
        {
            var configJson = Path.Combine(_apexSharpConfigSettings.CatchLocation.FullName, configFile);
            _apexSharpConfigSettings.ConfigLocation = new FileInfo(configJson);
            return this;
        }
    }
}