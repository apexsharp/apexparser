using NUnit.Framework;
using ApexParser.Parser;
using System;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApexParserTest
{
    [TestFixture, Explicit]
    public class ApexParserMasterTest
    {
        [Test(Description = "https://github.com/jayonsoftware/SalesForceApexSharp")]
        public void TestSalesForceApexSharp() =>
            // ParseAll("repos/jayonsoftware/SalesForceApexSharp/contents/src/classes");
            ParseZip("SalesForceApexSharp-master.zip");

        [Test(Description = "https://github.com/financialforcedev/fflib-apex-common")]
        public void TestFFlibApexCommon() =>
            // ParseAll("repos/financialforcedev/fflib-apex-common/contents/fflib/src/classes");
            ParseZip("fflib-apex-common-master.zip");

        [Test(Description = "https://github.com/financialforcedev/fflib-apex-mocks")]
        public void TestFFlibApexMocks() =>
            // ParseAll("repos/financialforcedev/fflib-apex-mocks/contents/master/src/classes");
            ParseZip("fflib-apex-mocks-master.zip");

        [Test(Description = "https://github.com/financialforcedev/ffhttp-core")]
        public void TestFFhttpCore() =>
            // ParseAll("repos/financialforcedev/ffhttp-core/contents/master/src/classes");
            ParseZip("ffhttp-core-master.zip");

        [Test(Description = "https://github.com/financialforcedev/fflib-apex-common-samplecode")]
        public void TestFFlibApexCommonSampleCode() =>
            // ParseAll("repos/financialforcedev/fflib-apex-common-samplecode/contents/master/fflib-sample-code/src/classes");
            ParseZip("fflib-apex-common-samplecode-master.zip");

        [Test(Description = "https://github.com/SalesforceFoundation/Cumulus")]
        public void TestCumulus() =>
            // ParseAll("repos/SalesforceFoundation/Cumulus/contents/master/src/classes");
            ParseZip("Cumulus-master.zip");

        [Test(Description = "https://github.com/muenzpraeger/salesforce-einstein-platform-apex")]
        public void TestEinsteinPlatform() =>
            // ParseAll("repos/muenzpraeger/salesforce-einstein-platform-apex/tree/master/force-app/main/default/classes");
            ParseZip("salesforce-einstein-platform-apex-master.zip");

        [Test(Description = "https://github.com/forcedotcom/sfdx-dreamhouse")]
        public void TestForceDotcomSfdxDreamhouse() =>
            // ParseAll("repos/forcedotcom/sfdx-dreamhouse/tree/master/force-app/main/default/classes");
            ParseZip("sfdx-dreamhouse-master.zip");

        [Test(Description = "https://github.com/ipavlic/apex-lambda")]
        public void TestApexLambda() =>
            // ParseAll("repos/ipavlic/apex-lambda/tree/master/src/classes");
            ParseZip("apex-lambda-master.zip");

        [Test(Description = "https://github.com/afawcett/apex-sobjectdataloader")]
        public void TestSObjectDataLoader() =>
            // ParseAll("repos/afawcett/apex-sobjectdataloader/tree/master/apex-sobjectdataloader/src/classes");
            ParseZip("apex-sobjectdataloader-master.zip");

        public void ParseZip(string fileName)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(currentPath, "ApexTestLibraries", fileName);
            var file = File.OpenRead(filePath);
            var zip = new ZipArchive(file, ZipArchiveMode.Read, false);
            var apexClasses =
                from entry in zip.Entries.OfType<ZipArchiveEntry>()
                where entry.Name.EndsWith(".cls", StringComparison.InvariantCultureIgnoreCase)
                select entry;

            // Process all Apex files, don't stop after the first error
            Assert.Multiple(() =>
            {
                foreach (var entry in apexClasses)
                {
                    using (var stream = entry.Open())
                    using (var reader = new StreamReader(stream))
                    {
                        // Report failing file names along with the parse exception
                        var fileContents = reader.ReadToEnd();
                        Assert.DoesNotThrow(() => Apex.ParseFile(fileContents),
                            $"Parsing failure on file: {entry.FullName}");
                    }
                }
            });
        }

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
