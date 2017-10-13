using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Parser;
using ApexParserTest.Properties;
using NUnit.Framework;
using Sprache;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.Parser
{
    [TestFixture]
    public class ApexResourceTests
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        // utility method used to compare method bodies ignoring the formatting
        private void CompareIgnoreFormatting(string expected, string actual)
        {
            if (string.IsNullOrEmpty(expected) || string.IsNullOrEmpty(actual))
            {
                Assert.AreEqual(expected, actual);
            }

            var ignoreWhiteSpace = new Regex(@"\s+");
            expected = ignoreWhiteSpace.Replace(expected, " ").Trim();
            actual = ignoreWhiteSpace.Replace(actual, " ").Trim();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CompareIgnoresFormattingDifferences()
        {
            CompareIgnoreFormatting(" hello ", "hello");
            CompareIgnoreFormatting(" hello    world!", "hello world!");

            Assert.Throws<AssertionException>(() => CompareIgnoreFormatting("hello", "world"));
        }

        // The original file location in the description is kept for future reference.
        // In case the original file is modified, it should be added as a new resource.
        // Old resource files should not be modified so that we don't need to fix the old tests.
        [Test(Description = @"\ApexParser\SalesForceApexSharp\src\classes\ClassOne.cls")]
        public void ClassOneIsParsed()
        {
            // formatted version should have the same AST as the original
            ParseAndValidate(ClassOne);
            ParseAndValidate(ClassOne_Formatted);

            // use local functions to share the validation code
            void ParseAndValidate(string text)
            {
                var cd = Apex.ClassDeclaration.Parse(text);
                Assert.AreEqual("ClassOne", cd.Identifier);
                Assert.AreEqual(1, cd.Methods.Count);

                var md = cd.Methods[0];
                Assert.AreEqual("CallClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("void", md.ReturnType.Identifier);

                var block = md.Statement as BlockStatementSyntax;
                Assert.NotNull(block);
                Assert.AreEqual(2, block.Statements.Count);
                Assert.AreEqual("ClassTwo classTwo = new ClassTwo()", block.Statements[0].Body);
                Assert.AreEqual("System.debug('Test')", block.Statements[1].Body);
            }
        }

        [Test(Description = @"\ApexParser\SalesForceApexSharp\src\classes\ClassTwo.cls")]
        public void ClassTwoIsParsed()
        {
            ParseAndValidate(ClassTwo);
            ParseAndValidate(ClassTwo_Formatted);

            void ParseAndValidate(string text)
            {
                var cd = Apex.ClassDeclaration.Parse(text);
                Assert.AreEqual("ClassTwo", cd.Identifier);
                Assert.AreEqual(2, cd.Methods.Count);

                var md = cd.Methods[0];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);

                var block = md.Statement as BlockStatementSyntax;
                Assert.NotNull(block);
                Assert.AreEqual(1, block.Statements.Count);
                Assert.AreEqual("System.debug('Test')", block.Statements[0].Body);

                md = cd.Methods[1];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);

                block = md.Statement as BlockStatementSyntax;
                Assert.NotNull(block);
                Assert.False(block.Statements.Any());
            }
        }

        [Test]
        public void ClassWithCommentsIsParsed()
        {
            ParseAndValidate(ClassWithComments);
            ParseAndValidate(ClassWithComments_Formatted);

            void ParseAndValidate(string text)
            {
                var cd = Apex.ClassDeclaration.Parse(text);
                Assert.AreEqual("ClassTwo", cd.Identifier);
                Assert.AreEqual(3, cd.Methods.Count);

                var md = cd.Methods[0];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.False(md.MethodParameters.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);

                var block = md.Statement as BlockStatementSyntax;
                Assert.NotNull(block);
                Assert.AreEqual(1, block.Statements.Count);
                Assert.AreEqual("System.debug('Test')", block.Statements[0].Body);
                Assert.AreEqual(1, block.Statements[0].CodeComments.Count);
                Assert.AreEqual("constructor", block.Statements[0].CodeComments[0].Trim());

                md = cd.Methods[1];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);
                Assert.AreEqual(1, md.MethodParameters.Count);
                Assert.AreEqual("String", md.MethodParameters[0].Type.Identifier);
                Assert.AreEqual("vin", md.MethodParameters[0].Identifier);

                block = md.Statement as BlockStatementSyntax;
                Assert.NotNull(block);
                Assert.False(block.Statements.Any());
                Assert.AreEqual(2, block.CodeComments.Count);
                Assert.AreEqual("another constructor", block.CodeComments[0].Trim());
                Assert.AreEqual("with a lot of misplaced comments", block.CodeComments[1].Trim());

                var mp = md.MethodParameters[0];
                Assert.AreEqual("String", mp.Type.Identifier);
                Assert.False(mp.Type.TypeParameters.Any());
                Assert.AreEqual("vin", mp.Identifier);

                md = cd.Methods[2];
                Assert.AreEqual("Hello", md.Identifier);
                Assert.AreEqual(1, md.CodeComments.Count);
                CompareIgnoreFormatting(@"
                * This  is a comment line one
                * This is a comment // line two", md.CodeComments[0]);

                Assert.False(md.Attributes.Any());
                Assert.False(md.MethodParameters.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("void", md.ReturnType.Identifier);

                block = md.Statement as BlockStatementSyntax;
                Assert.NotNull(block);
                Assert.AreEqual(1, block.Statements.Count);
                Assert.AreEqual("System.debug('Hello')", block.Statements[0].Body);
            }
        }

        [Test]
        public void Demo2IsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(Demo2);
            Assert.AreEqual("Demo2", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.AreEqual("MethodOne", md.Identifier);
            Assert.False(md.CodeComments.Any());
            Assert.False(md.Attributes.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.AreEqual("void", md.ReturnType.Identifier);

            var block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);

            var ifstmt = block.Statements[0] as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.AreEqual("x == 5", ifstmt.Expression);
            Assert.NotNull(ifstmt.ThenStatement);
            Assert.NotNull(ifstmt.ElseStatement);

            block = ifstmt.ThenStatement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(4, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(1)", block.Statements[0].Body);
            Assert.AreEqual("Console.WriteLine(2)", block.Statements[2].Body);
            Assert.AreEqual("Console.WriteLine(3)", block.Statements[3].Body);

            var innerIf = block.Statements[1] as IfStatementSyntax;
            Assert.NotNull(innerIf);
            Assert.AreEqual("x == 8", innerIf.Expression);
            Assert.NotNull(innerIf.ThenStatement);
            Assert.Null(innerIf.ElseStatement);

            block = innerIf.ThenStatement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(8)", block.Statements[0].Body);

            ifstmt = ifstmt.ElseStatement as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.NotNull(ifstmt.ThenStatement);
            Assert.NotNull(ifstmt.ElseStatement);

            block = ifstmt.ThenStatement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(6)", block.Statements[0].Body);

            block = ifstmt.ElseStatement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(7)", block.Statements[0].Body);
        }

        [Test(Description = @"\ApexParser\SalesForceApexSharp\src\classes\Demo.cls")]
        public void DemoIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(Demo);
            Assert.AreEqual("Demo", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.AreEqual("RunContactDemo", md.Identifier);
            Assert.False(md.CodeComments.Any());
            Assert.False(md.Attributes.Any());
            Assert.AreEqual(2, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.AreEqual("static", md.Modifiers[1]);
            Assert.AreEqual("void", md.ReturnType.Identifier);

            var block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(11, block.Statements.Count);

            var forStmt = block.Statements[4] as ForStatementSyntax;
            Assert.NotNull(forStmt);
            Assert.AreEqual("Contact c : contacts", forStmt.Expression);
            var loopBody = forStmt.LoopBody as BlockStatementSyntax;
            Assert.NotNull(loopBody);
            Assert.AreEqual(2, loopBody.Statements.Count);
            Assert.AreEqual("System.debug(c.Email)", loopBody.Statements[0].Body);
            Assert.AreEqual("c.Email = 'new@new.com'", loopBody.Statements[1].Body);

            var ifStmt = block.Statements[10] as IfStatementSyntax;
            Assert.NotNull(ifStmt);
            Assert.NotNull(ifStmt.ThenStatement);
            Assert.Null(ifStmt.ElseStatement);

            block = ifStmt.ThenStatement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug('Del Worked')", block.Statements[0].Body);
        }

        [Test(Description = @"\ApexParser\SalesForceApexSharp\src\classes\CustomerDto.cls")]
        public void CustomerDtoIsParsed()
        {
            ParseAndValidate(CustomerDto);
            ParseAndValidate(CustomerDto_Formatted);

            void ParseAndValidate(string text)
            {
                var cd = Apex.ClassDeclaration.Parse(text);
                Assert.False(cd.Attributes.Any());
                Assert.AreEqual("CustomerDto", cd.Identifier);
                Assert.AreEqual(1, cd.Modifiers.Count);
                Assert.AreEqual("public", cd.Modifiers[0]);
                Assert.AreEqual(3, cd.Properties.Count);

                var pd = cd.Properties[0];
                Assert.False(pd.Attributes.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("String", pd.Type.Identifier);
                Assert.AreEqual("make", pd.Identifier);

                pd = cd.Properties[1];
                Assert.False(pd.Attributes.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("String", pd.Type.Identifier);
                Assert.AreEqual("year", pd.Identifier);

                pd = cd.Properties[2];
                Assert.False(pd.Attributes.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("User", pd.Type.Identifier);
                Assert.AreEqual(1, pd.Type.Namespaces.Count);
                Assert.AreEqual("CustomerDto", pd.Type.Namespaces[0]);
                Assert.AreEqual("user", pd.Identifier);

                Assert.AreEqual(1, cd.InnerClasses.Count);
                cd = cd.InnerClasses[0];
                Assert.AreEqual("User", cd.Identifier);
                Assert.False(cd.Attributes.Any());
                Assert.AreEqual(1, cd.Modifiers.Count);
                Assert.AreEqual("public", cd.Modifiers[0]);
                Assert.AreEqual(1, cd.Properties.Count);

                pd = cd.Properties[0];
                Assert.False(pd.Attributes.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("string", pd.Type.Identifier);
                Assert.AreEqual("userName", pd.Identifier);
            }
        }

        [Test(Description = @"\ApexParser\SalesForceApexSharp\src\classes\ForIfWhile.cls")]
        public void ForIfWhileLoopsAreParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(ForIfWhile);
            Assert.False(cd.Attributes.Any());
            Assert.AreEqual("ForIfWhile", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with_sharing", cd.Modifiers[1]);
            Assert.AreEqual(5, cd.Methods.Count);
            Assert.False(cd.Properties.Any());

            var md = cd.Methods[0];
            Assert.AreEqual("MethodIfClean", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Attributes.Any());
            Assert.False(md.MethodParameters.Any());
            var block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(3, block.Statements.Count);

            md = cd.Methods[1];
            Assert.AreEqual("MethodForTraditional", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Attributes.Any());
            Assert.False(md.MethodParameters.Any());
            block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);

            md = cd.Methods[2];
            Assert.AreEqual("MethodForIteration", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Attributes.Any());
            Assert.False(md.MethodParameters.Any());
            block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);

            md = cd.Methods[3];
            Assert.AreEqual("MethodDo", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Attributes.Any());
            Assert.False(md.MethodParameters.Any());
            block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);

            md = cd.Methods[4];
            Assert.AreEqual("MethodWhile", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Attributes.Any());
            Assert.False(md.MethodParameters.Any());
            block = md.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);
        }
    }
}
