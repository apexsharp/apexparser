using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser;
using ApexParser.Parser;
using ApexParser.Visitors;
using NUnit.Framework;
using Sprache;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.Visitors
{
    [TestFixture]
    public class CSharpResourceTests : TestFixtureBase
    {
        private void Check(string apex, string csharp) =>
            CompareLineByLine(ApexParser.ApexSharpParser.ConvertApexToCSharp(apex), csharp);

        [Test]
        public void SoqlDemoIsGeneratedInCSharp() =>
            Check(SoqlDemo, SoqlDemoCS);

        [Test]
        public void ClassUnitTestIsGeneratedInCSharp() =>
            Check(ClassUnitTest, ClassUnitTest_CSharp);

        [Test]
        public void fflib_AnswerIsGeneratedInCSharp() =>
            Check(fflib_Answer, fflib_Answer_CSharp);

        [Test]
        public void fflib_AnswerTestIsGeneratedInCSharp() =>
            Check(fflib_AnswerTest, fflib_AnswerTest_CSharp);

        [Test]
        public void fflib_AnyOrderIsGeneratedInCSharp() =>
            Check(fflib_AnyOrder, fflib_AnyOrder_CSharp);

        [Test]
        public void fflib_AnyOrderTestIsGeneratedInCSharp() =>
            Check(fflib_AnyOrderTest, fflib_AnyOrderTest_CSharp);

        [Test]
        public void fflib_ApexMocksIsGeneratedInCSharp() =>
            Check(fflib_ApexMocks, fflib_ApexMocks_CSharp);

        [Test]
        public void fflib_ApexMocksConfigIsGeneratedInCSharp() =>
            Check(fflib_ApexMocksConfig, fflib_ApexMocksConfig_CSharp);

        [Test]
        public void fflib_ApexMocksTestIsGeneratedInCSharp() =>
            Check(fflib_ApexMocksTest, fflib_ApexMocksTest_CSharp);

        [Test]
        public void fflib_ApexMocksUtilsIsGeneratedInCSharp() =>
            Check(fflib_ApexMocksUtils, fflib_ApexMocksUtils_CSharp);

        [Test]
        public void fflib_ApexMocksUtilsTestIsGeneratedInCSharp() =>
            Check(fflib_ApexMocksUtilsTest, fflib_ApexMocksUtilsTest_CSharp);

        [Test]
        public void fflib_ArgumentCaptorIsGeneratedInCSharp() =>
            Check(fflib_ArgumentCaptor, fflib_ArgumentCaptor_CSharp);

        [Test]
        public void fflib_ArgumentCaptorTestIsGeneratedInCSharp() =>
            Check(fflib_ArgumentCaptorTest, fflib_ArgumentCaptorTest_CSharp);

        [Test]
        public void fflib_IDGeneratorIsGeneratedInCSharp() =>
            Check(fflib_IDGenerator, fflib_IDGenerator_CSharp);

        [Test]
        public void fflib_IDGeneratorTestIsGeneratedInCSharp() =>
            Check(fflib_IDGeneratorTest, fflib_IDGeneratorTest_CSharp);

        [Test]
        public void fflib_IMatcherIsGeneratedInCSharp() =>
            Check(fflib_IMatcher, fflib_IMatcher_CSharp);

        [Test]
        public void ApexDemoIsGeneratedInCSharp() =>
            Check(ApexDemo, ApexDemo_CSharp);

        [Test]
        public void ApexDemoTestIsGeneratedInCSharp() =>
            Check(ApexDemoTest, ApexDemoTest_CSharp);
    }
}
