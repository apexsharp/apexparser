using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ApexParser.ApexCodeFormatter;
using ApexParser.Lexer;
using ApexParser.MetaClass;

namespace ApexParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // string myDevdir = @"C:\DevSharp";
            // string apexCode = File.ReadAllText(myDevdir + @"\ApexParser\SalesForceApexSharp\src\classes\ClassDemo.cls");
            string apexCode = File.ReadAllText(@"C:\Dev\nadev12d\src\classes\TestDataFactory.cls");
            var apexCodeList = FormatApexCode.GetFormattedApexCode(apexCode);
            Console.WriteLine(apexCodeList);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}