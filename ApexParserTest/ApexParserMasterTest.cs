using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.IO;
using ApexParser.Parser;

namespace ApexParserTest
{
    [TestFixture]
    public class ApexParserMasterTest
    {
        [Test] //, Ignore("Ignored to enable the Appveyor build")]
        public void TestRemoteApexFile()
        {
            var apexFiles = GitHubHelper.GetCodeFromGitFolder("repos/jayonsoftware/SalesForceApexSharp/contents/src/classes", ".cls");

            // Process all Apex files, don't stop after the first error
            Assert.Multiple(() =>
            {
                foreach (var apexFile in apexFiles)
                {
                    // Report failing file names along with the parse exception
                    Assert.DoesNotThrow(() => Apex.ParseFile(apexFile.Value), "Parsing failure on file: {0}", apexFile.Key);
                }
            });
        }
    }
}
