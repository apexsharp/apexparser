using System;

namespace Apex.ApexAttrbutes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Future : global::System.Attribute
    {
        public Future()
        {
            CallOut = false;
        }

        public Future(bool callOut)
        {
            CallOut = callOut;
        }

        protected virtual bool CallOut { get; }
    }
}