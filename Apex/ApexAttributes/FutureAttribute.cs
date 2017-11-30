using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Future : Attribute
    {
        public bool callout { get; set; }
    }
}