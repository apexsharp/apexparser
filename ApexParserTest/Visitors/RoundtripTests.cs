using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser;
using NUnit.Framework;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.Visitors
{
    [TestFixture]
    public class RoundtripTests : TestFixtureBase
    {
        private void Check(string apexOriginal, string apexFormatted, string csharp)
        {
            Assert.Multiple(() =>
            {
                CompareLineByLine(ApexSharpParser.IndentApex(apexOriginal), apexFormatted);
                CompareLineByLine(ApexSharpParser.ConvertApexToCSharp(apexOriginal), csharp);
                CompareLineByLine(ApexSharpParser.ToApex(csharp)[0], apexFormatted);
            });
        }

        [Test]
        public void ClassAbstractRoundtrip() =>
            Check(ClassAbstract_Original, ClassAbstract_Formatted, ClassAbstract_CSharp);

        [Test]
        public void ClassEnumRoundtrip() =>
            Check(ClassEnum_Original, ClassEnum_Formatted, ClassEnum_CSharp);

        [Test]
        public void ClassExceptionRoundtrip() =>
            Check(ClassException_Original, ClassException_Formatted, ClassException_CSharp);
    }
}
