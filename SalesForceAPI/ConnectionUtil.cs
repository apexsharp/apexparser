using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using SalesForceAPI.ApexApi;
using SalesForceAPI.Model;
using SalesForceAPI.Model.BulkApi;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class ConnectionUtil
    {
        private static Project project = null;

        private int _limitNumber;
        private string _salesForcePassword;
        private string _salesForcePasswordToken;
        private string _salesForceUrl;
        private string _salesForceUserId;
        private int _skipNumer;

        private string _sqlConnectionString;

        public bool debug = true;

        // SF Conneciton 


             
        // SF Connection Details
        private static ConnectionDetail ConnectionDetail;
        public static ConnectionDetail GetConnectionDetail()
        {
            return ConnectionDetail;
        }

        public static void Connect(string SalesForceUrl, string SalesForceUserId, string SalesForcePassword, string SalesForcePasswordToken, string visualStudioProjFile)
        {
            // string projectDirectoryName = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            // List<string> cShaprFileList = Directory.GetFileSystemEntries(projectDirectoryName, "*.csproj").ToList();

            ConnectionDetail = LogIn.Connect(SalesForceUrl, SalesForceUserId, SalesForcePassword + SalesForcePasswordToken);
            Log.LogMsg("Connection Detail", ConnectionDetail);

   
         //   project = new Microsoft.Build.Evaluation.Project(visualStudioProjFile);

            
        }



        public bool ConnectToDb()
        {
            try
            {
                using (var connection = new SqlConnection(_sqlConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected To Database " + connection.Database);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false; // any error is considered as db connection error for now
            }
        }




        private void AddDirectory(string dirName)
        {
            Directory.CreateDirectory(project.DirectoryPath + @"\Model\");
            dirName = dirName + @"\";
            var projectItems = project.GetItems("Folder").ToList();
            if (projectItems.Any(x => x.EvaluatedInclude == dirName) == false)
            {
                project.AddItem("Folder", dirName);
                project.Save();
            }
        }


        //    AddCShaprFile("Model", "Demo.cs");
        public static void AddCShaprFile(string dirName, string fileName)
        {
            var cSharpFileName = dirName + @"\" + fileName;
            var projectItems = project.GetItems("Compile").ToList();

            if (projectItems.Any(x => x.EvaluatedInclude == cSharpFileName) == false)
            {
                project.AddItem("Compile", cSharpFileName);
                project.Save();
            }
        }

        // Get Count
        public int GetSalesForceRecordCount<T>(ConnectionDetail connectionDetail)
        {
            Db db = new Db(connectionDetail);
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

        public System.Collections.Generic.List<T> ToList<T>()
        {
            Db db = new Db(ConnectionDetail);

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

        public ConnectionUtil Limit(int limitNumber)
        {
            _limitNumber = limitNumber;
            return this;
        }

        public ConnectionUtil Offset(int skipNumber)
        {
            _skipNumer = skipNumber;
            return this;
        }

        public System.Collections.Generic.List<T> GetAllSalesForceRecords<T>(ConnectionDetail connection)
        {
            Db db = new Db(connection);
            var asyncWait = db.GetAllRecordsAsync<T>();
            asyncWait.Wait();
            return asyncWait.Result;
        }


        public System.Collections.Generic.List<T> GetSalesForceRecords<T>(ConnectionDetail connection, int offset,
            int limit)
        {
            Db db = new Db(connection);
            var asyncWait = db.GetAllRecordsAsync<T>(limit, offset);
            asyncWait.Wait();
            return asyncWait.Result;
        }


        public T GetRecordById<T>(ConnectionDetail connection, string id)
        {
            Db db = new Db(connection);

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
            BulkApi api = new BulkApi(ConnectionDetail);
            return api.BulkRequest<T>(checkIntervel);
        }


        public BulkInsertReply BulkInsert<T>(System.Collections.Generic.List<T> dataList) where T : SObject
        {
            // ToDo limit to 200 Exception 
            BulkInsertRequest<T> request = new BulkInsertRequest<T> { Records = new T[dataList.Count] };
            request.Records = dataList.ToArray();

            BulkApi api = new BulkApi(ConnectionDetail);
            var replyTask = api.CreateRecordBulk<T>(request);
            replyTask.Wait();
            return replyTask.Result;
        }

        // Get the API Limits
        public int GetRemainingApiLimit(ConnectionDetail connectionDetail, LimitType limitType)
        {
            Limits limits = new Limits(connectionDetail);
            return limits.GetRemainingApiLimit(limitType);
        }

        public int GetDailyApiLimit(ConnectionDetail connectionDetail, LimitType limitType)
        {
            Limits limits = new Limits(connectionDetail);
            return limits.GetDailyApiLimit(limitType);
        }
    }
}