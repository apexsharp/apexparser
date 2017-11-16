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


        public static List<T> Query<T>(string soql, object dynamicInput)
        {

            var dynamicType = dynamicInput.GetType();
            // Console.WriteLine(dynamicType.Name);
            // Console.WriteLine(dynamicInput);

            var varName = ":email";

            if (dynamicType.Name == "Int32")
            {

                string intValueInString = Convert.ToString(dynamicInput);
                soql = soql.Replace(varName, " " + intValueInString + " ");
            }
            else if (dynamicType.Name == "String")
            {

                soql = soql.Replace(varName, "'" + dynamicInput + "'");
            }
            else if (dynamicType.Name == "Id")
            {

                soql = soql.Replace(varName, "'" + dynamicInput + "'");
            }
            else
            {
                Console.WriteLine("Soql.Query Missing Type");
            }

            Console.WriteLine(soql);
            return Query<T>(soql);
        }


        public static void Insert<T>(T sObject) where T : SObject
        {
            Db db = new Db(ConnectionUtil.ConnectionDetail);
            Task<T> createRecord = db.CreateRecord<T>(sObject);
            createRecord.Wait();
        }

        public static void Update<T>(List<T> sObjectList) where T : SObject
        {
            Db db = new Db(ConnectionUtil.ConnectionDetail);
            Task<bool> updateRecord = db.UpdateRecord<T>(sObjectList);
            updateRecord.Wait();
        }

        public static void Update<T>(T sObject) where T : SObject
        {
            Db db = new Db(ConnectionUtil.ConnectionDetail);
            Task<bool> updateRecord = db.UpdateRecord<T>(sObject);
            updateRecord.Wait();
        }

        public static void Delete<T>(List<T> sObjectList) where T : SObject
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