using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Parser;
using NUnit.Framework;
using Sprache;

namespace ApexParserTest.Parser
{
    [TestFixture]
    public class ApexGrammarTests
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        [Test]
        public void IdentifierIsALetterFollowedByALetterOrDigit()
        {
            // every test case should include positive examples
            Assert.AreEqual("abc", Apex.Identifier.Parse(" abc "));
            Assert.AreEqual("Test123", Apex.Identifier.Parse("Test123"));

            // and negative ones
            Assert.Throws<ParseException>(() => Apex.Identifier.Parse("1"));
        }

        [Test]
        public void QualifiedIdentifierIsAnStreamOfIdentifiersDelimitedByDots()
        {
            var qi = Apex.QualifiedIdentifier.Parse(" System.debug ").ToList();
            Assert.AreEqual(2, qi.Count);
            Assert.AreEqual("System", qi[0]);
            Assert.AreEqual("debug", qi[1]);

            qi = Apex.QualifiedIdentifier.Parse(@" System . Collections  . Generic ").ToList();
            Assert.AreEqual(3, qi.Count);
            Assert.AreEqual("System", qi[0]);
            Assert.AreEqual("Collections", qi[1]);
            Assert.AreEqual("Generic", qi[2]);
        }

        [Test]
        public void KeywordIsNotAnIdentifier()
        {
            Assert.Throws<ParseException>(() => Apex.Identifier.Parse("class"));
            Assert.Throws<ParseException>(() => Apex.Identifier.Parse("public"));
            Assert.Throws<ParseException>(() => Apex.Identifier.Parse("private"));
            Assert.Throws<ParseException>(() => Apex.Identifier.Parse("static"));
        }

        [Test]
        public void AnnotationBeginsWithAtSign()
        {
            Assert.AreEqual("isTest", Apex.Annotation.Parse(" @isTest "));
        }

        [Test]
        public void PrimitiveTypeIsOneOfSpecificKeywords()
        {
            Assert.AreEqual(ApexKeywords.Void, Apex.PrimitiveType.Parse(" void ").Identifier);
            Assert.AreEqual(ApexKeywords.Int, Apex.PrimitiveType.Parse(" int ").Identifier);
            Assert.AreEqual(ApexKeywords.Boolean, Apex.PrimitiveType.Parse(" boolean ").Identifier);

            // these keywords aren't types
            Assert.Throws<ParseException>(() => Apex.PrimitiveType.Parse("class"));
            Assert.Throws<ParseException>(() => Apex.PrimitiveType.Parse("sharing"));
        }

        [Test]
        public void NonGenericTypeIsAPrimitiveTypeOrAnIdentifier()
        {
            Assert.AreEqual(ApexKeywords.Void, Apex.NonGenericType.Parse(" void ").Identifier);
            Assert.AreEqual("String", Apex.NonGenericType.Parse(" String ").Identifier);
            Assert.AreEqual("String", Apex.NonGenericType.Parse(" String ").Identifier);

            // not types or non-generic types
            Assert.Throws<ParseException>(() => Apex.PrimitiveType.Parse("class"));
            Assert.Throws<ParseException>(() => Apex.PrimitiveType.Parse("Map<string, string>"));
        }

        [Test]
        public void TypeParametersIsACommaSeparatedListOfTypeReferencesEnclosedInAngleBraces()
        {
            var tp = Apex.TypeParameters.Parse("<string>").ToList();
            Assert.AreEqual(1, tp.Count);
            Assert.AreEqual("string", tp[0].Identifier);

            tp = Apex.TypeParameters.Parse(" < System.Collections.Hashtable, string, int, void, System.Char > ").ToList();
            Assert.AreEqual(5, tp.Count);

            var type = tp[0];
            Assert.AreEqual(2, type.Namespaces.Count);
            Assert.AreEqual("System", type.Namespaces[0]);
            Assert.AreEqual("Collections", type.Namespaces[1]);
            Assert.AreEqual("Hashtable", type.Identifier);

            Assert.AreEqual("string", tp[1].Identifier);
            Assert.AreEqual("int", tp[2].Identifier);
            Assert.AreEqual("void", tp[3].Identifier);

            type = tp[4];
            Assert.AreEqual(1, type.Namespaces.Count);
            Assert.AreEqual("System", type.Namespaces.Single());
            Assert.AreEqual("Char", type.Identifier);

            // not types or non-generic types
            Assert.Throws<ParseException>(() => Apex.TypeParameters.Parse("string"));
            Assert.Throws<ParseException>(() => Apex.TypeParameters.Parse("Map<string, string>"));
        }

        [Test]
        public void TypeReferenceIsAGenericOrNonGenericType()
        {
            var tr = Apex.TypeReference.Parse(" String ");
            Assert.AreEqual("String", tr.Identifier);
            Assert.False(tr.Namespaces.Any());
            Assert.False(tr.TypeParameters.Any());

            tr = Apex.TypeReference.Parse("List<string>");
            Assert.AreEqual("List", tr.Identifier);
            Assert.False(tr.Namespaces.Any());

            Assert.AreEqual(1, tr.TypeParameters.Count);
            Assert.AreEqual("string", tr.TypeParameters[0].Identifier);
            Assert.False(tr.TypeParameters[0].Namespaces.Any());

            tr = Apex.TypeReference.Parse(" System.Collections.Map < System.string, List<Guid> >");
            Assert.AreEqual("Map", tr.Identifier);
            Assert.AreEqual(2, tr.Namespaces.Count);
            Assert.AreEqual("System", tr.Namespaces[0]);
            Assert.AreEqual("Collections", tr.Namespaces[1]);

            Assert.AreEqual(2, tr.TypeParameters.Count);
            var tp = tr.TypeParameters[0];

            Assert.AreEqual("string", tp.Identifier);
            Assert.AreEqual(1, tp.Namespaces.Count);
        }

        [Test]
        public void ParameterDeclarationIsTypeAndNamePair()
        {
            var pd = Apex.ParameterDeclaration.Parse(" int a");
            Assert.AreEqual("int", pd.Type.Identifier);
            Assert.AreEqual("a", pd.Identifier);

            pd = Apex.ParameterDeclaration.Parse(" SomeClass b");
            Assert.AreEqual("SomeClass", pd.Type.Identifier);
            Assert.AreEqual("b", pd.Identifier);

            pd = Apex.ParameterDeclaration.Parse(" List<string> stringList");
            Assert.AreEqual("List", pd.Type.Identifier);
            Assert.AreEqual(1, pd.Type.TypeParameters.Count);
            Assert.AreEqual("string", pd.Type.TypeParameters[0].Identifier);
            Assert.AreEqual("stringList", pd.Identifier);

            Assert.Throws<ParseException>(() => Apex.ParameterDeclaration.Parse("Hello!"));
        }

        [Test]
        public void ParameterDeclarationsIsACommaSeparaterListOfParameterDeclarations()
        {
            var pds = Apex.ParameterDeclarations.Parse(" int a, String b");
            Assert.AreEqual(2, pds.Count);

            var pd = pds[0];
            Assert.AreEqual("int", pd.Type.Identifier);
            Assert.AreEqual("a", pd.Identifier);

            pd = pds[1];
            Assert.AreEqual("String", pd.Type.Identifier);
            Assert.AreEqual("b", pd.Identifier);

            Assert.Throws<ParseException>(() => Apex.ParameterDeclaration.Parse("Hello!"));
        }

        [Test]
        public void MethodParametersCanBeJustEmptyBraces()
        {
            var mp = Apex.MethodParameters.Parse(" () ");
            Assert.NotNull(mp);
            Assert.False(mp.Any());

            // unmatched braces, bad input, etc
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("("));
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("(())"));
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("Hello"));
        }

        [Test]
        public void MethodParametersIsCommaSeparatedParameterDeclarationsWithinBraces()
        {
            var mp = Apex.MethodParameters.Parse(" (Integer a, char b,  System.List<Boolean> c123 ) ");
            Assert.NotNull(mp);
            Assert.AreEqual(3, mp.Count);

            var pd = mp[0];
            Assert.AreEqual("Integer", pd.Type.Identifier);
            Assert.AreEqual("a", pd.Identifier);

            pd = mp[1];
            Assert.AreEqual("char", pd.Type.Identifier);
            Assert.AreEqual("b", pd.Identifier);

            pd = mp[2];
            Assert.AreEqual("List", pd.Type.Identifier);
            Assert.AreEqual(1, pd.Type.Namespaces.Count);
            Assert.AreEqual("System", pd.Type.Namespaces[0]);
            Assert.AreEqual(1, pd.Type.TypeParameters.Count);
            Assert.AreEqual("Boolean", pd.Type.TypeParameters[0].Identifier);
            Assert.AreEqual("c123", pd.Identifier);

            // bad input examples
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse(" (Integer a, char b,  Boolean ) "));
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("123"));
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("int a"));
        }

        [Test]
        public void MemberVisibilityCanBePublicOrPrivate()
        {
            Assert.AreEqual("public", Apex.Modifier.Parse(" \n public "));
            Assert.AreEqual("private", Apex.Modifier.Parse(" private \t"));

            // bad input
            Assert.Throws<ParseException>(() => Apex.Modifier.Parse(" whatever "));
        }

        [Test]
        public void BlockSupportsNestedBlocks()
        {
            Assert.AreEqual("{123}", Apex.Block.Parse("{123}"));
            Assert.AreEqual("{ break; }", Apex.Block.Parse("{ break; }"));
            Assert.AreEqual("{ while() { } }", Apex.Block.Parse("{ while() { } }"));

            // bad input
            Assert.Throws<ParseException>(() => Apex.Block.End().Parse("{}}"));
            Assert.Throws<ParseException>(() => Apex.Block.Parse("{"));
        }

        [Test]
        public void MethodDeclarationIsAMethodSignatureWithABlock()
        {
            // parameterless method
            var md = Apex.MethodDeclaration.Parse("void Test() {}");
            Assert.False(md.Attributes.Any());
            Assert.False(md.Modifiers.Any());
            Assert.False(md.MethodParameters.Any());
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("Test", md.Identifier);

            // method with parameters
            md = Apex.MethodDeclaration.Parse(@"
            string Hello( String name, Boolean newLine )
            {
            } ");

            Assert.False(md.Attributes.Any());
            Assert.False(md.Modifiers.Any());
            Assert.AreEqual(2, md.MethodParameters.Count);
            Assert.AreEqual("string", md.ReturnType.Identifier);
            Assert.AreEqual("Hello", md.Identifier);

            var mp = md.MethodParameters;
            Assert.AreEqual(2, mp.Count);

            var pd = mp[0];
            Assert.AreEqual("String", pd.Type.Identifier);
            Assert.AreEqual("name", pd.Identifier);

            pd = mp[1];
            Assert.AreEqual("Boolean", pd.Type.Identifier);
            Assert.AreEqual("newLine", pd.Identifier);

            // method with visibility
            md = Apex.MethodDeclaration.Parse(@"
            public List<int> Add(int x, int y, int z)
            {
            } ");

            Assert.False(md.Attributes.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.AreEqual("List", md.ReturnType.Identifier);
            Assert.AreEqual(1, md.ReturnType.TypeParameters.Count);
            Assert.AreEqual("int", md.ReturnType.TypeParameters[0].Identifier);
            Assert.AreEqual("Add", md.Identifier);
            Assert.AreEqual(3, md.MethodParameters.Count);

            mp = md.MethodParameters;
            Assert.AreEqual(3, mp.Count);

            pd = mp[0];
            Assert.AreEqual("int", pd.Type.Identifier);
            Assert.AreEqual("x", pd.Identifier);

            // a method with annotation
            md = Apex.MethodDeclaration.Parse("@isTest void Test() {}");
            Assert.AreEqual(1, md.Attributes.Count);
            Assert.AreEqual("isTest", md.Attributes[0]);
            Assert.False(md.Modifiers.Any());
            Assert.False(md.MethodParameters.Any());
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("Test", md.Identifier);

            // invalid input
            Assert.Throws<ParseException>(() => Apex.MethodDeclaration.Parse("void Test {}"));
            Assert.Throws<ParseException>(() => Apex.MethodDeclaration.Parse("void AnotherTest()() {}"));
        }

        [Test]
        public void ConstructorDeclarationIsAMethodNamedTheSameAsItsClass()
        {
            // parameterless constructor
            var md = Apex.MethodDeclaration.Parse("public Disposable() {}");
            Assert.False(md.Attributes.Any());
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("public", md.Modifiers[0]);
            Assert.False(md.MethodParameters.Any());
            Assert.AreEqual("Disposable", md.ReturnType.Identifier);
            Assert.AreEqual("Disposable", md.Identifier);

            // constructor with parameters
            md = Apex.MethodDeclaration.Parse(@"
            MyService( String name, Boolean newLine )
            {
            } ");

            Assert.False(md.Attributes.Any());
            Assert.False(md.Modifiers.Any());
            Assert.AreEqual(2, md.MethodParameters.Count);
            Assert.AreEqual("MyService", md.ReturnType.Identifier);
            Assert.AreEqual("MyService", md.Identifier);

            var mp = md.MethodParameters;
            Assert.AreEqual(2, mp.Count);

            var pd = mp[0];
            Assert.AreEqual("String", pd.Type.Identifier);
            Assert.AreEqual("name", pd.Identifier);

            pd = mp[1];
            Assert.AreEqual("Boolean", pd.Type.Identifier);
            Assert.AreEqual("newLine", pd.Identifier);

            // a constructor with annotation
            md = Apex.MethodDeclaration.Parse("@isTest SampleClass() {}");
            Assert.AreEqual(1, md.Attributes.Count);
            Assert.AreEqual("isTest", md.Attributes[0]);
            Assert.False(md.Modifiers.Any());
            Assert.False(md.MethodParameters.Any());
            Assert.AreEqual("SampleClass", md.ReturnType.Identifier);
            Assert.AreEqual("SampleClass", md.Identifier);

            // invalid input
            Assert.Throws<ParseException>(() => Apex.MethodDeclaration.Parse("public Test {}"));
            Assert.Throws<ParseException>(() => Apex.MethodDeclaration.Parse("static AnotherTest()() {}"));
        }

        [Test]
        public void GetterOrSetterCanBeEmpty()
        {
            var get = Apex.GetterOrSetter.Parse(" get ; ");
            Assert.AreEqual("get", get.Item1);
            Assert.AreEqual(";", get.Item2);

            var set = Apex.GetterOrSetter.Parse(" set ; ");
            Assert.AreEqual("set", set.Item1);
            Assert.AreEqual(";", set.Item2);
        }

        [Test]
        public void GetterOrSetterCanHaveBlocks()
        {
            var get = Apex.GetterOrSetter.Parse(" get { return myProperty; } ");
            Assert.AreEqual("get", get.Item1);
            Assert.AreEqual("return myProperty;", get.Item2);

            var set = Apex.GetterOrSetter.Parse(" set { myProperty = value; while(true) { value++; } } ");
            Assert.AreEqual("set", set.Item1);
            Assert.AreEqual("myProperty = value; while(true) { value++; }", set.Item2);
        }

        [Test]
        public void PropertyHasTypeNameGettersAndOrSetters()
        {
            var prop = Apex.PropertyDeclaration.Parse(" int x { get; }");
            Assert.AreEqual("int", prop.Type.Identifier);
            Assert.AreEqual("x", prop.Identifier);
            Assert.AreEqual(null, prop.SetterCode);
            Assert.AreEqual(";", prop.GetterCode);

            prop = Apex.PropertyDeclaration.Parse(" String Version { set { version = value; } }");
            Assert.AreEqual("String", prop.Type.Identifier);
            Assert.AreEqual("Version", prop.Identifier);
            Assert.AreEqual(null, prop.GetterCode);
            Assert.AreEqual("version = value;", prop.SetterCode);
        }

        [Test]
        public void ClassMemberHeadingConstistsOfCommentsAttributesAndModifiers()
        {
            var cm = Apex.ClassMemberHeading.Parse(" /* test */ ");
            Assert.AreEqual(1, cm.CodeComments.Count);
            Assert.AreEqual(" test ", cm.CodeComments[0]);
            Assert.False(cm.Attributes.Any());
            Assert.False(cm.Modifiers.Any());

            cm = Apex.ClassMemberHeading.Parse(" public static ");
            Assert.AreEqual(2, cm.Modifiers.Count);
            Assert.AreEqual("public", cm.Modifiers[0]);
            Assert.AreEqual("static", cm.Modifiers[1]);
            Assert.False(cm.CodeComments.Any());
            Assert.False(cm.Attributes.Any());

            cm = Apex.ClassMemberHeading.Parse(" @isTest ");
            Assert.AreEqual(1, cm.Attributes.Count);
            Assert.AreEqual("isTest", cm.Attributes[0]);
            Assert.False(cm.CodeComments.Any());
            Assert.False(cm.Modifiers.Any());

            cm = Apex.ClassMemberHeading.Parse(" /* my class */ @isTest override ");
            Assert.AreEqual(1, cm.Attributes.Count);
            Assert.AreEqual("isTest", cm.Attributes[0]);
            Assert.AreEqual(1, cm.CodeComments.Count);
            Assert.AreEqual(" my class ", cm.CodeComments[0]);
            Assert.AreEqual(1, cm.Modifiers.Count);
            Assert.AreEqual("override", cm.Modifiers[0]);
        }

        [Test]
        public void ClassDeclarationBodyCanBeEmpty()
        {
            var cd = Apex.ClassDeclarationBody.Parse(" class Test {}");
            Assert.False(cd.Attributes.Any());
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Test", cd.Identifier);

            // incomplete class declarations
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class Test {"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class {}"));
        }

        [Test]
        public void ClassDeclarationCanBeEmpty()
        {
            var cd = Apex.ClassDeclaration.Parse(" class Test {}");
            Assert.False(cd.Attributes.Any());
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Test", cd.Identifier);

            // incomplete class declarations
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse(" class Test {"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class {}"));
        }

        [Test]
        public void ClassDeclarationCanHaveAnnotations()
        {
            var cd = Apex.ClassDeclaration.Parse("@one @two class Three {}");
            Assert.AreEqual(2, cd.Attributes.Count);
            Assert.AreEqual("one", cd.Attributes[0]);
            Assert.AreEqual("two", cd.Attributes[1]);
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Three", cd.Identifier);

            // bad class declarations
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("@class Test {"));
        }

        [Test]
        public void ClassDeclarationBodyCanDeclareMethods()
        {
            var cd = Apex.ClassDeclarationBody.Parse(" class Program { void main() {} }");
            Assert.True(cd.Methods.Any());
            Assert.AreEqual("Program", cd.Identifier);

            var md = cd.Methods.Single();
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("main", md.Identifier);
            Assert.False(md.MethodParameters.Any());

            // class declarations with bad methods
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class Test { void Main }"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class Apex { int main() }"));
        }

        [Test]
        public void ClassDeclarationCanDeclareMethods()
        {
            var cd = Apex.ClassDeclaration.Parse(" class Program { void main() {} }");
            Assert.True(cd.Methods.Any());
            Assert.AreEqual("Program", cd.Identifier);

            var md = cd.Methods.Single();
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("main", md.Identifier);
            Assert.False(md.MethodParameters.Any());

            // class declarations with bad methods
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse(" class Test { void Main }"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class Apex { int main() }"));
        }

        [Test]
        public void ClassDeclarationCanHaveMultipleModifiers()
        {
            var cd = Apex.ClassDeclaration.Parse(" public with   sharing webservice class Program { }");
            Assert.False(cd.Methods.Any());
            Assert.AreEqual("Program", cd.Identifier);

            Assert.AreEqual(3, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with_sharing", cd.Modifiers[1]);
            Assert.AreEqual("webservice", cd.Modifiers[2]);

            // class declarations with bad methods
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse(" class with Test { }"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class sharing Test { }"));
        }

        [Test]
        public void ClassMemberDeclarationCanBeMethodPropertyOrClass()
        {
            var cm = Apex.ClassMemberDeclaration.Parse("@testFixture public with   sharing class Program { }");
            var cd = cm as ClassSyntax;
            Assert.NotNull(cd);
            Assert.False(cd.Methods.Any());
            Assert.AreEqual("Program", cd.Identifier);

            Assert.AreEqual(1, cd.Attributes.Count);
            Assert.AreEqual("testFixture", cd.Attributes[0]);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with_sharing", cd.Modifiers[1]);

            cm = Apex.ClassMemberDeclaration.Parse("private Disposable() { return null; }");
            var md = cm as MethodSyntax;
            Assert.NotNull(md);

            Assert.AreEqual("Disposable", md.Identifier);
            Assert.AreEqual("Disposable", md.ReturnType.Identifier);
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("private", md.Modifiers[0]);
            Assert.AreEqual("return null;", md.CodeInsideMethod);

            cm = Apex.ClassMemberDeclaration.Parse("@required Boolean flag { set { throw; } get; }");
            var pd = cm as PropertySyntax;
            Assert.NotNull(pd);

            Assert.AreEqual("flag", pd.Identifier);
            Assert.AreEqual("Boolean", pd.Type.Identifier);
            Assert.AreEqual(";", pd.GetterCode);
            Assert.AreEqual("throw;", pd.SetterCode);
            Assert.AreEqual(1, pd.Attributes.Count);
            Assert.AreEqual("required", pd.Attributes[0]);
        }
    }
}
