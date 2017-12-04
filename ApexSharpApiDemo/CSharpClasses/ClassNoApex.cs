namespace ApexSharpApiDemo.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using ApexSharpApi.ApexApi;

    public class ClassNoApex
    {
        // Any methods in the NoApex name space will be commented out.
        public static string MethodOne()
        {
            return "Jay";

            NoApex.Console.WriteLine("Jay");
        }

        // Method Name staring with NoApex will be uncommented in C#
        public static void NoApexMethodTwo()
        {
            NoApex.Console.WriteLine("Jay");
        }
    }
}
