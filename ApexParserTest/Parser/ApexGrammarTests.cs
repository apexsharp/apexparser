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
            Assert.False(tr.IsArray);

            tr = Apex.TypeReference.Parse("List<string>");
            Assert.AreEqual("List", tr.Identifier);
            Assert.False(tr.Namespaces.Any());
            Assert.False(tr.IsArray);

            Assert.AreEqual(1, tr.TypeParameters.Count);
            Assert.AreEqual("string", tr.TypeParameters[0].Identifier);
            Assert.False(tr.TypeParameters[0].Namespaces.Any());
            Assert.False(tr.IsArray);

            tr = Apex.TypeReference.Parse(" System.Collections.Map < System.string, List<Guid> >");
            Assert.AreEqual("Map", tr.Identifier);
            Assert.AreEqual(2, tr.Namespaces.Count);
            Assert.AreEqual("System", tr.Namespaces[0]);
            Assert.AreEqual("Collections", tr.Namespaces[1]);
            Assert.False(tr.IsArray);

            Assert.AreEqual(2, tr.TypeParameters.Count);
            var tp = tr.TypeParameters[0];

            Assert.AreEqual("string", tp.Identifier);
            Assert.AreEqual(1, tp.Namespaces.Count);
            Assert.False(tp.IsArray);

            tr = Apex.TypeReference.Parse(" string [ ] ");
            Assert.AreEqual("string", tr.Identifier);
            Assert.False(tr.Namespaces.Any());
            Assert.True(tr.IsArray);

            tr = Apex.TypeReference.Parse(" List <string>[]");
            Assert.AreEqual("List", tr.Identifier);
            Assert.False(tr.Namespaces.Any());
            Assert.True(tr.IsArray);
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
            var pds = Apex.ParameterDeclarations.Parse(" int a, String b").ToList();
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
            var block = Apex.Block.Parse("{123;}");
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("123", block.Statements[0].Body);

            block = Apex.Block.Parse("{ break; return true; continue; }");
            Assert.AreEqual(3, block.Statements.Count);
            var breakStmt = block.Statements[0] as BreakStatementSyntax;
            Assert.NotNull(breakStmt);
            Assert.AreEqual("return true", block.Statements[1].Body);
            Assert.AreEqual("continue", block.Statements[2].Body);

            block = Apex.Block.Parse("{ if (false) { } }");
            Assert.AreEqual(1, block.Statements.Count);
            var ifstmt = block.Statements[0] as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.AreEqual("false", ifstmt.Expression);
            Assert.NotNull(ifstmt.ThenStatement);
            Assert.Null(ifstmt.ElseStatement);

            block = ifstmt.ThenStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.IsFalse(block.Statements.Any());

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
            Assert.False(md.Parameters.Any());
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("Test", md.Identifier);

            // method with parameters
            md = Apex.MethodDeclaration.Parse(@"
            string Hello( String name, Boolean newLine )
            {
            } ");

            Assert.False(md.Attributes.Any());
            Assert.False(md.Modifiers.Any());
            Assert.AreEqual(2, md.Parameters.Count);
            Assert.AreEqual("string", md.ReturnType.Identifier);
            Assert.AreEqual("Hello", md.Identifier);

            var mp = md.Parameters;
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
            Assert.AreEqual(3, md.Parameters.Count);

            mp = md.Parameters;
            Assert.AreEqual(3, mp.Count);

            pd = mp[0];
            Assert.AreEqual("int", pd.Type.Identifier);
            Assert.AreEqual("x", pd.Identifier);

            // a method with annotation
            md = Apex.MethodDeclaration.Parse("@isTest void Test() {}");
            Assert.AreEqual(1, md.Attributes.Count);
            Assert.AreEqual("isTest", md.Attributes[0]);
            Assert.False(md.Modifiers.Any());
            Assert.False(md.Parameters.Any());
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
            Assert.False(md.Parameters.Any());
            Assert.AreEqual("Disposable", md.ReturnType.Identifier);
            Assert.AreEqual("Disposable", md.Identifier);

            // constructor with parameters
            md = Apex.MethodDeclaration.Parse(@"
            MyService( String name, Boolean newLine )
            {
            } ");

            Assert.False(md.Attributes.Any());
            Assert.False(md.Modifiers.Any());
            Assert.AreEqual(2, md.Parameters.Count);
            Assert.AreEqual("MyService", md.ReturnType.Identifier);
            Assert.AreEqual("MyService", md.Identifier);

            var mp = md.Parameters;
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
            Assert.False(md.Parameters.Any());
            Assert.AreEqual("SampleClass", md.ReturnType.Identifier);
            Assert.AreEqual("SampleClass", md.Identifier);

            // invalid input
            Assert.Throws<ParseException>(() => Apex.MethodDeclaration.Parse("public Test {}"));
            Assert.Throws<ParseException>(() => Apex.MethodDeclaration.Parse("static AnotherTest()() {}"));
        }

        [Test]
        public void TypeAndNameIsJustATypeReferenceAndIdentifierPair()
        {
            var tn = Apex.TypeAndName.Parse("string a");
            Assert.AreEqual("string", tn.Type.Identifier);
            Assert.AreEqual("a", tn.Identifier);

            tn = Apex.TypeAndName.Parse("void Test");
            Assert.AreEqual("void", tn.Type.Identifier);
            Assert.AreEqual("Test", tn.Identifier);

            tn = Apex.TypeAndName.Parse("ClassOne");
            Assert.AreEqual("ClassOne", tn.Type.Identifier);
            Assert.IsNull(tn.Identifier);
        }

        [Test]
        public void GetterOrSetterCanBeEmpty()
        {
            var get = Apex.PropertyAccessor.Parse(" get ; ");
            Assert.AreEqual("get", get.Item1);
            Assert.True(get.Item2.IsEmpty);

            var set = Apex.PropertyAccessor.Parse(" set ; ");
            Assert.AreEqual("set", set.Item1);
            Assert.True(set.Item2.IsEmpty);
        }

        [Test]
        public void GetterOrSetterCanHaveBlocks()
        {
            var get = Apex.PropertyAccessor.Parse(" get { return myProperty; } ");
            Assert.AreEqual("get", get.Item1);

            var block = get.Item2 as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("return myProperty", block.Statements[0].Body);

            var set = Apex.PropertyAccessor.Parse(" set { myProperty = value; if (true) { value++; } } ");
            Assert.AreEqual("set", set.Item1);

            block = set.Item2 as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);
            Assert.AreEqual("myProperty = value", block.Statements[0].Body);

            var ifstmt = block.Statements[1] as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.NotNull(ifstmt.ThenStatement);
            Assert.Null(ifstmt.ElseStatement);

            block = ifstmt.ThenStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("value++", block.Statements[0].Body);
        }

        [Test]
        public void PropertyHasTypeNameGettersAndOrSetters()
        {
            var prop = Apex.PropertyDeclaration.Parse(" int x { get; }");
            Assert.AreEqual("int", prop.Type.Identifier);
            Assert.AreEqual("x", prop.Identifier);
            Assert.AreEqual(null, prop.SetterStatement);
            Assert.True(prop.GetterStatement.IsEmpty);

            prop = Apex.PropertyDeclaration.Parse(" String Version { set { version = value; } }");
            Assert.AreEqual("String", prop.Type.Identifier);
            Assert.AreEqual("Version", prop.Identifier);
            Assert.AreEqual(null, prop.GetterStatement);

            var block = prop.SetterStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("version = value", block.Statements[0].Body);

            prop = Apex.PropertyDeclaration.Parse(@"// length
                @dataMember
                int length { get; }");
            Assert.AreEqual(1, prop.CodeComments.Count);
            Assert.AreEqual("length", prop.CodeComments[0].Trim());
            Assert.AreEqual(1, prop.Attributes.Count);
            Assert.AreEqual("dataMember", prop.Attributes[0]);
            Assert.AreEqual("int", prop.Type.Identifier);
            Assert.AreEqual("length", prop.Identifier);
            Assert.AreEqual(null, prop.SetterStatement);
            Assert.True(prop.GetterStatement.IsEmpty);
        }

        [Test]
        public void FieldHasTypeAndNameWithoutPropertyAccessors()
        {
            var field = Apex.FieldDeclaration.Parse(" /* Counter */ @dataMember public static int counter;");
            Assert.AreEqual(1, field.CodeComments.Count);
            Assert.AreEqual("Counter", field.CodeComments[0].Trim());
            Assert.AreEqual(1, field.Attributes.Count);
            Assert.AreEqual("dataMember", field.Attributes[0]);
            Assert.AreEqual(2, field.Modifiers.Count);
            Assert.AreEqual("public", field.Modifiers[0]);
            Assert.AreEqual("static", field.Modifiers[1]);
            Assert.AreEqual("int", field.Type.Identifier);
            Assert.AreEqual("counter", field.Identifier);

            // not a field declaration
            Assert.Throws<ParseException>(() => Apex.FieldDeclaration.Parse("int x { get; }"));
        }

        [Test]
        public void FieldDeclarationCanHaveInitializerExpression()
        {
            var field = Apex.FieldDeclaration.Parse(" public string name = 'Bozo';");
            Assert.AreEqual(1, field.Modifiers.Count);
            Assert.AreEqual("public", field.Modifiers[0]);
            Assert.AreEqual("string", field.Type.Identifier);
            Assert.AreEqual("name", field.Identifier);
            Assert.AreEqual("'Bozo'", field.Expression);

            // incomplete field declaration
            Assert.Throws<ParseException>(() => Apex.FieldDeclaration.Parse("int x ="));
        }

        [Test]
        public void ClassMemberHeadingConstistsOfCommentsAttributesAndModifiers()
        {
            var cm = Apex.MemberDeclarationHeading.Parse(" /* test */ ");
            Assert.AreEqual(1, cm.CodeComments.Count);
            Assert.AreEqual(" test ", cm.CodeComments[0]);
            Assert.False(cm.Attributes.Any());
            Assert.False(cm.Modifiers.Any());

            cm = Apex.MemberDeclarationHeading.Parse(" public static ");
            Assert.AreEqual(2, cm.Modifiers.Count);
            Assert.AreEqual("public", cm.Modifiers[0]);
            Assert.AreEqual("static", cm.Modifiers[1]);
            Assert.False(cm.CodeComments.Any());
            Assert.False(cm.Attributes.Any());

            cm = Apex.MemberDeclarationHeading.Parse(" @isTest ");
            Assert.AreEqual(1, cm.Attributes.Count);
            Assert.AreEqual("isTest", cm.Attributes[0]);
            Assert.False(cm.CodeComments.Any());
            Assert.False(cm.Modifiers.Any());

            cm = Apex.MemberDeclarationHeading.Parse(" /* my class */ @isTest override ");
            Assert.AreEqual(1, cm.Attributes.Count);
            Assert.AreEqual("isTest", cm.Attributes[0]);
            Assert.AreEqual(1, cm.CodeComments.Count);
            Assert.AreEqual(" my class ", cm.CodeComments[0]);
            Assert.AreEqual(1, cm.Modifiers.Count);
            Assert.AreEqual("override", cm.Modifiers[0]);
        }

        [Test]
        public void MethodOrPropertyOrFieldDeclarationCanReturnEitherMethodOrPropertyOrField()
        {
            var pm = Apex.MethodPropertyOrFieldDeclaration.Parse("void Test(int x) {}");
            var md = pm as MethodDeclarationSyntax;
            Assert.NotNull(md);
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("Test", md.Identifier);
            Assert.AreEqual(1, md.Parameters.Count);
            Assert.AreEqual("int", md.Parameters[0].Type.Identifier);
            Assert.AreEqual("x", md.Parameters[0].Identifier);

            var block = md.Body;
            Assert.NotNull(block);
            Assert.False(block.Statements.Any());

            pm = Apex.MethodPropertyOrFieldDeclaration.Parse("string Test { get; }");
            var pd = pm as PropertyDeclarationSyntax;
            Assert.NotNull(pd);
            Assert.AreEqual("string", pd.Type.Identifier);
            Assert.AreEqual("Test", pd.Identifier);
            Assert.NotNull(pd.GetterStatement);
            Assert.Null(pd.SetterStatement);

            pm = Apex.MethodPropertyOrFieldDeclaration.Parse("DateTime now = DateTime.Now();");
            var fd = pm as FieldDeclarationSyntax;
            Assert.NotNull(fd);
            Assert.AreEqual("DateTime", fd.Type.Identifier);
            Assert.AreEqual("now", fd.Identifier);
            Assert.AreEqual("DateTime.Now()", fd.Expression);
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
            Assert.False(md.Parameters.Any());

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
            Assert.False(md.Parameters.Any());

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
            var cd = cm as ClassDeclarationSyntax;
            Assert.NotNull(cd);
            Assert.False(cd.Methods.Any());
            Assert.AreEqual("Program", cd.Identifier);

            Assert.AreEqual(1, cd.Attributes.Count);
            Assert.AreEqual("testFixture", cd.Attributes[0]);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with_sharing", cd.Modifiers[1]);

            cm = Apex.ClassMemberDeclaration.Parse("private Disposable() { return null; }");
            var md = cm as MethodDeclarationSyntax;
            Assert.NotNull(md);

            Assert.AreEqual("Disposable", md.Identifier);
            Assert.AreEqual("Disposable", md.ReturnType.Identifier);
            Assert.AreEqual(1, md.Modifiers.Count);
            Assert.AreEqual("private", md.Modifiers[0]);

            var block = md.Body;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("return null", block.Statements[0].Body);

            cm = Apex.ClassMemberDeclaration.Parse("@required Boolean flag { set { throw; } get; }");
            var pd = cm as PropertyDeclarationSyntax;
            Assert.NotNull(pd);

            Assert.AreEqual("flag", pd.Identifier);
            Assert.AreEqual("Boolean", pd.Type.Identifier);
            Assert.NotNull(pd.GetterStatement);
            Assert.True(pd.GetterStatement.IsEmpty);

            block = pd.SetterStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("throw", block.Statements[0].Body);
            Assert.AreEqual(1, pd.Attributes.Count);
            Assert.AreEqual("required", pd.Attributes[0]);

            cm = Apex.ClassMemberDeclaration.Parse("className Test { get; set; }");
            pd = cm as PropertyDeclarationSyntax;
            Assert.NotNull(pd);
            Assert.NotNull(pd.GetterStatement);
            Assert.NotNull(pd.SetterStatement);
        }

        [Test]
        public void UnknownGenericStatementIsAnythingExceptBlockEndingWithATerminator()
        {
            var stmt = Apex.UnknownGenericStatement.Parse("return 'Hello World';");
            Assert.AreEqual("return 'Hello World'", stmt.Body);

            Assert.Throws<ParseException>(() => Apex.UnknownGenericStatement.Parse("if {}"));
        }

        [Test]
        public void GenericExpressionInBracesCanBeAnythingProvidedThatBracesAreMatched()
        {
            var expr = Apex.GenericExpressionInBraces.Parse("(something.IsEmpty)");
            Assert.AreEqual("something.IsEmpty", expr);

            expr = Apex.GenericExpressionInBraces.Parse(" ( something.IsEmpty( ) ) ");
            Assert.AreEqual("something.IsEmpty()", expr);

            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces.Parse("(something.IsEmpty(()"));
            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces.Parse("("));
            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces.Parse(")"));
        }

        [Test]
        public void GenericExpressionCanBeAnythingProvidedThatBracesAreMatched()
        {
            var expr = Apex.GenericExpression.Parse("something.IsEmpty()");
            Assert.AreEqual("something.IsEmpty()", expr);

            expr = Apex.GenericExpression.Parse("(something.IsEmpty)");
            Assert.AreEqual("(something.IsEmpty)", expr);

            expr = Apex.GenericExpression.Parse(" ( something.IsEmpty( ) ) ");
            Assert.AreEqual("(something.IsEmpty())", expr);

            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces.Parse("(something.IsEmpty(()"));
            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces.Parse("("));
            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces.Parse(")"));
        }

        [Test]
        public void SimpleIfStatementCanCompileWithoutElseBranch()
        {
            var ifstmt = Apex.IfStatement.Parse("if (true) return null;");
            Assert.AreEqual("true", ifstmt.Expression);
            Assert.AreEqual("return null", ifstmt.ThenStatement.Body);
            Assert.IsNull(ifstmt.ElseStatement);

            Assert.Throws<ParseException>(() => Apex.IfStatement.Parse("if {}"));
            Assert.Throws<ParseException>(() => Apex.IfStatement.Parse("if )"));
        }

        [Test]
        public void IfStatementCanCompileWithElseBranch()
        {
            var ifstmt = Apex.IfStatement.Parse("if (false) return 'yes'; else return 'no';");
            Assert.AreEqual("false", ifstmt.Expression);
            Assert.AreEqual("return 'yes'", ifstmt.ThenStatement.Body);
            Assert.AreEqual("return 'no'", ifstmt.ElseStatement.Body);

            Assert.Throws<ParseException>(() => Apex.IfStatement.End().Parse("if (true) return null; else}"));
        }

        [Test]
        public void IfStatementCanHaveBlocksForThenAndElseBranches()
        {
            var ifstmt = Apex.IfStatement.Parse("if (false) { return 'yes'; } else { return 'no'; }");
            Assert.AreEqual("false", ifstmt.Expression);

            var block = ifstmt.ThenStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("return 'yes'", block.Statements[0].Body);

            block = ifstmt.ElseStatement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("return 'no'", block.Statements[0].Body);

            Assert.Throws<ParseException>(() => Apex.IfStatement.End().Parse("if (true) {return null; else {}"));
        }

        [Test]
        public void ForEachStatementHasAnExpressionAndABody()
        {
            var forStmt = Apex.ForEachStatement.Parse(@"
            for (Contact c : contacts)
            {
                System.debug(c.Email);
            }");

            Assert.AreEqual("Contact", forStmt.Type.Identifier);
            Assert.AreEqual("c", forStmt.Identifier);
            Assert.AreEqual("contacts", forStmt.Expression);

            var blockStmt = forStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("System.debug(c.Email)", blockStmt.Statements[0].Body);

            Assert.Throws<ParseException>(() => Apex.ForEachStatement.Parse("for (;;) {}"));
        }

        [Test]
        public void ForStatementHasAnExpressionAndABody()
        {
            var forStmt = Apex.ForStatement.Parse(@"
            for (;;)
            {
                System.debug(c.Email);
            }");
            Assert.NotNull(forStmt);
            Assert.IsNull(forStmt.Declaration);
            Assert.IsNull(forStmt.Condition);
            Assert.NotNull(forStmt.Incrementors);
            Assert.False(forStmt.Incrementors.Any());

            var blockStmt = forStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("System.debug(c.Email)", blockStmt.Statements[0].Body);

            Assert.Throws<ParseException>(() => Apex.ForStatement.Parse("for {}"));
        }

        [Test]
        public void DoStatementHasABodyAndAnExpression()
        {
            var doWhileStmt = Apex.DoStatement.Parse(@"
            do
            {
                list.add(c.Email);
            }
            while (list.isEmpty());");

            Assert.NotNull(doWhileStmt);
            Assert.AreEqual("list.isEmpty()", doWhileStmt.Expression);

            var blockStmt = doWhileStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("list.add(c.Email)", blockStmt.Statements[0].Body);

            Assert.Throws<ParseException>(() => Apex.ForStatement.Parse("do {} while;"));
        }

        [Test]
        public void VariableDeclarationCanDeclareOneOrMoreVariablesWithOptionalInitializerExpressions()
        {
            var variable = Apex.VariableDeclaration.Parse(" int x ; ");
            Assert.AreEqual("int", variable.Type.Identifier);
            Assert.IsFalse(variable.Type.IsArray);
            Assert.AreEqual(1, variable.Variables.Count);
            Assert.AreEqual("x", variable.Variables[0].Identifier);
            Assert.IsNull(variable.Variables[0].Expression);

            variable = Apex.VariableDeclaration.Parse(" string name, lastName; ");
            Assert.AreEqual("string", variable.Type.Identifier);
            Assert.IsFalse(variable.Type.IsArray);
            Assert.AreEqual(2, variable.Variables.Count);
            Assert.AreEqual("name", variable.Variables[0].Identifier);
            Assert.IsNull(variable.Variables[0].Expression);
            Assert.AreEqual("lastName", variable.Variables[1].Identifier);
            Assert.IsNull(variable.Variables[1].Expression);

            variable = Apex.VariableDeclaration.Parse(" Map<string, DateTime>[] dates = GetDates(), temp, other; ");
            Assert.AreEqual("Map", variable.Type.Identifier);
            Assert.IsTrue(variable.Type.IsArray);
            Assert.AreEqual(2, variable.Type.TypeParameters.Count);
            Assert.AreEqual("string", variable.Type.TypeParameters[0].Identifier);
            Assert.AreEqual("DateTime", variable.Type.TypeParameters[1].Identifier);
            Assert.AreEqual(3, variable.Variables.Count);
            Assert.AreEqual("dates", variable.Variables[0].Identifier);
            Assert.AreEqual("GetDates()", variable.Variables[0].Expression);
            Assert.AreEqual("temp", variable.Variables[1].Identifier);
            Assert.IsNull(variable.Variables[1].Expression);
            Assert.AreEqual("other", variable.Variables[2].Identifier);
            Assert.IsNull(variable.Variables[2].Expression);

            // incomplete variable declarations
            Assert.Throws<ParseException>(() => Apex.VariableDeclarator.End().Parse("int x = "));
            Assert.Throws<ParseException>(() => Apex.VariableDeclarator.End().Parse("char c, b = ;"));
        }

        [Test]
        public void VariableDeclaratorIsAnIdentifierFollowedByOptionalExpression()
        {
            var variable = Apex.VariableDeclarator.Parse(" name = 'Bozo'");
            Assert.AreEqual("name", variable.Identifier);
            Assert.AreEqual("'Bozo'", variable.Expression);

            variable = Apex.VariableDeclarator.Parse(" date ");
            Assert.AreEqual("date", variable.Identifier);
            Assert.IsNull(variable.Expression);

            variable = Apex.VariableDeclarator.Parse(" now = DateTime.Now()");
            Assert.AreEqual("now", variable.Identifier);
            Assert.AreEqual("DateTime.Now()", variable.Expression);

            // incomplete variable declarator
            Assert.Throws<ParseException>(() => Apex.VariableDeclarator.End().Parse("x = "));
            Assert.Throws<ParseException>(() => Apex.VariableDeclarator.End().Parse("y = ;"));
        }

        [Test]
        public void WhileStatementHasAnExpressionAndABody()
        {
            var whileStmt = Apex.WhileStatement.Parse(@"
            while (list.isEmpty())
            {
                list.add(c.Email);
            }");

            Assert.NotNull(whileStmt);
            Assert.AreEqual("list.isEmpty()", whileStmt.Expression);

            var blockStmt = whileStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("list.add(c.Email)", blockStmt.Statements[0].Body);

            Assert.Throws<ParseException>(() => Apex.ForStatement.Parse("while true {}"));
        }

        [Test]
        public void BreakStatementHasNoParameters()
        {
            var breakStmt = Apex.BreakStatement.Parse(" break ; ");
            Assert.NotNull(breakStmt);

            Assert.Throws<ParseException>(() => Apex.BreakStatement.Parse("break"));
        }

        [Test]
        public void StatementRuleParsesStatementsOfAnyKind()
        {
            var stmt = Apex.Statement.Parse("if (false) return 'yes'; else return 'no';");
            var ifstmt = stmt as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.AreEqual("false", ifstmt.Expression);
            Assert.AreEqual("return 'yes'", ifstmt.ThenStatement.Body);
            Assert.AreEqual("return 'no'", ifstmt.ElseStatement.Body);

            stmt = Apex.Statement.Parse("{ return x; }");
            var blockStmt = stmt as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("return x", blockStmt.Statements[0].Body);

            stmt = Apex.Statement.Parse("return 'Hello World';");
            Assert.AreEqual("return 'Hello World'", stmt.Body);

            stmt = Apex.Statement.Parse("insert something;");
            Assert.AreEqual("insert something", stmt.Body);

            stmt = Apex.Statement.Parse(@"
            for (Contact c : contacts)
            {
                System.debug(c.Email);
            }");
            var forEachStmt = stmt as ForEachStatementSyntax;
            Assert.NotNull(forEachStmt);
            Assert.AreEqual("Contact", forEachStmt.Type.Identifier);
            Assert.AreEqual("c", forEachStmt.Identifier);
            Assert.AreEqual("contacts", forEachStmt.Expression);

            blockStmt = forEachStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("System.debug(c.Email)", blockStmt.Statements[0].Body);

            stmt = Apex.ForStatement.Parse(@"
            for (;;)
            {
                System.debug(c.Email);
            }");
            var forStmt = stmt as ForStatementSyntax;
            Assert.NotNull(forStmt);
            Assert.IsNull(forStmt.Declaration);
            Assert.IsNull(forStmt.Condition);
            Assert.NotNull(forStmt.Incrementors);
            Assert.False(forStmt.Incrementors.Any());

            blockStmt = forStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("System.debug(c.Email)", blockStmt.Statements[0].Body);

            stmt = Apex.Statement.Parse(@"
            do
            {
                break;
            }
            while (list.isEmpty());");

            var doWhileStmt = stmt as DoStatementSyntax;
            Assert.NotNull(doWhileStmt);
            Assert.AreEqual("list.isEmpty()", doWhileStmt.Expression);

            blockStmt = doWhileStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            var breakStmt = blockStmt.Statements[0] as BreakStatementSyntax;
            Assert.NotNull(breakStmt);

            stmt = Apex.Statement.Parse(@"
            while (list.isEmpty())
            {
                list.add(c.Email);
            }");

            var whileStmt = stmt as WhileStatementSyntax;
            Assert.NotNull(whileStmt);
            Assert.AreEqual("list.isEmpty()", whileStmt.Expression);

            blockStmt = whileStmt.Statement as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("list.add(c.Email)", blockStmt.Statements[0].Body);

            stmt = Apex.Statement.Parse(@" int x = 10;");
            var varDecl = stmt as VariableDeclarationSyntax;
            Assert.NotNull(varDecl);
        }

        [Test]
        public void StatementRuleSupportsComments()
        {
            var stmt = Apex.Statement.Parse(@"
            // this is a dumb comment
            if (false) return 'yes'; else return 'no';");
            var ifstmt = stmt as IfStatementSyntax;
            Assert.NotNull(ifstmt);
            Assert.AreEqual("false", ifstmt.Expression);
            Assert.AreEqual("return 'yes'", ifstmt.ThenStatement.Body);
            Assert.AreEqual("return 'no'", ifstmt.ElseStatement.Body);

            stmt = Apex.Statement.Parse("/* here goes the block statement */ { return x; }");
            var blockStmt = stmt as BlockSyntax;
            Assert.NotNull(blockStmt);
            Assert.AreEqual(1, blockStmt.Statements.Count);
            Assert.AreEqual("return x", blockStmt.Statements[0].Body);

            stmt = Apex.Statement.Parse("/* greeting //*/ return 'Hello World';");
            Assert.AreEqual("return 'Hello World'", stmt.Body);
        }

        [Test]
        public void BlockStatementCanBeCommented()
        {
            var stmt = Apex.Block.Parse(@"
            {
                final string methodSig = 'Something'; // method contents might not be valid
                return new List<string>(); /* comments */
            }");

            Assert.AreEqual(2, stmt.Statements.Count);
            Assert.AreEqual("final string methodSig = 'Something'", stmt.Statements[0].Body);
            Assert.AreEqual("return new List<string>()", stmt.Statements[1].Body);
        }
    }
}
