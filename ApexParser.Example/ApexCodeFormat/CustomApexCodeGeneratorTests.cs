using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using static System.Math;

namespace ApexSharpDemo.ApexCodeFormat
{
    [TestFixture]
    public class CustomApexCodeGeneratorTests
    {
        private void Check(string source, string expected, Settings settings = null) =>
            CompareLineByLine(CustomApexCodeGenerator.FormatApex(source, settings), expected);

        protected void CompareLineByLine(string actual, string expected)
        {
            var actualList = actual.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var expectedList = expected.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < Min(expectedList.Length, actualList.Length); i++)
            {
                Assert.AreEqual(expectedList[i].Trim(), actualList[i].Trim());
            }

            if (Abs(expectedList.Length - actualList.Length) > 1)
            {
                Assert.Fail("Too many difference in lines: expected {0}, actual {1}", expectedList.Length, actualList.Length);
            }
        }

        [Test]
        public void TestUsingInvalidInput()
        {
            Assert.AreEqual(string.Empty, CustomApexCodeGenerator.FormatApex(null));
            Assert.AreEqual(string.Empty, CustomApexCodeGenerator.FormatApex("  "));
            Assert.AreEqual(string.Empty, CustomApexCodeGenerator.GenerateApex(null));
        }

        [Test]
        public void TestUsingDefaultSettings()
        {
            Check(@"class Sample {
                public void sampleMethod() {
                    User newUser = [SELECT Id FROM User LIMIT 1];
                    System.runAs(newUser) {
                        System.debug('Hello!');
                    }
                }
            }", @"class Sample
            {
                public void sampleMethod()
                {
                    User newUser = [SELECT Id FROM User LIMIT 1];
                    System.runAs(newUser)
                    {
                        System.debug('Hello!');
                    }
                }
            }");
        }

        [Test]
        public void TestUsingSingleLineOption()
        {
            Check(@"class Sample {
                public void sample() {
                    Integer a =
                        1 + 2 +
                        3 + 'Hello'.length();
                }
            }", @"class Sample
            {
                public void sample()
                {
                    Integer a = 1 + 2 + 3 + 'Hello'.length();
                }
            }",
            new Settings
            {
                SingleLine = true
            });
        }

        [Test, Ignore("TODO")]
        public void TestUsingOpenBracesOption()
        {
            Check(@"class Sample
            {
                public void sampleMethod()
                {
                    User newUser = [SELECT Id FROM User LIMIT 1];
                    System.runAs(newUser)
                    {
                        System.debug('Hello!');
                    }
                }
            }", @"class Sample {
                public void sampleMethod() {
                    User newUser = [SELECT Id FROM User LIMIT 1];
                    System.runAs(newUser) {
                        System.debug('Hello!');
                    }
                }
            }",
            new Settings
            {
                OpenCloseBracesInNewLine = true
            });
        }
    }
}
