using Apex.System;

namespace Apex.ApexSharp
{
    using SalesForceAPI;
    using SalesForceAPI.ApexApi;

    public class Soql
    {
        public static List<T> Query<T>(string soql, object dynamicInput)
        {

            return ConvertList(SoqlApi.Query<T>(soql, dynamicInput));
        }

        public static List<T> Query<T>(string soql)
        {
            return ConvertList(SoqlApi.Query<T>(soql));
        }

        public static T QuerySingle<T>(string soql)
        {

            List<T> dataList = ConvertList(SoqlApi.Query<T>(soql));
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

            SoqlApi.Insert<T>(sObject);
        }

        public static void Update<T>(List<T> sObjectList) where T : SObject
        {

            SoqlApi.Update<T>(sObjectList);
        }

        public static void Update<T>(T sObject) where T : SObject
        {

            SoqlApi.Update<T>(sObject);
        }

        public static void Delete<T>(List<T> sObjectList) where T : SObject
        {

            SoqlApi.Delete<T>(sObjectList);
        }
    }
}