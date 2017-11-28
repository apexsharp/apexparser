using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class TestSeeAllData : Attribute
    {
        public TestSeeAllData(bool seeAllData)
        {
            SeeAllData = seeAllData;
        }

        protected virtual bool SeeAllData { get; }
    }
}