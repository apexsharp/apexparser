using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;
using SalesForceAPI.Attribute;
using SalesForceAPI.Model;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class Db
    {
        private readonly ConnectionDetail _connectionDetail;

        public static readonly NullValueHandling JsonNullValue = NullValueHandling.Ignore;

        public Db(ConnectionDetail connectionDetail)
        {
            _connectionDetail = connectionDetail;
        }


        public async Task<List<T>> Query<T>(string query)
        {
            Console.WriteLine(query);

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v37.0/query/?q=" + query),
                Method = HttpMethod.Get
            };

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            request.Headers.Accept.Add(mediaType);
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);


            HttpClient httpClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                    RecordReadList<T> returnData = JsonConvert.DeserializeObject<RecordReadList<T>>(jsonData,
                        new JsonSerializerSettings { NullValueHandling = JsonNullValue });
                    return returnData.records;
                default:
                    Log.LogMsg("Query Error", responseMessage.Content.ReadAsStringAsync().Result);
                    throw new Exception(responseMessage.Content.ReadAsStringAsync().Result);
            }
        }

        public string GetSalesForceObjectName<T>()
        {
            string objectName = typeof(T).Name;
            ObjectNameAttribute objectNameAttrbute = typeof(T).GetCustomAttributes(typeof(ObjectNameAttribute), true).FirstOrDefault() as ObjectNameAttribute;
            if (objectNameAttrbute != null)
            {
                objectName = objectNameAttrbute.SalesForceObjectName;
            }
            return objectName;
        }


        public async Task<int> Count<T>()
        {
            var objectName = GetSalesForceObjectName<T>();
            string query = "SELECT COUNT(Id) FROM " + objectName;

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v37.0/query/?q=" + query),
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);

            HttpClient httpClient = new HttpClient();

            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;

                    Count returnData = JsonConvert.DeserializeObject<Count>(jsonData,
                        new JsonSerializerSettings { NullValueHandling = JsonNullValue });
                    if (returnData.totalSize > 0)
                    {
                        return returnData.records[0].expr0;
                    }
                    break;
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine("Connection Expired");
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }
            return 0;
        }

        public async Task<T> CreateRecord<T>(T obj) where T : SObject
        {
            var objectName = GetSalesForceObjectName<T>();

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v40.0/sobjects/" + objectName),
                Method = HttpMethod.Post
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);

            var requestJson = JsonFactory.GetJson(obj);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.Created:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(jsonData);

                    RecordCreateResponse recordCreateResponse = JsonConvert.DeserializeObject<RecordCreateResponse>(jsonData);
                    obj.Id = recordCreateResponse.id;
                    return obj;
                case HttpStatusCode.BadRequest:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }
            return default(T);
        }

        public async Task<T> GetSingleRecordByIdAsync<T>(string id)
        {
            string query = new SoqlCreator().GetSoql<T>();
            query = query + " WHERE Id = '" + id + "'";

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v37.0/query/?q=" + query),
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                    RecordReadList<T> returnData = JsonConvert.DeserializeObject<RecordReadList<T>>(jsonData,
                        new JsonSerializerSettings { NullValueHandling = JsonNullValue });
                    System.Collections.Generic.List<T> dataList = returnData.records;
                    if (dataList.Count == 1) return dataList[0];
                    else return default(T);
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }
            return default(T);
        }


        public async Task<bool> UpdateRecord<T>(List<T> objList) where T : SObject
        {
            foreach (var obj in objList)
            {
                await UpdateRecord<T>(obj);
            }
            return true;
        }
        public async Task<bool> UpdateRecord<T>(T obj) where T : SObject
        {
            var objectName = GetSalesForceObjectName<T>();


            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v38.0/sobjects/" + objectName + "/" + obj.Id),
                Method = new HttpMethod("PATCH")
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);




            var requestJson = JsonFactory.GetJsonUpdate(obj);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);


            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return true;
                case HttpStatusCode.BadRequest:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }

            return false;
        }

        public async Task<bool> DeleteRecord<T>(T baseObject) where T : SObject
        {
            var objectName = GetSalesForceObjectName<T>();

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v40.0/sobjects/" + objectName + "/" +
                                     baseObject.Id),
                Method = HttpMethod.Delete
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            Console.WriteLine("Deleted Records " + responseMessage.StatusCode);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return true;
                case HttpStatusCode.BadRequest:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }
            return false;
        }


        public async Task<SalesForceApiLimits> GetApiLimits()
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v37.0/limits/"),
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            // Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                    SalesForceApiLimits returnData = JsonConvert.DeserializeObject<SalesForceApiLimits>(jsonData,
                        new JsonSerializerSettings { NullValueHandling = JsonNullValue });
                    return returnData;
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }
            return null;
        }


        public async Task<System.Collections.Generic.List<T>> GetAllRecordsAsyncLimit<T>(int limit)
        {
            SoqlCreator soql = new SoqlCreator();
            string query = soql.GetSoql<T>();

            query = query + " LIMIT " + limit;
            Console.WriteLine(query);
            return Query<T>(query).Result;
        }

        public async Task<System.Collections.Generic.List<T>> GetAllRecordsAsyncOffset<T>(int offset)
        {
            SoqlCreator soql = new SoqlCreator();
            string query = soql.GetSoql<T>();

            query = query + " OFFSET " + offset;
            Console.WriteLine(query);
            return Query<T>(query).Result;
        }

        public async Task<System.Collections.Generic.List<T>> GetAllRecordsAsync<T>(int limit, int offset)
        {
            SoqlCreator soql = new SoqlCreator();
            string query = soql.GetSoql<T>();

            query = query + " LIMIT " + limit + " OFFSET " + offset;
            return Query<T>(query).Result;
        }

        public async Task<System.Collections.Generic.List<T>> GetAllRecordsAsync<T>()
        {
            System.Collections.Generic.List<T> allRecords = new System.Collections.Generic.List<T>();

            var soql = new SoqlCreator();
            string query = soql.GetSoql<T>();


            query = query.Replace("SmallBannerPhotoUrl,", "");
            query = query.Replace("MediumBannerPhotoUrl,", "");
            query = query.Replace("CreatedBy,", "");
            query = query.Replace("LastModifiedBy,", "");


            //  Console.WriteLine(query);

            RecordReadList<T> result = BigQuery<T>(query).Result;
            allRecords.AddRange(result.records);


            while (result.done == false)
            {
                Console.WriteLine(allRecords.Count + "  OF " + result.totalSize);

                var url = result.nextRecordsUrl;
                url = url.Replace("/services", "");

                var httpManager = new HttpManager();
                var data = await httpManager.Get(_connectionDetail, url);
                result = JsonConvert.DeserializeObject<RecordReadList<T>>(data,
                    new JsonSerializerSettings { NullValueHandling = JsonNullValue });
                allRecords.AddRange(result.records);
            }


            return allRecords;
        }


        private async Task<RecordReadList<T>> BigQuery<T>(string query)
        {
            //   query = query + " LIMIT 1";

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v37.0/query/?q=" + query),
                Method = HttpMethod.Get
            };


            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);


            WebProxy proxy = new WebProxy { Address = new Uri("http://naproxy.gm.com:80") };

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };


            HttpClient httpClient = new HttpClient(httpClientHandler);

            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;

                    RecordReadList<T> returnData = JsonConvert.DeserializeObject<RecordReadList<T>>(jsonData,
                        new JsonSerializerSettings { NullValueHandling = JsonNullValue });
                    return returnData;
                case HttpStatusCode.Unauthorized:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    break;
            }
            return null;
        }


    }
}