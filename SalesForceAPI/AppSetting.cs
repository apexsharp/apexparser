using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForceAPI
{
    public class AppSetting
    {
        public static FileInfo GetConfiLocation()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                var location = appSettings["SalesForceRoot"];
                return new FileInfo(location);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return null;
        }
    }
}
