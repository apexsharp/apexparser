using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using SalesForceAPI.Model.RestApi;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SalesForceAPI
{
    public class ApexSharpConfig
    {
        public string SalesForceUrl { get; set; }
        public string HttpProxy { get; set; }
        public string SalesForceUserId { get; set; }
        public string SalesForcePassword { get; set; }
        public string SalesForcePasswordToken { get; set; }
        public int SalesForceApiVersion { get; set; }
        public string DirLocationAndFileName { get; set; }
        public string SessionId { get; set; }
        public string RestUrl { get; set; }
        public string RestSessionId { get; set; }
        public DateTime SessionCreationDateTime { get; set; }
    }

    public class ConnectionUtil
    {
        public static ApexSharpConfig ConnectionDetail { get; set; }

        public static ApexSharpConfig GetConnectionDetail()
        {
            return ConnectionDetail;
        }

        public static void Connect(ApexSharpConfig config)
        {
            config.SalesForceUrl = config.SalesForceUrl + "services/Soap/c/" + config.SalesForceApiVersion + ".0/";
            ConnectionDetail = GetNewConnection(config);

            FileInfo saveFileInfo = new FileInfo(config.DirLocationAndFileName);
            string json = JsonConvert.SerializeObject(ConnectionDetail, Formatting.Indented);
            File.WriteAllText(saveFileInfo.FullName, json);
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

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = httpClient.SendAsync(request).Result;

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string xml = responseMessage.Content.ReadAsStringAsync().Result;

                    Serilog.Log.Logger.Information(xml);

                    return xml;
                default:
                    Console.WriteLine("Error on Posting To " + url);
                    Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);
                    return null;
            }
        }
    }
}