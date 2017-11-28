using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apex.ApexSharp.SharpToApex
{
    public class ApexLineGenerator
    {
        public static List<Type> TypeList = new List<Type>()
        {
            new Type("Integer", "int"),
            new Type("String", "string"),
            new Type("Boolean", "bool")
        };

        public static string GetApexTypes(string cSharpType)
        {
            Type type = TypeList.Where(x => x.CSharpType.Equals(cSharpType)).FirstOrDefault();
            return type?.ApexType;
        }

        public static string GetCSharpTypes(string apexType)
        {
            Type type = TypeList.Where(x => x.ApexType.Equals(apexType)).FirstOrDefault();
            return type?.CSharpType;
        }

        public static string ParameterCreater(List<ApexParameterListSyntax> apexParameters)
        {
            var sb = new StringBuilder();


            for (int i = 0; i < apexParameters.Count; i++)
            {
                if (i == 0) sb.Append(apexParameters[i].Type).AppendSpace().Append(apexParameters[i].Identifier);
                else sb.Append(", ").Append(apexParameters[i].Type).AppendSpace().Append(apexParameters[i].Identifier);
            }
            return sb.ToString();
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

            return sb.ToString();
        }

        public static string GetApexLine(string cSharpLine)
        {
            if (cSharpLine.StartsWith("NoApex"))
            {
                cSharpLine = "//" + cSharpLine;

                return cSharpLine;
            }

            if (cSharpLine.Contains("Soql.Query")) cSharpLine = SoqlSelect(cSharpLine);
            else if (cSharpLine.Contains("Soql.Update")) cSharpLine = SoqlUpdate(cSharpLine);
            else if (cSharpLine.Contains("Soql.Upsert")) cSharpLine = SoqlUpdate(cSharpLine);
            else if (cSharpLine.Contains("Soql.Insert")) cSharpLine = SoqlInsert(cSharpLine);
            else if (cSharpLine.Contains("Soql.Delete")) cSharpLine = SoqlDelete(cSharpLine);
            else if (cSharpLine.Contains("Soql.UnDelete")) cSharpLine = SoqlUnDelete(cSharpLine);
            else if (cSharpLine.Contains("JSON.deserialize")) cSharpLine = JsonDeSerialize(cSharpLine);

            cSharpLine = cSharpLine.Replace('\"', '\'');


            return cSharpLine;

        }


        // List<Contact> contacts = Soql.Query<Contact>("SELECT ID, Email, Name FROM Contact WHERE ID = :contactNewId LIMIT 1", new { contactNewId });
        // List<Contact> contacts = [SELECT ID, Email, Name FROM Contact WHERE ID = :contactNewId LIMIT 1];
        public static string SoqlSelect(string cSharpLine)
        {
            var left = cSharpLine.Substring(0, cSharpLine.IndexOf('='));
            var right = cSharpLine.Substring(cSharpLine.IndexOf('"') + 1);
            var newright = right.Substring(0, right.IndexOf('"'));
            var newSoql = left + "= [" + newright + "]";
            return newSoql;
        }

        // Soql.Update(accountList);
        // update accountList;
        public static string SoqlUpdate(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "update " + value;
        }

        // Soql.Upsert(accountList);
        // upsert accountList;
        public static string SoqlUpsert(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "upsert " + value;
        }

        // Soql.Insert(accountList);
        // insert accountList
        public static string SoqlInsert(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "insert " + value;
        }

        // Soql.Delete(accountList);
        // delete accountList
        public static string SoqlDelete(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "delete " + value;
        }

        // Soql.UnDelete(accountList);
        // undelete accountList
        public static string SoqlUnDelete(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "undelete " + value;
        }

        // List<Account> newnewDateTime = JSON.deserialize<List<Account>>(newDateTimeJson);
        // List<Account> newnewDateTime = (List<Account>)JSON.deserialize(newDateTimeJson, List<Account>.class);
        public static string JsonDeSerialize(string cSharpLine)
        {
            var left = cSharpLine.Substring(0, cSharpLine.IndexOf("=", StringComparison.Ordinal) + 1).Trim();
            var right = cSharpLine.Substring(cSharpLine.IndexOf("=", StringComparison.Ordinal) + 1).Trim();

            var index = right.IndexOf("<", StringComparison.Ordinal);
            var lastIndex = right.LastIndexOf(">", StringComparison.Ordinal);
            var jsonType = right.Substring(index + 1, lastIndex - (index + 1));

            var value = right.Split('(', ')')[1];

            var returnString = left + " (" + jsonType + ")JSON.deserialize(" + value + "," + jsonType + ".class)";

            return returnString;
        }
    }
}