using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;

namespace SalesForceAPI
{
    public class SoqlApi
    {
        public List<T> Query<T>(string soql, object dynamicInput)
        {
            soql = GetFormatedSoql(soql, dynamicInput);
            return Query<T>(soql);
        }

        public static string GetFormatedSoql(string soql, object dynamicInput)
        {
            var dynamicType = dynamicInput.GetType();
            PropertyInfo[] pi = dynamicType.GetProperties();

            foreach (PropertyInfo p in pi)
            {
                Console.WriteLine(p.PropertyType.Name);
                Console.WriteLine(p.Name);

                var varName = ":" + p.Name + " ";

                if (p.PropertyType.Name == "Int32")
                {
                    int intValue = (int) p.GetValue(dynamicInput);
                    string intValueInString = Convert.ToString(intValue);
                    soql = soql.Replace(varName, " " + intValueInString + " ");
                }
                else if (p.PropertyType.Name == "String")
                {
                    string stringValue = (string) p.GetValue(dynamicInput);
                    soql = soql.Replace(varName, " '" + stringValue + "' ");
                }
                else if (p.PropertyType.Name == "Id")
                {
                    Id id = (Id) p.GetValue(dynamicInput);
                    string stringValue = id.ToString();
                    soql = soql.Replace(varName, " '" + stringValue + "' ");
                }
                else
                {
                    Console.WriteLine("Soql.Query Missing Type");
                }
            }
            return soql;
        }

        public List<T> Query<T>(string soql)
        {
            Db db = new Db();
            return db.Query<T>(soql);        
        }


        public void Insert<T>(T sObject) where T : SObject
        {
            Db db = new Db(ConnectionUtil.ConnectionDetail);
            Task<T> createRecord = db.CreateRecord<T>(sObject);
            createRecord.Wait();
        }

        public void Update<T>(List<T> sObjectList) where T : SObject
        {
            Db db = new Db(ConnectionUtil.ConnectionDetail);
            Task<bool> updateRecord = db.UpdateRecord<T>(sObjectList);
            updateRecord.Wait();
        }

        public void Update<T>(T sObject) where T : SObject
        {
            Db db = new Db(ConnectionUtil.ConnectionDetail);
            Task<bool> updateRecord = db.UpdateRecord<T>(sObject);
            updateRecord.Wait();
        }

        public void Delete<T>(List<T> sObjectList) where T : SObject
        {
            foreach (var obj in sObjectList)
            {
                Db db = new Db(ConnectionUtil.ConnectionDetail);
                global::System.Threading.Tasks.Task<bool> deleteRecord = db.DeleteRecord<T>(obj);
                deleteRecord.Wait();
            }
        }

    }
}