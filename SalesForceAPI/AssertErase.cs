using System;
using System.Runtime.CompilerServices;

namespace SalesForceAPI
{
    public static class AssertErase
    {
        public static void AreSame(int a, int b, string msg, [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string caller = null)
        {
            if (a == b)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg + ":" + sourceFilePath + ":" + caller + ":" + lineNumber);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msg + ":" + sourceFilePath + ":" + caller + ":" + lineNumber);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void AreSame(string a, string b, string msg, [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string sourceFilePath = "", [CallerMemberName] string caller = null)
        {
            if (a == b)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg + ":" + sourceFilePath + ":" + caller + ":" + lineNumber);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msg + ":" + sourceFilePath + ":" + caller + ":" + lineNumber);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}