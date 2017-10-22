using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RestSharp;

namespace ApexParserTest
{
#pragma warning disable IDE1006 // Naming Styles

    public class GitHubFile
    {
        public string Name { get; set; }
        public string download_url { get; set; }
    }

#pragma warning restore IDE1006 // Naming Styles

    public class GitHubHelper
    {
        public static Dictionary<string, string> GetCodeFromGitFolder(string gitResource, string extension)
        {
            Dictionary<string, string> codeFromGit = new Dictionary<string, string>();

            var client = new RestClient("https://api.github.com/");
            var request = new RestRequest(gitResource, Method.GET);
            var response = client.Execute<List<GitHubFile>>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Assert.Warn($"Cannot download the code from url: {gitResource}. Error code: {response.StatusCode}");
            }

            List<GitHubFile> newFilteredList = response.Data.Where(x => x?.Name?.EndsWith(extension) ?? false).ToList();

            foreach (var gitHubFile in newFilteredList)
            {
                client = new RestClient(gitHubFile.download_url);
                request = new RestRequest(Method.GET);
                var code = client.Execute(request).Content;

                codeFromGit.Add(gitHubFile.download_url, code);
            }

            return codeFromGit;
        }
    }
}