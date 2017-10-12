

using Apex.ApexAttrbutes;
using Apex.System;

namespace ApexSharpDemo.ApexCode
{
    using ApexSharpBase.Attribute;

    [ApexGlobel]
    public class ClassGlobal
    {
        public virtual void VirtualMethod()
        {
        }

        [ApexFuture]
        public static void FutureMethod()
        {
        }

        [ApexFuture(true)]
        public static void FutureMethodWithCallOut()
        {
        }
    }
}
