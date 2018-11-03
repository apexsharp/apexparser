using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
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

        private string[] GetFields(string soqlQuery) => GenericExpressionHelper.GetSoqlFields(soqlQuery);

        [Test]
        public void GetSoqlFieldsForInvalidInputDoesntThrowExpressions()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.True(GetFields(null).IsNullOrEmpty());
                Assert.True(GetFields(string.Empty).IsNullOrEmpty());
                Assert.True(GetFields("Hello World").IsNullOrEmpty());
            });
        }

        [Test]
        public void GetSoqlFieldsForValidSoqlQueriesReturnsArrayOfFieldNamesBetweenSelectAndFrom()
        {
            var fields = GetFields(@"SELECT Id, Email,
                Phone
                FROM
                Contact WHERE Email = :email");
            Assert.NotNull(fields);
            Assert.AreEqual(3, fields.Length);
            Assert.AreEqual("Id", fields[0]);
            Assert.AreEqual("Email", fields[1]);
            Assert.AreEqual("Phone", fields[2]);

            fields = GetFields("SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id or Email = :email");
            Assert.NotNull(fields);
            Assert.AreEqual(2, fields.Length);
            Assert.AreEqual("Id", fields[0]);
            Assert.AreEqual("Email", fields[1]);

            fields = GetFields("SELECT A,b,something, 123, c from COntacts");
            Assert.NotNull(fields);
            Assert.AreEqual(5, fields.Length);
            Assert.AreEqual("A", fields[0]);
            Assert.AreEqual("b", fields[1]);
            Assert.AreEqual("something", fields[2]);
            Assert.AreEqual("123", fields[3]);
            Assert.AreEqual("c", fields[4]);
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

            text = "Soql.Upsert(contact);";
            apex = GenericExpressionHelper.ConvertSoqlStatementsToApex(text);
            Assert.AreEqual("upsert contact;", apex);

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
        }

        [Test]
        public void ComplexClassExpressionsAreConvertedToTypeof()
        {
            var text = "Map<string, string>.class";
            var csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(Map<string, string>)", csharp);

            text = "string12.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(string12)", csharp);

            text = "Some.New.Stuff.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(Some.New.Stuff)", csharp);

            text = "List<string>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(List<string>)", csharp);

            text = "List<System.string1>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(List<System.string1>)", csharp);

            text = "System.List<string>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(System.List<string>)", csharp);

            text = "System.Map<string, string, string>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(System.Map<string, string, string>)", csharp);

            text = "Map<List<string>, string>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(Map<List<string>, string>)", csharp);

            text = "Map<string, System.List<string>>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(Map<string, System.List<string>>)", csharp);

            text = "Map<List<string>, System.List<string>>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(Map<List<string>, System.List<string>>)", csharp);

            text = "Map<System.Map<int , string> , System.List<string, int>>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("typeof(Map<System.Map<int , string> , System.List<string, int>>)", csharp);

            // more than two levels deep — cannot be supported by regular expressions
            text = "Map<List<Set<string>>, System.List<string>>.class";
            csharp = GenericExpressionHelper.ConvertTypeofExpressionsToCSharp(text);
            Assert.AreEqual("Map<List<Set<string>>, System.List<string>>.class", csharp);
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

        [Test]
        public void ApexConstructorInitializerIsConvertedToCSharp()
        {
            string Convert(string x) =>
                GenericExpressionHelper.ConvertApexConstructorInitializerToCSharp(x);

            var text = "new Something(Property = 1)";
            var csharp = Convert(text);
            Assert.AreEqual("new Something { Property = 1 }", csharp);

            text = @"new MyClass (Email = 'some\'@example.com', Name = 'Hello')";
            csharp = Convert(text);
            Assert.AreEqual(@"new MyClass { Email = 'some\'@example.com', Name = 'Hello' }", csharp);

            text = @"int c = new Contact(ID = 'Hello', Date = Date.NewInstance(1,2,3), Name='y@e\mail.com') + 2";
            csharp = Convert(text);
            Assert.AreEqual(@"int c = new Contact { ID = 'Hello', Date = Date.NewInstance(1,2,3), Name='y@e\mail.com' } + 2", csharp);

            text = @"int c = new Contact(ID = 'Hello', Stuff = [SELECT ID FROM DUAL]), int y = 10";
            csharp = Convert(text);
            Assert.AreEqual(@"int c = new Contact { ID = 'Hello', Stuff = [SELECT ID FROM DUAL] }, int y = 10", csharp);
        }

        [Test]
        public void ApexAnnotationParametersAreConvertedToCSharp()
        {
            string Convert(string x) =>
                GenericExpressionHelper.ConvertApexAnnotationParametersToCSharp(x);

            var text = "Property = 1";
            var csharp = Convert(text);
            Assert.AreEqual("Property = 1", csharp);

            text = @"Email = 'some\'@example.com' Name = 'Hello'";
            csharp = Convert(text);
            Assert.AreEqual(@"Email = 'some\'@example.com', Name = 'Hello'", csharp);

            text = @"ID = 'Hello' Date = Date.NewInstance(1,2,3) Name='y@e\mail.com'";
            csharp = Convert(text);
            Assert.AreEqual(@"ID = 'Hello', Date = Date.NewInstance(1,2,3), Name='y@e\mail.com'", csharp);

            text = @"ID='Hello'Date=Date.NewInstance(1,2,3)TestAll=true Value=10.12e+11Name='y@e\mail.com'";
            csharp = Convert(text);
            Assert.AreEqual(@"ID='Hello', Date=Date.NewInstance(1,2,3), TestAll=true, Value=10.12e+11, Name='y@e\mail.com'", csharp);
        }

        [Test]
        public void ApexInstanceOfConvertedToCSharpAndBackAgain()
        {
            string ToCSharp(string x) => GenericExpressionHelper.ConvertApexInstanceOfTypeExpressionToCSharp(x);
            string ToApex(string x) => GenericExpressionHelper.ConvertCSharpIsTypeExpressionToApex(x);

            var text = "Property instanceof int";
            var csharp = ToCSharp(text);
            Assert.AreEqual("Property is int", csharp);

            var apex = ToApex(csharp);
            Assert.AreEqual(text, apex);

            text = @"int a = Value instanceof Map<string, string> ? 10 : 20";
            csharp = ToCSharp(text);
            Assert.AreEqual(@"int a = Value is Map<string, string> ? 10 : 20", csharp);

            apex = ToApex(csharp);
            Assert.AreEqual(text, apex);
        }

        [Test]
        public void ConvertTypeNames()
        {
            string ToCSharp(string type) => GenericExpressionHelper.ConvertApexTypeToCSharp(type);
            string ToApex(string type) => GenericExpressionHelper.ConvertCSharpTypeToApex(type);

            Assert.AreEqual("bool", ToCSharp(ApexKeywords.Boolean));
            Assert.AreEqual("bool", ToCSharp("boolean"));
            Assert.AreEqual("bool", ToCSharp("BOOLEAN"));
            Assert.AreEqual("string", ToCSharp(ApexKeywords.String));
            Assert.AreEqual("string", ToCSharp("string"));
            Assert.AreEqual("int", ToCSharp(ApexKeywords.Integer));

            Assert.AreEqual(ApexKeywords.Integer, ToApex("int"));
            Assert.AreEqual(ApexKeywords.String, ToApex("string"));
            Assert.AreEqual(ApexKeywords.Datetime, ToApex("Datetime")); // not sure if we convert System.DateTime to Apex.System.Datetime?
            Assert.AreEqual(ApexKeywords.Time, ToApex("Time"));
        }

        [Test]
        public void ConvertTypesInExpressions()
        {
            string ToCSharp(string expr) => GenericExpressionHelper.ConvertApexTypesToCSharp(expr);
            string ToApex(string expr) => GenericExpressionHelper.ConvertCSharpTypesToApex(expr);

            Assert.AreEqual("Map<string, string>", ToCSharp("Map<String, String>"));
            Assert.AreEqual("Map<String, String>", ToApex("Map<string, string>"));
        }

        [Test]
        public void ApexPritimiveTypesStaticMethodsAreNotConvertedToCSharp()
        {
            string ToCSharp(string expr) => GenericExpressionHelper.ConvertApexTypesToCSharp(expr);
            string ToApex(string expr) => GenericExpressionHelper.ConvertCSharpTypesToApex(expr);

            Assert.AreEqual("a = (int)b", ToCSharp("a = (Integer)b"));
            Assert.AreEqual("a = Integer.valueOf(b)", ToCSharp("a = Integer.valueOf(b)"));

            Assert.AreEqual("a = (Integer)b", ToApex("a = (int)b"));
            Assert.AreEqual("a = Integer.valueOf(b)", ToApex("a = Integer.valueOf(b)"));
        }

        [Test]
        public void DoubleLiteralsAreConvertedToDecimalLiterals()
        {
            string ToCSharp(string expr) => GenericExpressionHelper.ConvertApexDoubleLiteralsToDecimals(expr);
            string ToApex(string expr) => GenericExpressionHelper.ConvertCSharpDecimalLiteralsToDoubles(expr);

            Assert.AreEqual("a = 123.45m", ToCSharp("a = 123.45"));
            Assert.AreEqual("a = .45m", ToCSharp("a = .45"));
            Assert.AreEqual("a = 1.0m", ToCSharp("a = 1.0"));
            Assert.AreEqual("a123.45", ToCSharp("a123.45"));
            Assert.AreEqual("a.", ToCSharp("a."));

            Assert.AreEqual("a = 123.45", ToApex("a = 123.45m"));
            Assert.AreEqual("a = .45", ToApex("a = .45M"));
            Assert.AreEqual("a = 1.0", ToApex("a = 1.0m"));
            Assert.AreEqual("a123.45m", ToApex("a123.45m"));
            Assert.AreEqual("a.m", ToCSharp("a.m"));
        }
    }
}
