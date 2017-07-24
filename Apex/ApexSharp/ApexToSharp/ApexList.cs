using System.Collections.Generic;
using System.Text;
using Apex.ApexSharp.MetaClass;

namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexList
    {
        public readonly List<string> ApexComments = new List<string>();
        public readonly List<ApexTocken> ApexTockens = new List<ApexTocken>();

        public ApexType ApexType;

        // 0 : No, 1: Only on Null, 2: Everything
        public string GetTokenListForInsert(int detailLevel)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var apexComment in ApexComments)
            {
                sb.AppendLine(apexComment);
            }

            if (detailLevel == 0)
            {
                sb.AppendLine(ApexFormater.PrintCleanLine(ApexTockens));
            }

            else if (detailLevel == 1)
            {
                sb.AppendLine("//::    " + ApexType);
                sb.AppendLine(ApexFormater.PrintCleanLine(ApexTockens));

                if (ApexType == ApexType.NotFound)
                {
                    int j = 0;
                    foreach (var apexTocken in ApexTockens)
                    {
                        j++;
                        sb.AppendLine("//:: " + j + "." + apexTocken);
                    }
                    sb.AppendLine();
                }
            }
            else if (detailLevel == 2)
            {
                sb.AppendLine("//::    " + ApexType);
                sb.AppendLine(ApexFormater.PrintCleanLine(ApexTockens));

                int j = 0;
                foreach (var apexTocken in ApexTockens)
                {
                    j++;
                    sb.AppendLine("//:: " + j + "." + apexTocken);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}