using System;

namespace Apex.ApexAttrbutes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ApexTestSeeAllData : global::System.Attribute
    {
        public ApexTestSeeAllData()
        {
            IsSharing = true;
        }

        protected virtual bool IsSharing { get; }
    }


    [AttributeUsage(AttributeTargets.Method)]
    public class ApexFuture : global::System.Attribute
    {
        public ApexFuture()
        {
            CallOut = false;
        }

        public ApexFuture(bool callOut)
        {
            CallOut = callOut;
        }

        protected virtual bool CallOut{ get; }
    }
}