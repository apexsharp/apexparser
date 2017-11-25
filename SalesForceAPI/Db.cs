using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;
using SalesForceAPI.Attribute;
using SalesForceAPI.Model;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class Db
    {
        private readonly ApexSharpConfig _connectionDetail;

        public static readonly NullValueHandling JsonNullValue = NullValueHandling.Ignore;

        public Db()
        {
            _connectionDetail = ConnectionUtil.GetSession();
        }


        public List<T> Query<T>(string query)
        {
            Serilog.Log.Information(query);

            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(_connectionDetail.RestUrl + "/data/v40.0/query/?q=" + query),
                Method = HttpMethod.Get
            };

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            request.Headers.Accept.Add(mediaType);
            request.Headers.Add("Authorization", _connectionDetail.RestSessionId);


            HttpClient httpClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpResponseMessage responseMessage = httpClient.SendAsync(request).Result;
            string jsonData = responseMessage.Content.ReadAsStringAsync().Result;

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:



                    RecordReadList<T> returnData = JsonConvert.DeserializeObject<RecordReadList<T>>(jsonData,
                        new JsonSerializerSettings { NullValueHandling = JsonNullValue });


                    string jsonFormatted = JToken.Parse(jsonData).ToString(Formatting.Indented);
                    Serilog.Log.Information(jsonFormatted);

                    return returnData.records;
                default:
                    Serilog.Log.Error(jsonData);
                    return new List<T>();
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

    }
}