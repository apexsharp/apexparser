using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser;
using NUnit.Framework;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.CodeGenerators
{
    [TestFixture]
    public class ApexResourceTests : TestFixtureBase
    {
        private void Check(string source, string expected) =>
            CompareLineByLine(ApexParser.ApexParser.IndentApex(source), expected);

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
    }
}
