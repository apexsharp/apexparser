using System;

namespace ApexSharpDemo.NoApex
{
    public class Console
    {
        public static String ReadLine(string msg)
        {
            System.Console.Write(msg);
            return System.Console.ReadLine();
        }

        public static void WriteLine(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}