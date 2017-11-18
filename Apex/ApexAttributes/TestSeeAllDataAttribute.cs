using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class TestSeeAllDataAttribute : Attribute
    {
        public TestSeeAllDataAttribute()
        {
            IsSharing = true;
        }

        protected virtual bool IsSharing { get; }
    }
}