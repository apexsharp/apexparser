namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class ClassNoApex
    {
        // Any methods in the NoApex name space will be commented out.
        public static void MethodOne()
        {
            System.Debug("Jay");

            NoApex.Console.WriteLine("Jay");
        }

        // Method Name staring with NoApex will be uncommented in C#
        public static void NoApexMethodTwo()
        {
            NoApex.Console.WriteLine("Jay");
        }
    }
}
