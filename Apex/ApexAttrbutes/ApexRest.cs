using System;

namespace Apex.ApexAttrbutes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApexRest : global::System.Attribute
    {
        public ApexRest(string url)
        {
            Url = url;
        }

        public virtual string Url { get; }
    }
}