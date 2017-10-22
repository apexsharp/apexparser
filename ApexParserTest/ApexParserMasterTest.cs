using NUnit.Framework;
using ApexParser.Parser;
using System;

namespace ApexParserTest
{
    [TestFixture]
    public class ApexParserMasterTest
    {
        [Test, Ignore("Violates Github API limits")]
        public void TestSalesForceApexSharp() =>
            ParseAll("repos/jayonsoftware/SalesForceApexSharp/contents/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestFFlibApexCommon() =>
            ParseAll("repos/financialforcedev/fflib-apex-common/contents/fflib/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestFFlibApexMocks() =>
            ParseAll("repos/financialforcedev/fflib-apex-mocks/contents/master/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestFFhttpCore() =>
            ParseAll("repos/financialforcedev/ffhttp-core/contents/master/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestFFlibApexCommonSampleCode() =>
            ParseAll("repos/financialforcedev/fflib-apex-common-samplecode/contents/master/fflib-sample-code/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestCumulus() =>
            ParseAll("repos/SalesforceFoundation/Cumulus/contents/master/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestEinsteinPlatform() =>
            ParseAll("repos/muenzpraeger/salesforce-einstein-platform-apex/tree/master/force-app/main/default/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestForceDotcomSfdxDreamhouse() =>
            ParseAll("repos/forcedotcom/sfdx-dreamhouse/tree/master/force-app/main/default/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestApexLambda() =>
            ParseAll("repos/ipavlic/apex-lambda/tree/master/src/classes");

        [Test, Ignore("Violates Github API limits")]
        public void TestSObjectDataLoader() =>
            ParseAll("repos/afawcett/apex-sobjectdataloader/tree/master/apex-sobjectdataloader/src/classes");

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
