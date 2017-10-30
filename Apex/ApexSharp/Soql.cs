using Apex.System;
using SalesForceAPI;
using SalesForceAPI.ApexApi;

namespace Apex.ApexSharp
{
    public class Soql
    {
        public static List<T> Query<T>(string soql, object dynamicInput)
        {
            SoqlApi api = new SoqlApi();
            return ConvertList(api.Query<T>(soql, dynamicInput));
        }

        public static List<T> Query<T>(string soql)
        {
            SoqlApi api = new SoqlApi();
            return ConvertList(api.Query<T>(soql));        
        }

        public static T QuerySingle<T>(string soql)
        {
            SoqlApi api = new SoqlApi();
            List<T> dataList = ConvertList(api.Query<T>(soql));
            return dataList[0];
        }


        private static List<T> ConvertList<T>(global::System.Collections.Generic.List<T> result)
        {
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