using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RestResourceAttribute : Attribute
    {
        public RestResourceAttribute(string url)
        {
            Url = url;
        }

        public virtual string Url { get; }
    }
}