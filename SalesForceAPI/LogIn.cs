using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesForceAPI.Model;
using SalesForceAPI.Model.RestApi;

namespace SalesForceAPI
{
    public class LogIn
    {
        /**
         * Get the SF Session and Save. If the saved session is more then 2 hours old then get a new session
         */
        public ConnectionDetail Connect(string url, string userId, string password)
        {
            Dictionary<string, ConnectionDetail> conectionDetails = new Dictionary<string, ConnectionDetail>();

            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            FileInfo configFileInfo = new FileInfo(path + @"\confi1g.json");

            if (configFileInfo.Exists)
            {
                var connectionDetailJson = File.ReadAllText(configFileInfo.FullName);
                conectionDetails = JsonConvert.DeserializeObject<Dictionary<string, ConnectionDetail>>(connectionDetailJson);

                ConnectionDetail connection;
                if (conectionDetails.TryGetValue(userId, out connection))
                {
                    if (connection.SessionCreationDateTime.AddHours(2) > DateTime.Now)
                    {
                        Console.WriteLine("Session Found For " + userId);
                        connection.Message = "OK";
                        return connection;
                    }
                }
            }

            //Console.WriteLine("Session Expired or not found, Obtaining a new session for " + userId);

            var newConectionDetails = GetNewConnection(url, userId, password);

            if (newConectionDetails != null)
            {
                if (configFileInfo.Exists)
                {
                    var connectionDetailJson = File.ReadAllText(configFileInfo.FullName);
                    conectionDetails =
                        JsonConvert.DeserializeObject<Dictionary<string, ConnectionDetail>>(connectionDetailJson);
                }

                newConectionDetails.UserId = userId;
                conectionDetails[userId] = newConectionDetails;

                var newConnectionDetailJson = JsonConvert.SerializeObject(conectionDetails, Formatting.Indented);
                File.WriteAllText(path + @"\config.json", newConnectionDetailJson);

                newConectionDetails.Message = "OK";

                return newConectionDetails;
            }
            else
            {
                newConectionDetails = new ConnectionDetail();
                newConectionDetails.Message = "Error";
                return newConectionDetails;
            }
        }

        private ConnectionDetail GetNewConnection(string url, string userId, string password)
        {
            var xml =
                @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:enterprise.soap.sforce.com"">
                            <soapenv:Header>
                                <urn:LoginScopeHeader>
                                <urn:organizationId></urn:organizationId>
                                <urn:portalId></urn:portalId>
                                </urn:LoginScopeHeader>
                            </soapenv:Header>
                                <soapenv:Body>
                                    <urn:login>
                                        <urn:username>" + userId + "</urn:username>" +
                "<urn:password>" + password + "</urn:password>" +
                "</urn:login>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope>";


            var waitTask = PostLoginTask(url, xml);
            waitTask.Wait();


            if (waitTask.Result != null)
            {
                Envelope envelope = UtilXml.DeSerilizeFromXML<Envelope>(waitTask.Result);

                var soapIndex =
                    envelope.Body.loginResponse.result.serverUrl.IndexOf(@"/Soap", StringComparison.Ordinal);
                var restUrl = envelope.Body.loginResponse.result.serverUrl.Substring(0, soapIndex);
                var restSessionId = "Bearer " + envelope.Body.loginResponse.result.sessionId;

                var connectDetail = new ConnectionDetail()
                {
                    Url = envelope.Body.loginResponse.result.serverUrl,
                    SessionId = envelope.Body.loginResponse.result.sessionId,
                    RestUrl = restUrl,
                    RestSessionId = restSessionId,
                    SessionCreationDateTime = DateTime.Now
                };

                return connectDetail;
            }
            else
            {
                return null;
            }
        }

        private async Task<string> PostLoginTask(string url, string json)
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
            HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

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
    }
}