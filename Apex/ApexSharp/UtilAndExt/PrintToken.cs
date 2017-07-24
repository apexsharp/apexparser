using System;
using System.Collections.Generic;
using Apex.ApexSharp.ApexToSharp;

namespace Apex.ApexSharp.Util
{
    public class PrintToken
    {
        public static void Print(List<ApexTocken> apexTokenList)
        {
            foreach (ApexTocken apexToken in apexTokenList)
            {
                Console.WriteLine($"{apexToken.TockenType}:{apexToken.Tocken}");
            }
        }
    }
}