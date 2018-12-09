using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;
using NUnit.Framework;
using static ApexSharp.ApexParser.Tests.Properties.Resources;

namespace ApexSharp.ApexParser.Tests.Visitors
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
