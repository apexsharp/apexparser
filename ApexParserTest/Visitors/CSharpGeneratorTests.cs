using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApexParser.MetaClass;
using ApexParser.Parser;
using ApexParser.Visitors;
using NUnit.Framework;

namespace ApexParserTest.Visitors
{
    [TestFixture]
    public class CSharpGeneratorTests : TestFixtureBase
    {
        protected void Check(BaseSyntax node, string expected) =>
            CompareLineByLine(node.ToCSharp(), expected);

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
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class TestClass
                    {
                    }
                }");
        }

        [Test]
        public void EmptyTestClassDeclarationProducesTheRequiredNamespaceImports()
        {
            var cd = new ClassDeclarationSyntax
            {
                Identifier = "TestClass",
                Annotations = new List<AnnotationSyntax>
                {
                    new AnnotationSyntax
                    {
                        Identifier = "IsTest"
                    }
                }
            };

            Check(cd,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.ApexSharp.NUnit;
                    using Apex.System;
                    using SObjects;

                    [TestFixture]
                    class TestClass
                    {
                    }
                }");
        }

        [Test]
        public void EnumDeclarationProducesTheRequiredNamespaceImports()
        {
            var cd = new EnumDeclarationSyntax
            {
                Identifier = "TestEnum",
                Members = new List<EnumMemberDeclarationSyntax>
                {
                    new EnumMemberDeclarationSyntax { Identifier = "TestMember" },
                },
            };

            Check(cd,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    enum TestEnum
                    {
                        TestMember
                    }
                }");
        }

        [Test]
        public void ClassDeclarationWithAnnotatedTestMethodProducesTheRequiredNamespaceImports()
        {
            var cd = new ClassDeclarationSyntax
            {
                Identifier = "TestClass",
                Members = new List<MemberDeclarationSyntax>
                {
                    new MethodDeclarationSyntax
                    {
                        ReturnType = new TypeSyntax("void"),
                        Identifier = "Sample",
                        Annotations = new List<AnnotationSyntax>
                        {
                            new AnnotationSyntax { Identifier = "isTest" }
                        },
                        Body = new BlockSyntax(),
                    }
                }
            };

            Check(cd,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.ApexSharp.NUnit;
                    using Apex.System;
                    using SObjects;

                    class TestClass
                    {
                        [Test]
                        void Sample()
                        {
                        }
                    }
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
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
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
                LeadingComments = new List<string> { @" Test class
                    with several lines
                    of comments " },
                Identifier = "TestClass"
            };

            Check(cd,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
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
            Assert.AreEqual("string[]", apexArray.ToCSharp());
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
                Body = new BlockSyntax(),
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
                Body = new BlockSyntax(),
                Parameters = new List<ParameterSyntax>
                {
                    new ParameterSyntax("int", "x"),
                    new ParameterSyntax("int", "y")
                }
            };

            Check(constr,
                @"[DefaultConstructor]
                public Sample(int x, int y)
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
                Expression = new ExpressionSyntax("true"),
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
                Expression = new ExpressionSyntax("true"),
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
                Expression = new ExpressionSyntax("true"),
                ThenStatement = new StatementSyntax("hello()"),
                ElseStatement = new IfStatementSyntax
                {
                    Expression = new ExpressionSyntax("false"),
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
                        Expression = new ExpressionSyntax("'alice@wonderland.net'")
                    },
                    new VariableDeclaratorSyntax
                    {
                        Identifier = "bob",
                        Expression = new ExpressionSyntax("'bob@microsoft.com'")
                    }
                }
            };

            Check(decl, @"CustomerDto.User alice = ""alice@wonderland.net"", bob = ""bob@microsoft.com"";");
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
                            Expression = new ExpressionSyntax("0")
                        },
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "j",
                            Expression = new ExpressionSyntax("100")
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
                Condition = new ExpressionSyntax("i < 10"),
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
                Incrementors = new List<ExpressionSyntax>
                {
                    new ExpressionSyntax("i++"),
                    new ExpressionSyntax("j *= 2")
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
                            Expression = new ExpressionSyntax("0")
                        },
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "j",
                            Expression = new ExpressionSyntax("1")
                        }
                    }
                },
                Condition = new ExpressionSyntax("j < 1000"),
                Incrementors = new List<ExpressionSyntax>
                {
                    new ExpressionSyntax("i++"),
                    new ExpressionSyntax("j *= 2")
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
                Expression = new ExpressionSyntax("contacts"),
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
                Expression = new ExpressionSyntax("contacts.IsEmpty()"),
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
                Expression = new ExpressionSyntax("true"),
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
                Expression = new ExpressionSyntax("contacts.IsEmpty()"),
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
        public void ApexInsertStatementGeneratesSoqlHelperMethod()
        {
            var insertStatement = new InsertStatementSyntax
            {
                Expression = new ExpressionSyntax("contactNew")
            };

            Check(insertStatement, @"Soql.insert(contactNew);");
        }

        [Test]
        public void ApexUpdateStatementGeneratesSoqlHelperMethod()
        {
            var updateStatement = new UpdateStatementSyntax
            {
                Expression = new ExpressionSyntax("contacts")
            };

            Check(updateStatement, @"Soql.update(contacts);");
        }

        [Test]
        public void ApexDeleteStatementGeneratesSoqlHelperMethod()
        {
            var deleteStatement = new DeleteStatementSyntax
            {
                Expression = new ExpressionSyntax("contactOld")
            };

            Check(deleteStatement, @"Soql.delete(contactOld);");
        }

        [Test]
        public void ApexSoqlQueryIsTranslatedProperly()
        {
            var stmt = new StatementSyntax
            {
                Body = "[SELECT Id, Name FROM Contact WHERE Email = :email]"
            };

            Check(stmt, "Soql.query<Contact>(\"SELECT Id, Name FROM Contact WHERE Email = :email\", email);");
        }

        [Test]
        public void ApexTestAnnotationsConvertedToNUnitAttributes()
        {
            var annotation = new AnnotationSyntax { Identifier = ApexKeywords.IsTest };
            var attribute = CSharpCodeGenerator.ConvertUnitTestAnnotation(annotation, new ClassDeclarationSyntax());
            Assert.AreEqual("TestFixture", attribute.Identifier);

            attribute = CSharpCodeGenerator.ConvertUnitTestAnnotation(annotation, new MethodDeclarationSyntax());
            Assert.AreEqual("Test", attribute.Identifier);
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
                        Expression = new ExpressionSyntax("customer"),
                    },
                },
            };

            Check(acc,
                @"set
                {
                    Soql.insert(customer);
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
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    public class MyDemo
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
                            Soql.insert(c);
                        }

                        public float Size
                        {
                            get;
                            private set
                            {
                                System.debug(""trying to set"");
                            }
                        }
                    }
                }");
        }

        [Test]
        public void SingleStaticInitializerIsGeneratedAsStaticConstructor()
        {
            var apex = Apex.ParseClass(@"
            class SingleStaticInitializer {
                static {
                    System.debug('Hello');
                }
            }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class SingleStaticInitializer
                    {
                        static SingleStaticInitializer()
                        {
                            System.debug(""Hello"");
                        }
                    }
                }");
        }

        [Test]
        public void MultipleStaticInitializersAreGeneratedAsStaticConstructorWithSeveralBlocks()
        {
            var apex = Apex.ParseClass(@"
            class MultipleStaticInitializers {
                // the first initializer
                static {
                    System.debug(1);
                }
                // the second initializer
                static {
                    System.debug(2);
                }
                // the last initializer
                static {
                    System.debug(3);
                } // trailing comment
            }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class MultipleStaticInitializers
                    {
                        // the first initializer
                        static MultipleStaticInitializers()
                        {
                            // the first initializer
                            {
                                System.debug(1);
                            }

                            // the second initializer
                            {
                                System.debug(2);
                            }

                            // the last initializer
                            {
                                System.debug(3);
                            } // trailing comment
                        }
                    }
                }");
        }

        [Test]
        public void UnsupportedModifiersGetConvertedIntoAttributes()
        {
            var apex = Apex.ParseClass(@"// unsupported modifiers
            public global class TestClass {
                private with sharing class Inner1 { }
                public without sharing class Inner2 { }
                private testMethod void MyTest(final int x) { }
                public webservice void MyService() { }
                transient int TransientField = 0;
            }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    // unsupported modifiers
                    [Global]
                    public class TestClass
                    {
                        [WithSharing]
                        private class Inner1
                        {
                        }

                        [WithoutSharing]
                        public class Inner2
                        {
                        }

                        [Test]
                        private void MyTest([Final] int x)
                        {
                        }

                        [WebService]
                        public void MyService()
                        {
                        }

                        [Transient]
                        int TransientField = 0;
                    }
                }");
        }

        [Test]
        public void CustomCSharpNamespaceIsSupported()
        {
            var apex = Apex.ParseClass(@"// custom namespace
            public global class TestClass {
                private with sharing class TestInner { }
            }");

            CompareLineByLine(apex.ToCSharp(@namespace: "MyNamespace"),
                @"namespace MyNamespace
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    // custom namespace
                    [Global]
                    public class TestClass
                    {
                        [WithSharing]
                        private class TestInner
                        {
                        }
                    }
                }");
        }

        [Test]
        public void BuiltinApexTypesConvertedToCSharpTypes()
        {
            string Normalize(string id) =>
                new CSharpCodeGenerator().NormalizeTypeName(id);

            Assert.AreEqual("string", Normalize(ApexKeywords.String));
            Assert.AreEqual("bool", Normalize(ApexKeywords.Boolean));

            // not anymore: Apex.System.Datetime is used instead of System.DateTime
            //Assert.AreEqual(nameof(DateTime), Normalize(ApexKeywords.Datetime));
        }

        [Test]
        public void CommentsPrefixedWithNoApexAreCopiedAsIs()
        {
            var apex = Apex.ParseClass(@"
            public class MyDemo {
                private MyDemo(float s) { Size = s;while(true){break;} }
                public void Test(string name, int age) {
                    Contact c = new Contact(name, age);
                    insert c;
                }
                //:NoApex public float Size
                //:NoApex {
                //:noApex     get;
                //:noApex     private set;
                //:NoApex }
            }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    public class MyDemo
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
                            Soql.insert(c);
                        }

                        public float Size
                        {
                            get;
                            private set;
                        }
                    }
                }");
        }

        [Test]
        public void MapStringStringIsGeneratedProperly()
        {
            var apex = Apex.ParseClass(
                @"class Sample
                {
                    public static Map<String, string> SomeMap { get; private set; }

                    public Map<string, STRING> SomeMethod()
                    {
                        return new Map<String, string>();
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        public static Map<string, string> SomeMap { get; private set; }

                        public Map<string, string> SomeMethod()
                        {
                            return new Map<string, string>();
                        }
                    }
                }");
        }

        [Test]
        public void SomeDotClassGotReplacedWithTypeofSome()
        {
            var apex = Apex.ParseClass(
               @"class Sample {
                    public Datetime SomeMethod() {
                        Type type = Sample.class;
                        AnotherMethod(String.class);
                        return Datetime.now();
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        public Datetime SomeMethod()
                        {
                            Type type = typeof(Sample);
                            AnotherMethod(typeof(string));
                            return Datetime.now();
                        }
                    }
                }");
        }

        [Test]
        public void StringValueOfXIsReplacedWithXToString()
        {
            var apex = Apex.ParseClass(
               @"class Sample {
                    public Date SomeMethod() {
                        String x = string.valueOf(123);
                        String y = String.valueOf(10 + 20);
                        return Date.Today();
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        public Date SomeMethod()
                        {
                            string x = 123.ToString();
                            string y = (10 + 20).ToString();
                            return Date.Today();
                        }
                    }
                }");
        }

        [Test]
        public void AnnotationWithParametersIsConvertedProperly()
        {
            var apex = Apex.ParseClass(
               @"class Sample {
                    @future(CallOut=true)
                    @Some(Name='test')
                    public Datetime SomeMethod() {
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        [Future(CallOut=true)]
                        [Some(Name=""test"")]
                        public Datetime SomeMethod()
                        {
                        }
                    }
                }");
        }

        [Test]
        public void RunAsStatementIsGeneratedAsUsingSystemRunAs()
        {
            var apex = Apex.ParseClass(
               @"class Sample {
                    public void SomeMethod() {
                        System.runAs(me) {
                            System.debug('Hi there!');
                        }
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        public void SomeMethod()
                        {
                            using (System.runAs(me))
                            {
                                System.debug(""Hi there!"");
                            }
                        }
                    }
                }");
        }

        [Test]
        public void GlobalModifierGeneratesPublicClassWithGlobalAttribute()
        {
            var apex = Apex.ParseClass(
               @"global class Sample {
                    global static void SomeMethod() {
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    [Global]
                    public class Sample
                    {
                        [Global]
                        public static void SomeMethod()
                        {
                        }
                    }
                }");
        }

        [Test]
        public void AnnotationsWithMultipleParametersAreCommaSeparated()
        {
            var apex = Apex.ParseClass(
               @"class Sample {
                    @InvocableMethod(label='Get Account Names' description='Returns the list of account names corresponding to the specified account IDs.')
                    void SomeMethod() {
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        [InvocableMethod(label=""Get Account Names"", description=""Returns the list of account names corresponding to the specified account IDs."")]
                        void SomeMethod()
                        {
                        }
                    }
                }");
        }

        [Test]
        public void ApexInstanceOfIsConvertedToCSharpIs()
        {
            var apex = Apex.ParseClass(
               @"class Sample {
                    static void SomeMethod() {
                        bool t = Sample instanceof SObject;
                    }
                }");

            Check(apex,
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.ApexSharp.ApexAttributes;
                    using Apex.System;
                    using SObjects;

                    class Sample
                    {
                        static void SomeMethod()
                        {
                            bool t = Sample is SObject;
                        }
                    }
                }");
        }
    }
}
