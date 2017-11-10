using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Toolbox;
using ApexParser.Visitors;
using NUnit.Framework;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.CodeGenerators
{
    [TestFixture]
    public class SimpleExpressionSplitterTests
    {
        private void Check(string expr, int count)
        {
            var parts = SimpleExpressionSplitter.Split(expr);
            Assert.AreEqual(expr ?? string.Empty, string.Concat(parts));
            Assert.AreEqual(count, parts.Length);
        }

        private void Check(string expr, params string[] expectedParts)
        {
            var parts = SimpleExpressionSplitter.Split(expr);
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
    }
}
