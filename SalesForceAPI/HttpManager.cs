using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SalesForceAPI.Model;
using Console = Colorful.Console;

namespace SalesForceAPI
{
    public class HttpManager
    {
        public async Task<string> Get(ApexSharpConfig connectionDetail, string endPoint)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(connectionDetail.RestUrl + endPoint),
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Authorization", connectionDetail.RestSessionId);


            //     Console.WriteLine(request.RequestUri);

            //   WebProxy proxy = new WebProxy { Address = new Uri("http://naproxy.gm.com:80") };

            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                //     Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpClient httpClient = new HttpClient(httpClientHandler);

            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                    return jsonData;
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