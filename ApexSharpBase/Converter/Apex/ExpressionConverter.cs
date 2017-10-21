using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharpBase.Ext;
using Microsoft.CodeAnalysis.Text;

namespace ApexSharpBase.Converter.Apex
{
    public class ExpressionConverter
    {
        public static string GetApexLine(SourceText sourceText)
        {
            var line = sourceText.ToString().Trim();
            return GetApexLine(line);
        }

        public static string GetApexLine(string line)
        {
            if (line.StartsWith("NoApex"))
            {
                line = "//" + line;
                return line;
            }

            if (line.Contains("Soql.Query")) line = SoqlSelect(line);
            else if (line.Contains("Soql.Update")) line = SoqlUpdate(line);
            else if (line.Contains("Soql.Upsert")) line = SoqlUpsert(line);
            else if (line.Contains("Soql.Insert")) line = SoqlInsert(line);
            else if (line.Contains("Soql.Delete")) line = SoqlDelete(line);
            else if (line.Contains("Soql.UnDelete")) line = SoqlUnDelete(line);
            else if (line.Contains("JSON.deserialize")) line = JsonDeSerialize(line);

            line = line.Replace('\"', '\'');
            line = TypeConverter(line);
            return line;
        }

        // Make this Smart
        public static string TypeConverter(string line)
        {
            if(line.Contains("int")) return line.Replace("int", "Integer");
            if (line.Contains("string")) return line.Replace("string", "String");
            if (line.Contains("bool")) return line.Replace("int", "Boolean");
            return line;
        }

        // List<Contact> contacts = Soql.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });
        // List<Contact> contacts = [SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1];
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
