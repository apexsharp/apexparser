using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;
using NUnit.Framework;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.CodeGenerators
{
    [TestFixture]
    public class SoqlExtractorTests
    {
        [Test]
        public void SoqlExtractorExtractsAllSoqlDemo2Queries()
        {
            var queries = SoqlExtractor.ExtractAllQueries(SoqlDemo2);
            Assert.AreEqual(23, queries.Length);
        }
    }
}
