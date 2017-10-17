using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.MetaClass;
using ApexParser.Parser;
using ApexParser.Visitors;
using NUnit.Framework;

namespace ApexParserTest.CodeGenerators
{
    [TestFixture]
    public class CSharpGeneratorTests : TestFixtureBase
    {
        [Test]
        public void EmptyClassDeclarationProducesTheRequiredNamespaceImports()
        {
            var cd = new ClassDeclarationSyntax
            {
                Identifier = "TestClass"
            };

            CompareLineByLine(cd.ToCSharp(),
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.System;
                    using SObjects;

                    class TestClass
                    {
                    }
                }");
        }

        [Test]
        public void ApexTypesGetConvertedToCSharpTypes()
        {
            var apexVoid = new TypeSyntax(ApexKeywords.Void);
            Assert.AreEqual("void", apexVoid.ToCSharp());

            var apexContact = new TypeSyntax("MyApp", "Dto", "Contact");
            Assert.AreEqual("MyApp.Dto.Contact", apexContact.ToCSharp());

            var apexList = new TypeSyntax("List")
            {
                TypeParameters = new List<TypeSyntax>
                {
                    new TypeSyntax("Custom", "Class")
                }
            };

            Assert.AreEqual("List<Custom.Class>", apexList.ToCSharp());

            var apexArray = new TypeSyntax("String") { IsArray = true };
            Assert.AreEqual("String[]", apexArray.ToCSharp());
        }

        [Test]
        public void ApexVoidMethodIsConvertedToCSharp()
        {
            var method = new MethodDeclarationSyntax
            {
                Attributes = new List<string> { "TestAttribute" },
                Modifiers = new List<string> { "public", "static" },
                ReturnType = new TypeSyntax("void"),
                Identifier = "Sample",
                Parameters = new List<ParameterSyntax>
                {
                    new ParameterSyntax("int", "x"),
                    new ParameterSyntax("int", "y")
                }
            };

            Check(method,
                @"[TestAttribute]
                public static void Sample(int x, int y)
                {
                }");
        }

        [Test]
        public void UnknownGenericStatementIsEmittedAsIs()
        {
            var st = new StatementSyntax("UnknownGenericStatement();");
            Assert.AreEqual("UnknownGenericStatement();", st.ToCSharp().Trim());
        }

        [Test]
        public void ApexIfStatementWithoutElseIsSupported()
        {
            var ifStatement = new IfStatementSyntax
            {
                Expression = "true",
                ThenStatement = new BreakStatementSyntax()
            };

            Check(ifStatement,
                @"if (true)
                    break;");
        }

        [Test]
        public void ApexIfStatementWithElseIsSupported()
        {
            var ifStatement = new IfStatementSyntax
            {
                Expression = "true",
                ThenStatement = new StatementSyntax("hello();"),
                ElseStatement = new BreakStatementSyntax(),
            };

            Check(ifStatement,
                @"if (true)
                    hello();
                else
                    break;");
        }

        [Test]
        public void ApexIfStatementWithElseIfBranchIsSupported()
        {
            var ifStatement = new IfStatementSyntax
            {
                Expression = "true",
                ThenStatement = new StatementSyntax("hello();"),
                ElseStatement = new IfStatementSyntax
                {
                    Expression = "false",
                    ThenStatement = new StatementSyntax("goodbye();"),
                    ElseStatement = new BreakStatementSyntax()
                }
            };

            Check(ifStatement,
                @"if (true)
                    hello();
                else if (false)
                    goodbye();
                else
                    break;");
        }

        [Test]
        public void ApexForStatementWithEmptyDeclarationIsSupported()
        {
            var forStatement = new ForStatementSyntax
            {
                Statement = new BreakStatementSyntax()
            };

            Check(forStatement,
                @"for (;;)
                    break;");
        }

        [Test]
        public void ApexVariableDeclarationIsHandled()
        {
            var decl = new VariableDeclarationSyntax
            {
                Type = new TypeSyntax("CustomerDto.User"),
                Variables = new List<VariableDeclaratorSyntax>
                {
                    new VariableDeclaratorSyntax
                    {
                        Identifier = "alice",
                        Expression = "'alice@wonderland.net'"
                    },
                    new VariableDeclaratorSyntax
                    {
                        Identifier = "bob",
                        Expression = "'bob@microsoft.com'"
                    }
                }
            };

            Check(decl, @"CustomerDto.User alice = 'alice@wonderland.net', bob = 'bob@microsoft.com';");
        }
    }
}
