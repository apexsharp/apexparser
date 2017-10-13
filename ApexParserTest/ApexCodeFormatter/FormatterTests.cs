using System;
using NUnit.Framework;
using static ApexParser.ApexCodeFormatter.FormatApexCode;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.ApexCodeFormatter
{
    [TestFixture]
    public class FormatterTests
    {
        private void Validate(string source, string expected) =>
            Assert.AreEqual(expected, GetFormattedApexCode(source));

        public void ValidateLineByLine(string source, string expected)
        {
            var formatted = GetFormattedApexCode(source);
            var formattedList = formatted.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var expectedList = expected.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            Assert.AreEqual(expectedList.Length, formattedList.Length);

            for (int i = 0; i < expectedList.Length; i++)
            {
                Assert.AreEqual(expectedList[i], formattedList[i]);
            }
        }

        [Test]
        public void ClassOneIsFormatted() =>
            ValidateLineByLine(ClassOne, ClassOne_Formatted);

        [Test]
        public void ClassTwoIsFormatted() =>
            ValidateLineByLine(ClassTwo, ClassTwo_Formatted);

        [Test]
        public void ClassWithCommentsIsFormatted() =>
            ValidateLineByLine(ClassWithComments, ClassWithComments_Formatted);

        [Test]
        public void DuplicateSpacesAreReplacedWithSingleSpace() =>
            ValidateLineByLine("class    Name", "class Name\n");

        [Test]
        public void ReturnInsideAStatementIsIgnored() =>
            ValidateLineByLine("Integer\n x = 10;", "Integer x = 10;\n");

        [Test]
        public void ReturnInsideAStatementIsReplacedWithASpace() =>
            ValidateLineByLine("Boolean\nflag = true;", "Boolean flag = true;\n");

        [Test]
        public void MultipleReturnsInsideAStatementBecomeSingleSpace() =>
            ValidateLineByLine("Int\n\nx\n\n=y", "Int x =y\n");

        [Test]
        public void EmptyGetSetWithSemicolonsEndUpOnTheSameLine()
        {
            ValidateLineByLine("get;\nset;\n", "get; set;\n");
            ValidateLineByLine("set;\nget;\n", "set; get;\n");
            ValidateLineByLine("get;\n", "get;\n");
        }

        [Test]
        public void EmptyGetSetInsideCurlyBracketsEndUpOnTheSameLine() =>
            ValidateLineByLine("{\nget;\nset;\n}\n", "{ get; set; }\n");

        [Test]
        public void PropertyDefinitionWithEmptyGetSetInsideCurlyBracketsEndUpOnTheSameLine() =>
            ValidateLineByLine("public\nstring\nName\n{\nget;\nset;\n}\n", "public string Name { get; set; }\n");

        [Test]
        public void FormatDemoIsFormatted() =>
            ValidateLineByLine(FormatDemo, FormatDemo_Formatted);

        [Test]
        public void CustomerDtoIsFormatted() =>
            ValidateLineByLine(CustomerDto, CustomerDto_Formatted);
    }
}
