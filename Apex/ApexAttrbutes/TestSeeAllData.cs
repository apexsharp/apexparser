using System;

namespace Apex.ApexAttrbutes
{
    [AttributeUsage(AttributeTargets.All)]
    public class TestSeeAllData : global::System.Attribute
    {
        public TestSeeAllData()
        {
            IsSharing = true;
        }

        protected virtual bool IsSharing { get; }
    }
}