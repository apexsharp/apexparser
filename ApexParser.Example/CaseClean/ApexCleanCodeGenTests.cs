using System;
using System.Collections.Generic;
using System.Text;
using ApexSharp.ApexParser;
using NUnit.Framework;

namespace ApexSharpDemo.CaseClean
{
    [TestFixture]
    public class ApexCleanCodeGenTests
    {
        private Func<string, string> Normalize => ApexCleanCodeGen.NormalizeWords;

        [Test]
        public void NormalizeWorksAsExpected()
        {
            Assert.AreEqual(null, Normalize(null));
            Assert.AreEqual(string.Empty, Normalize(string.Empty));
            Assert.AreEqual("  ", Normalize("  "));
            Assert.AreEqual("FooBar+123", Normalize("FooBar+123"));
            Assert.AreEqual("AbstractRecordField", Normalize("abstractrecordfield"));
            Assert.AreEqual("getContext or fooBar + setRecord", Normalize("GetContext or fooBar + setrecord"));
        }

        [Test]
        public void TestCodeGeneration()
        {
            var apex = @"
                class Something
                {
                    @IsTest
                    public static void testPluckDecimals()
                    {
                        List<Account> accounts = testData();
                        LIST<decimal> revenues = Pluck.decimals(accounts, Account.AnnualRevenue);
                        System.deBUG(4, revenues.size());
                        System.assertNOTequals(100.0, revenues[0]); // SYSTEM
                        system.assertEquals(60.0, revenues[1]); // 'deBUG'
                        SYSTEM.assertEquals(150.0, revenues[2]);
                        System.Debug(150.0, revenues[3]);
                    }
                }";

            var parsed = ApexSharpParser.GetApexAst(apex);
            var generated = ApexCleanCodeGen.GenerateApex(parsed);
            Assert.NotNull(generated);
            Assert.AreEqual(@"class Something
{
    @IsTest
    public static void testPluckDecimals()
    {
        List<Account> accounts = testData();
        List<Decimal> revenues = Pluck.decimals(accounts, Account.AnnualRevenue);
        System.debug(4, revenues.size());
        System.assertNotEquals(100.0, revenues[0]); // SYSTEM
        System.assertEquals(60.0, revenues[1]); // 'deBUG'
        System.assertEquals(150.0, revenues[2]);
        System.debug(150.0, revenues[3]);
    }
}
", generated);
        }
    }
}
