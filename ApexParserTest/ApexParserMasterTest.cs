using NUnit.Framework;
using ApexParser.Parser;

namespace ApexParserTest
{
    [TestFixture]
    public class ApexParserMasterTest
    {
        [Test]
        public void TestSalesForceApexSharp() =>
            ParseAll("repos/jayonsoftware/SalesForceApexSharp/contents/src/classes");

        [Test, Ignore("TODO")]
        public void TestFFlibApexCommon() =>
            ParseAll("repos/financialforcedev/fflib-apex-common/contents/fflib/src/classes");

        [Test]
        public void TestFFlibApexMocks() =>
            ParseAll("repos/financialforcedev/fflib-apex-mocks/contents/master/src/classes");

        [Test]
        public void TestFFhttpCore() =>
            ParseAll("repos/financialforcedev/ffhttp-core/contents/master/src/classes");

        [Test]
        public void TestFFlibApexCommonSampleCode() =>
            ParseAll("repos/financialforcedev/fflib-apex-common-samplecode/contents/master/fflib-sample-code/src/classes");

        [Test]
        public void TestCumulus() =>
            ParseAll("repos/SalesforceFoundation/Cumulus/contents/master/src/classes");

        public void ParseAll(string repoURL)
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
