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

        [Test]
        public void ClassRestTestRoundtrip() =>
            Check(ClassRestTest_Original, ClassRestTest_Formatted, ClassRestTest_CSharp);

        [Test]
        public void ClassUnitTestRoundtrip()
        {
            // formatted class doesn't match the converted class because of the testMethod modifiers
            CompareLineByLine(ApexSharpParser.IndentApex(ClassUnitTest_Original), ClassUnitTest_Formatted);
            CompareLineByLine(ApexSharpParser.ConvertApexToCSharp(ClassUnitTest_Original), ClassUnitTest_CSharp1);
            CompareLineByLine(ApexSharpParser.ToApex(ClassUnitTest_CSharp1)[0], ClassUnitTest_Converted);
        }

        [Test]
        public void ClassUnitTestRunAsRoundtrip() =>
            Check(ClassUnitTestRunAs_Original, ClassUnitTestRunAs_Formatted, ClassUnitTestRunAs_CSharp);

        [Test]
        public void ClassUnitTestSeeAllDataRoundtrip() =>
            Check(ClassUnitTestSeeAllData_Original, ClassUnitTestSeeAllData_Formatted, ClassUnitTestSeeAllData_CSharp);

        [Test]
        public void ClassWithoutSharingRoundtrip() =>
            Check(ClassWithOutSharing_Original, ClassWithOutSharing_Formatted, ClassWithOutSharing_CSharp);

        [Test]
        public void ClassWithSharingRoundtrip() =>
            Check(ClassWithSharing_Original, ClassWithSharing_Formatted, ClassWithSharing_CSharp);

        [Test]
        public void CollectionsRoundtrip() =>
            Check(Collections_Original, Collections_Formatted, Collections_CSharp);

        [Test]
        public void CommentsRoundtrip() =>
            Check(Comments_Original, Comments_Formatted, Comments_CSharp);

        [Test]
        public void DemoRoundtrip() =>
            Check(Demo_Original, Demo_Formatted, Demo_CSharp);

        [Test]
        public void DemoControllerRoundtrip() =>
            Check(DemoController_Original, DemoController_Formatted, DemoController_CSharp);

        [Test]
        public void DemoTestRoundtrip() =>
            Check(DemoTest_Original, DemoTest_Formatted, DemoTest_CSharp);
    }
}
