using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using SalesForceAPI.Model.RestApi;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Serilog;

namespace SalesForceAPI
{
    public class ConnectionUtil
    {
        public static ApexSharpConfig GetConnectionDetail()
        {

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.ColoredConsole()
            .CreateLogger();

            FileInfo loadFileInfo = AppSetting.GetConfiLocation();

            if (loadFileInfo.Exists)
            {

                string json = File.ReadAllText(loadFileInfo.FullName);
                ApexSharpConfig config = JsonConvert.DeserializeObject<ApexSharpConfig>(json);
                return config;
            }
            else
            {
                throw new SalesForceLoginException("Error in JSON");
            }



        }


        public static bool Connect(ApexSharpConfig config)
        {
            config.SalesForceUrl = config.SalesForceUrl + "services/Soap/c/" + config.SalesForceApiVersion + ".0/";

            var connected = GetNewConnection(config);

            FileInfo saveFileInfo = AppSetting.GetConfiLocation();
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(saveFileInfo.FullName, json);

            Log.Information(json);

            return connected;

        }


        private static bool GetNewConnection(ApexSharpConfig config)
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


            var retrunXml = PostLoginTask(config.SalesForceUrl, xml);

            if (retrunXml.Contains("INVALID_LOGIN"))
            {
                throw new SalesForceLoginException(retrunXml);
            }

            Envelope envelope = UtilXml.DeSerilizeFromXML<Envelope>(retrunXml);

            var soapIndex = envelope.Body.loginResponse.result.serverUrl.IndexOf(@"/Soap", StringComparison.Ordinal);
            var restUrl = envelope.Body.loginResponse.result.serverUrl.Substring(0, soapIndex);
            var restSessionId = "Bearer " + envelope.Body.loginResponse.result.sessionId;


            config.SalesForceUrl = envelope.Body.loginResponse.result.serverUrl;
            config.SessionId = envelope.Body.loginResponse.result.sessionId;
            config.RestUrl = restUrl;
            config.RestSessionId = restSessionId;
            config.SessionCreationDateTime = DateTime.Now.AddSeconds(envelope.Body.loginResponse.result.userInfo.sessionSecondsValid);
            return true;
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

            string xml = responseMessage.Content.ReadAsStringAsync().Result;
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    Log.Logger.Information(xml);
                    return xml;
                default:
                    Log.Logger.Error(xml);
                    return xml;
            }
        }
    }
}