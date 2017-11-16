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
        public void EnumDeclarationIsGenerated()
        {
            var ed = new EnumDeclarationSyntax
            {
                Identifier = "Boo",
                Members = new List<EnumMemberDeclarationSyntax>
                {
                    new EnumMemberDeclarationSyntax { Identifier = "Tru" },
                    new EnumMemberDeclarationSyntax { Identifier = "Fa" },
                },
            };

            Check(ed,
                @"enum Boo
                {
                    Tru,
                    Fa
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
        public void ApexVoidMethodIsGenerated()
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
                },
                Body = new BlockSyntax(),
            };

            Check(method,
                @"@TestAttribute
                public static void Sample(int x, int y)
                {
                }");
        }

        [Test]
        public void ApexConstructorIsGenerated()
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
                },
                Body = new BlockSyntax(),
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
                LeadingComments = new List<string>
                {
                    " this is the first leading comment",
                    " this is the second leading comment"
                },
                Expression = new ExpressionSyntax("true"),
                ThenStatement = new StatementSyntax("hello()").WithTrailingComment(" hello"),
                ElseStatement = new BreakStatementSyntax(),
            };

            Check(ifStatement,
                @"// this is the first leading comment
                // this is the second leading comment
                if (true)
                    hello(); // hello
                else
                    break;");
        }

        [Test]
        public void ApexIfStatementWithElseIfBranchIsSupported()
        {
            var ifStatement = new IfStatementSyntax
            {
                LeadingComments = new List<string> { "some nested ifs" },
                Expression = new ExpressionSyntax("true"),
                ThenStatement = new StatementSyntax("hello()").WithTrailingComment("there"),
                ElseStatement = new IfStatementSyntax
                {
                    Expression = new ExpressionSyntax("false"),
                    ThenStatement = new StatementSyntax("goodbye()").WithTrailingComment("and everywhere"),
                    ElseStatement = new BreakStatementSyntax().WithTrailingComment("down")
                }
            };

            Check(ifStatement,
                @"//some nested ifs
                if (true)
                    hello(); //there
                else if (false)
                    goodbye(); //and everywhere
                else
                    break; //down");
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
                            Expression = new ExpressionSyntax("0")
                        },
                        new VariableDeclaratorSyntax
                        {
                            Identifier = "j",
                            Expression = new ExpressionSyntax("1")
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
                Expression = new ExpressionSyntax("contacts"),
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
        public void ApexRunAsStatementIsSupported()
        {
            var runAsStatement = new RunAsStatementSyntax
            {
                Expression = new ExpressionSyntax("getCurrentUser()"),
                Statement = new BlockSyntax
                {
                    new BreakStatementSyntax()
                }
            };

            Check(runAsStatement,
                @"System.runAs(getCurrentUser())
                {
                    break;
                }");
        }

        [Test]
        public void ApexInsertStatementIsGenerated()
        {
            var insertStatement = new InsertStatementSyntax
            {
                Expression = new ExpressionSyntax("contactNew")
            };

            Check(insertStatement, @"insert contactNew;");
        }

        [Test]
        public void ApexUpdateStatementIsGenerated()
        {
            var updateStatement = new UpdateStatementSyntax
            {
                Expression = new ExpressionSyntax("contacts")
            };

            Check(updateStatement, @"update contacts;");
        }

        [Test]
        public void ApexDeleteStatementIsGenerated()
        {
            var deleteStatement = new DeleteStatementSyntax
            {
                Expression = new ExpressionSyntax("contactOld")
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
                        Expression = new ExpressionSyntax("customer"),
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
            // this is my demo
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
                @"// this is my demo
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

                    public void Test(String name, int age)
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

        [Test]
        public void LeadingAndTrailingCommentsForStatements()
        {
            var apex = Apex.ParseClass(@"
            @isTest
            public class ClassUnitTest
            {
                static testMethod void StaticTestMethodOne()
                {
                    //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    System.AssertNotEquals(5, 0, 'Assert Not Equal');
                }

                static testMethod void StaticTestMethodTwo()
                {
                    //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    System.AssertNotEquals(5, 0, 'Assert Not Equal');
                }
             }");

            Check(apex,
                @"@IsTest
                public class ClassUnitTest
                {
                    static testMethod void StaticTestMethodOne()
                    {
                        //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                        System.AssertNotEquals(5, 0, 'Assert Not Equal');

                        //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                        System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    }

                    static testMethod void StaticTestMethodTwo()
                    {
                        //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                        System.AssertNotEquals(5, 0, 'Assert Not Equal');

                        //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                        System.AssertNotEquals(5, 0, 'Assert Not Equal');
                    }
                 }");

            var md = apex.Methods.First();
            CompareLineByLine(md.GetCodeInsideMethod(),
                @"//System.AssertNotEquals(5, 0, 'Assert Not Equal');
                System.AssertNotEquals(5, 0, 'Assert Not Equal');

                //System.AssertNotEquals(5, 0, 'Assert Not Equal');
                System.AssertNotEquals(5, 0, 'Assert Not Equal');");
        }

        [Test]
        public void LeadingAndTrailingCommentsForStatementsAreGeneratedProperly()
        {
            var apex = Apex.ParseClass(@"// TestClass leading comment
            public class TestClass {
                // sample method leading comment
                void SampleMethod()
                // block leading comment
                {
                    // variable declaration leading comment1
                    // variable declaration leading comment2
                    int i = 10; // variable declaration trailing comment
                    // while loop leading comment
                    while (true) { break; } // loop body trailing comment

                    // return null leading comment
                    return null; // return null trailing comment
                // block inner comment
                } // block trailing comment
            } // class trailing comment");

            Check(apex,
                @"// TestClass leading comment
                public class TestClass
                {
                    // sample method leading comment
                    void SampleMethod()
                    // block leading comment
                    {
                        // variable declaration leading comment1
                        // variable declaration leading comment2
                        int i = 10; // variable declaration trailing comment

                        // while loop leading comment
                        while (true)
                        {
                            break;
                        } // loop body trailing comment

                        // return null leading comment
                        return null; // return null trailing comment

                        // block inner comment
                    } // block trailing comment
                } // class trailing comment");

            var md = apex.Methods.First();
            CompareLineByLine(md.GetCodeInsideMethod(),
                @"// variable declaration leading comment1
                // variable declaration leading comment2
                int i = 10; // variable declaration trailing comment

                // while loop leading comment
                while (true)
                {
                    break;
                } // loop body trailing comment

                // return null leading comment
                return null; // return null trailing comment

                // block inner comment");
        }

        [Test]
        public void RunAsStatementIsGenerated()
        {
            var text = @"
            class Test
            {
                @isTest public static void LogErrorExceptionWithMessage()
                {
                    System.runAs(Info3TestFactory.getGatewayAdminUser())
                    {

                    }
                }
            }";

            var apex = Apex.ParseFile(text);
            Check(apex,
                @"class Test
                {
                    @IsTest
                    public static void LogErrorExceptionWithMessage()
                    {
                        System.runAs(Info3TestFactory.getGatewayAdminUser())
                        {
                        }
                    }
                }");
        }

        [Test]
        public void JsonStringGeneratorIsGenerated()
        {
            var text = @"
            class Test
            {
                public void Test()
                {
                    string reqBody = '';
                    reqBody = '{""size"":1,""application"":[{""id"":""com.gm.testxy111.pkg"",""version"":""1"",""action"":""Update"",""status"":{""code"":""success""}}]}';
                    reqBody = '{""size"":0,""application"":[]}';
                    reqBody = '{""size"":1,""application"":[{""id"":""com.gm.testxy111.pkg"",""version"":""1"",""action"":""Update"",""status"":{""code"":""success""}}]}';
                }
            }";

            var apex = Apex.ParseFile(text);
            Check(apex,
                @"class Test
                {
                    public void Test()
                    {
                        String reqBody = '';
                        reqBody = '{""size"":1,""application"":[{""id"":""com.gm.testxy111.pkg"",""version"":""1"",""action"":""Update"",""status"":{""code"":""success""}}]}';
                        reqBody = '{""size"":0,""application"":[]}';
                        reqBody = '{""size"":1,""application"":[{""id"":""com.gm.testxy111.pkg"",""version"":""1"",""action"":""Update"",""status"":{""code"":""success""}}]}';
                    }
                }");
        }

        [Test]
        public void JsonStringGeneratorIsGenerated2()
        {
            var text = @"
            class Test
            {
                public void Test()
                {
                    tmp.Some_Identifier__tmp = '{""category"":[{""name"":""L10N.Translations"",""details"":{""data"":[{""val"":""50"",""name"":""RecentDestinationsMax""}]}},{""name"":""L10N.Translations"",""metadata"":{""property"":[{""val"":""6"",""name"":""HomeScreenPages""},{""val"":""2"",""name"":""HomeScreenRows""},{""val"":""4"",""name"":""HomeScreenCols""}]}},{""name"":""L10N.Translations"",""metadata"":{""property"":[{""val"":""40"",""name"":""FavoritesAudioMax""},{""val"":""10"",""name"":""Unidentified""},';
                    tmp.Some_Identifier__tmp += '{""tmp"":""20"",""name"":""Something""},{""val"":""2"",""name"":""FavoriteBundleVersion""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},';
                    tmp.Some_Identifier__tmp += '{""tmp"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},';
                    tmp.Some_Identifier__tmp += '{""tmp"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""true"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""true"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},';
                    tmp.Some_Identifier__tmp += '{""tmp"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""}]}},{""name"":""L10N.Translations"",""metadata"":{""property"":[{""val"":""1920x1080"",""name"":""HMIResolution""},{""val"":""5.1.1"",""name"":""AndroidOS""},{""val"":""tmp"",""name"":""BuildVersion""},{""val"":""1"",""name"":""GMFrameworkVersion""},{""val"":""From CPUInfo"",""name"":""DeviceProcessor""}]}}]}';
                }
            }";

            var apex = Apex.ParseFile(text);
            Check(apex,
                @"class Test
                {
                    public void Test()
                    {
                        tmp.Some_Identifier__tmp = '{""category"":[{""name"":""L10N.Translations"",""details"":{""data"":[{""val"":""50"",""name"":""RecentDestinationsMax""}]}},{""name"":""L10N.Translations"",""metadata"":{""property"":[{""val"":""6"",""name"":""HomeScreenPages""},{""val"":""2"",""name"":""HomeScreenRows""},{""val"":""4"",""name"":""HomeScreenCols""}]}},{""name"":""L10N.Translations"",""metadata"":{""property"":[{""val"":""40"",""name"":""FavoritesAudioMax""},{""val"":""10"",""name"":""Unidentified""},';
                        tmp.Some_Identifier__tmp += '{""tmp"":""20"",""name"":""Something""},{""val"":""2"",""name"":""FavoriteBundleVersion""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},';
                        tmp.Some_Identifier__tmp += '{""tmp"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},';
                        tmp.Some_Identifier__tmp += '{""tmp"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""true"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""true"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},{""val"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""},';
                        tmp.Some_Identifier__tmp += '{""tmp"":""false"",""name"":""IRQ_NOT_LESS_OR_EQUAL""}]}},{""name"":""L10N.Translations"",""metadata"":{""property"":[{""val"":""1920x1080"",""name"":""HMIResolution""},{""val"":""5.1.1"",""name"":""AndroidOS""},{""val"":""tmp"",""name"":""BuildVersion""},{""val"":""1"",""name"":""GMFrameworkVersion""},{""val"":""From CPUInfo"",""name"":""DeviceProcessor""}]}}]}';
                    }
                }");
        }

        [Test]
        public void JsonStringGeneratorIsGenerated3()
        {
            var text = @"
            class Test
            {
                public void Test()
                {
                    tmp.requestBody = Blob.valueof('{test}');
                    tmp.requestBody = Blob.valueof('{test}');
                    tmp.setBody('{""name"":""name""}');
                    tmp.tmpName = new List<String> {'test1', 'test2'};
                    tmp.tmpDesc = new List<String> {'test1', 'test2'};
                    tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                    tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                    tmp.SomeIdentifier__tmp = ' {}8236kjah{}{';
                    tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                    tmp.SomeIdentifier__tmp = 'value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                    tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                    tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                    tmp.SomeIdentifier__tmp = ' : ""Temp"" } ] }';
                    tmp.requestBody = Blob.valueOf('{ ""checklist"": [ { ""checklistItem"": [ { ""id"": ""' + existingChecklist0.id + '"", ""isChecked"": false }, { ""id"": ""' + newChecklist0.id + '"", ""isChecked"": true }, { ""id"": ""' + newChecklist1.id + '"", ""isChecked"": false } ] } ] }');
                    result = Something.replace(new List<tmp.SomeRequest>{req}, tmp);
                }
            }";

            var apex = Apex.ParseFile(text);
            Check(apex,
                @"class Test
                {
                    public void Test()
                    {
                        tmp.requestBody = Blob.valueof('{test}');
                        tmp.requestBody = Blob.valueof('{test}');
                        tmp.setBody('{""name"":""name""}');
                        tmp.tmpName = new List<String>{'test1', 'test2'};
                        tmp.tmpDesc = new List<String>{'test1', 'test2'};
                        tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                        tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                        tmp.SomeIdentifier__tmp = ' {}8236kjah{}{';
                        tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                        tmp.SomeIdentifier__tmp = 'value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                        tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                        tmp.SomeIdentifier__tmp = '{ ""SegmentationFault"" : [ { ""key"" : ""Age"", ""value"" : ""1234"" }, { ""key"" : ""Species"", ""value"" : ""Beaver"" } ] }';
                        tmp.SomeIdentifier__tmp = ' : ""Temp"" } ] }';
                        tmp.requestBody = Blob.valueOf('{ ""checklist"": [ { ""checklistItem"": [ { ""id"": ""'+ existingChecklist0.id + '"", ""isChecked"": false }, { ""id"": ""'+ newChecklist0.id + '"", ""isChecked"": true }, { ""id"": ""'+ newChecklist1.id + '"", ""isChecked"": false } ] } ] }');
                        result = Something.replace(new List<tmp.SomeRequest>{req}, tmp);
                    }
                }");
        }

        [Test]
        public void NewListExpressionIsGenerated()
        {
            var text = @"
            class Test {
                void Test() {
                    objCtrl.deliverableTypesName = new List<String>
                    {
                            'test1', 'test2'
                    }

                    ;
                    objCtrl.deliverableTypesDescription = new List<String>
                    {
                            'test1', 'test2'
                    }

                    ;
                }
            }";

            var apex = Apex.ParseFile(text);
            Check(apex,
                @"class Test
                {
                    void Test()
                    {
                        objCtrl.deliverableTypesName = new List<String>{'test1', 'test2'};
                        objCtrl.deliverableTypesDescription = new List<String>{'test1', 'test2'};
                    }
                }");
        }

        [Test]
        public void ModifiersAreGeneratedProperly()
        {
            var apex = Apex.ParseClass(@"// these modifiers are not supported by C# target
            public global class TestClass {
                private with sharing class Inner1 { }
                public without sharing class Inner2 { }
                private testMethod void MyTest(final int x) { }
                public webservice void MyService() { }
                transient void TransientMethod() { }
            }");

            Check(apex,
                @"// these modifiers are not supported by C# target
                public global class TestClass
                {
                    private with sharing class Inner1
                    {
                    }

                    public without sharing class Inner2
                    {
                    }

                    private testMethod void MyTest(final int x)
                    {
                    }

                    public webservice void MyService()
                    {
                    }

                    transient void TransientMethod()
                    {
                    }
                }");
        }

        [Test]
        public void ClassInnerCommentsAfterInnerClassAreGenerated()
        {
            var apex = Apex.ParseClass(
                @"class Test
                {
                    // leading
                    class InnerTest
                    {
                        // inner comment
                    } // trailing comment
                    // inner comments
                } // trailing");

            Check(apex,
                @"class Test
                {
                    // leading
                    class InnerTest
                    {
                        // inner comment
                    } // trailing comment

                    // inner comments
                } // trailing");
        }

        [Test]
        public void ClassInnerEnumsAreGenerated()
        {
            var apex = Apex.ParseClass(
                @"class Test
                {
                    // leading
                    public enum Boo
                    {
                        // true
                        Tru,
                        // false
                        Fa
                        // inner enum comment
                    } // trailing comment
                    // inner comments
                } // trailing");

            Check(apex,
                @"class Test
                {
                    // leading
                    public enum Boo
                    {
                        // true
                        Tru,
                        // false
                        Fa
                        // inner enum comment
                    } // trailing comment

                    // inner comments
                } // trailing");
        }
    }
}
