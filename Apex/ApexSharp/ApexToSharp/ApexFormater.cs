using System.Collections.Generic;
using System.Text;
using Apex.ApexSharp.Util;

namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexFormater
    {
        public static string PrintCleanLine(List<ApexTocken> apexTokens)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < apexTokens.Count; i++)
            {
                if (apexTokens[i].TockenType == TockenType.Dot || apexTokens[i].TockenType == TockenType.OpenBrackets ||
                    apexTokens[i].TockenType == TockenType.CloseBrackets ||
                    apexTokens[i].TockenType == TockenType.StatmentTerminator)
                {
                    sb.Append(apexTokens[i].Tocken);
                }
                else if (i > 0 && (apexTokens[i - 1].TockenType == TockenType.Dot ||
                                   apexTokens[i - 1].TockenType == TockenType.OpenBrackets))
                {
                    sb.Append(apexTokens[i].Tocken);
                }
                else
                {
                    sb.AppendSpace().Append(apexTokens[i].Tocken);
                }
            }

            return sb.ToString().Trim();
        }
    }
}