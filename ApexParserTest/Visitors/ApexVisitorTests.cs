using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using ApexParser.Visitors;
using NUnit.Framework;
using Sprache;

namespace ApexParserTest.Visitors
{
    [TestFixture, Ignore("TODO: Fix the newline comparisons")]
    public class ApexVisitorTests
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        [Test]
        public void EmptyClassDeclarationIsFormatted()
        {
            var cd = Apex.ClassDeclaration.Parse("class Test {}");
            var result = ApexCodeGenerator.Generate(cd);

            Assert.AreEqual(
@"class Test
{
}
", result);
        }

        [Test]
        public void NonEmptyClassDeclarationIsFormatted()
        {
            var cd = Apex.ClassDeclaration.Parse("class Program{void Main(string arg){}}");
            var result = ApexCodeGenerator.Generate(cd);

            Assert.AreEqual(
@"class Program
{
    void Main(string arg)
    {
    }
}
", result);
        }

    }
}
