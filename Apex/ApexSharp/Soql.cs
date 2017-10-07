using System;
using System.Reflection;
using Apex.System;
using SalesForceAPI;
using SalesForceAPI.Apex;
using Exception = System.Exception;

namespace Apex.ApexSharp
{
    public class Soql
    {
        public static System.List<T> Query<T>(string soql, object dynamicInput)
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

        public static System.List<T> Query<T>(string soql)
        {
            var connectiondetail = ConnectionUtil.GetConnectionDetail();
            Db db = new Db(connectiondetail);

            var asyncWait = db.Query<T>(soql);

            try
            {
                asyncWait.Wait();
            }
            catch (Exception e)
            {
                return new List<T>();
            }

            System.List<T> dataList = new System.List<T>();
            foreach (var record in asyncWait.Result)
            {
                dataList.Add(record);
            }
            return dataList;
        }

        public static T QuerySingle<T>(string soql, object expando)
        {
            return QuerySingle<T>(soql);
        }

        public static T QuerySingle<T>(string soql)
        {
            Db db = new Db(ConnectionUtil.GetConnectionDetail());

            var asyncWait = db.Query<T>(soql);
            asyncWait.Wait();
            var result = (global::System.Collections.Generic.List<T>)asyncWait.Result;


            System.List<T> dataList = new System.List<T>();

            foreach (var record in result)
            {
                dataList.Add(record);
            }
            return dataList[0];
        }

        public static void Insert<T>(T sObject) where T : SObject
        {
            Db db = new Db(ConnectionUtil.GetConnectionDetail());
            global::System.Threading.Tasks.Task<T> createRecord = db.CreateRecord<T>(sObject);
            createRecord.Wait();
            Console.WriteLine(createRecord.Result.Id);
        }

        public static void Update<T>(List<T> sObjectList) where T : SObject
        {

            global::System.Collections.Generic.List<T> sObjects = new global::System.Collections.Generic.List<T>();

            foreach (var obj in sObjectList)
            {
                sObjects.Add(obj);
            }


            Db db = new Db(ConnectionUtil.GetConnectionDetail());
            global::System.Threading.Tasks.Task<bool> updateRecord = db.UpdateRecord<T>(sObjects);
            updateRecord.Wait();
            Console.WriteLine(updateRecord.Result);
        }

        public static void Update<T>(T sObject) where T : SObject
        {
            Db db = new Db(ConnectionUtil.GetConnectionDetail());
            global::System.Threading.Tasks.Task<bool> updateRecord = db.UpdateRecord<T>(sObject);
            updateRecord.Wait();
            Console.WriteLine(updateRecord.Result);
        }


        public static void Delete<T>(List<T> sObjectList) where T : SObject
        {

            global::System.Collections.Generic.List<T> sObjects = new global::System.Collections.Generic.List<T>();

            foreach (var obj in sObjectList)
            {
                sObjects.Add(obj);

                Db db = new Db(ConnectionUtil.GetConnectionDetail());
                global::System.Threading.Tasks.Task<bool> deleteRecord = db.DeleteRecord<T>(obj);
                deleteRecord.Wait();
                Console.WriteLine(deleteRecord.Result);
            }



        }

    }
}