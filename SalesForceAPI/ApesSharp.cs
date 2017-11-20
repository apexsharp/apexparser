using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SalesForceAPI.ApexApi;
using SalesForceAPI.Model.BulkApi;

namespace SalesForceAPI
{
    public class ApexSharp
    {
        public ApexSharpConfig ApexSharpConfigSettings = new ApexSharpConfig();

        public void Connect()
        {
            ConnectionUtil.Connect(ApexSharpConfigSettings);
        }


        public void Connect(string configLocation)
        {
            FileInfo loadFileInfo = new FileInfo(configLocation);
            string json = File.ReadAllText(loadFileInfo.FullName);
            ApexSharpConfigSettings = JsonConvert.DeserializeObject<ApexSharpConfig>(json);
            Connect();
        }


        public void CreateOfflineClasses(string sObjectName)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            ModelGen modelGen = new ModelGen();
            modelGen.CreateOfflineSymbolTable(sObjectName, path);
        }


        public int GetSalesForceRecordCount<T>()
        {
            Db db = new Db();
            var asyncWait = db.Count<T>();
            try
            {
                asyncWait.Wait();
                return asyncWait.Result;
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e);
            }
            // ToDo : Exception
            return 0;
        }

        private int _limitNumber, _skipNumer = 0;
        public System.Collections.Generic.List<T> ToList<T>()
        {
            Db db = new Db();

            if (_limitNumber > 0 && _skipNumer > 0)
            {
                var asyncWait = db.GetAllRecordsAsync<T>(_limitNumber, _skipNumer);
                asyncWait.Wait();
                return asyncWait.Result;
            }
            else if (_limitNumber > 0)
            {
                var asyncWait = db.GetAllRecordsAsyncLimit<T>(_limitNumber);
                asyncWait.Wait();
                return asyncWait.Result;
            }
            else if (_skipNumer > 0)
            {
                var asyncWait = db.GetAllRecordsAsyncOffset<T>(_skipNumer);
                asyncWait.Wait();
                return asyncWait.Result;
            }
            else
            {
                var asyncWait = db.GetAllRecordsAsync<T>();
                asyncWait.Wait();
                return asyncWait.Result;
            }
        }


        public System.Collections.Generic.List<T> GetAllSalesForceRecords<T>()
        {
            Db db = new Db();
            var asyncWait = db.GetAllRecordsAsync<T>();
            asyncWait.Wait();
            return asyncWait.Result;
        }

        public T GetRecordById<T>(string id)
        {
            Db db = new Db();

            var asyncWait = db.GetSingleRecordByIdAsync<T>(id);
            asyncWait.Wait();

            return asyncWait.Result;
        }


        //public T CreateOrUpdateRecord<T>(ConnectionDetail connection, T data) where T : BaseObject
        //{
        //    Db db = new Db(connection);

        //    if (data.Id == null)
        //    {
        //        var waitForInsert = db.CreateRecord(data);
        //        waitForInsert.Wait();
        //        return waitForInsert.Result;
        //    }

        //    Regex regex = new Regex(@"[a-zA-Z0-9]{18}");
        //    var match = regex.Match(data.Id);

        //    if (match.Success)
        //    {
        //        var waitForInsert = db.UpdateRecord(data);
        //        waitForInsert.Wait();
        //        return waitForInsert.Result;

        //    }
        //    else
        //    {
        //        var waitForInsert = db.CreateRecord(data);
        //        waitForInsert.Wait();
        //        return waitForInsert.Result;
        //    }
        //}

        public string BulkRequest<T>(int checkIntervel)
        {
            BulkApi api = new BulkApi(ConnectionUtil.GetConnectionDetail());
            return api.BulkRequest<T>(checkIntervel);
        }

        public BulkInsertReply BulkInsert<T>(System.Collections.Generic.List<T> dataList) where T : SObject
        {
            // ToDo limit to 200 Exception 
            BulkInsertRequest<T> request = new BulkInsertRequest<T> { Records = new T[dataList.Count] };
            request.Records = dataList.ToArray();

            BulkApi api = new BulkApi(ConnectionUtil.GetConnectionDetail());
            var replyTask = api.CreateRecordBulk<T>(request);
            replyTask.Wait();
            return replyTask.Result;
        }






        public ApexSharp SaveApexSharpConfigAs(string dirLocationAndFileName)
        {
            ApexSharpConfigSettings.DirLocationAndFileName = dirLocationAndFileName;
            return this;
        }

        public ApexSharp SalesForceUrl(string salesForceUrl)
        {
            ApexSharpConfigSettings.SalesForceUrl = salesForceUrl;
            return this;
        }

        public ApexSharp WithUserId(string salesForceUserId)
        {
            ApexSharpConfigSettings.SalesForceUserId = salesForceUserId;
            return this;
        }

        public ApexSharp AndPassword(string salesForcePassword)
        {
            ApexSharpConfigSettings.SalesForcePassword = salesForcePassword;
            return this;
        }

        public ApexSharp AndToken(string salesForcePasswordToken)
        {
            ApexSharpConfigSettings.SalesForcePasswordToken = salesForcePasswordToken;
            return this;
        }
        public ApexSharp AndSalesForceApiVersion(int apiVersion)
        {
            ApexSharpConfigSettings.SalesForceApiVersion = apiVersion;
            return this;
        }
        public ApexSharp AddHttpProxy(string httpProxy)
        {
            ApexSharpConfigSettings.HttpProxy = httpProxy;
            return this;
        }
    }
}