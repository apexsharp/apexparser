using Apex.System;

namespace Apex.ApexSharp
{
    using SalesForceAPI;
    using SalesForceAPI.ApexApi;

    public class Soql
    {
        public static SoqlQuery<T> Query<T>(string soql, params object[] parameters)
        {
            return SoqlApi.Query<T>(soql, parameters);
        }

        public static SoqlQuery<T> Query<T>(string soql)
        {
            return SoqlApi.Query<T>(soql);
        }

        public static void Insert<T>(T sObject) where T : SObject
        {
            SoqlApi.Insert(sObject);
        }

        public static void Update<T>(T sObject) where T : SObject
        {
            SoqlApi.Update(sObject);
        }

        public static void Delete<T>(T sObject) where T : SObject
        {
            SoqlApi.Delete(sObject);
        }
    }
}