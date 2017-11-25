using System;
using System.IO;
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
        public string SessionId { get; set; }
        public string RestUrl { get; set; }
        public string RestSessionId { get; set; }
        public FileInfo ConfigLocation { get; set; }
        public DirectoryInfo CatchLocation { get; set; }
        public long SessionCreationDateTime { get; set; }
    }
}