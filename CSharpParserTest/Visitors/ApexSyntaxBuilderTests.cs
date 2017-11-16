using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Visitors;
using CSharpParser;
using CSharpParser.Visitors;
using NUnit.Framework;

namespace CSharpParserTest.Visitors
{
    [TestFixture]
    public class ApexSyntaxBuilderTests : TestFixtureBase
    {
        protected void Check(BaseSyntax node, string expected) =>
            CompareLineByLine(node.ToApex(), ApexParser.ApexParser.IndentApex(expected));

        protected void Check(string csharpUnit, params string[] apexClasses)
        {
            var csharpNode = CSharpHelper.ParseText(csharpUnit);
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
            var cs = CSharpHelper.ParseText(csharp);
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
        public void EnumIsGenerated()
        {
            Check("enum A { B }", "enum A { B }");
            Check("public enum C { D, E, F }", "public enum C { D, E, F }");
            Check("enum X { Y } enum Z { T }", "enum X { Y }", "enum Z { T }");
        }

        [Test]
        public void ConstructorParametersAreGenerated()
        {
            Check("class T {T(int x){}}", "class T {T(int x){}}");
            Check("class Sample {Sample(int x, Customer y){}}", "class Sample {Sample(int x, Customer y){}}");
        }

        [Test]
        public void ClassWithMethodIsGenerated()
        {
            Check("class A { void X(){} }", "class A { void X(){} }");
            Check("class B { public int T(int R){} }", "class B { public int T(int R){} }");
            Check("class A { void X(){} int Y(){} }", "class A { void X(){} int Y(){} }");
        }

        [Test]
        public void ClassWithFieldIsGenerated()
        {
            Check("class X { int Y; }", "class X { int Y; }");
            Check("class X { public int Y, Z; }", "class X { public int Y, Z; }");
            Check("class Test { public int Y = 10, Z = 20 + 30; }", "class Test { public int Y = 10, Z = 20 + 30; }");
        }

        [Test]
        public void ClassWithPropertyIsGenerated()
        {
            Check("class X { int Y { get; } }", "class X { int Y { get; } }");
            Check("class Test { public int Name { get; set; } }", "class Test { public int Name { get; set; } }");
        }

        [Test]
        public void VariableDeclarationIsGenerated()
        {
            Check("class T { void Z() { int x; } }", "class T { void Z() { int x; } }");
            Check("class T { void Z() { int x = 10; } }", "class T { void Z() { int x = 10; } }");
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
            Check("class T { void Z() { int x; { int y; } int z; } }", "class T { void Z() { int x; { int y; } int z; } }");
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
            Check("class A { void T() { if (true) int x; } }", "class A { void T() { if (true) int x; } }");
            Check("class A { void T() { if (true) { } else int y; } }", "class A { void T() { if (true) { } else int y; } }");
            Check("class A { void T() { if (true) { } else if (false) int y; } }", "class A { void T() { if (true) { } else if (false) int y; } }");
        }

        [Test]
        public void ReturnStatementIsGenerated()
        {
            Check("class A { void T() { return x; } }", "class A { void T() { return x; } }");
            Check("class A { void T() { return; } }", "class A { void T() { return; } }");
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

            var apexClasses = CSharpHelper.ToApex(csharpCode);
            Assert.AreEqual(1, apexClasses.Length);
        }
    }
}
