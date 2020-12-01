using System.IO;
using ApexSharp.ApexParser;
using ApexSharp.ApexParser.Syntax;
using ApexTestFind;
using NUnit.Framework;
using Array = System.Array;

namespace ApexSharpDemo
{
    [TestFixture]
    public class ApexTestFinderTest
    {
        [Test]
        public void GetApexClassesReferencingAGivenClassReturnsEmptyArrayForEmptyList()
        {
            var result = ApexTestFinder.GetApexClassesReferencingAGivenClass((string[])null, "Something");
            Assert.NotNull(result);
            Assert.IsEmpty(result);

            result = ApexTestFinder.GetApexClassesReferencingAGivenClass(Array.Empty<string>(), "Something");
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetCSharpClassesReferencingAGivenClassReturnsEmptyArrayForEmptyList()
        {
            var result = ApexTestFinder.GetApexClassesReferencingAGivenClass((MemberDeclarationSyntax[])null, "Something");
            Assert.NotNull(result);
            Assert.IsEmpty(result);

            result = ApexTestFinder.GetApexClassesReferencingAGivenClass(Array.Empty<MemberDeclarationSyntax>(), "Something");
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        private static ClassDeclarationSyntax ParseClass(string apexClass)
        {
            var result = ApexSharpParser.GetApexAst(apexClass) as ClassDeclarationSyntax;
            Assert.NotNull(result);
            return result;
        }

        [Test]
        public void IsTestClassReturnsFalseForNullOrEmptyClass()
        {
            Assert.IsFalse(ApexTestFinder.IsTestClass(null));

            var emptyClass = new ClassDeclarationSyntax { Identifier = "EmptyClass" };
            Assert.IsFalse(ApexTestFinder.IsTestClass(emptyClass));

            var sampleClass = ParseClass("class SampleClass {}");
            Assert.IsFalse(ApexTestFinder.IsTestClass(sampleClass));
        }

        [Test]
        public void IsTestClassReturnsTrueForClassDecoratedWithTestAttribute()
        {
            var testFixture = ParseClass("@TestFixture class NUnitSample {}");
            Assert.NotNull(testFixture);
            Assert.IsTrue(ApexTestFinder.IsTestClass(testFixture));

            testFixture = ParseClass("@Test class MSTestSample {}");
            Assert.NotNull(testFixture);
            Assert.IsTrue(ApexTestFinder.IsTestClass(testFixture));

            testFixture = ParseClass("@isTest class ApexSample {}");
            Assert.NotNull(testFixture);
            Assert.IsTrue(ApexTestFinder.IsTestClass(testFixture));

            testFixture = ParseClass("@Dummy class NotATestSample {}");
            Assert.NotNull(testFixture);
            Assert.IsFalse(ApexTestFinder.IsTestClass(testFixture));
        }

        [Test]
        public void TextReferencesAClass()
        {
            Assert.IsFalse(ApexTestFinder.TextReferencesAClass(null, "Foo"));
            Assert.IsFalse(ApexTestFinder.TextReferencesAClass(" ", "Foo"));
            Assert.IsFalse(ApexTestFinder.TextReferencesAClass("Foo", null));
            Assert.IsFalse(ApexTestFinder.TextReferencesAClass("Foo", " "));
            Assert.IsTrue(ApexTestFinder.TextReferencesAClass("Foo.Bar()", "Foo"));
            Assert.IsFalse(ApexTestFinder.TextReferencesAClass("NonFoo.Bar()", "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsFalseForNullOrEmptyClass()
        {
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(null, "Foo"));

            var emptyClass = new ClassDeclarationSyntax { Identifier = "EmptyClass" };
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(emptyClass, "Foo"));

            var sampleClass = ParseClass("class SampleClass {}");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForStaticMethodCallReferences()
        {
            var sampleClass = ParseClass("class SampleClass { void Test() { Foo.Bar(); } }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { void Test() { /* Foo.Bar(); */ } }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForTheVariableDeclarations()
        {
            var sampleClass = ParseClass("class SampleClass { void Test() { Foo x; } }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { void Test() { /* Foo x; */ } }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForTheVariableInitializationExpressions()
        {
            var sampleClass = ParseClass("class SampleClass { void Test() { object x = new Foo(); } }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { void Test() { /* object x = new Foo(); */ } }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForTheConstructorExpressions()
        {
            var sampleClass = ParseClass("class SampleClass { void Test() { new Foo(); } }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { void Test() { /* new Foo(); */ } }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForTheFieldDeclarations()
        {
            var sampleClass = ParseClass("class SampleClass { Foo f; }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { /* Foo f; */ }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForTheFieldInitializationExpressions()
        {
            var sampleClass = ParseClass("class SampleClass { object x = new Foo(); }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { /* object x = new Foo(); */ }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForThePropertyDeclarations()
        {
            var sampleClass = ParseClass("class SampleClass { Foo f { get; } }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { /* Foo f { get; } */ }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void HasReferencesToReturnsTrueForTheMethodDeclarations()
        {
            var sampleClass = ParseClass("class SampleClass { Foo getSomething() { } }");
            Assert.IsTrue(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));

            // commented out
            sampleClass = ParseClass("class SampleClass { /* Foo getSomething() { } */ }");
            Assert.IsFalse(ApexTestFinder.HasReferencesTo(sampleClass, "Foo"));
        }

        [Test]
        public void GetApexClassesReferencingAGivenClassReturnsClassNames()
        {
            var classes = ApexTestFinder.GetApexClassesReferencingAGivenClass(new[]
            {
                "class NotATestClass { Foo x; }",
                "@isTest class ClassWithNoReferencesToFoo { /* Foo x = commented out */ }",
                "@isTest class MyClass { void Test() { Foo x = new Foo(); } }",
                "@isTest class NotMyClass { void Test() { FooBar x = new FooBar(); } }",
            }, "Foo");

            Assert.AreEqual(1, classes.Length);
            Assert.AreEqual("MyClass", classes[0]);
        }

        [Test]
        public void GetAllTextClassesReturnsAllTextClassesAsExpected()
        {
            var location = Path.GetDirectoryName(typeof(ApexTestFinderTest).Assembly.Location);
            var subfolder = Path.Combine(location, "..", "..", "..", "ApexClasses");
            var classes = ApexTestFinder.GetAllTestClasses(subfolder, "Dx");

            Assert.AreEqual(1, classes.Count);
            Assert.IsTrue(classes.Contains("DxTestTwo"));
        }

        private static bool IsTestClass(MemberDeclarationSyntax ast)
        {
            var cds = ast as ClassDeclarationSyntax;
            return ApexTestFinder.IsTestClass(cds);
        }

        [Test]
        public void DetectTestClassExample1()
        {
            var ast = ApexSharpParser.GetApexAst("class Example1 {}");
            Assert.IsFalse(IsTestClass(ast));
        }

        [Test]
        public void DetectTestClassExample2()
        {
            var ast = ApexSharpParser.GetApexAst("@isTest class Example2 {}");
            Assert.IsTrue(IsTestClass(ast));
        }

        [Test]
        public void DetectTestClassExample3()
        {
            var ast = ApexSharpParser.GetApexAst("@isTest enum Example2 { A, B, C }");
            Assert.IsFalse(IsTestClass(ast));
        }
    }
}
