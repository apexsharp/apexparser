using System;

namespace Apex.ApexAttrbutes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RestResource : global::System.Attribute
    {
        public RestResource(string url)
        {
            Url = url;
        }

        public virtual string Url { get; }
    }
}