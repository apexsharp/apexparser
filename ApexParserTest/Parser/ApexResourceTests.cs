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
                Assert.False(md.Annotations.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("void", md.ReturnType.Identifier);

                var block = md.Body as BlockSyntax;
                Assert.NotNull(block);
                Assert.AreEqual(2, block.Statements.Count);

                var varDecl = block.Statements[0] as VariableDeclarationSyntax;
                Assert.NotNull(varDecl);
                Assert.AreEqual("ClassTwo", varDecl.Type.Identifier);
                Assert.AreEqual(1, varDecl.Variables.Count);
                Assert.AreEqual("classTwo", varDecl.Variables[0].Identifier);
                Assert.AreEqual("new ClassTwo()", varDecl.Variables[0].Expression);
                Assert.AreEqual("System.debug('Test')", block.Statements[1].Body);
            }
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassTwo.cls")]
        public void ClassTwoIsParsed()
        {
            ParseAndValidate(ClassTwo);
            ParseAndValidate(ClassTwo_Formatted);

            void ParseAndValidate(string text)
            {
                var cd = Apex.ClassDeclaration.Parse(text);
                Assert.AreEqual("ClassTwo", cd.Identifier);
                Assert.False(cd.Methods.Any());
                Assert.AreEqual(2, cd.Constructors.Count);

                var md = cd.Constructors[0];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Annotations.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);

                var block = md.Body;
                Assert.NotNull(block);
                Assert.AreEqual(1, block.Statements.Count);
                Assert.AreEqual("System.debug('Test')", block.Statements[0].Body);

                md = cd.Constructors[1];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Annotations.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);

                block = md.Body;
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
                Assert.AreEqual(2, cd.Constructors.Count);
                Assert.AreEqual(1, cd.Methods.Count);

                var cc = cd.Constructors[0];
                Assert.AreEqual("ClassTwo", cc.Identifier);
                Assert.False(cc.CodeComments.Any());
                Assert.False(cc.Annotations.Any());
                Assert.False(cc.Parameters.Any());
                Assert.AreEqual(1, cc.Modifiers.Count);
                Assert.AreEqual("public", cc.Modifiers[0]);
                Assert.AreEqual("ClassTwo", cc.ReturnType.Identifier);

                var block = cc.Body;
                Assert.NotNull(block);
                Assert.AreEqual(1, block.Statements.Count);
                Assert.AreEqual("System.debug('Test')", block.Statements[0].Body);
                Assert.AreEqual(1, block.Statements[0].CodeComments.Count);
                Assert.AreEqual("constructor", block.Statements[0].CodeComments[0].Trim());

                cc = cd.Constructors[1];
                Assert.AreEqual("ClassTwo", cc.Identifier);
                Assert.False(cc.CodeComments.Any());
                Assert.False(cc.Annotations.Any());
                Assert.AreEqual(1, cc.Modifiers.Count);
                Assert.AreEqual("public", cc.Modifiers[0]);
                Assert.AreEqual("ClassTwo", cc.ReturnType.Identifier);
                Assert.AreEqual(1, cc.Parameters.Count);
                Assert.AreEqual("String", cc.Parameters[0].Type.Identifier);
                Assert.AreEqual("vin", cc.Parameters[0].Identifier);

                block = cc.Body;
                Assert.NotNull(block);
                Assert.False(block.Statements.Any());
                Assert.AreEqual(2, block.CodeComments.Count);
                Assert.AreEqual("another constructor", block.CodeComments[0].Trim());
                Assert.AreEqual("with a lot of misplaced comments", block.CodeComments[1].Trim());

                var mp = cc.Parameters[0];
                Assert.AreEqual("String", mp.Type.Identifier);
                Assert.False(mp.Type.TypeParameters.Any());
                Assert.AreEqual("vin", mp.Identifier);

                var md = cd.Methods[0];
                Assert.AreEqual("Hello", md.Identifier);
                Assert.AreEqual(1, md.CodeComments.Count);
                CompareIgnoreFormatting(@"
                * This  is a comment line one
                * This is a comment // line two", md.CodeComments[0]);

                Assert.False(md.Annotations.Any());
                Assert.False(md.Parameters.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("void", md.ReturnType.Identifier);

                block = md.Body;
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
            Assert.False(md.Annotations.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.AreEqual("void", md.ReturnType.Identifier);

            var block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);

            var ifstmt = block.Statements[0] as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.AreEqual("x == 5", ifstmt.Expression);
            Assert.NotNull(ifstmt.ThenStatement);
            Assert.NotNull(ifstmt.ElseStatement);

            block = ifstmt.ThenStatement as BlockSyntax;
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

            block = innerIf.ThenStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(8)", block.Statements[0].Body);

            ifstmt = ifstmt.ElseStatement as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.NotNull(ifstmt.ThenStatement);
            Assert.NotNull(ifstmt.ElseStatement);

            block = ifstmt.ThenStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(6)", block.Statements[0].Body);

            block = ifstmt.ElseStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("Console.WriteLine(7)", block.Statements[0].Body);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\Demo.cls")]
        public void DemoIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(Demo);
            Assert.AreEqual("Demo", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.AreEqual("RunContactDemo", md.Identifier);
            Assert.False(md.CodeComments.Any());
            Assert.False(md.Annotations.Any());
            Assert.AreEqual(2, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.AreEqual("static", md.Modifiers[1]);
            Assert.AreEqual("void", md.ReturnType.Identifier);

            var block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(11, block.Statements.Count);

            var forStmt = block.Statements[4] as ForEachStatementSyntax;
            Assert.NotNull(forStmt);
            Assert.AreEqual("Contact", forStmt.Type.Identifier);
            Assert.AreEqual("c", forStmt.Identifier);
            Assert.AreEqual("contacts", forStmt.Expression);
            var loopBody = forStmt.Statement as BlockSyntax;
            Assert.NotNull(loopBody);
            Assert.AreEqual(2, loopBody.Statements.Count);
            Assert.AreEqual("System.debug(c.Email)", loopBody.Statements[0].Body);
            Assert.AreEqual("c.Email = 'new@new.com'", loopBody.Statements[1].Body);

            var ifStmt = block.Statements[10] as IfStatementSyntax;
            Assert.NotNull(ifStmt);
            Assert.NotNull(ifStmt.ThenStatement);
            Assert.Null(ifStmt.ElseStatement);

            block = ifStmt.ThenStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug('Del Worked')", block.Statements[0].Body);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\CustomerDto.cls")]
        public void CustomerDtoIsParsed()
        {
            ParseAndValidate(CustomerDto);
            ParseAndValidate(CustomerDto_Formatted);

            void ParseAndValidate(string text)
            {
                var cd = Apex.ClassDeclaration.Parse(text);
                Assert.False(cd.Annotations.Any());
                Assert.AreEqual("CustomerDto", cd.Identifier);
                Assert.AreEqual(1, cd.Modifiers.Count);
                Assert.AreEqual("public", cd.Modifiers[0]);
                Assert.AreEqual(3, cd.Properties.Count);

                var pd = cd.Properties[0];
                Assert.False(pd.Annotations.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("String", pd.Type.Identifier);
                Assert.AreEqual("make", pd.Identifier);

                pd = cd.Properties[1];
                Assert.False(pd.Annotations.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("String", pd.Type.Identifier);
                Assert.AreEqual("year", pd.Identifier);

                pd = cd.Properties[2];
                Assert.False(pd.Annotations.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("User", pd.Type.Identifier);
                Assert.AreEqual(1, pd.Type.Namespaces.Count);
                Assert.AreEqual("CustomerDto", pd.Type.Namespaces[0]);
                Assert.AreEqual("user", pd.Identifier);

                Assert.AreEqual(1, cd.InnerClasses.Count);
                cd = cd.InnerClasses[0];
                Assert.AreEqual("User", cd.Identifier);
                Assert.False(cd.Annotations.Any());
                Assert.AreEqual(1, cd.Modifiers.Count);
                Assert.AreEqual("public", cd.Modifiers[0]);
                Assert.AreEqual(1, cd.Properties.Count);

                pd = cd.Properties[0];
                Assert.False(pd.Annotations.Any());
                Assert.AreEqual(1, pd.Modifiers.Count);
                Assert.AreEqual("public", pd.Modifiers[0]);
                Assert.AreEqual("string", pd.Type.Identifier);
                Assert.AreEqual("userName", pd.Identifier);
            }
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ForIfWhile.cls")]
        public void ForIfWhileLoopsAreParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(ForIfWhile);
            Assert.False(cd.Annotations.Any());
            Assert.AreEqual("ForIfWhile", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with sharing", cd.Modifiers[1]);
            Assert.AreEqual(5, cd.Methods.Count);
            Assert.False(cd.Properties.Any());

            var md = cd.Methods[0];
            Assert.AreEqual("MethodIfClean", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            var block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(3, block.Statements.Count);

            md = cd.Methods[1];
            Assert.AreEqual("MethodForTraditional", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);

            var forStmt = block.Statements[0] as ForStatementSyntax;
            Assert.NotNull(forStmt);
            Assert.NotNull(forStmt.Declaration);
            Assert.AreEqual("Integer", forStmt.Declaration.Type.Identifier);
            Assert.AreEqual(2, forStmt.Declaration.Variables.Count);
            Assert.AreEqual("i", forStmt.Declaration.Variables[0].Identifier);
            Assert.AreEqual("0", forStmt.Declaration.Variables[0].Expression);
            Assert.AreEqual("j", forStmt.Declaration.Variables[1].Identifier);
            Assert.AreEqual("0", forStmt.Declaration.Variables[1].Expression);
            Assert.AreEqual("i < 10", forStmt.Condition);
            Assert.NotNull(forStmt.Incrementors);
            Assert.AreEqual(1, forStmt.Incrementors.Count);
            Assert.AreEqual("i++", forStmt.Incrementors[0]);

            block = forStmt.Statement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug (i + 1)", block.Statements[0].Body);

            md = cd.Methods[2];
            Assert.AreEqual("MethodForIteration", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);

            var forEachStmt = block.Statements[1] as ForEachStatementSyntax;
            Assert.NotNull(forEachStmt);
            Assert.AreEqual("Integer", forEachStmt.Type.Identifier);
            Assert.AreEqual("i", forEachStmt.Identifier);
            Assert.AreEqual("myInts", forEachStmt.Expression);
            block = forEachStmt.Statement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug (i)", block.Statements[0].Body);

            md = cd.Methods[3];
            Assert.AreEqual("MethodDo", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);

            md = cd.Methods[4];
            Assert.AreEqual("MethodWhile", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\DataAccessDemo.cls")]
        public void DataAccessDemoIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(DataAccessDemo);
            Assert.False(cd.Annotations.Any());
            Assert.AreEqual("DataAccessDemo", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with sharing", cd.Modifiers[1]);
            Assert.AreEqual(1, cd.Constructors.Count);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.False(cd.Properties.Any());
            Assert.AreEqual(1, cd.Fields.Count);

            var fd = cd.Fields[0];
            Assert.False(fd.Annotations.Any());
            Assert.False(fd.CodeComments.Any());
            Assert.AreEqual(1, fd.Modifiers.Count);
            Assert.AreEqual("private", fd.Modifiers[0]);
            Assert.AreEqual("DataAccessLayerI", fd.Type.Identifier);
            Assert.AreEqual("dl", fd.Identifier);

            var cc = cd.Constructors[0];
            Assert.AreEqual("DataAccessDemo", cc.Identifier);
            Assert.AreEqual("DataAccessDemo", cc.ReturnType.Identifier);
            Assert.False(cc.Annotations.Any());
            Assert.False(cc.Parameters.Any());
            Assert.AreEqual(1, cc.Modifiers.Count);
            Assert.AreEqual("public", cc.Modifiers[0]);

            var block = cc.Body;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);

            var ifstmt = block.Statements[0] as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.NotNull(ifstmt.ThenStatement as BlockSyntax);
            Assert.NotNull(ifstmt.ElseStatement as BlockSyntax);

            var md = cd.Methods[0];
            Assert.AreEqual("UpdateContactEmailAddress", md.Identifier);
            Assert.AreEqual("String", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.AreEqual(3, md.Parameters.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\PropertyAndField.cls")]
        public void PropertyAndFieldIsParsed()
        {
            var cd = Apex.ClassDeclaration.Parse(PropertyAndField);
            Assert.AreEqual("PropertyAndField", cd.Identifier);
            Assert.AreEqual(1, cd.CodeComments.Count);
            Assert.AreEqual("ClassDeclaration", cd.CodeComments[0].Trim());
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual(3, cd.Properties.Count);
            Assert.AreEqual(8, cd.Fields.Count);

            var pd = cd.Properties[2];
            Assert.AreEqual("DateTimeGetSetArray", pd.Identifier);
            Assert.AreEqual("DateTime", pd.Type.Identifier);
            Assert.False(pd.Type.TypeParameters.Any());
            Assert.IsTrue(pd.Type.IsArray);
            Assert.NotNull(pd.GetterStatement);
            Assert.NotNull(pd.SetterStatement);

            var fd = cd.Fields[2];
            Assert.AreEqual("DateTimeList", fd.Identifier);
            Assert.AreEqual("List", fd.Type.Identifier);
            Assert.AreEqual(1, fd.Type.TypeParameters.Count);
            Assert.AreEqual("DateTime", fd.Type.TypeParameters[0].Identifier);
            Assert.IsFalse(fd.Type.IsArray);
            Assert.AreEqual("new List<DateTime>()", fd.Expression);

            fd = cd.Fields[3];
            Assert.False(fd.Annotations.Any());
            Assert.AreEqual("DateTimeArrary", fd.Identifier);
            Assert.AreEqual("DateTime", fd.Type.Identifier);
            Assert.False(fd.Type.TypeParameters.Any());
            Assert.IsTrue(fd.Type.IsArray);
            Assert.AreEqual(1, fd.Modifiers.Count);
            Assert.AreEqual("public", fd.Modifiers[0]);
            Assert.AreEqual("new DateTime[5]", fd.Expression);

            fd = cd.Fields[7];
            Assert.False(fd.Annotations.Any());
            Assert.AreEqual("NameStaticFinal", fd.Identifier);
            Assert.AreEqual("String", fd.Type.Identifier);
            Assert.False(fd.Type.TypeParameters.Any());
            Assert.False(fd.Type.IsArray);
            Assert.AreEqual(3, fd.Modifiers.Count);
            Assert.AreEqual("public", fd.Modifiers[0]);
            Assert.AreEqual("static", fd.Modifiers[1]);
            Assert.AreEqual("final", fd.Modifiers[2]);
            Assert.AreEqual("'jay'", fd.Expression);

            var md = cd.Methods[0];
            Assert.AreEqual("MethodOne", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);

            var block = md.Body;
            Assert.NotNull(block);
            Assert.AreEqual(8, block.Statements.Count);
        }

        [Test]
        public void SoqlDemoIsParsed()
        {
            var soql = Apex.ClassDeclaration.Parse(SoqlDemo);
            Assert.AreEqual(1, soql.Methods.Count);
            Assert.AreEqual("void", soql.Methods[0].ReturnType.Identifier);
            Assert.AreEqual("CrudExample", soql.Methods[0].Identifier);
        }
    }
}
