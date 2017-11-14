using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using SalesForceAPI.ApexApi;
using SalesForceAPI.Model;
using SalesForceAPI.Model.BulkApi;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class ApexSharpConfig
    {
        public string ApexFileLocation { get; set; }
        public string SalesForceUrl { get; set; }
        public string HttpProxy { get; set; }
        public string SalesForceUserId { get; set; }
        public string SalesForcePassword { get; set; }
        public string SalesForcePasswordToken { get; set; }
        public int SalesForceApiVersion { get; set; }
        public string VisualStudioProjectFile { get; set; }
        public string DirLocationAndFileName { get; set; }
        public LogLevel LogLevel { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string SessionId { get; set; }
        public string RestUrl { get; set; }
        public string RestSessionId { get; set; }
        public DateTime SessionCreationDateTime { get; set; }
    }

    public class ConnectionUtil
    {
        public static ApexSharpConfig ConnectionDetail { get; set; }

        public static void Connect(ApexSharpConfig config)
        {
            config.SalesForceUrl = config.SalesForceUrl + "services/Soap/c/" + config.SalesForceApiVersion + ".0/";
            ConnectionDetail = GetNewConnection(config);

            FileInfo saveFileInfo = new FileInfo(config.DirLocationAndFileName);
            string json = JsonConvert.SerializeObject(ConnectionDetail, Formatting.Indented);
            File.WriteAllText(saveFileInfo.FullName, json);
        }

        public static void Connect(string configLocation)
        {
            FileInfo loadFileInfo = new FileInfo(configLocation);
            string json = File.ReadAllText(loadFileInfo.FullName);
            ConnectionDetail = JsonConvert.DeserializeObject<ApexSharpConfig>(json);
        }



        private static ApexSharpConfig GetNewConnection(ApexSharpConfig config)
        {
            var xml = @"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:enterprise.soap.sforce.com"">
                    <soapenv:Header>
                        <urn:LoginScopeHeader>
                        <urn:organizationId></urn:organizationId>
                        <urn:portalId></urn:portalId>
                        </urn:LoginScopeHeader>
                    </soapenv:Header>
                    <soapenv:Body>
                        <urn:login>
                            <urn:username>" + config.SalesForceUserId + "</urn:username>" +
                            "<urn:password>" + config.SalesForcePassword + config.SalesForcePasswordToken + "</urn:password>" +
                        "</urn:login>" +
                    "</soapenv:Body>" +
                "</soapenv:Envelope>";


            var waitTask = PostLoginTask(config.SalesForceUrl, xml);



            if (waitTask != null)
            {
                Envelope envelope = UtilXml.DeSerilizeFromXML<Envelope>(waitTask);

                var soapIndex = envelope.Body.loginResponse.result.serverUrl.IndexOf(@"/Soap", StringComparison.Ordinal);
                var restUrl = envelope.Body.loginResponse.result.serverUrl.Substring(0, soapIndex);
                var restSessionId = "Bearer " + envelope.Body.loginResponse.result.sessionId;


                config.SalesForceUrl = envelope.Body.loginResponse.result.serverUrl;
                config.SessionId = envelope.Body.loginResponse.result.sessionId;
                config.RestUrl = restUrl;
                config.RestSessionId = restSessionId;
                config.SessionCreationDateTime = DateTime.Now;


                return config;
            }
            else
            {
                return null;
            }
        }

        private static string PostLoginTask(string url, string json)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
                Content = new StringContent(json, Encoding.UTF8, "text/xml")
            };
            request.Headers.Add("SOAPAction", "''");

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = httpClient.SendAsync(request).Result;

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string xml = responseMessage.Content.ReadAsStringAsync().Result;
                    return xml;
                default:
                    Console.WriteLine("Error on Posting To " + url);
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    return null;
            }
        }


        public void SetupProject()
        {
            // string projectDirectoryName = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            // List<string> cShaprFileList = Directory.GetFileSystemEntries(projectDirectoryName, "*.csproj").ToList();
            //   project = new Microsoft.Build.Evaluation.Project(visualStudioProjFile);
        }

        private void AddDirectory(string dirName)
        {
            var project = new Project(dirName);
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
            var project = new Project(dirName);
            var cSharpFileName = dirName + @"\" + fileName;
            var projectItems = project.GetItems("Compile").ToList();

            if (projectItems.Any(x => x.EvaluatedInclude == cSharpFileName) == false)
            {
                project.AddItem("Compile", cSharpFileName);
                project.Save();
            }
        }

        // Get Count
        public int GetSalesForceRecordCount<T>(ApexSharpConfig connectionDetail)
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

        private int _limitNumber, _skipNumer = 0;
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

        public System.Collections.Generic.List<T> GetAllSalesForceRecords<T>(ApexSharpConfig connection)
        {
            Db db = new Db(connection);
            var asyncWait = db.GetAllRecordsAsync<T>();
            asyncWait.Wait();
            return asyncWait.Result;
        }




        public T GetRecordById<T>(ApexSharpConfig connection, string id)
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

    }
}