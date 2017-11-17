using System;

namespace Apex.ApexAttrbutes
{
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