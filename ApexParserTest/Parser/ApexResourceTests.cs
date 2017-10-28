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
                var cd = Apex.ParseClass(text);
                Assert.AreEqual("ClassOne", cd.Identifier);
                Assert.AreEqual(1, cd.Methods.Count);

                var md = cd.Methods[0];
                Assert.AreEqual("CallClassTwo", md.Identifier);
                Assert.False(md.LeadingComments.Any());
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
                var cd = Apex.ParseClass(text);
                Assert.AreEqual("ClassTwo", cd.Identifier);
                Assert.False(cd.Methods.Any());
                Assert.AreEqual(2, cd.Constructors.Count);

                var md = cd.Constructors[0];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.LeadingComments.Any());
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
                Assert.False(md.LeadingComments.Any());
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
                var cd = Apex.ParseClass(text);
                Assert.AreEqual("ClassTwo", cd.Identifier);
                Assert.AreEqual(2, cd.Constructors.Count);
                Assert.AreEqual(1, cd.Methods.Count);

                var cc = cd.Constructors[0];
                Assert.AreEqual("ClassTwo", cc.Identifier);
                Assert.False(cc.LeadingComments.Any());
                Assert.False(cc.Annotations.Any());
                Assert.False(cc.Parameters.Any());
                Assert.AreEqual(1, cc.Modifiers.Count);
                Assert.AreEqual("public", cc.Modifiers[0]);
                Assert.AreEqual("ClassTwo", cc.ReturnType.Identifier);

                var block = cc.Body;
                Assert.NotNull(block);
                Assert.AreEqual(1, block.Statements.Count);
                Assert.AreEqual("System.debug('Test')", block.Statements[0].Body);
                Assert.AreEqual(1, block.Statements[0].LeadingComments.Count);
                Assert.AreEqual("constructor", block.Statements[0].LeadingComments[0].Trim());

                cc = cd.Constructors[1];
                Assert.AreEqual("ClassTwo", cc.Identifier);
                Assert.False(cc.LeadingComments.Any());
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
                Assert.AreEqual(2, block.InnerComments.Count);
                Assert.AreEqual("another constructor", block.InnerComments[0].Trim());
                Assert.AreEqual("with a lot of misplaced comments", block.InnerComments[1].Trim());

                var mp = cc.Parameters[0];
                Assert.AreEqual("String", mp.Type.Identifier);
                Assert.False(mp.Type.TypeParameters.Any());
                Assert.AreEqual("vin", mp.Identifier);

                var md = cd.Methods[0];
                Assert.AreEqual("Hello", md.Identifier);
                Assert.AreEqual(1, md.LeadingComments.Count);
                CompareIgnoreFormatting(@"
                * This  is a comment line one
                * This is a comment // line two", md.LeadingComments[0]);

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
            var cd = Apex.ParseClass(Demo2);
            Assert.AreEqual("Demo2", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.AreEqual("MethodOne", md.Identifier);
            Assert.False(md.LeadingComments.Any());
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
            var cd = Apex.ParseClass(Demo);
            Assert.AreEqual("Demo", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.AreEqual("RunContactDemo", md.Identifier);
            Assert.False(md.LeadingComments.Any());
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
                var cd = Apex.ParseClass(text);
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
            var cd = Apex.ParseClass(ForIfWhile);
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
            var cd = Apex.ParseClass(DataAccessDemo);
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
            Assert.False(fd.LeadingComments.Any());
            Assert.AreEqual(1, fd.Modifiers.Count);
            Assert.AreEqual("private", fd.Modifiers[0]);
            Assert.AreEqual("DataAccessLayerI", fd.Type.Identifier);
            Assert.AreEqual(1, fd.Fields.Count);
            Assert.AreEqual("dl", fd.Fields[0].Identifier);

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
            var cd = Apex.ParseClass(PropertyAndField);
            Assert.AreEqual("PropertyAndField", cd.Identifier);
            Assert.AreEqual(1, cd.LeadingComments.Count);
            Assert.AreEqual("ClassDeclaration", cd.LeadingComments[0].Trim());
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual(3, cd.Properties.Count);
            Assert.AreEqual(8, cd.Fields.Count);

            var pd = cd.Properties[2];
            Assert.AreEqual("DateTimeGetSetArray", pd.Identifier);
            Assert.AreEqual("DateTime", pd.Type.Identifier);
            Assert.False(pd.Type.TypeParameters.Any());
            Assert.IsTrue(pd.Type.IsArray);
            Assert.NotNull(pd.Getter);
            Assert.NotNull(pd.Setter);

            var fd = cd.Fields[2];
            Assert.AreEqual("DateTimeList", fd.Fields[0].Identifier);
            Assert.AreEqual("list", fd.Type.Identifier);
            Assert.AreEqual(1, fd.Type.TypeParameters.Count);
            Assert.AreEqual("DateTime", fd.Type.TypeParameters[0].Identifier);
            Assert.IsFalse(fd.Type.IsArray);
            Assert.AreEqual("new list<DateTime>()", fd.Fields[0].Expression);

            fd = cd.Fields[3];
            Assert.False(fd.Annotations.Any());
            Assert.AreEqual("DateTimeArrary", fd.Fields[0].Identifier);
            Assert.AreEqual("DateTime", fd.Type.Identifier);
            Assert.False(fd.Type.TypeParameters.Any());
            Assert.IsTrue(fd.Type.IsArray);
            Assert.AreEqual(1, fd.Modifiers.Count);
            Assert.AreEqual("public", fd.Modifiers[0]);
            Assert.AreEqual("new DateTime[5]", fd.Fields[0].Expression);

            fd = cd.Fields[7];
            Assert.False(fd.Annotations.Any());
            Assert.AreEqual("NameStaticFinal", fd.Fields[0].Identifier);
            Assert.AreEqual("String", fd.Type.Identifier);
            Assert.False(fd.Type.TypeParameters.Any());
            Assert.False(fd.Type.IsArray);
            Assert.AreEqual(3, fd.Modifiers.Count);
            Assert.AreEqual("public", fd.Modifiers[0]);
            Assert.AreEqual("static", fd.Modifiers[1]);
            Assert.AreEqual("final", fd.Modifiers[2]);
            Assert.AreEqual("'jay'", fd.Fields[0].Expression);

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

        [Test(Description = @"SalesForceApexSharp\src\classes\SoqlDemo.cls")]
        public void SoqlDemoIsParsed()
        {
            var soql = Apex.ParseClass(SoqlDemo);
            Assert.AreEqual(1, soql.Methods.Count);
            Assert.AreEqual("void", soql.Methods[0].ReturnType.Identifier);
            Assert.AreEqual("CrudExample", soql.Methods[0].Identifier);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassAbstract.cls")]
        public void ClassAbstractIsParsed()
        {
            var cd = Apex.ParseClass(ClassAbstract);
            Assert.AreEqual("ClassAbstract", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("abstract", cd.Modifiers[1]);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassEnum.cls")]
        public void ClassEnumIsParsed()
        {
            var ed = Apex.ParseFile(ClassEnum) as EnumDeclarationSyntax;
            Assert.AreEqual("ClassEnum", ed.Identifier);
            Assert.AreEqual(1, ed.Modifiers.Count);
            Assert.AreEqual("public", ed.Modifiers[0]);

            Assert.AreEqual(3, ed.Members.Count);
            Assert.AreEqual("America", ed.Members[0].Identifier);
            Assert.AreEqual("Canada", ed.Members[1].Identifier);
            Assert.AreEqual("Russia", ed.Members[2].Identifier);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassException.cls")]
        public void ClassExceptionIsParsed()
        {
            var cd = Apex.ParseClass(ClassException);
            Assert.AreEqual("ClassException", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("exception", cd.BaseType.Identifier);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassGlobal.cls")]
        public void ClassGlobalIsParsed()
        {
            var cd = Apex.ParseClass(ClassGlobal);
            Assert.AreEqual("ClassGlobal", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("global", cd.Modifiers[0]);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassInterface.cls")]
        public void ClassInterfaceIsParsed()
        {
            var cd = Apex.ParseClass(ClassInterface);
            Assert.AreEqual("ClassInterface", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(1, cd.Interfaces.Count);
            Assert.AreEqual("IClassInterface", cd.Interfaces[0].Identifier);

            Assert.AreEqual(2, cd.Methods.Count);
            Assert.AreEqual("Id", cd.Methods[0].ReturnType.Identifier);
            Assert.AreEqual("GetId", cd.Methods[0].Identifier);
            Assert.AreEqual(1, cd.Methods[0].Body.Statements.Count);
            Assert.AreEqual("string", cd.Methods[1].ReturnType.Identifier);
            Assert.AreEqual("GetName", cd.Methods[1].Identifier);
            Assert.AreEqual(1, cd.Methods[0].Body.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassInternal.cls")]
        public void ClassInternalIsParsed()
        {
            var cd = Apex.ParseClass(ClassInternal);
            Assert.AreEqual("ClassInternal", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with sharing", cd.Modifiers[1]);
            Assert.AreEqual(2, cd.InnerClasses.Count);
            Assert.AreEqual("InternalClassOne", cd.InnerClasses[0].Identifier);
            Assert.AreEqual("InternalClassTwo", cd.InnerClasses[1].Identifier);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassRest.cls")]
        public void ClassRestIsParsed()
        {
            var cd = Apex.ParseClass(ClassRest);
            Assert.AreEqual("ClassRest", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("global", cd.Modifiers[0]);
            Assert.AreEqual(5, cd.Methods.Count);
            Assert.AreEqual(1, cd.Annotations.Count);
            Assert.AreEqual("RestResource", cd.Annotations[0].Identifier);
            Assert.AreEqual("urlMapping='/api/v1/RestDemo'", cd.Annotations[0].Parameters);

            Assert.AreEqual(1, cd.Methods[0].Annotations.Count);
            Assert.AreEqual("httpDelete", cd.Methods[0].Annotations[0].Identifier);
            Assert.AreEqual("DoDelete", cd.Methods[0].Identifier);
            Assert.AreEqual("void", cd.Methods[0].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[1].Annotations.Count);
            Assert.AreEqual("httpPost", cd.Methods[1].Annotations[0].Identifier);
            Assert.AreEqual("Post", cd.Methods[1].Identifier);
            Assert.AreEqual("void", cd.Methods[1].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[2].Annotations.Count);
            Assert.AreEqual("httpGet", cd.Methods[2].Annotations[0].Identifier);
            Assert.AreEqual("Get", cd.Methods[2].Identifier);
            Assert.AreEqual("string", cd.Methods[2].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[3].Annotations.Count);
            Assert.AreEqual("httpPatch", cd.Methods[3].Annotations[0].Identifier);
            Assert.AreEqual("Patch", cd.Methods[3].Identifier);
            Assert.AreEqual("void", cd.Methods[3].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[4].Annotations.Count);
            Assert.AreEqual("httpPut", cd.Methods[4].Annotations[0].Identifier);
            Assert.AreEqual("Put", cd.Methods[4].Identifier);
            Assert.AreEqual("void", cd.Methods[4].ReturnType.Identifier);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassUnitTest.cls")]
        public void ClassUnitTestIsParsed()
        {
            var cd = Apex.ParseClass(ClassUnitTest);
            Assert.AreEqual("ClassUnitTest", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(7, cd.Methods.Count);
            Assert.AreEqual(1, cd.Annotations.Count);
            Assert.AreEqual("isTest", cd.Annotations[0].Identifier);

            Assert.AreEqual(1, cd.Methods[0].Annotations.Count);
            Assert.AreEqual("TestSetup", cd.Methods[0].Annotations[0].Identifier);
            Assert.AreEqual("Setup", cd.Methods[0].Identifier);
            Assert.AreEqual("void", cd.Methods[0].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[1].Annotations.Count);
            Assert.AreEqual("isTest", cd.Methods[1].Annotations[0].Identifier);
            Assert.AreEqual("AssertTrue", cd.Methods[1].Identifier);
            Assert.AreEqual("void", cd.Methods[1].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[2].Annotations.Count);
            Assert.AreEqual("isTest", cd.Methods[2].Annotations[0].Identifier);
            Assert.AreEqual("AssertEquals", cd.Methods[2].Identifier);
            Assert.AreEqual("void", cd.Methods[2].ReturnType.Identifier);

            Assert.AreEqual(1, cd.Methods[3].Annotations.Count);
            Assert.AreEqual("isTest", cd.Methods[3].Annotations[0].Identifier);
            Assert.AreEqual("AssertNotEquals", cd.Methods[3].Identifier);
            Assert.AreEqual("void", cd.Methods[3].ReturnType.Identifier);

            Assert.False(cd.Methods[4].Annotations.Any());
            Assert.AreEqual(3, cd.Methods[4].Modifiers.Count);
            Assert.AreEqual("testmethod", cd.Methods[4].Modifiers[0]);
            Assert.AreEqual("public", cd.Methods[4].Modifiers[1]);
            Assert.AreEqual("static", cd.Methods[4].Modifiers[2]);
            Assert.AreEqual("AssertNew", cd.Methods[4].Identifier);
            Assert.AreEqual("void", cd.Methods[4].ReturnType.Identifier);

            Assert.False(cd.Methods[5].Annotations.Any());
            Assert.AreEqual(3, cd.Methods[5].Modifiers.Count);
            Assert.AreEqual("static", cd.Methods[5].Modifiers[0]);
            Assert.AreEqual("testmethod", cd.Methods[5].Modifiers[1]);
            Assert.AreEqual("public", cd.Methods[5].Modifiers[2]);
            Assert.AreEqual("AssertEqualsNew", cd.Methods[5].Identifier);
            Assert.AreEqual("void", cd.Methods[5].ReturnType.Identifier);

            Assert.False(cd.Methods[6].Annotations.Any());
            Assert.AreEqual(3, cd.Methods[6].Modifiers.Count);
            Assert.AreEqual("static", cd.Methods[6].Modifiers[0]);
            Assert.AreEqual("public", cd.Methods[6].Modifiers[1]);
            Assert.AreEqual("testmethod", cd.Methods[6].Modifiers[2]);
            Assert.AreEqual("AssertNotEqualsNew", cd.Methods[6].Identifier);
            Assert.AreEqual("void", cd.Methods[6].ReturnType.Identifier);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassUnitTestSeeAllData.cls")]
        public void ClassUnitTestSeeAllDataIsParsed()
        {
            var cd = Apex.ParseClass(ClassUnitTestSeeAllData);
            Assert.AreEqual("ClassUnitTestSeeAllData", cd.Identifier);
            Assert.AreEqual(1, cd.Annotations.Count);
            Assert.AreEqual("isTest", cd.Annotations[0].Identifier);
            Assert.AreEqual("SeeAllData=true", cd.Annotations[0].Parameters);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassVirtual.cls")]
        public void ClassVirtualIsParsed()
        {
            var cd = Apex.ParseClass(ClassVirtual);
            Assert.AreEqual("ClassVirtual", cd.Identifier);
            Assert.AreEqual(0, cd.Annotations.Count);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("virtual", cd.Modifiers[1]);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassWithOutSharing.cls")]
        public void ClassWithOutSharingIsParsed()
        {
            var cd = Apex.ParseClass(ClassWithOutSharing);
            Assert.AreEqual("ClassWithOutSharing", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("without sharing", cd.Modifiers[1]);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassWithSharing.cls")]
        public void ClassWithSharingIsParsed()
        {
            var cd = Apex.ParseClass(ClassWithSharing);
            Assert.AreEqual("ClassWithSharing", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with sharing", cd.Modifiers[1]);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ExceptionDemo.cls")]
        public void ExceptionDemoIsParsed()
        {
            var cd = Apex.ParseClass(ExceptionDemo);
            Assert.AreEqual("ExceptionDemo", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(2, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.NotNull(md.Body);
            Assert.AreEqual(1, md.Body.Statements.Count);

            var ts = md.Body.Statements[0] as TryStatementSyntax;
            Assert.NotNull(ts);
            Assert.NotNull(ts.Block);
            Assert.AreEqual(1, ts.Block.Statements.Count);
            Assert.AreEqual(1, ts.Catches.Count);

            var cc = ts.Catches[0];
            Assert.AreEqual("MathException", cc.Type.Identifier);
            Assert.AreEqual("e", cc.Identifier);
            Assert.NotNull(cc.Block);
            Assert.AreEqual(1, cc.Block.Statements.Count);

            var fc = ts.Finally;
            Assert.NotNull(fc);
            Assert.NotNull(fc.Block);
            Assert.AreEqual(1, fc.Block.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ForIfWhile.cls")]
        public void ForIfWhile2AreParsed()
        {
            var cd = Apex.ParseClass(ForIfWhile2);
            Assert.False(cd.Annotations.Any());
            Assert.AreEqual("ForIfWhile", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(7, cd.Methods.Count);
            Assert.False(cd.Properties.Any());

            var md = cd.Methods[0];
            Assert.AreEqual("MethodIfClean", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.AreEqual(1, md.Parameters.Count);
            Assert.AreEqual("place", md.Parameters[0].Identifier);
            Assert.AreEqual("Integer", md.Parameters[0].Type.Identifier);
            var block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);

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
            Assert.AreEqual(1, forStmt.Declaration.Variables.Count);
            Assert.AreEqual("i", forStmt.Declaration.Variables[0].Identifier);
            Assert.AreEqual("0", forStmt.Declaration.Variables[0].Expression);
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
            Assert.AreEqual("myInt", forEachStmt.Identifier);
            Assert.AreEqual("myInts", forEachStmt.Expression);
            block = forEachStmt.Statement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug (myInt)", block.Statements[0].Body);

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

            md = cd.Methods[5];
            Assert.AreEqual("ForLoopTest", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);

            md = cd.Methods[6];
            Assert.AreEqual("GetContact", md.Identifier);
            Assert.AreEqual("string", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.AreEqual(1, md.Parameters.Count);
            block = md.Body as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\GetSetDemo.cls")]
        public void GetSetDemoIsParsed()
        {
            var cd = Apex.ParseClass(GetSetDemo);
            Assert.AreEqual("GetSetDemo", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(0, cd.Methods.Count);
            Assert.AreEqual(11, cd.Properties.Count);
            Assert.AreEqual(3, cd.Fields.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\IClassInterface.cls")]
        public void IClassInterfaceIsParsed()
        {
            var cd = Apex.ParseClass(IClassInterface);
            Assert.AreEqual("IClassInterface", cd.Identifier);
            Assert.IsTrue(cd.IsInterface);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.IsTrue(cd.Methods[0].IsAbstract);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\IClassInterfaceExt.cls")]
        public void IClassInterfaceExtIsParsed()
        {
            var cd = Apex.ParseClass(IClassInterfaceExt);
            Assert.AreEqual("IClassInterfaceExt", cd.Identifier);
            Assert.IsTrue(cd.IsInterface);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(1, cd.Methods.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\JsonExample.cls")]
        public void JsonExampleIsParsed()
        {
            var cd = Apex.ParseClass(JsonExample);
            Assert.AreEqual("JsonExample", cd.Identifier);
            Assert.IsFalse(cd.IsInterface);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual("JsonExampleMethod", cd.Methods[0].Identifier);
            Assert.AreEqual(3, cd.Methods[0].Body.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ListAndArrayDemo.cls")] //, Ignore("TODO: array and list initializers")]
        public void ListAndArrayDemoIsParsed()
        {
            var cd = Apex.ParseClass(ListAndArrayDemo);
            Assert.AreEqual("ListAndArrayDemo", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(2, cd.Fields.Count);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual("Method", cd.Methods[0].Identifier);
            Assert.AreEqual(2, cd.Methods[0].Body.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\MethodAndConstructor.cls")]
        public void MethodAndConstructorIsParsed()
        {
            var cd = Apex.ParseClass(MethodAndConstructor);
            Assert.AreEqual("MethodAndConstructor", cd.Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("global", cd.Modifiers[0]);
            Assert.AreEqual("abstract", cd.Modifiers[1]);
            Assert.AreEqual(2, cd.Constructors.Count);
            Assert.AreEqual(17, cd.Methods.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\PrimitiveTypes.cls")]
        public void PrimitiveTypesIsParsed()
        {
            var cd = Apex.ParseClass(PrimitiveTypes);
            Assert.AreEqual("PrimitiveTypes", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(11, cd.Fields.Count);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual("DemoMethod", cd.Methods[0].Identifier);
            Assert.AreEqual(2, cd.Methods[0].Body.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\PropertyAndField.cls")]
        public void PropertyAndField2IsParsed()
        {
            var cd = Apex.ParseClass(PropertyAndField2);
            Assert.AreEqual("PropertyAndField", cd.Identifier);
            Assert.AreEqual(0, cd.LeadingComments.Count);
            Assert.AreEqual(1, cd.Methods.Count);
            Assert.AreEqual(3, cd.Properties.Count);
            Assert.AreEqual(9, cd.Fields.Count);

            var pd = cd.Properties[2];
            Assert.AreEqual("DateTimeGetSetArray", pd.Identifier);
            Assert.AreEqual("DateTime", pd.Type.Identifier);
            Assert.False(pd.Type.TypeParameters.Any());
            Assert.IsTrue(pd.Type.IsArray);
            Assert.NotNull(pd.Getter);
            Assert.NotNull(pd.Setter);

            var fd = cd.Fields[3];
            Assert.AreEqual("DateTimeList", fd.Fields[0].Identifier);
            Assert.AreEqual("list", fd.Type.Identifier);
            Assert.AreEqual(1, fd.Type.TypeParameters.Count);
            Assert.AreEqual("DateTime", fd.Type.TypeParameters[0].Identifier);
            Assert.IsFalse(fd.Type.IsArray);
            Assert.AreEqual("new list<DateTime>()", fd.Fields[0].Expression);

            fd = cd.Fields[4];
            Assert.False(fd.Annotations.Any());
            Assert.AreEqual("DateTimeArray", fd.Fields[0].Identifier);
            Assert.AreEqual("DateTime", fd.Type.Identifier);
            Assert.False(fd.Type.TypeParameters.Any());
            Assert.IsTrue(fd.Type.IsArray);
            Assert.AreEqual(1, fd.Modifiers.Count);
            Assert.AreEqual("public", fd.Modifiers[0]);
            Assert.AreEqual("new DateTime[5]", fd.Fields[0].Expression);

            fd = cd.Fields[8];
            Assert.False(fd.Annotations.Any());
            Assert.AreEqual("NameStaticFinal", fd.Fields[0].Identifier);
            Assert.AreEqual("String", fd.Type.Identifier);
            Assert.False(fd.Type.TypeParameters.Any());
            Assert.False(fd.Type.IsArray);
            Assert.AreEqual(3, fd.Modifiers.Count);
            Assert.AreEqual("public", fd.Modifiers[0]);
            Assert.AreEqual("static", fd.Modifiers[1]);
            Assert.AreEqual("final", fd.Modifiers[2]);
            Assert.AreEqual("'jay'", fd.Fields[0].Expression);

            var md = cd.Methods[0];
            Assert.AreEqual("MethodOne", md.Identifier);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Parameters.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);

            var block = md.Body;
            Assert.NotNull(block);
            Assert.AreEqual(7, block.Statements.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\RunAll.cls")]
        public void RunAllIsParsed()
        {
            var cd = Apex.ParseClass(RunAll);
            Assert.AreEqual("RunAll", cd.Identifier);
            Assert.AreEqual(1, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual(0, cd.Fields.Count);
            Assert.AreEqual(1, cd.Constructors.Count);
            Assert.AreEqual("RunAll", cd.Constructors[0].Identifier);
            Assert.AreEqual(10, cd.Constructors[0].Body.Statements.Count);
        }

        [Test]
        public void CommentsFileIsParsed()
        {
            var cd = Apex.ParseClass(Comments);
            Assert.AreEqual("Comments", cd.Identifier);
            Assert.AreEqual(1, cd.LeadingComments.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ClassUnitTestRunAs.cls")]
        public void ClassUnitTestRunAsIsParsed()
        {
            var cd = Apex.ParseClass(ClassUnitTestRunAs);
            Assert.AreEqual("ClassUnitTestRunAs", cd.Identifier);
            Assert.AreEqual(1, cd.Methods.Count);

            var md = cd.Methods[0];
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("RunAsExample", md.Identifier);
            Assert.AreEqual(2, md.Modifiers.Count);
            Assert.AreEqual("static", md.Modifiers[0]);
            Assert.AreEqual("testmethod", md.Modifiers[1]);

            var block = md.Body;
            Assert.AreEqual(2, block.Statements.Count);

            var runAs = block.Statements[1] as RunAsStatementSyntax;
            Assert.NotNull(runAs);
            Assert.AreEqual("newUser", runAs.Expression);
            Assert.NotNull(runAs.Statement as BlockSyntax);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\CommentFail.cls")]
        public void CommentsFailFileIsParsed()
        {
            var cd = Apex.ParseClass(CommentFail);
            Assert.AreEqual("CommentFail", cd.Identifier);
            Assert.AreEqual(0, cd.LeadingComments.Count);
            Assert.AreEqual(1, cd.TrailingComments.Count);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\ForIfWhile.cls")]
        public void ForIfWhile3IsParsed()
        {
            var cd = Apex.ParseClass(ForIfWhile3);
            Assert.AreEqual("ForIfWhile", cd.Identifier);
            Assert.AreEqual(8, cd.Methods.Count);

            var md = cd.Methods[7];
            Assert.AreEqual("ForSoql", md.Identifier);
            Assert.AreEqual(1, md.Body.Statements.Count);

            var forEach = md.Body.Statements[0] as ForEachStatementSyntax;
            Assert.AreEqual("Contact", forEach.Type.Identifier);
            Assert.AreEqual("contactList", forEach.Identifier);
            Assert.AreEqual("[SELECT Id, Name FROM Contact]", forEach.Expression);
            Assert.NotNull(forEach.Statement as BlockSyntax);
        }

        [Test(Description = @"SalesForceApexSharp\src\classes\PropertyAndField.cls")]
        public void PropertyAndField3IsParsed()
        {
            var cd = Apex.ParseClass(PropertyAndField3);
            Assert.AreEqual("PropertyAndField", cd.Identifier);

            Assert.AreEqual(1, cd.Initializers.Count);
            Assert.AreEqual(1, cd.Initializers[0].Body.Statements.Count);
            Assert.AreEqual("shouldRedirect =false", cd.Initializers[0].Body.Statements[0].Body);
        }

        [Test(Description = @"Portions of https://raw.githubusercontent.com/financialforcedev/fflib-apex-common/master/fflib/src/classes/fflib_ApplicationTest.cls")]
        public void ApplicationTestIsParsed()
        {
            var cd = Apex.ParseClass(ApplicationTest);
            Assert.AreEqual("ApplicationTest", cd.Identifier);
        }
    }
}
