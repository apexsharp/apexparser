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
    public class ApexGeneratorTests : TestFixtureBase
    {
        protected void Check(BaseSyntax node, string expected) =>
            CompareLineByLine(node.ToApex(), expected);

        [Test]
        public void EmptyClassDeclarationProducesTheRequiredNamespaceImports()
        {
            var cd = new ClassDeclarationSyntax
            {
                Identifier = "TestClass"
            };

            Check(cd,
                @"class TestClass
                {
                }");
        }

        [Test]
        public void ClassDeclarationWithCommentsIsEmittedWithSingleLineComments()
        {
            var cd = new ClassDeclarationSyntax
            {
                LeadingComments = new List<string> { " Test class" },
                Identifier = "TestClass"
            };

            Check(cd,
                @"// Test class
                class TestClass
                {
                }");
        }

        [Test]
        public void ClassDeclarationWithCommentsIsEmittedWithMultiLineComments()
        {
            var cd = new ClassDeclarationSyntax
            {
                LeadingComments = new List<string> { @" Test class
                    with several lines
                    of comments " },
                Identifier = "TestClass"
            };

            Check(cd,
                @"/* Test class
                with several lines
                of comments */
                class TestClass
                {
                }");
        }

        [Test]
        public void ApexTypesAreSupported()
        {
            var apexVoid = new TypeSyntax(ApexKeywords.Void);
            Assert.AreEqual("void", apexVoid.ToApex());

            var apexContact = new TypeSyntax("MyApp", "Dto", "Contact");
            Assert.AreEqual("MyApp.Dto.Contact", apexContact.ToApex());

            var apexList = new TypeSyntax("List")
            {
                TypeParameters = new List<TypeSyntax>
                {
                    new TypeSyntax("Custom", "Class")
                }
            };

            Assert.AreEqual("List<Custom.Class>", apexList.ToApex());

            var apexArray = new TypeSyntax("String") { IsArray = true };
            Assert.AreEqual("String[]", apexArray.ToApex());
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
                @"@TestAttribute
                public static void Sample(int x, int y)
                {
                }");
        }

        [Test]
        public void ApexConstructorIsConvertedToCSharp()
        {
            var constr = new ConstructorDeclarationSyntax
            {
                Annotations = new List<AnnotationSyntax>
                {
                    new AnnotationSyntax
                    {
                        Identifier = "DefaultConstructor"
                    },
                },
                Modifiers = new List<string> { "public" },
                ReturnType = new TypeSyntax("Sample"),
                Identifier = "Sample",
                Parameters = new List<ParameterSyntax>
                {
                    new ParameterSyntax("int", "x"),
                    new ParameterSyntax("int", "y")
                }
            };

            Check(constr,
                @"@DefaultConstructor
                public Sample(int x, int y)
                {
                }");
        }

        [Test]
        public void UnknownGenericStatementIsEmittedAsIs()
        {
            var st = new StatementSyntax("UnknownGenericStatement()");
            Assert.AreEqual("UnknownGenericStatement();", st.ToApex().Trim());
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
                @"for (Contact c : contacts)
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

        [Test]
        public void ApexInsertStatementIsGenerated()
        {
            var insertStatement = new InsertStatementSyntax
            {
                Expression = "contactNew"
            };

            Check(insertStatement, @"insert contactNew;");
        }

        [Test]
        public void ApexUpdateStatementIsGenerated()
        {
            var updateStatement = new UpdateStatementSyntax
            {
                Expression = "contacts"
            };

            Check(updateStatement, @"update contacts;");
        }

        [Test]
        public void ApexDeleteStatementIsGenerated()
        {
            var deleteStatement = new DeleteStatementSyntax
            {
                Expression = "contactOld"
            };

            Check(deleteStatement, @"delete contactOld;");
        }

        [Test]
        public void EmptyPublicGetAccessorIsGenerated()
        {
            var acc = new AccessorDeclarationSyntax
            {
                IsGetter = true,
                Modifiers = new List<string> { "public" }
            };

            Check(acc, "public get;");
        }

        [Test]
        public void EmptyPrivateSetAccessorIsGenerated()
        {
            var acc = new AccessorDeclarationSyntax
            {
                IsGetter = false,
                Modifiers = new List<string> { "private" }
            };

            Check(acc, "private set;");
        }

        [Test]
        public void NonEmptyProtectedGetAccessorIsGenerated()
        {
            var acc = new AccessorDeclarationSyntax
            {
                IsGetter = true,
                Modifiers = new List<string> { "protected" },
                Body = new BlockSyntax
                {
                    new BreakStatementSyntax(),
                },
            };

            Check(acc,
                @"protected get
                {
                    break;
                }");
        }

        [Test]
        public void NonEmptySetAccessorIsGenerated()
        {
            var acc = new AccessorDeclarationSyntax
            {
                IsGetter = false,
                Body = new BlockSyntax
                {
                    new InsertStatementSyntax
                    {
                        Expression = "customer",
                    },
                },
            };

            Check(acc,
                @"set
                {
                    insert customer;
                }");
        }

        [Test]
        public void AutomaticPropertyIsGeneratedAsGetSetRegardlessOfTheAccessorOrder()
        {
            var prop = new PropertyDeclarationSyntax
            {
                Modifiers = new List<string> { "public" },
                Type = new TypeSyntax("string"),
                Identifier = "Name",
                Accessors = new List<AccessorDeclarationSyntax>
                {
                    new AccessorDeclarationSyntax
                    {
                        IsGetter = false,
                    },
                    new AccessorDeclarationSyntax
                    {
                        IsGetter = true
                    }
                }
            };

            Check(prop, "public string Name { get; set; }");
        }

        [Test]
        public void AutomaticPropertyIsGeneratedWithModifiersOnASingleLine()
        {
            var prop = new PropertyDeclarationSyntax
            {
                Modifiers = new List<string> { "protected" },
                Type = new TypeSyntax("int"),
                Identifier = "Age",
                Accessors = new List<AccessorDeclarationSyntax>
                {
                    new AccessorDeclarationSyntax
                    {
                        IsGetter = true,
                        Modifiers = new List<string>{ "public" },
                    },
                    new AccessorDeclarationSyntax
                    {
                        IsGetter = false,
                        Modifiers = new List<string>{ "private" },
                    }
                }
            };

            Check(prop, "protected int Age { public get; private set; }");
        }

        [Test]
        public void NonAutomaticPropertyIsGeneratedOnMultipleLines()
        {
            var prop = new PropertyDeclarationSyntax
            {
                Type = new TypeSyntax("object"),
                Identifier = "Something",
                Accessors = new List<AccessorDeclarationSyntax>
                {
                    new AccessorDeclarationSyntax
                    {
                        IsGetter = true,
                        Body = new BlockSyntax
                        {
                            new StatementSyntax { Body = "return null" },
                        },
                    },
                    new AccessorDeclarationSyntax
                    {
                        IsGetter = false,
                    }
                }
            };

            Check(prop,
                @"object Something
                {
                    get
                    {
                        return null;
                    }
                    set;
                }");
        }

        [Test]
        public void ClassWithConstructorMethodAndPropertyIsGenerated()
        {
            var apex = Apex.ParseClass(@"
            public class MyDemo {
                private MyDemo(float s) { Size = s;while(true){break;} }
                public void Test(string name, int age) {
                    Contact c = new Contact(name, age);
                    insert c;
                }
                public float Size { get; private set {
                        System.debug('trying to set');
                    }
                }
            }");

            Check(apex,
                @"public class MyDemo
                {
                    private MyDemo(float s)
                    {
                        Size = s;
                        while (true)
                        {
                            break;
                        }
                    }

                    public void Test(string name, int age)
                    {
                        Contact c = new Contact(name, age);
                        insert c;
                    }

                    public float Size
                    {
                        get;
                        private set
                        {
                            System.debug('trying to set');
                        }
                    }
                }");
        }
    }
}
