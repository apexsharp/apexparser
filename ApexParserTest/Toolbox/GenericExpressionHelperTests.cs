using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;
using ApexParser.Visitors;
using NUnit.Framework;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.Toolbox
{
    [TestFixture]
    public class GenericExpressionHelperTests
    {
        private void Check(string expr, int count)
        {
            var parts = GenericExpressionHelper.Split(expr);
            Assert.AreEqual(expr ?? string.Empty, string.Concat(parts));
            Assert.AreEqual(count, parts.Length);
        }

        private void Check(string expr, params string[] expectedParts)
        {
            var parts = GenericExpressionHelper.Split(expr);
            Assert.AreEqual(expr, string.Concat(parts));

            if (!expectedParts.IsNullOrEmpty())
            {
                Assert.AreEqual(expectedParts.Length, parts.Length);
                foreach (var part in parts.Zip(expectedParts, (p, e) => new { p, e }))
                {
                    Assert.AreEqual(part.e, part.p);
                }
            }
        }

        private void CheckMany(params string[] expressions) =>
            Assert.Multiple(() =>
            {
                foreach (var expr in expressions)
                {
                    Check(expr);
                }
            });

        [Test]
        public void NullExpressionHasNoParts() =>
            Check(null, 0);

        [Test]
        public void EmptyExpressionHasNoParts() =>
            Check(string.Empty, 0);

        [Test]
        public void SimpleExpressionIsNotSplit() =>
            Check("x = 1;", 1);

        [Test]
        public void StringsAreExtracted() =>
            Check("'x = 1;'", 1);

        [Test]
        public void StringsAreDetachedFromOtherParts() =>
            Check("x = '1234'", "x = ", "'1234'");

        [Test]
        public void SoqlQueriesAreExtracted() =>
            Check("[select id from contact]", 1);

        [Test]
        public void SoqlQueriesAreDetachedFromOtherParts() =>
            Check("x = [select id from contact]", "x = ", "[select id from contact]");

        [Test]
        public void StringaAndSoqlQueriesAreDetachedFromOtherParts() =>
            Check("x = [select id from contact] + 'nothing'",
                "x = ", "[select id from contact]", " + ", "'nothing'");

        [Test]
        public void MixedStringsAndSoqlQueriesAreExtracted() =>
            Check("x = ''; y = [SELECT c, '' from z]; z = 'this is a test: [select x]'; t = a[0];",
                "x = ", "''", "; y = ", "[SELECT c, '' from z]",
                "; z = ", "'this is a test: [select x]'", "; t = a[0];");

        [Test]
        public void StringEscapesAreSupported() =>
            Check(@"'a\b\'c'", 1);

        [Test]
        public void MixedStringsAndSoqlQueriesWithStringEscapesAreExtracted() =>
            Check(@"x = ''; y = [SELECT c, '' from z]; z = 'this is \a \'test\': [select x]'; t = a[0];",
                "x = ", "''", "; y = ", "[SELECT c, '' from z]",
                "; z = ", @"'this is \a \'test\': [select x]'", "; t = a[0];");

        [Test]
        public void ApexCodeIsSplitAndAssembledBackWithoutErrors() => // CommentFails has unmatched quote
            CheckMany(fflib_Answer, fflib_AnswerTest, ApplicationTest, ClassAbstract, ClassEnum,
                ClassException, ClassGlobal, ClassInterface, ClassInternal, ClassOne, ClassOne_Formatted,
                ClassRest, ClassTwo, ClassTwo_Formatted, ClassUnitTest, ClassUnitTestRunAs,
                ClassUnitTestSeeAllData, ClassVirtual, ClassWithComments, ClassWithComments_Formatted,
                ClassWithOutSharing, ClassWithSharing, Comments, CustomerDto, CustomerDto_Formatted,
                DataAccessDemo, Demo, Demo2, ExceptionDemo, ForIfWhile, ForIfWhile2, ForIfWhile3,
                FormatDemo, FormatDemo_Formatted, GetSetDemo, IClassInterface, IClassInterfaceExt,
                JsonExample, ListAndArrayDemo, MethodAndConstructor, PrimitiveTypes, PropertyAndField,
                PropertyAndField2, PropertyAndField3, RunAll, SoqlDemo, SoqlDemo2);

        private string GetTableName(string soqlQuery) => GenericExpressionHelper.GetSoqlTableName(soqlQuery);

        [Test]
        public void GetSoqlTableNameForInvalidInputDoesntThrowExpressions()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.True(string.IsNullOrEmpty(GetTableName(null)));
                Assert.True(string.IsNullOrEmpty(GetTableName(string.Empty)));
                Assert.True(string.IsNullOrEmpty(GetTableName("Hello World")));
            });
        }

        [Test]
        public void GetSoqlTableNameForValidSoqlQueriesReturnsTableNameSpecifiedAfterFromKeyword() =>
            Assert.AreEqual("Contact", GetTableName(@"SELECT Id, Email, Phone
                FROM
                Contact WHERE Email = :email"));

        private string[] GetParameters(string soqlQuery) => GenericExpressionHelper.GetSoqlParameters(soqlQuery);

        [Test]
        public void GetSoqlParametersForInvalidInputDoesntThrowExpressions()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.True(GetParameters(null).IsNullOrEmpty());
                Assert.True(GetParameters(string.Empty).IsNullOrEmpty());
                Assert.True(GetParameters("Hello World").IsNullOrEmpty());
            });
        }

        [Test]
        public void GetSoqlParametersForValidSoqlQueriesReturnsTableNameSpecifiedAfterFromKeyword()
        {
            var args = GetParameters(@"SELECT Id, Email, Phone
                FROM
                Contact WHERE Email = :email");
            Assert.NotNull(args);
            Assert.AreEqual(1, args.Length);
            Assert.AreEqual("email", args[0]);

            args = GetParameters("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id or Email = :email");
            Assert.NotNull(args);
            Assert.AreEqual(2, args.Length);
            Assert.AreEqual("contactNew.Id", args[0]);
            Assert.AreEqual("email", args[1]);
        }

        [Test]
        public void GetSoqlExpressionReturnsApexSoqlExpressionBody()
        {
            var text = "List<Contact> contacts = Soql.Query<Contact>(\"SELECT Id, Email, Phone FROM Contact\");";
            var expr = GenericExpressionHelper.ExtractSoqlQueries(text);
            Assert.AreEqual(1, expr.Length);
            Assert.AreEqual("SELECT Id, Email, Phone FROM Contact", expr[0]);

            text = "Soql.Query<Contact>(\"SELECT Id, Email, Phone FROM Contact WHERE Email = :email\", email);";
            expr = GenericExpressionHelper.ExtractSoqlQueries(text);
            Assert.AreEqual(1, expr.Length);
            Assert.AreEqual("SELECT Id, Email, Phone FROM Contact WHERE Email = :email", expr[0]);
        }

        [Test]
        public void ConvertSoqlExpressionReturnsConvertedApexSoqlExpression()
        {
            var text = "List<Contact> contacts = Soql.Query<Contact>(\"SELECT Id, Email, Phone FROM Contact\");";
            var expr = GenericExpressionHelper.ConvertSoqlQueriesToApex(text);
            Assert.AreEqual("List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact];", expr);

            text = @"List<Contact> contacts = Soql.Query<Contact>(""SELECT Id, Email, Phone FROM Contact WHERE Email = :email"", email);";
            expr = GenericExpressionHelper.ConvertSoqlQueriesToApex(text);
            Assert.AreEqual("List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact WHERE Email = :email];", expr);
        }

        [Test]
        public void ConvertSoqlInserUpdateDeleteOperationsReturnsApexStatements()
        {
            var text = "Soql.Insert(contact);";
            var apex = GenericExpressionHelper.ConvertSoqlStatementsToApex(text);
            Assert.AreEqual("insert contact;", apex);

            text = "Soql.Update(contact);";
            apex = GenericExpressionHelper.ConvertSoqlStatementsToApex(text);
            Assert.AreEqual("update contact;", apex);

            text = "Soql.Delete(contact);";
            apex = GenericExpressionHelper.ConvertSoqlStatementsToApex(text);
            Assert.AreEqual("delete contact;", apex);
        }

        [Test]
        public void SomeDotClassIsConvertedToTypeofClass()
        {
            var text = "string.class";
            var csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(string)", csharp);

            text = "mock(MyLittleClass.class)";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("mock(typeof(MyLittleClass))", csharp);

            // not supported by regex-based expression helper
            text = "Map<string, string>.class)";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("Map<string, string>.class)", csharp);
        }

        [Test]
        public void TypeofSomeIsConvertedToSomeDotClass()
        {
            var text = "typeof(string)";
            var apex = GenericExpressionHelper.ConvertTypeofExpressionsToApex(text);
            Assert.AreEqual("string.class", apex);

            text = "mock(typeof(MyLittleClass))";
            apex = GenericExpressionHelper.ConvertTypeofExpressionsToApex(text);
            Assert.AreEqual("mock(MyLittleClass.class)", apex);

            // backward conversion is supported
            text = "typeof(Map<string, string>)";
            apex = GenericExpressionHelper.ConvertTypeofExpressionsToApex(text);
            Assert.AreEqual("Map<string, string>.class", apex);
        }

        [Test]
        public void StringValueofSomethingIsConvertedToSomethingToString()
        {
            var text = "string.valueOf(1)";
            var csharp = GenericExpressionHelper.ConvertStringValueofToString(text);
            Assert.AreEqual("1.ToString()", csharp);

            text = "string.valueOf(something)";
            csharp = GenericExpressionHelper.ConvertStringValueofToString(text);
            Assert.AreEqual("something.ToString()", csharp);

            text = "string.valueOf(something+1)";
            csharp = GenericExpressionHelper.ConvertStringValueofToString(text);
            Assert.AreEqual("(something+1).ToString()", csharp);

            // not supported by regex-based expression helper
            text = "string.valueOf(method())";
            csharp = GenericExpressionHelper.ConvertStringValueofToString(text);
            Assert.AreEqual("string.valueOf(method())", csharp);
        }

        [Test]
        public void DateTimeNowAndDateTodayAreConverted()
        {
            var text = "Datetime.now()";
            var csharp = GenericExpressionHelper.ConvertApexDateTimeNowToCSharp(text);
            Assert.AreEqual("DateTime.Now", csharp);

            text = "Date.today()";
            csharp = GenericExpressionHelper.ConvertApexDateTodayToCSharp(text);
            Assert.AreEqual("DateTime.Today", csharp);

            text = "DateTime.Now";
            var apex = GenericExpressionHelper.ConvertCSharpDateTimeNowToApex(text);
            Assert.AreEqual("Datetime.now()", apex);

            text = "DateTime.Today";
            apex = GenericExpressionHelper.ConvertCSharpDateTimeTodayToApex(text);
            Assert.AreEqual("Date.today()", apex);
        }
    }
}
