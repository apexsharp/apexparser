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

            Check(cd,
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
        public void ClassDeclarationWithCommentsIsEmittedWithSingleLineComments()
        {
            var cd = new ClassDeclarationSyntax
            {
                CodeComments = new List<string> { " Test class" },
                Identifier = "TestClass"
            };

            Check(cd,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.System;
                    using SObjects;

                    // Test class
                    class TestClass
                    {
                    }
                }");
        }

        [Test]
        public void ClassDeclarationWithCommentsIsEmittedWithMultiLineComments()
        {
            var cd = new ClassDeclarationSyntax
            {
                CodeComments = new List<string> { @" Test class
                    with several lines
                    of comments " },
                Identifier = "TestClass"
            };

            Check(cd,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.System;
                    using SObjects;

                    /* Test class
                    with several lines
                    of comments */
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
                Annotations = new List<AnnotationSyntax>
                {
                    new AnnotationSyntax
                    {
                        Identifier = "TestAttribute"
                    },
                },
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
            var st = new StatementSyntax("UnknownGenericStatement()");
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
                ThenStatement = new StatementSyntax("hello()"),
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
                ThenStatement = new StatementSyntax("hello()"),
                ElseStatement = new IfStatementSyntax
                {
                    Expression = "false",
                    ThenStatement = new StatementSyntax("goodbye()"),
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

        [Test]
        public void ApexBlockSyntaxIsSupported()
        {
            var block = new BlockSyntax
            {
                new StatementSyntax("CallSomeMethod()"),
                new BreakStatementSyntax()
            };

            Check(block,
                @"{
                    CallSomeMethod();
                    break;
                }");
        }

        [Test]
        public void ApexForStatementWithVariableDeclarationIsSupported()
        {
            var forStatement = new ForStatementSyntax
            {
                Declaration = new VariableDeclarationSyntax
                {
                    Type = new TypeSyntax("int"),
                    Variables = new List<VariableDeclaratorSyntax>
                    {
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "i",
                            Expression = "0"
                        },
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "j",
                            Expression = "100"
                        }
                    }
                },
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(forStatement,
                @"for (int i = 0, j = 100;;)
                {
                    break;
                }");
        }

        [Test]
        public void ApexForStatementWithConditionIsSupported()
        {
            var forStatement = new ForStatementSyntax
            {
                Condition = "i < 10",
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(forStatement,
                @"for (; i < 10;)
                {
                    break;
                }");
        }

        [Test]
        public void ApexForStatementWithIncrementorsIsSupported()
        {
            var forStatement = new ForStatementSyntax
            {
                Incrementors = new List<string>
                {
                    "i++", "j *= 2"
                },
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(forStatement,
                @"for (;; i++, j *= 2)
                {
                    break;
                }");
        }

        [Test]
        public void ApexForStatementWithVariableDeclarationConditionAndIncrementorsIsSupported()
        {
            var forStatement = new ForStatementSyntax
            {
                Declaration = new VariableDeclarationSyntax
                {
                    Type = new TypeSyntax("int"),
                    Variables = new List<VariableDeclaratorSyntax>
                    {
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "i",
                            Expression = "0"
                        },
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "j",
                            Expression = "1"
                        }
                    }
                },
                Condition = "j < 1000",
                Incrementors = new List<string>
                {
                    "i++", "j *= 2"
                },
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(forStatement,
                @"for (int i = 0, j = 1; j < 1000; i++, j *= 2)
                {
                    break;
                }");
        }

        [Test]
        public void ApexForEachStatementIsSupported()
        {
            var forEachStatement = new ForEachStatementSyntax
            {
                Type = new TypeSyntax("Contact"),
                Identifier = "c",
                Expression = "contacts",
                Statement = new BlockSyntax()
            };

            Check(forEachStatement,
                @"foreach (Contact c in contacts)
                {
                }");
        }

        [Test]
        public void ApexDoStatementWithBlockSyntaxIsSupported()
        {
            var doStatement = new DoStatementSyntax
            {
                Expression = "contacts.IsEmpty()",
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(doStatement,
                @"do
                {
                    break;
                }
                while (contacts.IsEmpty());");
        }

        [Test]
        public void ApexDoStatementWithoutBlockSyntaxIsSupported()
        {
            var doStatement = new DoStatementSyntax
            {
                Expression = "true",
                Statement = new BreakStatementSyntax()
            };

            Check(doStatement,
                @"do
                    break;
                while (true);");
        }

        [Test]
        public void ApexWhileStatementIsSupported()
        {
            var whileStatement = new WhileStatementSyntax
            {
                Expression = "contacts.IsEmpty()",
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(whileStatement,
                @"while (contacts.IsEmpty())
                {
                    break;
                }");
        }
    }
}
