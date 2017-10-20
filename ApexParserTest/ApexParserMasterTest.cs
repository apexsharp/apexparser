using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using ApexParser.Toolbox;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;

namespace ApexParserTest
{
#pragma warning disable IDE1006 // Naming Styles

    public class GitHubFile
    {
        public string name { get; set; }
        public string path { get; set; }
        public string sha { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string git_url { get; set; }
        public string download_url { get; set; }
        public string type { get; set; }
        public Links _links { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string git { get; set; }
        public string html { get; set; }
    }

#pragma warning restore IDE1006 // Naming Styles

    [TestFixture]
    public class ApexParserMasterTest
    {
        [Test, Ignore("Temporarily ignored to enable the Appveyor builds")]
        public void TestRemoteApexFile()
        {
            var endPoint = "https://api.github.com/";
            var resource = "repos/jayonsoftware/SalesForceApexSharp/contents/src/classes";

            var client = new RestClient(endPoint);
            var request = new RestRequest(resource, Method.GET);
            var response = client.Execute<List<GitHubFile>>(request);

            List<GitHubFile> newFilteredList = response.Data.Where(x => x.name.Contains(".cls-meta.xml") == false).ToList();

            foreach (var gitHubFile in newFilteredList)
            {
                client = new RestClient(gitHubFile.download_url);
                request = new RestRequest(Method.GET);
                var apexCode = client.Execute(request).Content;

                try
                {
                    ApexGrammar apex = new ApexGrammar();
                    var cd = apex.ClassDeclaration.ParseEx(apexCode);
                }
                catch (ParseExceptionCustom ex)
                {
                    Console.WriteLine(gitHubFile.name);
                    Assert.NotNull(ex);
                    Assert.False(ex.Message.Contains("Parsing failure:"), ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
