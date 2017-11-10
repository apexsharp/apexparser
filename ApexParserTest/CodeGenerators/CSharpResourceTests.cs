using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using ApexParser.Visitors;
using NUnit.Framework;
using Sprache;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.CodeGenerators
{
    [TestFixture]
    public class CSharpResourceTests : TestFixtureBase
    {
        private void Check(string source, string expected) =>
            CompareLineByLine(ApexParser.ApexParser.ConvertApexToCSharp(source), expected);

        [Test]
        public void SoqlDemoIsGeneratedInCSharp()
        {
            var soqlDemo = new ApexGrammar().ClassDeclaration.Parse(SoqlDemo);
            var soqlCSharp = soqlDemo.ToCSharp();
            Assert.NotNull(soqlCSharp);
        }

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
    }
}
