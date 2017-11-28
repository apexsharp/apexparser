using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Future : Attribute
    {
        public Future()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class FutureCallOutTrue : Attribute
    {

        public FutureCallOutTrue()
        {

        }
    }
}