using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser;
using ApexParser.MetaClass;
using ApexParser.Visitors;
using NUnit.Framework;

namespace ApexParserTest.Visitors
{
    [TestFixture]
    public class ApexSyntaxBuilderTests : TestFixtureBase
    {
        private static Regex NoWhitespaceRegex = new Regex(@"\s", RegexOptions.Compiled);

        protected void Check(BaseSyntax node, string expected)
        {
            string nows(string s) =>
                NoWhitespaceRegex.Replace(s, string.Empty);

            var nodeToApex = node.ToApex();
            Assert.AreEqual(nows(expected), nows(nodeToApex));
            CompareLineByLine(nodeToApex, ApexSharpParser.IndentApex(expected));
        }

        protected void Check(string csharpUnit, params string[] apexClasses)
        {
            var csharpNode = ApexSharpParser.ParseText(csharpUnit);
            var apexNodes = ApexSyntaxBuilder.GetApexSyntaxNodes(csharpNode);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(apexClasses.Length, apexNodes.Count);
                foreach (var apexItem in apexNodes.Zip(apexClasses, (node, text) => new { node, text }))
                {
                    Check(apexItem.node, apexItem.text);
                }
            });
        }

        [Test]
        public void ApexBuilderForNullReturnsEmptyListOfApexSyntaxTrees()
        {
            var nodes = ApexSyntaxBuilder.GetApexSyntaxNodes(null);
            Assert.IsNotNull(nodes);
            Assert.IsFalse(nodes.Any());
        }

        [Test]
        public void EmptyClassIsGenerated()
        {
            var csharp = "class Test {}";
            var cs = ApexSharpParser.ParseText(csharp);
            var apex = ApexSyntaxBuilder.GetApexSyntaxNodes(cs);
            Assert.NotNull(apex);
            Assert.AreEqual(1, apex.Count);

            var cd = apex.OfType<ClassDeclarationSyntax>().FirstOrDefault();
            Assert.NotNull(cd);
            Assert.AreEqual("Test", cd.Identifier);

            Check(csharp, "class Test {}");
        }

        [Test]
        public void MultipleClassesAreGeneratedAsDifferentFiles()
        {
            Check("class Test1{} class Test2{}", "class Test1{}", "class Test2{}");
            Check("class t1{}class t2{}class t3{}class t4", "class t1{}", "class t2{}", "class t3{}", "class t4{}");
        }

        [Test]
        public void BaseClassIsGenerated()
        {
            Check("class Test : Base {}", "class Test extends Base {}");
            Check("class Test : List<Customer> {}", "class Test extends List<Customer> {}");
            Check("class MyClass : MyBase, IDisposable, IMaybe<Entity> {}", "class MyClass extends MyBase implements IDisposable, IMaybe<Entity> {}");

            // TODO: fix the ConvertType method
            ////Check("class Test : List<string> {}", "class Test extends List<string> {}");
        }

        [Test]
        public void ClassModifiersAreGenerated()
        {
            Check("public class Test {}", "public class Test {}");
            Check("static class Test {}", "static class Test {}");
            Check("public static class Test : Base {}", "public static class Test extends Base {}");
        }

        [Test]
        public void ClassWithConstructorIsGenerated()
        {
            Check("class A { A(){} }", "class A { A(){} }");
            Check("class B { public B(){} }", "class B { public B(){} }");
        }

        [Test]
        public void InterfaceIsGenerated()
        {
            Check("interface A { }", "interface A { }");
            Check("public interface B { int x { get; } }", "public interface B { Integer x { get; } }");
            Check("interface C : IDisposable { }", "interface C extends IDisposable { }");
            Check("public class D : B { interface C : IDisposable { } }", "public class D extends B { interface C extends IDisposable { } }");
            Check("interface E { } interface F { }", "interface E { }", "interface F { }");
        }

        [Test]
        public void EnumIsGenerated()
        {
            Check("enum A { B }", "enum A { B }");
            Check("public enum C { D, E, F }", "public enum C { D, E, F }");
            Check("enum X { Y } enum Z { T }", "enum X { Y }", "enum Z { T }");
            Check("public class D { enum X { Y } enum Z { T } }", "public class D { enum X { Y } enum Z { T } }");
        }

        [Test]
        public void ConstructorParametersAreGenerated()
        {
            Check("class T {T(int x){}}", "class T {T(Integer x){}}");
            Check("class Sample {Sample(int x, Customer y){}}", "class Sample {Sample(Integer x, Customer y){}}");
        }

        [Test]
        public void ClassWithMethodIsGenerated()
        {
            Check("class A { void X(){} }", "class A { void X(){} }");
            Check("class B { public int T(int R){} }", "class B { public Integer T(Integer R){} }");
            Check("class A { void X(){} int Y(){} }", "class A { void X(){} Integer Y(){} }");
        }

        [Test]
        public void ClassWithFieldIsGenerated()
        {
            Check("class X { int Y; }", "class X { Integer Y; }");
            Check("class X { public int Y, Z; }", "class X { public Integer Y, Z; }");
            Check("class Test { public int Y = 10, Z = 20 + 30; }", "class Test { public Integer Y = 10, Z = 20 + 30; }");
        }

        [Test]
        public void ClassWithPropertyIsGenerated()
        {
            Check("class X { int Y { get; } }", "class X { Integer Y { get; } }");
            Check("class Test { public int Name { get; set; } }", "class Test { public Integer Name { get; set; } }");
        }

        [Test]
        public void VariableDeclarationIsGenerated()
        {
            Check("class T { void Z() { int x; } }", "class T { void Z() { Integer x; } }");
            Check("class T { void Z() { int x = 10; } }", "class T { void Z() { Integer x = 10; } }");
            Check("class T { void Z() { MyString a = \"yes\"; } }", "class T { void Z() { MyString a = 'yes'; } }");

            // TODO: convert built-in types
            ////Check("class T { void Z() { MyString a = \"yes\"; } }", "class T { void Z() { MyString a = 'yes'; } }");

            // var keyword requires semantic analysis
            ////Check("class T { void Z() { var x = 10; } }", "class T { void Z() { int x = 10; } }");
        }

        [Test]
        public void NestedBlockStatementIsGenerated()
        {
            Check("class T { void Z() { {} } }", "class T { void Z() { {} } }");
            Check("class T { void Z() { int x; { int y; } int z; } }", "class T { void Z() { Integer x; { Integer y; } Integer z; } }");
        }

        [Test]
        public void NestedClassesAndEnumsAreGenerated()
        {
            Check("class O { class I {} }", "class O { class I {} }");
            Check("class O { enum I { A } }", "class O { enum I { A } }");
            Check("class O { class T { } enum I { A } }", "class O { class T { } enum I { A } }");
            Check("class O { class T { enum I { A } } }", "class O { class T { enum I { A } } }");
            Check("class O { class T { enum I { A } } enum Z { X } }", "class O { class T { enum I { A } } enum Z { X } }");
        }

        [Test]
        public void IfStatementIsGenerated()
        {
            Check("class A { void T() { if (true) int x; } }", "class A { void T() { if (true) Integer x; } }");
            Check("class A { void T() { if (true) { } else int y; } }", "class A { void T() { if (true) { } else Integer y; } }");
            Check("class A { void T() { if (true) { } else if (false) int y; } }", "class A { void T() { if (true) { } else if (false) Integer y; } }");
        }

        [Test]
        public void ReturnStatementIsGenerated()
        {
            Check("class A { void T() { return x; } }", "class A { void T() { return x; } }");
            Check("class A { void T() { return; } }", "class A { void T() { return; } }");
        }

        [Test]
        public void SoqlQueryIsGenerated()
        {
            Check("class A { void T() { Customer c = Soql.Query<T>(\"SELECT Id FROM Customer\"); } }",
                "class A { void T() { Customer c = [SELECT Id FROM Customer]; } }");
        }

        [Test]
        public void SoqlInsertUpdateDeleteStatementsAreGenerated()
        {
            Check("class A { void T() { Soql.Insert(customerNew); } }", "class A { void T() { insert customerNew; } }");
            Check("class A { void T() { Soql.Update(customer); } }", "class A { void T() { update customer; } }");
            Check("class A { void T() { Soql.Delete(customer); } }", "class A { void T() { delete customer; } }");
        }

        [Test]
        public void TryCatchIsGenerated()
        {
            Check("class X { void T() { try {} catch {} } }", "class X { void T() { try {} catch {} } }");
            Check("class X { void T() { try {} catch(Exception) {} } }", "class X { void T() { try {} catch(Exception) {} } }");
            Check("class X { void T() { try {} catch(Exception ex) {} } }", "class X { void T() { try {} catch(Exception ex) {} } }");
            Check("class X { void T() { try {} catch(Exception ex) {} catch {} } }", "class X { void T() { try {} catch(Exception ex) {} catch {} } }");
        }

        [Test]
        public void TryFinallyAndTryCatchFinallyIsGenerated()
        {
            Check("class X { void T() { try {} finally {} } }", "class X { void T() { try {} finally {} } }");
            Check("class X { void T() { try {} catch {} finally {} } }", "class X { void T() { try {} catch {} finally {} } }");
            Check("class X { void T() { try {} catch(Exception) {} finally {}} }", "class X { void T() { try {} catch(Exception) {} finally {}} }");
            Check("class X { void T() { try {} catch(Exception ex) {} finally {} } }", "class X { void T() { try {} catch(Exception ex) {} finally {} } }");
            Check("class X { void T() { try {} catch(Exception ex) {} catch {} finally {} } }", "class X { void T() { try {} catch(Exception ex) {} catch {} finally {} } }");
        }

        [Test]
        public void WhileStatementIsGenerated()
        {
            Check("class A { void T() { while(true) return x; } }", "class A { void T() { while(true) return x; } }");
            Check("class A { void T() { while(false) { return; } } }", "class A { void T() { while(false) { return; } } }");
        }

        [Test]
        public void BreakStatementIsGenerated()
        {
            Check("class A { void T() { while(true) break; } }", "class A { void T() { while(true) break; } }");
            Check("class A { void T() { while(false) { break; } } }", "class A { void T() { while(false) { break; } } }");
        }

        [Test]
        public void ContinueStatementIsGenerated()
        {
            Check("class A { void T() { while(true) continue; } }", "class A { void T() { while(true) continue; } }");
            Check("class A { void T() { while(false) { continue; } } }", "class A { void T() { while(false) { continue; } } }");
        }

        [Test]
        public void DoStatementIsGenerated()
        {
            Check("class A { void T() { do { break; } while(true); } }", "class A { void T() { do { break; } while(true); } }");
            Check("class A { void T() { do continue; while(false); } }", "class A { void T() { do continue; while(false); } }");
        }

        [Test]
        public void ForeachStatementIsGenerated()
        {
            Check("class A { void T() { foreach (X x in y) { break; } } }", "class A { void T() { for (X x : y) { break; } } }");
            Check("class A { void T() { foreach (X x in new y[10]) continue; } }", "class A { void T() { for (X x : new y[10]) continue; } }");
        }

        [Test]
        public void ForStatementIsGenerated()
        {
            Check("class A { void T() { for (;;) { break; } } }", "class A { void T() { for (;;) { break; } } }");
            Check("class A { void T() { for (int x = 0;;) continue; } }", "class A { void T() { for (Integer x = 0;;) continue; } }");
            Check("class A { void T() { for (int x = 0, y = 10;;) continue; } }", "class A { void T() { for (Integer x = 0, y = 10;;) continue; } }");
            Check("class A { void T() { for (;i<10;) continue; } }", "class A { void T() { for (;i<10;) continue; } }");
            Check("class A { void T() { for (;;i++) continue; } }", "class A { void T() { for (;;i++) continue; } }");
            Check("class A { void T() { for (int i = 0;i < 10;i++,j--) continue; } }", "class A { void T() { for (Integer i = 0;i < 10;i++,j--) continue; } }");
        }

        [Test]
        public void TypeofXIsConvertedToXDotClass()
        {
            Check("class A { void T() { Type x = typeof(A); } }", "class A { void T() { Type x = A.class; } }");
            Check("class A { void T() { Type x = typeof(List<string>); } }", "class A { void T() { Type x = List<String>.class; } }");
        }

        // [Test]
        public void DateTimeNowAndDateTimeTodayAreConverted()
        {
            // DateTime.Now/Today is not converted anymore, custom Date and DateTime classes are used instead
            Check("class A { void T() { System.debug(DateTime.Now); } }", "class A { void T() { System.debug(Datetime.now()); } }");
            Check("class A { void T() { System.debug(DateTime.Today); } }", "class A { void T() { System.debug(Date.today()); } }");
        }

        [Test]
        public void ApexClassAttributesAreConvertedToModifiers()
        {
            Check("[Global] public class X {}", "global class X {}");
            Check("[WithSharing] public class X {}", "public with sharing class X {}");
            Check("[WithoutSharing] public class X {}", "public without sharing class X {}");
        }

        [Test]
        public void ApexMethodAndFieldAttributesAreConvertedToModifiers()
        {
            Check("public class X { [WebService] public void Y() { } }", "public class X { public webservice void Y() { } }");
            Check("public class X { [Transient] private int counter = 10; }", "public class X { private transient Integer counter = 10; }");
            Check("public class X { static void Z([Final] int y) { } }", "public class X { static void Z(final Integer y) { } }");
        }

        [Test]
        public void ApexAnnotationWithArgumentsIsGenerated()
        {
            Check("public class X { [Future(callOut=true)] public void Y() {} } ", "public class X { @Future(callOut=true) public void Y() { } }");
            Check("public class X { [Some(callOut=true, timeout=1000, description=\"Task\")] public void Y() {} } ", "public class X { @Some(callOut=true timeout=1000 description='Task') public void Y() { } }");
        }

        [Test]
        public void CSharpIsTypeExpressionIsConvertedToApexInstanceOfType()
        {
            Check("public class X { public void Y() { bool t = X is string; } } ", "public class X { public void Y() { Boolean t = X instanceof String; } }");
        }

        [Test]
        public void UsingSystemRunAsTranslatesToApexRunAsStatement()
        {
            Check("class T { void F() { using (System.RunAs(0)) System.Debug(\"Yo!\"); } }", "class T { void F() { System.runAs(0) System.Debug('Yo!'); } }");
            Check("class T { void F() { using (System.RunAs(me)) { System.Debug(123); } } }", "class T { void F() { System.runAs(me) { System.Debug(123); } } }");
        }

        [Test]
        public void ThrowStatementIsGenerated()
        {
            Check("class T { void F() { throw new Exception(); } }", "class T { void F() { throw new Exception(); } }");
            Check("class T { void F() { throw; } }", "class T { void F() { throw; } }");
        }

        [Test]
        public void ClassLeadingAndTrailingCommentsAreConvertedToApex()
        {
            Check(@"// this is a comment
            /* this is another
             * multi-line comment */
            class T
            {
               void F()
               {
                    Soql.Insert(tmp);
               }
            } // trailing test
            // another trailing test",
            @"// this is a comment
            /* this is another
             * multi-line comment */
            class T
            {
                void F()
                {
                    insert tmp;
                }
            } // trailing test");
        }

        [Test]
        public void MethodLeadingAndTrailingCommentsAreConvertedToApex()
        {
            Check(@"class T
            {
               // leading
               void F()
               {
                    Soql.Insert(tmp);
               } // trailing test
            }",
            @"class T
            {
                // leading
                void F()
                {
                    insert tmp;
                } // trailing test
            }");
        }

        [Test]
        public void CommentOutNoApexCode()
        {
            var builder = new ApexSyntaxBuilder();
            var code = builder.CommentOutNoApexCode("int x;");
            Assert.AreEqual(1, code.Count);
            Assert.AreEqual(":NoApex int x;", code[0]);

            code = builder.CommentOutNoApexCode(@"[Test]
            public void CommentOutNoApexCode()
            {
                var builder = new ApexSyntaxBuilder();
                    var code = builder.CommentOutNoApexCode();
            }");

            Assert.AreEqual(6, code.Count);
            Assert.AreEqual(":NoApex [Test]", code[0]);
            Assert.AreEqual(":NoApex public void CommentOutNoApexCode()", code[1]);
            Assert.AreEqual(":NoApex {", code[2]);
            Assert.AreEqual(":NoApex     var builder = new ApexSyntaxBuilder();", code[3]);
            Assert.AreEqual(":NoApex         var code = builder.CommentOutNoApexCode();", code[4]);
            Assert.AreEqual(":NoApex }", code[5]);
        }

        [Test]
        public void DemoIsConverted()
        {
            var csharpCode =
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.System;
                    using SObjects;

                    public class Demo
                    {
                        public Contact contact { get; set; }

                        public Demo()
                        {
                            contact = new Contact();
                        }

                        public PageReference Save()
                        {
                            try
                            {
                                Soql.Insert(contact);
                            }
                            catch (DmlException e)
                            {
                                ApexPages.AddMessages(e);
                            }

                            return null;
                        }

                        public static string UpdatePhone(string email, string newPhone)
                        {
                            List<Contact> contacts = GetContactByEMail(email);
                            if (contacts.IsEmpty())
                            {
                                return ""Not Found"";
                            }
                            else
                            {
                                contacts[0].Phone = newPhone;
                                UpdateContacts(contacts);
                                return ""Phone Number Updated"";
                            }
                        }

                        public static List<Contact> GetContactByEMail(string email)
                        {
                            List<Contact> contacts = Soql.Query<Contact>(""SELECT Id, Email, Phone FROM Contact WHERE Email = :email"", email);
                            return contacts;
                        }

                        public static List<Contact> GetContacts()
                        {
                            List<Contact> contacts = Soql.Query<Contact>(""SELECT Id, Email, Phone FROM Contact"");
                            return contacts;
                        }

                        public static void UpdateContacts(List<Contact> contacts)
                        {
                            Soql.Update(contacts);
                        }
                    }
                }";

            var apexClasses = ApexSharpParser.ToApex(csharpCode);
            Assert.AreEqual(1, apexClasses.Length);
            CompareLineByLine(apexClasses[0],
                @"public class Demo
                {
                    public Contact contact { get; set; }

                    public Demo()
                    {
                        contact = new Contact();
                    }

                    public PageReference Save()
                    {
                        try
                        {
                            insert contact;
                        }
                        catch (DmlException e)
                        {
                            ApexPages.AddMessages(e);
                        }

                        return null;
                    }

                    public static String UpdatePhone(String email, String newPhone)
                    {
                        List<Contact> contacts = GetContactByEMail(email);
                        if (contacts.IsEmpty())
                        {
                            return 'Not Found';
                        }
                        else
                        {
                            contacts[0].Phone = newPhone;
                            UpdateContacts(contacts);
                            return 'Phone Number Updated';
                        }
                    }

                    public static List<Contact> GetContactByEMail(String email)
                    {
                        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact WHERE Email = :email];
                        return contacts;
                    }

                    public static List<Contact> GetContacts()
                    {
                        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact];
                        return contacts;
                    }

                    public static void UpdateContacts(List<Contact> contacts)
                    {
                        update contacts;
                    }
                }");
        }

        [Test]
        public void DemoTestIsConverted()
        {
            var csharpCode =
                @"namespace ApexSharpDemo.ApexCode
                {
                    using Apex.ApexSharp;
                    using Apex.System;
                    using SObjects;
                    using NUnit.Framework;

                    [TestFixture]
                    public class DemoTest
                    {
                        [OneTimeSetUp]
                        public void NoApexSetup()
                        {
                            new ApexSharp().Connect(""C:\\DevSharp\\connect.json"");
                        }

                        [SetUp]
                        public static void Setup()
                        {
                            Contact contactNew = new Contact();
                            contactNew.LastName = ""Jay"";
                            contactNew.Email = ""jay@jay.com"";
                            Soql.Insert(contactNew);
                        }

                        [OneTimeSetUp]
                        public void noApexSetupAgain()
                        {
                            new ApexSharp().Connect(""C:\\DevSharp\\connect.json"");
                        }

                        [Test]
                        public static void UpdatePhoneTestValidEmail()
                        {
                            Demo.UpdatePhone(""jay@jay.com"", ""555-1212"");
                            List<Contact> contacts = Soql.Query<Contact>(""SELECT Id, Email, Phone FROM Contact WHERE Email = 'jay@jay.com'"");
                            System.AssertEquals(contacts[0].Phone, ""555-1212"");
                        }

                        [Test]
                        public static void UpdatePhoneTestNotValidEmail()
                        {
                            Demo.UpdatePhone(""nojay@jay.com"", ""555-1212"");
                            List<Contact> contacts = Soql.Query<Contact>(""SELECT Id, Email, Phone FROM Contact WHERE Email = 'nojay@jay.com'"");
                            System.AssertEquals(contacts.Size(), 0);
                        }
                    }
                }";

            var apexClasses = ApexSharpParser.ToApex(csharpCode);
            Assert.AreEqual(1, apexClasses.Length);
            CompareLineByLine(apexClasses[0],
                @"@IsTest
                public class DemoTest
                {
                    //:NoApex [OneTimeSetUp]
                    //:NoApex public void NoApexSetup()
                    //:NoApex {
                    //:NoApex     new ApexSharp().Connect(""C:\\DevSharp\\connect.json"");
                    //:NoApex }
                    @TestSetup
                    public static void Setup()
                    {
                        Contact contactNew = new Contact();
                        contactNew.LastName = 'Jay';
                        contactNew.Email = 'jay@jay.com';
                        insert contactNew;
                    }

                    //:NoApex [OneTimeSetUp]
                    //:NoApex public void noApexSetupAgain()
                    //:NoApex {
                    //:NoApex     new ApexSharp().Connect(""C:\\DevSharp\\connect.json"");
                    //:NoApex }
                    @IsTest
                    public static void UpdatePhoneTestValidEmail()
                    {
                        Demo.UpdatePhone('jay@jay.com', '555-1212');
                        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact WHERE Email = 'jay@jay.com'];
                        System.AssertEquals(contacts[0].Phone, '555-1212');
                    }

                    @IsTest
                    public static void UpdatePhoneTestNotValidEmail()
                    {
                        Demo.UpdatePhone('nojay@jay.com', '555-1212');
                        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact WHERE Email = 'nojay@jay.com'];
                        System.AssertEquals(contacts.Size(), 0);
                    }
                }");
        }
    }
}
