using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FutureAttribute : Attribute
    {
        public FutureAttribute()
        {
            CallOut = false;
        }

        public FutureAttribute(bool callOut)
        {
            CallOut = callOut;
        }

        protected virtual bool CallOut { get; }
    }
}