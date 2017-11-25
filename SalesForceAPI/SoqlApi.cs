using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;
using Serilog;

namespace SalesForceAPI
{

    public class SoqlApi
    {
        public static List<T> Query<T>(string soql, params object[] param)
        {
            var newSoql = ConvertSoql(soql, param);
            return Query<T>(newSoql);
        }

        public static List<T> Query<T>() where T : SObject
        {
            SoqlCreator soqlCreator = new SoqlCreator();
            var soql = soqlCreator.GetSoql<T>();

            Db db = new Db();
            return db.Query<T>(soql);
        }

        public static List<T> Query<T>(string soql)
        {
            Db db = new Db();
            return db.Query<T>(soql);
        }

        public static string ConvertSoql(string soql, params object[] param)
        {
            var matches = Regex.Matches(soql, "(\\:\\S+)");
            if (matches.Count == param.Length)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    if (param[i].GetType().Name == "Int32")
                    {
                        soql = soql.Replace(matches[i].Value, " " + param[i] + " ");
                    }
                    else
                    {
                        soql = soql.Replace(matches[i].Value, "'" + param[i] + "'");
                    }
                }
            }
            else
            {
                Log.Logger.Error("Fail in ConvertSoql");
            }
            return soql;
        }

        public static void Insert<T>(T sObject) where T : SObject
        {
            Db db = new Db();
            Task<T> createRecord = db.CreateRecord<T>(sObject);
            createRecord.Wait();
        }

        public static void Update<T>(List<T> sObjectList) where T : SObject
        {
            Db db = new Db();
            Task<bool> updateRecord = db.UpdateRecord<T>(sObjectList);
            updateRecord.Wait();
        }

        public static void Update<T>(T sObject) where T : SObject
        {
            Db db = new Db();
            Task<bool> updateRecord = db.UpdateRecord<T>(sObject);
            updateRecord.Wait();
        }

        public static void Delete<T>(List<T> sObjectList) where T : SObject
        {
            foreach (var obj in sObjectList)
            {
                Db db = new Db();
                global::System.Threading.Tasks.Task<bool> deleteRecord = db.DeleteRecord<T>(obj);
                deleteRecord.Wait();
            }
        }
    }
}