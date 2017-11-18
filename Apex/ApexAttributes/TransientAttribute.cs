using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class TransientAttribute : Attribute
    {
        public TransientAttribute()
        {
        }
    }
}