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

        [Test]
        public void ClassGlobalRoundtrip() =>
            Check(ClassGlobal_Original, ClassGlobal_Formatted, ClassGlobal_CSharp);

        [Test]
        public void ClassInitializationRoundtrip() =>
            Check(ClassInitialization_Original, ClassInitialization_Formatted, ClassInitialization_CSharp);

        [Test]
        public void ClassInterfaceRoundtrip() =>
            Check(ClassInterface_Original, ClassInterface_Formatted, ClassInterface_CSharp);

        [Test]
        public void ClassInternalRoundtrip() =>
            Check(ClassInternal_Original, ClassInternal_Formatted, ClassInternal_CSharp);

        [Test]
        public void ClassNoApexRoundtrip() =>
            Check(ClassNoApex_Original, ClassNoApex_Formatted, ClassNoApex_CSharp);

        [Test]
        public void ClassRestRoundtrip() =>
            Check(ClassRest_Original, ClassRest_Formatted, ClassRest_CSharp);
    }
}
