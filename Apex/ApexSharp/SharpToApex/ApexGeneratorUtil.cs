using System.Collections.Generic;
using System.Text;
using Apex.ApexSharp.Util;

namespace Apex.ApexSharp.SharpToApex
{
    public class ApexGeneratorUtil
    {
        public static string AttributeCreater(string attribute)
        {
            // @RestResource(urlMapping='/api/v1/restdemo')
            if (attribute.ToLower() == "@istest") return "[TestFixture]";
            if (attribute.ToLower() == "@httpget") return "[ApexHttpPut]";
            if (attribute.ToLower().Contains("restresource"))
            {
                var startIndex = attribute.IndexOf('\'');
                var endIndex = attribute.LastIndexOf('\'');
                var url = attribute.Slice(startIndex + 1, endIndex);
                return "[ApexRest(\"" + url + "\")]";
            }

            return attribute;
        }

        public static string ModifierCreater(List<string> apexModifierList)
        {
            StringBuilder sb = new StringBuilder();

            if (apexModifierList.Count == 1)
            {
                // public : public
                sb.Append(apexModifierList[0]);
            }
            else if (apexModifierList.Count == 2)
            {
                // public readonly : public final  
                if (apexModifierList[1] == "const")
                {
                    sb.Append(apexModifierList[0]).AppendSpace().Append("final");
                }
                else
                {
                    // public static : public static
                    sb.Append(apexModifierList[0]).AppendSpace().Append(apexModifierList[1]);
                }
            }
            else if (apexModifierList.Count == 3)
            {
                // public static readonly : public static final 
                sb.Append(apexModifierList[0]).AppendSpace().Append(apexModifierList[1]).AppendSpace().Append("final");
            }

            return sb.ToString();
        }

        public static string ModifierAttributeCreater(List<string> attributeLists)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var attributeList in attributeLists)
            {
                //@RestResource(urlMapping='/api/v1/restdemo') 
                if (attributeList.Contains("ApexRest"))
                {
                    string url = attributeList.Split('"', '"')[1];
                    sb.AppendLine("@RestResource(urlMapping='" + url + "')");
                }
                else if (attributeList.Contains("TestFixture")) sb.AppendLine("@isTest");
                else if (attributeList.Contains("Test")) return sb.Append("@isTest").ToString();
            }

            // global static 
            //if (attributeLists.Contains("ApexGlobel") && apexModifierList.Count == 2) sb.Append("global").AppendSpace().Append(apexModifierList[1]);
            // global
            //else if (attributeLists.Contains("ApexGlobel") && apexModifierList.Count == 1) sb.Append("global");


            return sb.ToString();
        }
    }
}