using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RestResourceAttribute : Attribute
    {
        public string UrlMapping { get; set; }
    }
}