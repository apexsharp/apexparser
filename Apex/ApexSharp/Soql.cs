using System;
using System.Reflection;
using Apex.System;
using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;


namespace Apex.ApexSharp
{
    public class SOQL
    {
        public static List<T> Query<T>(string soql, object dynamicInput)
        {
            var dynamicType = dynamicInput.GetType();
            PropertyInfo[] pi = dynamicType.GetProperties();

            foreach (PropertyInfo p in pi)
            {
                var varName = ":" + p.Name + " ";

                if (p.PropertyType.Name == "Int32")
                {
                    int intValue = (int)p.GetValue(dynamicInput);
                    string intValueInString = Convert.ToString(intValue);
                    soql = soql.Replace(varName, " " + intValueInString + " ");
                }
                else if (p.PropertyType.Name == "String")
                {
                    string stringValue = (string)p.GetValue(dynamicInput);
                    soql = soql.Replace(varName, " '" + stringValue + "' ");
                }
                else if (p.PropertyType.Name == "Id")
                {
                    Id id = (Id)p.GetValue(dynamicInput);
                    string stringValue = id.ToString();
                    soql = soql.Replace(varName, " '" + stringValue + "' ");
                }
                else
                {
                    Console.WriteLine("Soql.Query Missing Type");
                }
            }
            return Query<T>(soql);
        }

        public static List<T> Query<T>(string soql)
        {
            SoqlApi api = new SoqlApi();

            global::System.Collections.Generic.List<T> result = api.Query<T>(soql);

            List<T> dataList = new List<T>();

            foreach (var record in result)
            {
                dataList.Add(record);
            }

            return dataList;
        }

        public static void Insert<T>(T sObject) where T : SObject
        {
            SoqlApi api = new SoqlApi();
            api.Insert<T>(sObject);
        }

        public static void Update<T>(List<T> sObjectList) where T : SObject
        {
            SoqlApi api = new SoqlApi();
            api.Update<T>(sObjectList);
        }

        public static void Update<T>(T sObject) where T : SObject
        {
            SoqlApi api = new SoqlApi();
            api.Update<T>(sObject);
        }


        public static void Delete<T>(List<T> sObjectList) where T : SObject
        {
            SoqlApi api = new SoqlApi();
            api.Delete<T>(sObjectList);
        }

    }
}