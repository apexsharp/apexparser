using System.Collections.Generic;
using System.Linq;

namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexToCSharpPrimitives
    {
        public ApexToCSharpPrimitives(string apexName, string cSharpName)
        {
            ApexName = apexName;
            CSharpName = cSharpName;
        }

        public string ApexName { get; }
        public string CSharpName { get; }
    }

    public class ConvertToCSharpPrimitives
    {
        public static void ChangeToCSharpNames(ApexClassContainer apexClassContainer)
        {
            List<ApexToCSharpPrimitives> apexToCSharp = new List<ApexToCSharpPrimitives>
            {
                new ApexToCSharpPrimitives("global", "public"),
                new ApexToCSharpPrimitives("Boolean", "bool"),
                new ApexToCSharpPrimitives("Integer", "int"),
                new ApexToCSharpPrimitives("Decimal", "decimal"),
                new ApexToCSharpPrimitives("Double", "double"),
                new ApexToCSharpPrimitives("Long", "long"),
                new ApexToCSharpPrimitives("String", "string"),
                new ApexToCSharpPrimitives("Object", "object")
            };


            foreach (var apexTokenList in apexClassContainer.ApexListList)
            {
                foreach (var apexTocken in apexTokenList.ApexTockens)
                {
                    foreach (var apexToCSharpPrimitivese in apexToCSharp)
                    {
                        // If the value is Generic
                        if (apexTocken.TockenType == TockenType.ClassNameGeneric)
                        {
                            var genericToken = apexTocken.Tocken.Substring(1, apexTocken.Tocken.Length - 2);
                            var genericTokenList = genericToken.Split(',').ToList();
                            foreach (var token in genericTokenList)
                            {
                                if (token == apexToCSharpPrimitivese.ApexName)
                                {
                                    genericToken = genericToken.Replace(apexToCSharpPrimitivese.ApexName,
                                        apexToCSharpPrimitivese.CSharpName);
                                }
                            }
                            apexTocken.Tocken = "<" + genericToken + ">";
                        }
                        else if (apexTocken.Tocken == apexToCSharpPrimitivese.ApexName)
                        {
                            apexTocken.Tocken = apexToCSharpPrimitivese.CSharpName;
                        }
                    }
                }
            }
        }

        public static void UpdateString(ApexClassContainer apexClassContainer)
        {
            foreach (var apexTokenList in apexClassContainer.ApexListList)
            {
                foreach (var apexTocken in apexTokenList.ApexTockens)
                {
                    if (apexTocken.TockenType == TockenType.QuotedString)
                    {
                        var newString = apexTocken.Tocken.Replace("\'", "\"");
                        apexTocken.Tocken = newString;
                    }
                }
            }
        }
    }
}