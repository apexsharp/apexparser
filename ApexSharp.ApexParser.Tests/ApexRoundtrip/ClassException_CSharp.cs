namespace ApexSharpDemo.ApexCode
{
    using Apex;
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.ApexSharp.Extensions;
    using Apex.System;
    using SObjects;

    public class ClassException : Exception
    {
        [ApexSharpGenerated]
        public ClassException() : base()
        {
        }

        [ApexSharpGenerated]
        public ClassException(string message) : base(message)
        {
        }

        [ApexSharpGenerated]
        public ClassException(Exception e) : base(e)
        {
        }

        [ApexSharpGenerated]
        public ClassException(string message, Exception e) : base(message, e)
        {
        }
    }
}
