using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSharpDemo.NoApex
{
    public class SalesForceLogIn
    {
        public static void Login()
        {
            var connection = ConfigurationManager.AppSettings["SalesForceRoot"];
            new SalesForceAPI.ApexSharp().Connect("C:\\DevSharp\\connect.json");
        }
    }
}
