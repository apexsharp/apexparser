using System;
using Serilog;

namespace SalesForceAPI
{
    public class ApexSharpConfig
    {
        public string SalesForceUrl { get; set; }
        public string HttpProxy { get; set; }
        public string SalesForceUserId { get; set; }
        public string SalesForcePassword { get; set; }
        public string SalesForcePasswordToken { get; set; }
        public int SalesForceApiVersion { get; set; }
        public string DirLocationAndFileName { get; set; }
        public string SessionId { get; set; }
        public string RestUrl { get; set; }
        public string RestSessionId { get; set; }
        public DateTime SessionCreationDateTime { get; set; }
    }
}