using System;

namespace SalesForceAPI.Model
{
    public class ConnectionDetail
    {
        public string UserId { get; set; }
        public string Url { get; set; }
        public string SessionId { get; set; }
        public string RestUrl { get; set; }
        public string RestSessionId { get; set; }
        public DateTime SessionCreationDateTime { get; set; }
        public string Message { get; set; }
    }
}