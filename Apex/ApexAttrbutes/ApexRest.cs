using System;

namespace Apex.ApexAttrbutes.ApexSharpBase.Attribute
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