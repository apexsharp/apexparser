using Apex.System;
using SalesForceAPI.ApexApi;


namespace Apex.ApexSharp
{
    public class SOQL
    {
        public static List<T> Query<T>(string soql, object dynamicInput)
        {
            SoqlApi api = new SoqlApi();

            global::System.Collections.Generic.List<T> result = api.Query<T>(soql, dynamicInput);

            List<T> dataList = new List<T>();

            foreach (var record in result)
            {
                dataList.Add(record);
            }

            return dataList;
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