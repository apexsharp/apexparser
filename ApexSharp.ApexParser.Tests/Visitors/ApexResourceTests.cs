using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser;
using NUnit.Framework;
using static ApexSharp.ApexParser.Tests.Properties.Resources;

namespace ApexSharp.ApexParser.Tests.Visitors
{
    [TestFixture]
    public class ApexResourceTests : TestFixtureBase
    {
        private void Check(string source, string expected) =>
            CompareLineByLine(ApexSharpParser.IndentApex(source), expected);

        [Test]
        public void ClassOneIsFormattedUsingNewApexFormatter() =>
            Check(ClassOne, ClassOne_Formatted);

        [Test]
        public void ClassTwoIsFormattedUsingNewApexFormatter() =>
            Check(ClassTwo, ClassTwo_Formatted);

        [Test]
        public void ClassWithCommentsIsFormattedUsingNewApexFormatter() =>
            Check(ClassWithComments, ClassWithComments_Formatted);

        [Test]
        public void CustomerDtoIsFormattedUsingNewApexFormatter() =>
            Check(CustomerDto, CustomerDto_Formatted);

        [Test]
        public void FormatDemoIsFormattedUsingNewApexFormatter() =>
            Check(FormatDemo, FormatDemo_Formatted);

        [Test]
        public void fflib_AnswerIsFormattedUsingNewApexFormatter() =>
            Check(fflib_Answer, fflib_Answer_Formatted);

        [Test]
        public void fflib_AnswerTestIsFormattedUsingNewApexFormatter() =>
            Check(fflib_AnswerTest, fflib_AnswerTest_Formatted);

        [Test]
        public void fflib_AnyOrderIsFormattedUsingNewApexFormatter() =>
            Check(fflib_AnyOrder, fflib_AnyOrder_Formatted);

        [Test]
        public void fflib_AnyOrderTestIsFormattedUsingNewApexFormatter() =>
            Check(fflib_AnyOrderTest, fflib_AnyOrderTest_Formatted);

        [Test]
        public void fflib_ApexMocksIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ApexMocks, fflib_ApexMocks_Formatted);

        [Test]
        public void fflib_ApexMocksConfigIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ApexMocksConfig, fflib_ApexMocksConfig_Formatted);

        [Test]
        public void fflib_ApexMocksTestIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ApexMocksTest, fflib_ApexMocksTest_Formatted);

        [Test]
        public void fflib_ApexMocksUtilsIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ApexMocksUtils, fflib_ApexMocksUtils_Formatted);

        [Test]
        public void fflib_ApexMocksUtilsTestIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ApexMocksUtilsTest, fflib_ApexMocksUtilsTest_Formatted);

        [Test]
        public void fflib_ArgumentCaptorIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ArgumentCaptor, fflib_ArgumentCaptor_Formatted);

        [Test]
        public void fflib_ArgumentCaptorTestIsFormattedUsingNewApexFormatter() =>
            Check(fflib_ArgumentCaptorTest, fflib_ArgumentCaptorTest_Formatted);

        [Test]
        public void fflib_IDGeneratorIsFormattedUsingNewApexFormatter() =>
            Check(fflib_IDGenerator, fflib_IDGenerator_Formatted);

        [Test]
        public void fflib_IDGeneratorTestIsFormattedUsingNewApexFormatter() =>
            Check(fflib_IDGeneratorTest, fflib_IDGeneratorTest_Formatted);

        [Test]
        public void fflib_IMatcherIsFormattedUsingNewApexFormatter() =>
            Check(fflib_IMatcher, fflib_IMatcher_Formatted);

        [Test]
        public void JSONParseIsFormattedUsingNewApexFormatter() =>
            Check(JSONParse, JSONParse_Formatted);

        [Test]
        public void JSONParseTestsIsFormattedUsingNewApexFormatter() =>
            Check(JSONParseTests, JSONParseTests_Formatted);

        [Test]
        public void ApexDemoIsFormattedUsingNewApexFormatter() =>
            Check(ApexDemo, ApexDemo_Formatted);

        [Test]
        public void ApexDemoTestIsFormattedUsingNewApexFormatter() =>
            Check(ApexDemoTest, ApexDemoTest_Formatted);
    }
}
