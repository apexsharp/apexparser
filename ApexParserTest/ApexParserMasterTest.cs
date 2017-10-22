using NUnit.Framework;
using ApexParser.Parser;

namespace ApexParserTest
{
    [TestFixture]
    public class ApexParserMasterTest
    {
        [Test][Ignore("This Fails")]
        public void TestRemoteApexFile()
        {
            TestRemoteApexFile("repos/jayonsoftware/SalesForceApexSharp/contents/src/classes");
            TestRemoteApexFile("repos/financialforcedev/fflib-apex-common/contents/fflib/src/classes");
            TestRemoteApexFile("repos/financialforcedev/fflib-apex-mocks/contents/master/src/classes");
            TestRemoteApexFile("repos/financialforcedev/ffhttp-core/contents/master/src/classes");
            TestRemoteApexFile("repos/financialforcedev/fflib-apex-common-samplecode/contents/master/fflib-sample-code/src/classes");
            TestRemoteApexFile("repos/SalesforceFoundation/Cumulus/contents/master/src/classes");
        }
        
        public void TestRemoteApexFile(string repoURL)
        {
            var apexFiles = GitHubHelper.GetCodeFromGitFolder(repoURL, ".cls");

            // Process all Apex files, don't stop after the first error
            Assert.Multiple(() =>
            {
                foreach (var apexFile in apexFiles)
                {
                    // Report failing file names along with the parse exception
                    Assert.DoesNotThrow(() => Apex.ParseFile(apexFile.Value), $"Parsing failure on file: {apexFile.Key}");
                }
            });
        }
    }
}
