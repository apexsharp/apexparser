using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser;
using NUnit.Framework;
using static ApexParser.ApexParser;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.CodeGenerators
{
    [TestFixture]
    public class ApexResourceTests : TestFixtureBase
    {
        private void Check(string source, string expected) =>
            CompareLineByLine(expected, IndentApex(source));

        [Test]
        public void ClassOneIsFormattedUsingNewApexFormatter() =>
            Check(ClassOne, ClassOne_Formatted);

        [Test]
        public void ClassTwoIsFormattedUsingNewApexFormatter() =>
            Check(ClassTwo, ClassTwo_Formatted);
    }
}
