using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RestResourceAttribute : Attribute
    {
        public string urlMapping { get; set; }
    }
}