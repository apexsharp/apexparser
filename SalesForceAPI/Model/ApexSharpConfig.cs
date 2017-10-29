using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForceAPI.Model
{

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

        public string UserId { get; set; }
        public string Url { get; set; }
        public string SessionId { get; set; }
        public string RestUrl { get; set; }
        public string RestSessionId { get; set; }
        public DateTime SessionCreationDateTime { get; set; }
    }
}
   
