using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

                CompareIgnoreFormatting(@"
                ClassTwo classTwo = new ClassTwo();
                System.debug('Test');", md.CodeInsideMethod);
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
                Assert.AreEqual("System.debug('Test');", md.CodeInsideMethod);

                md = cd.Methods[1];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);
                Assert.AreEqual(string.Empty, md.CodeInsideMethod);
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
                CompareIgnoreFormatting(@"
                    // constructor
                    System.debug('Test');", md.CodeInsideMethod);

                md = cd.Methods[1];
                Assert.AreEqual("ClassTwo", md.Identifier);
                Assert.False(md.CodeComments.Any());
                Assert.False(md.Attributes.Any());
                Assert.AreEqual(1, md.Modifiers.Count);
                Assert.AreEqual("public", md.Modifiers[0]);
                Assert.AreEqual("ClassTwo", md.ReturnType.Identifier);
                CompareIgnoreFormatting(@"
                    // another constructor
                    // with a lot of misplaced comments", md.CodeInsideMethod);
                Assert.AreEqual(1, md.MethodParameters.Count);

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
                CompareIgnoreFormatting(@"System.debug('Hello');", md.CodeInsideMethod);
            }
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
            CompareIgnoreFormatting(@"Contact contactNew = new Contact(LastName = 'Jay1', EMail = 'abc@abc.com');
                insert contactNew;
                System.debug(contactNew.Id);

                List<Contact> contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
                for (Contact c : contacts)
                {
                    System.debug(c.Email); c.Email = 'new@new.com';
                }
                update contacts;
                contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
                for (Contact c : contacts)
                {
                    System.debug(c.Email);
                }
                delete contacts;
                contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
                if (contacts.isEmpty())
                {
                    System.debug('Del Worked');
                }", md.CodeInsideMethod);
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
    }
}
