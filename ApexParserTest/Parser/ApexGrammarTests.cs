using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Parser;
using ApexParser.Toolbox;
using NUnit.Framework;
using Sprache;

namespace ApexParserTest.Parser
{
    [TestFixture]
    public class ApexGrammarTests
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        [Test]
        public void IdentifierIsALetterFollowedByALetterOrDigitOrUnderscore()
        {
            // every test case should include positive examples
            Assert.AreEqual("c", Apex.Identifier.Parse("c"));
            Assert.AreEqual("abc", Apex.Identifier.Parse(" abc "));
            Assert.AreEqual("Test123", Apex.Identifier.Parse("Test123"));
            Assert.AreEqual("Hello_cruel_world", Apex.Identifier.Parse("Hello_cruel_world"));

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

            qi = Apex.QualifiedIdentifier.Parse(" System.Exception ").ToList();
            Assert.AreEqual(2, qi.Count);
            Assert.AreEqual("System", qi[0]);
            Assert.AreEqual("Exception", qi[1]);

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
        public void KeywordIsReturnedInItsNormalizedRepresentation()
        {
            Assert.AreEqual(ApexKeywords.Void, Apex.Keyword(ApexKeywords.Void).Parse("voiD"));
            Assert.AreEqual(ApexKeywords.TestMethod, Apex.Keyword(ApexKeywords.TestMethod).Parse("TeStMeThOD"));
            Assert.AreEqual(ApexKeywords.Last90Days, Apex.Keyword(ApexKeywords.Last90Days).Parse("LAST_90_days"));
        }

        [Test]
        public void AnnotationBeginsWithAtSign()
        {
            var ann = Apex.Annotation.Parse(" @isTest ");
            Assert.AreEqual("IsTest", ann.Identifier);
            Assert.IsNull(ann.Parameters);

            Assert.Throws<ParseException>(() => Apex.Annotation.Parse(" isTest "));
        }

        [Test]
        public void AnnotationsCanHaveParameters()
        {
            var ann = Apex.Annotation.Parse(" @isTest(SeeAllData = true) ");
            Assert.AreEqual("IsTest", ann.Identifier);
            Assert.AreEqual("SeeAllData = true", ann.Parameters);

            Assert.Throws<ParseException>(() => Apex.Annotation.Parse(" @class "));
        }

        [Test]
        public void KnownAnnotationsHaveTheirCanonicalForm()
        {
            var ann = Apex.Annotation.Parse(" @future ");
            Assert.AreEqual("Future", ann.Identifier);
            Assert.IsNull(ann.Parameters);

            ann = Apex.Annotation.Parse(" @unknownAnnotation ");
            Assert.AreEqual("unknownAnnotation", ann.Identifier);
            Assert.IsNull(ann.Parameters);

            Assert.Throws<ParseException>(() => Apex.Annotation.Parse(" @public "));
        }

        [Test]
        public void SystemTypeIsOneOfSpecificKeywords()
        {
            Assert.AreEqual(ApexKeywords.Void, Apex.SystemType.Parse(" void ").Identifier);
            Assert.AreEqual(ApexKeywords.Int, Apex.SystemType.Parse(" int ").Identifier);
            Assert.AreEqual(ApexKeywords.Boolean, Apex.SystemType.Parse(" boolean ").Identifier);

            // these keywords aren't types
            Assert.Throws<ParseException>(() => Apex.SystemType.Parse("class"));
            Assert.Throws<ParseException>(() => Apex.SystemType.Parse("sharing"));
        }

        [Test]
        public void SystemTypesAreCaseInsensitive()
        {
            Assert.AreEqual(ApexKeywords.Void, Apex.SystemType.Parse(" Void ").Identifier);
            Assert.AreEqual(ApexKeywords.Int, Apex.SystemType.Parse(" INT ").Identifier);
            Assert.AreEqual(ApexKeywords.Boolean, Apex.SystemType.Parse(" BooLeAn ").Identifier);

            // these keywords aren't types
            Assert.Throws<ParseException>(() => Apex.SystemType.Parse("class"));
            Assert.Throws<ParseException>(() => Apex.SystemType.Parse("sharing"));
        }

        [Test]
        public void NonGenericTypeIsAPrimitiveTypeOrAnIdentifier()
        {
            Assert.AreEqual(ApexKeywords.Void, Apex.NonGenericType.Parse(" void ").Identifier);
            Assert.AreEqual("String", Apex.NonGenericType.Parse(" String ").Identifier);
            Assert.AreEqual("boolean", Apex.NonGenericType.Parse(" Boolean ").Identifier);
            Assert.AreEqual("Integer", Apex.NonGenericType.Parse(" Integer ").Identifier);

            // not types or non-generic types
            Assert.Throws<ParseException>(() => Apex.SystemType.Parse("class"));
            Assert.Throws<ParseException>(() => Apex.SystemType.End().Parse("Map<string, string>"));
        }

        [Test]
        public void TypeParametersIsACommaSeparatedListOfTypeReferencesEnclosedInAngularBraces()
        {
            var tp = Apex.TypeParameters.Parse("<string>").ToList();
            Assert.AreEqual(1, tp.Count);
            Assert.AreEqual("string", tp[0].Identifier);

            tp = Apex.TypeParameters.Parse(" < Sys.Collections.Hashtable, string, int, void, Sys.Character > ").ToList();
            Assert.AreEqual(5, tp.Count);

            var type = tp[0];
            Assert.AreEqual(2, type.Namespaces.Count);
            Assert.AreEqual("Sys", type.Namespaces[0]);
            Assert.AreEqual("Collections", type.Namespaces[1]);
            Assert.AreEqual("Hashtable", type.Identifier);

            Assert.AreEqual("string", tp[1].Identifier);
            Assert.AreEqual("int", tp[2].Identifier);
            Assert.AreEqual("void", tp[3].Identifier);

            type = tp[4];
            Assert.AreEqual(1, type.Namespaces.Count);
            Assert.AreEqual("Sys", type.Namespaces.Single());
            Assert.AreEqual("Character", type.Identifier);

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

            tr = Apex.TypeReference.Parse(" Sys.Collections.MyMap < Sys.string, List<Guid> >");
            Assert.AreEqual("MyMap", tr.Identifier);
            Assert.AreEqual(2, tr.Namespaces.Count);
            Assert.AreEqual("Sys", tr.Namespaces[0]);
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

            tr = Apex.TypeReference.Parse(" System.TypeException ");
            Assert.AreEqual("TypeException", tr.Identifier);
        }

        [Test]
        public void ParameterDeclarationIsTypeAndNamePair()
        {
            var pd = Apex.ParameterDeclaration.Parse(" int a");
            Assert.AreEqual("int", pd.Type.Identifier);
            Assert.AreEqual("a", pd.Identifier);
            Assert.AreEqual(0, pd.Modifiers.Count);

            pd = Apex.ParameterDeclaration.Parse(" SomeClass b");
            Assert.AreEqual("SomeClass", pd.Type.Identifier);
            Assert.AreEqual("b", pd.Identifier);
            Assert.AreEqual(0, pd.Modifiers.Count);

            pd = Apex.ParameterDeclaration.Parse(" List<string> stringList");
            Assert.AreEqual("List", pd.Type.Identifier);
            Assert.AreEqual(1, pd.Type.TypeParameters.Count);
            Assert.AreEqual("string", pd.Type.TypeParameters[0].Identifier);
            Assert.AreEqual("stringList", pd.Identifier);
            Assert.AreEqual(0, pd.Modifiers.Count);

            pd = Apex.ParameterDeclaration.Parse(" final int a");
            Assert.AreEqual("int", pd.Type.Identifier);
            Assert.AreEqual("a", pd.Identifier);
            Assert.AreEqual(1, pd.Modifiers.Count);
            Assert.AreEqual("final", pd.Modifiers[0]);

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
            var mp = Apex.MethodParameters.Parse(" (Integer a, char b,  Sys.MyList<Boolean> c123 ) ");
            Assert.NotNull(mp);
            Assert.AreEqual(3, mp.Count);

            var pd = mp[0];
            Assert.AreEqual("Integer", pd.Type.Identifier);
            Assert.AreEqual("a", pd.Identifier);

            pd = mp[1];
            Assert.AreEqual("char", pd.Type.Identifier);
            Assert.AreEqual("b", pd.Identifier);

            pd = mp[2];
            Assert.AreEqual("MyList", pd.Type.Identifier);
            Assert.AreEqual(1, pd.Type.Namespaces.Count);
            Assert.AreEqual("Sys", pd.Type.Namespaces[0]);
            Assert.AreEqual(1, pd.Type.TypeParameters.Count);
            Assert.AreEqual("boolean", pd.Type.TypeParameters[0].Identifier);
            Assert.AreEqual("c123", pd.Identifier);

            // bad input examples
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse(" (Integer a, char b,  Boolean ) "));
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("123"));
            Assert.Throws<ParseException>(() => Apex.MethodParameters.Parse("int a"));
        }

        [Test]
        public void ModifiersCanBePublicPrivateEtc()
        {
            Assert.AreEqual("public", Apex.Modifier.Parse(" \n public "));
            Assert.AreEqual("private", Apex.Modifier.Parse(" private \t"));
            Assert.AreEqual("with sharing", Apex.Modifier.Parse(@" with
                sharing"));

            // bad input
            Assert.Throws<ParseException>(() => Apex.Modifier.Parse(" whatever "));
        }

        [Test]
        public void ModifiersAreCaseInsensitive()
        {
            Assert.AreEqual("public", Apex.Modifier.Parse(" \n Public "));
            Assert.AreEqual("private", Apex.Modifier.Parse(" PRIVATE \t"));
            Assert.AreEqual("with sharing", Apex.Modifier.Parse(@" With
                SHARING"));

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
            Assert.False(md.IsAbstract);
            Assert.False(md.Annotations.Any());
            Assert.False(md.Modifiers.Any());
            Assert.False(md.Parameters.Any());
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("Test", md.Identifier);

            // method with parameters
            md = Apex.MethodDeclaration.Parse(@"
            string Hello( String name, Boolean newLine )
            {
            } ");

            Assert.False(md.IsAbstract);
            Assert.False(md.Annotations.Any());
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
            Assert.AreEqual("boolean", pd.Type.Identifier);
            Assert.AreEqual("newLine", pd.Identifier);

            // method with visibility
            md = Apex.MethodDeclaration.Parse(@"
            public List<int> Add(int x, int y, int z)
            {
            } ");

            Assert.False(md.IsAbstract);
            Assert.False(md.Annotations.Any());
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
            Assert.False(md.IsAbstract);
            Assert.AreEqual(1, md.Annotations.Count);
            Assert.AreEqual("IsTest", md.Annotations[0].Identifier);
            Assert.False(md.Modifiers.Any());
            Assert.False(md.Parameters.Any());
            Assert.AreEqual("void", md.ReturnType.Identifier);
            Assert.AreEqual("Test", md.Identifier);

            // a method without the body
            md = Apex.MethodDeclaration.Parse("@isTest void Test();");
            Assert.True(md.IsAbstract);
            Assert.AreEqual(1, md.Annotations.Count);
            Assert.AreEqual("IsTest", md.Annotations[0].Identifier);
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
            Assert.False(md.Annotations.Any());
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

            Assert.False(md.Annotations.Any());
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
            Assert.AreEqual("boolean", pd.Type.Identifier);
            Assert.AreEqual("newLine", pd.Identifier);

            // a constructor with annotation
            md = Apex.MethodDeclaration.Parse("@isTest SampleClass() {}");
            Assert.AreEqual(1, md.Annotations.Count);
            Assert.AreEqual("IsTest", md.Annotations[0].Identifier);
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
        public void PropertyAccessorCanBeEmpty()
        {
            var get = Apex.PropertyAccessor.Parse(" get ; ");
            Assert.False(get.LeadingComments.Any());
            Assert.False(get.Annotations.Any());
            Assert.False(get.Modifiers.Any());
            Assert.True(get.IsGetter);
            Assert.Null(get.Body);

            var set = Apex.PropertyAccessor.Parse(" set ; ");
            Assert.False(set.LeadingComments.Any());
            Assert.False(set.Annotations.Any());
            Assert.False(set.Modifiers.Any());
            Assert.True(set.IsSetter);
            Assert.Null(set.Body);
        }

        [Test]
        public void PropertyAccessorCanHaveBlocks()
        {
            var get = Apex.PropertyAccessor.Parse(" get { return myProperty; } ");
            Assert.False(get.LeadingComments.Any());
            Assert.False(get.Annotations.Any());
            Assert.False(get.Modifiers.Any());
            Assert.True(get.IsGetter);

            var block = get.Body;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("return myProperty", block.Statements[0].Body);

            var set = Apex.PropertyAccessor.Parse(" set { myProperty = value; if (true) { value++; } } ");
            Assert.False(set.LeadingComments.Any());
            Assert.False(set.Annotations.Any());
            Assert.False(set.Modifiers.Any());
            Assert.IsTrue(set.IsSetter);

            block = set.Body;
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
        public void PropertyAccessorCanHaveAccessModifiers()
        {
            var get = Apex.PropertyAccessor.Parse(" public get { return 0; }");
            Assert.False(get.LeadingComments.Any());
            Assert.False(get.Annotations.Any());
            Assert.AreEqual(1, get.Modifiers.Count);
            Assert.AreEqual("public", get.Modifiers[0]);
            Assert.True(get.IsGetter);

            var block = get.Body;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("return 0", block.Statements[0].Body);
        }

        [Test]
        public void PropertyAccessorCanHaveComments()
        {
            var get = Apex.PropertyAccessor.End().Parse(" get; // getter");
            Assert.False(get.LeadingComments.Any());
            Assert.False(get.Annotations.Any());
            Assert.AreEqual(0, get.Modifiers.Count);
            Assert.True(get.IsGetter);
            Assert.Null(get.Body);
        }

        [Test]
        public void PropertyHasTypeNameGettersAndOrSetters()
        {
            var prop = Apex.PropertyDeclaration.Parse(" int x { get; }");
            Assert.AreEqual("int", prop.Type.Identifier);
            Assert.AreEqual("x", prop.Identifier);
            Assert.Null(prop.Setter);
            Assert.NotNull(prop.Getter);
            Assert.True(prop.Getter.IsEmpty);

            prop = Apex.PropertyDeclaration.Parse(" String Version { set { version = value; } }");
            Assert.AreEqual("String", prop.Type.Identifier);
            Assert.AreEqual("Version", prop.Identifier);
            Assert.Null(prop.Getter);
            Assert.NotNull(prop.Setter);

            var block = prop.Setter.Body;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("version = value", block.Statements[0].Body);

            prop = Apex.PropertyDeclaration.Parse(@"// length
                @dataMember
                int length { get; }");
            Assert.AreEqual(1, prop.LeadingComments.Count);
            Assert.AreEqual("length", prop.LeadingComments[0].Trim());
            Assert.AreEqual(1, prop.Annotations.Count);
            Assert.AreEqual("dataMember", prop.Annotations[0].Identifier);
            Assert.AreEqual("int", prop.Type.Identifier);
            Assert.AreEqual("length", prop.Identifier);
            Assert.Null(prop.Setter);
            Assert.NotNull(prop.Getter);
            Assert.True(prop.Getter.IsEmpty);
        }

        [Test]
        public void FieldHasTypeAndNameWithoutPropertyAccessors()
        {
            var field = Apex.FieldDeclaration.Parse(" /* Counter */ @dataMember public static int counter;");
            Assert.AreEqual(1, field.LeadingComments.Count);
            Assert.AreEqual("Counter", field.LeadingComments[0].Trim());
            Assert.AreEqual(1, field.Annotations.Count);
            Assert.AreEqual("dataMember", field.Annotations[0].Identifier);
            Assert.AreEqual(2, field.Modifiers.Count);
            Assert.AreEqual("public", field.Modifiers[0]);
            Assert.AreEqual("static", field.Modifiers[1]);
            Assert.AreEqual("int", field.Type.Identifier);
            Assert.AreEqual(1, field.Fields.Count);
            Assert.AreEqual("counter", field.Fields[0].Identifier);

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
            Assert.AreEqual(1, field.Fields.Count);
            Assert.AreEqual("name", field.Fields[0].Identifier);
            Assert.AreEqual("'Bozo'", field.Fields[0].Expression);

            field = Apex.FieldDeclaration.Parse("public Set<String> stringSet = new Set<String>{};");
            Assert.AreEqual(1, field.Modifiers.Count);
            Assert.AreEqual("public", field.Modifiers[0]);
            Assert.AreEqual("Set", field.Type.Identifier);
            Assert.AreEqual(1, field.Fields.Count);
            Assert.AreEqual("stringSet", field.Fields[0].Identifier);
            Assert.AreEqual("new Set<String>{}", field.Fields[0].Expression);

            // incomplete field declaration
            Assert.Throws<ParseException>(() => Apex.FieldDeclaration.Parse("int x ="));
        }

        [Test]
        public void FieldDeclarationCanHaveInitializedWithCommasInItsExpression()
        {
            var field = Apex.FieldDeclaration.Parse("public Map<String, String> stringMap = new Map<String, String>() { 1, 2, 3 };");
            Assert.AreEqual(1, field.Modifiers.Count);
            Assert.AreEqual("public", field.Modifiers[0]);
            Assert.AreEqual("Map", field.Type.Identifier);
            Assert.AreEqual(1, field.Fields.Count);
            Assert.AreEqual("stringMap", field.Fields[0].Identifier);
            Assert.AreEqual("new Map<String, String>(){1, 2, 3}", field.Fields[0].Expression);
        }

        [Test]
        public void FieldDeclarationCanHaveMultipleDeclarators()
        {
            var field = Apex.FieldDeclaration.Parse(" string name, lastName; ");
            Assert.AreEqual("string", field.Type.Identifier);
            Assert.AreEqual(2, field.Fields.Count);
            Assert.AreEqual("name", field.Fields[0].Identifier);
            Assert.IsNull(field.Fields[0].Expression);
            Assert.AreEqual("lastName", field.Fields[1].Identifier);
            Assert.IsNull(field.Fields[1].Expression);

            field = Apex.FieldDeclaration.Parse(@" string name = 'a\'b', lastName = 'b'; ");
            Assert.AreEqual("string", field.Type.Identifier);
            Assert.AreEqual(2, field.Fields.Count);
            Assert.AreEqual("name", field.Fields[0].Identifier);
            Assert.AreEqual(@"'a\'b'", field.Fields[0].Expression);
            Assert.AreEqual("lastName", field.Fields[1].Identifier);
            Assert.AreEqual("'b'", field.Fields[1].Expression);

            field = Apex.FieldDeclaration.Parse(" Map<string, string> map1 = new Map<string, string>(), map2 = null; ");
            Assert.AreEqual("Map", field.Type.Identifier);
            Assert.AreEqual(2, field.Fields.Count);
            Assert.AreEqual("map1", field.Fields[0].Identifier);
            Assert.AreEqual(@"new Map<string, string>()", field.Fields[0].Expression);
            Assert.AreEqual("map2", field.Fields[1].Identifier);
            Assert.AreEqual("null", field.Fields[1].Expression);
        }

        [Test]
        public void ClassMemberHeadingConstistsOfCommentsAttributesAndModifiers()
        {
            var cm = Apex.MemberDeclarationHeading.Parse(" /* test */ ");
            Assert.AreEqual(1, cm.LeadingComments.Count);
            Assert.AreEqual(" test ", cm.LeadingComments[0]);
            Assert.False(cm.Annotations.Any());
            Assert.False(cm.Modifiers.Any());

            cm = Apex.MemberDeclarationHeading.Parse(" public static ");
            Assert.AreEqual(2, cm.Modifiers.Count);
            Assert.AreEqual("public", cm.Modifiers[0]);
            Assert.AreEqual("static", cm.Modifiers[1]);
            Assert.False(cm.LeadingComments.Any());
            Assert.False(cm.Annotations.Any());

            cm = Apex.MemberDeclarationHeading.Parse(" @isTest ");
            Assert.AreEqual(1, cm.Annotations.Count);
            Assert.AreEqual("IsTest", cm.Annotations[0].Identifier);
            Assert.False(cm.LeadingComments.Any());
            Assert.False(cm.Modifiers.Any());

            cm = Apex.MemberDeclarationHeading.Parse(" /* my class */ @isTest override ");
            Assert.AreEqual(1, cm.Annotations.Count);
            Assert.AreEqual("IsTest", cm.Annotations[0].Identifier);
            Assert.AreEqual(1, cm.LeadingComments.Count);
            Assert.AreEqual(" my class ", cm.LeadingComments[0]);
            Assert.AreEqual(1, cm.Modifiers.Count);
            Assert.AreEqual("override", cm.Modifiers[0]);
        }

        [Test]
        public void MethodOrPropertyDeclarationCanReturnEitherMethodOrProperty()
        {
            var pm = Apex.MethodOrPropertyDeclaration.Parse("void Test(int x) {}");
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

            pm = Apex.MethodOrPropertyDeclaration.Parse("string Test { get; }");
            var pd = pm as PropertyDeclarationSyntax;
            Assert.NotNull(pd);
            Assert.AreEqual("string", pd.Type.Identifier);
            Assert.AreEqual("Test", pd.Identifier);
            Assert.NotNull(pd.Getter);
            Assert.True(pd.Getter.IsEmpty);
            Assert.Null(pd.Setter);

            // not supported anymore
            Assert.Throws<ParseException>(() =>
            {
                pm = Apex.MethodOrPropertyDeclaration.Parse("DateTime now = DateTime.Now();");
                var fd = pm as FieldDeclarationSyntax;
                Assert.NotNull(fd);
                Assert.AreEqual("DateTime", fd.Type.Identifier);
                Assert.AreEqual(1, fd.Fields.Count);
                Assert.AreEqual("now", fd.Fields[0].Identifier);
                Assert.AreEqual("DateTime.Now()", fd.Fields[0].Expression);
            });
        }

        [Test]
        public void ClassDeclarationBodyCanBeEmpty()
        {
            var cd = Apex.ClassDeclarationBody.Parse(" class Test {}");
            Assert.False(cd.IsInterface);
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Test", cd.Identifier);

            cd = Apex.ClassDeclarationBody.Parse(" class Test /* another one */ {}");
            Assert.False(cd.IsInterface);
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Test", cd.Identifier);

            // incomplete class declarations
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class Test {"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class {}"));
        }

        [Test]
        public void InterfaceDeclarationBodyCanBeEmpty()
        {
            var cd = Apex.ClassDeclarationBody.Parse(" interface Test {}");
            Assert.True(cd.IsInterface);
            Assert.False(cd.Annotations.Any());
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
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Test", cd.Identifier);

            // incomplete class declarations
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse(" class Test {"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class {}"));
        }

        [Test]
        public void ClassDeclarationCanBeEmptyWithComments()
        {
            var cd = Apex.ClassDeclaration.Parse(" class Test /* Comment1 */ { /* Comment2 */ } // Comment3");
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Test", cd.Identifier);
            Assert.AreEqual(1, cd.InnerComments.Count);
            Assert.AreEqual("Comment2", cd.InnerComments[0].Trim());
            Assert.AreEqual(1, cd.TrailingComments.Count);
            Assert.AreEqual("Comment3", cd.TrailingComments[0].Trim());
        }

        [Test]
        public void ClassDeclarationCanHaveAnnotations()
        {
            var cd = Apex.ClassDeclaration.Parse("@one @two class Three {}");
            Assert.AreEqual(2, cd.Annotations.Count);
            Assert.AreEqual("one", cd.Annotations[0].Identifier);
            Assert.AreEqual("two", cd.Annotations[1].Identifier);
            Assert.IsNull(cd.BaseType);
            Assert.False(cd.Methods.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.AreEqual("Three", cd.Identifier);

            // bad class declarations
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("@class Test {"));
        }

        [Test]
        public void ClassCanExtendOtherClass()
        {
            var cd = Apex.ClassDeclaration.Parse("class Derived extends Base {}");
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.False(cd.Interfaces.Any());
            Assert.NotNull(cd.BaseType);
            Assert.AreEqual("Derived", cd.Identifier);
            Assert.AreEqual("Base", cd.BaseType.Identifier);

            // bad inheritance list
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class Derived extends Base, OtherBase {}"));
        }

        [Test]
        public void ClassCanImplementInterfaces()
        {
            var cd = Apex.ClassDeclaration.Parse("class Disposable implements IDisposable {}");
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.Null(cd.BaseType);
            Assert.AreEqual("Disposable", cd.Identifier);
            Assert.AreEqual(1, cd.Interfaces.Count);
            Assert.AreEqual("IDisposable", cd.Interfaces[0].Identifier);

            cd = Apex.ClassDeclaration.Parse("class MyClass implements IEntity, IMyInterface {}");
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.Null(cd.BaseType);
            Assert.AreEqual("MyClass", cd.Identifier);
            Assert.AreEqual(2, cd.Interfaces.Count);
            Assert.AreEqual("IEntity", cd.Interfaces[0].Identifier);
            Assert.AreEqual("IMyInterface", cd.Interfaces[1].Identifier);

            // incomplete class declaration
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class Derived implements {}"));
        }

        [Test]
        public void ClassCanExtendBaseClassAndImplementMultipleInterfaces()
        {
            var cd = Apex.ClassDeclaration.Parse("class Derived extends Base implements IOne, ITwo {}");
            Assert.False(cd.Annotations.Any());
            Assert.False(cd.Modifiers.Any());
            Assert.NotNull(cd.BaseType);
            Assert.AreEqual("Derived", cd.Identifier);
            Assert.AreEqual("Base", cd.BaseType.Identifier);
            Assert.AreEqual(2, cd.Interfaces.Count);
            Assert.AreEqual("IOne", cd.Interfaces[0].Identifier);
            Assert.AreEqual("ITwo", cd.Interfaces[1].Identifier);

            // bad inheritance list
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class Derived extends {}"));
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
        public void ClassDeclarationBodyCanDeclareEnums()
        {
            var cd = Apex.ClassDeclarationBody.Parse(" class Dummy { enum Boo { Tru, Fal, Unk } }");
            Assert.False(cd.Methods.Any());
            Assert.AreEqual(1, cd.Enums.Count);
            Assert.AreEqual("Dummy", cd.Identifier);

            var ed = cd.Enums.Single();
            Assert.AreEqual("Boo", ed.Identifier);
            Assert.AreEqual(3, ed.Members.Count);
            Assert.AreEqual("Tru", ed.Members[0].Identifier);
            Assert.AreEqual("Fal", ed.Members[1].Identifier);
            Assert.AreEqual("Unk", ed.Members[2].Identifier);

            // class declarations with bad methods
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class Test { enum Boo }"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclarationBody.Parse(" class Apex { enum Boo{} }"));
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
            Assert.AreEqual("with sharing", cd.Modifiers[1]);
            Assert.AreEqual("webservice", cd.Modifiers[2]);

            // class declarations with bad methods
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse(" class with Test { }"));
            Assert.Throws<ParseException>(() => Apex.ClassDeclaration.Parse("class sharing Test { }"));
        }

        [Test]
        public void ClassInitializerIsABlockWithOptionalStaticModifier()
        {
            var ci = Apex.ClassInitializer.Parse(" /* initial value */ { i = 0; } ");
            Assert.NotNull(ci.Body);
            Assert.AreEqual(0, ci.Modifiers.Count);
            Assert.AreEqual(1, ci.LeadingComments.Count);
            Assert.AreEqual("initial value", ci.LeadingComments[0].Trim());
            Assert.AreEqual(1, ci.Body.Statements.Count);
            Assert.AreEqual("i = 0", ci.Body.Statements[0].Body);

            ci = Apex.ClassInitializer.Parse(" static { counter = 0; } ");
            Assert.NotNull(ci.Body);
            Assert.AreEqual(0, ci.LeadingComments.Count);
            Assert.AreEqual(1, ci.Modifiers.Count);
            Assert.AreEqual("static", ci.Modifiers[0]);
            Assert.AreEqual(1, ci.Body.Statements.Count);
            Assert.AreEqual("counter = 0", ci.Body.Statements[0].Body);

            // bad class initializers
            Assert.Throws<ParseException>(() => Apex.ClassInitializer.Parse("}"));
            Assert.Throws<ParseException>(() => Apex.ClassInitializer.End().Parse("{}}"));
        }

        [Test]
        public void ClassDeclarationCanHaveInstanceInitializationCode()
        {
            var cd = Apex.ClassDeclaration.Parse(@"class InitializedClass
            {
                // instance initializer
                {
                    System.debug('instance initialization code #1');
                }
                static {
                    System.debug('static initialization code #0');
                }
                // another instance initializer
                {
                    System.debug('instance initialization code #2');
                }
            }");

            Assert.False(cd.Methods.Any());
            Assert.AreEqual(3, cd.Initializers.Count);

            var init = cd.Initializers[0];
            Assert.AreEqual(0, init.Modifiers.Count);
            Assert.AreEqual(1, init.LeadingComments.Count);
            Assert.AreEqual("instance initializer", init.LeadingComments[0].Trim());
            Assert.NotNull(init.Body);
            Assert.AreEqual(1, init.Body.Statements.Count);
            Assert.AreEqual("System.debug('instance initialization code #1')", init.Body.Statements[0].Body);

            init = cd.Initializers[1];
            Assert.AreEqual(0, init.LeadingComments.Count);
            Assert.AreEqual(1, init.Modifiers.Count);
            Assert.AreEqual("static", init.Modifiers[0]);
            Assert.NotNull(init.Body);
            Assert.AreEqual(1, init.Body.Statements.Count);
            Assert.AreEqual("System.debug('static initialization code #0')", init.Body.Statements[0].Body);

            init = cd.Initializers[2];
            Assert.AreEqual(1, init.LeadingComments.Count);
            Assert.AreEqual("another instance initializer", init.LeadingComments[0].Trim());
            Assert.AreEqual(0, init.Modifiers.Count);
            Assert.NotNull(init.Body);
            Assert.AreEqual(1, init.Body.Statements.Count);
            Assert.AreEqual("System.debug('instance initialization code #2')", init.Body.Statements[0].Body);
        }

        [Test]
        public void ClassMemberDeclarationCanBeMethodPropertyOrClass()
        {
            var cm = Apex.ClassMemberDeclaration.Parse("@testFixture public with   sharing class Program { }");
            var cd = cm as ClassDeclarationSyntax;
            Assert.NotNull(cd);
            Assert.False(cd.Methods.Any());
            Assert.AreEqual("Program", cd.Identifier);

            Assert.AreEqual(1, cd.Annotations.Count);
            Assert.AreEqual("testFixture", cd.Annotations[0].Identifier);
            Assert.AreEqual(2, cd.Modifiers.Count);
            Assert.AreEqual("public", cd.Modifiers[0]);
            Assert.AreEqual("with sharing", cd.Modifiers[1]);

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
            Assert.AreEqual("boolean", pd.Type.Identifier);
            Assert.NotNull(pd.Getter);
            Assert.True(pd.Getter.IsEmpty);

            Assert.NotNull(pd.Setter);
            block = pd.Setter.Body;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("throw", block.Statements[0].Body);
            Assert.AreEqual(1, pd.Annotations.Count);
            Assert.AreEqual("required", pd.Annotations[0].Identifier);

            cm = Apex.ClassMemberDeclaration.Parse("className Test { get; set; }");
            pd = cm as PropertyDeclarationSyntax;
            Assert.NotNull(pd);
            Assert.NotNull(pd.Getter);
            Assert.NotNull(pd.Setter);
            Assert.True(pd.Getter.IsEmpty);
            Assert.True(pd.Setter.IsEmpty);
        }

        [Test]
        public void EnumMemberDeclarationIsAnIdenfierWithPossibleComments()
        {
            var em = Apex.EnumMember.Parse(" /* default */ @test SomeValue ");
            Assert.AreEqual(1, em.LeadingComments.Count);
            Assert.AreEqual("default", em.LeadingComments[0].Trim());
            Assert.AreEqual(1, em.Annotations.Count);
            Assert.AreEqual("test", em.Annotations[0].Identifier);
            Assert.AreEqual("SomeValue", em.Identifier);

            em = Apex.EnumMember.End().Parse(" SomeValue /* some value */");
            Assert.AreEqual(0, em.Annotations.Count);
            Assert.AreEqual("SomeValue", em.Identifier);

            Assert.Throws<ParseException>(() => Apex.EnumMember.Parse(" enum "));
        }

        [Test]
        public void EnumDeclarationIsANamedListOfCommaDelimitedIdentifiers()
        {
            var en = Apex.EnumDeclaration.Parse(" enum Boo { Tru, Fa } ");
            Assert.AreEqual("Boo", en.Identifier);
            Assert.AreEqual(0, en.Modifiers.Count);
            Assert.AreEqual(0, en.LeadingComments.Count);
            Assert.AreEqual(0, en.Annotations.Count);
            Assert.AreEqual(2, en.Members.Count);
            Assert.AreEqual("Tru", en.Members[0].Identifier);
            Assert.AreEqual("Fa", en.Members[1].Identifier);

            en = Apex.EnumDeclaration.Parse(" /* Months */ @test public enum Month { /* January */ Jan, Mar, Dec } ");
            Assert.AreEqual("Month", en.Identifier);
            Assert.AreEqual(1, en.Modifiers.Count);
            Assert.AreEqual("public", en.Modifiers[0]);
            Assert.AreEqual(1, en.LeadingComments.Count);
            Assert.AreEqual("Months", en.LeadingComments[0].Trim());
            Assert.AreEqual(1, en.Annotations.Count);
            Assert.AreEqual("test", en.Annotations[0].Identifier);
            Assert.AreEqual(3, en.Members.Count);
            Assert.AreEqual("Jan", en.Members[0].Identifier);
            Assert.AreEqual(1, en.Members[0].LeadingComments.Count);
            Assert.AreEqual("January", en.Members[0].LeadingComments[0].Trim());
            Assert.AreEqual("Mar", en.Members[1].Identifier);
            Assert.AreEqual("Dec", en.Members[2].Identifier);

            en = Apex.EnumDeclaration.Parse(" enum Boo /* Boolean */ { Tru, Fa } ");
            Assert.AreEqual("Boo", en.Identifier);
            Assert.AreEqual(0, en.Modifiers.Count);
            Assert.AreEqual(0, en.LeadingComments.Count);
            Assert.AreEqual(0, en.Annotations.Count);
            Assert.AreEqual(2, en.Members.Count);
            Assert.AreEqual("Tru", en.Members[0].Identifier);
            Assert.AreEqual("Fa", en.Members[1].Identifier);
        }

        [Test]
        public void KeywordExpressionStatementHandlesInsertDeleteAndReturn()
        {
            var ins = Apex.InsertStatement.Parse("  insert contact; ");
            Assert.AreEqual("contact", ins.Expression);

            var del = Apex.DeleteStatement.Parse("  delete\tuser;");
            Assert.AreEqual("user", del.Expression);

            var upd = Apex.UpdateStatement.Parse("  update \r\n items;");
            Assert.AreEqual("items", upd.Expression);

            Assert.Throws<ParseException>(() => Apex.InsertStatement.Parse(" insert ;"));
            Assert.Throws<ParseException>(() => Apex.DeleteStatement.Parse(" delete ;"));
            Assert.Throws<ParseException>(() => Apex.UpdateStatement.Parse(" update ;"));
        }

        [Test]
        public void UnknownGenericStatementIsAnythingExceptBlockEndingWithATerminator()
        {
            var stmt = Apex.UnknownGenericStatement.Parse("return 'Hello World';");
            Assert.AreEqual("return 'Hello World'", stmt.Body);

            Assert.Throws<ParseException>(() => Apex.UnknownGenericStatement.Parse("if {}"));
        }

        [Test]
        public void StringLiteralIsAnExpressionInSingleQuotes()
        {
            var str = Apex.StringLiteral.Parse("  'Hello'  ");
            Assert.AreEqual("'Hello'", str);

            str = Apex.StringLiteral.Parse(@"   'wo\rld\'!'  ");
            Assert.AreEqual(@"'wo\rld\'!'", str);

            str = Apex.StringLiteral.Parse(@"   '  wo\rld\'!  '  ");
            Assert.AreEqual(@"'  wo\rld\'!  '", str);

            Assert.Throws<ParseException>(() => Apex.StringLiteral.Parse("'"));
        }

        [Test]
        public void GenericExpressionInBracesCanBeAnythingProvidedThatBracesAreMatched()
        {
            var expr = Apex.GenericExpressionInBraces().Parse("(something.IsEmpty)");
            Assert.AreEqual("something.IsEmpty", expr);

            expr = Apex.GenericExpressionInBraces().Parse(" ( something.IsEmpty( ) ) ");
            Assert.AreEqual("something.IsEmpty()", expr);

            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces().Parse("(something.IsEmpty(()"));
            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces().Parse("("));
            Assert.Throws<ParseException>(() => Apex.GenericExpressionInBraces().Parse(")"));
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

            expr = Apex.GenericExpression.Parse("(a, b, c(d, e))");
            Assert.AreEqual("(a, b, c(d, e))", expr);

            expr = Apex.GenericExpression.Parse("[select a, b from users]");
            Assert.AreEqual("[select a, b from users]", expr);

            expr = Apex.GenericExpression.Parse("';'");
            Assert.AreEqual("';'", expr);

            expr = Apex.GenericExpression.Parse(" new Map<string, string> ");
            Assert.AreEqual("new Map<string, string>", expr);

            Assert.Throws<ParseException>(() => Apex.GenericExpression.Parse("(something.IsEmpty(()"));
            Assert.Throws<ParseException>(() => Apex.GenericExpression.Parse("("));
            Assert.Throws<ParseException>(() => Apex.GenericExpression.Parse(")"));
        }

        [Test]
        public void GenericNewExpressionIsANewKeywordFollowedByATypeReference()
        {
            var nx = Apex.GenericNewExpression.Parse(" new string ");
            Assert.AreEqual("new string", nx);

            nx = Apex.GenericNewExpression.Parse(" new Map<string, string> ");
            Assert.AreEqual("new Map<string, string>", nx);

            nx = Apex.GenericNewExpression.Parse(" new Map< List < string, boolean >, string> ");
            Assert.AreEqual("new Map<List<string, boolean>, string>", nx);

            Assert.Throws<ParseException>(() => Apex.GenericNewExpression.Parse(" new "));
            Assert.Throws<ParseException>(() => Apex.GenericNewExpression.End().Parse(" new map <>"));
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
        public void IfStatementCanCompileWithACommentBeforeTheElseBranch()
        {
            var ifstmt = Apex.IfStatement.Parse("if (false) return 'yes'; /* oops */ else return 'no';");
            Assert.AreEqual("false", ifstmt.Expression);
            Assert.AreEqual("return 'yes'", ifstmt.ThenStatement.Body);
            Assert.AreEqual("return 'no'", ifstmt.ElseStatement.Body);

            Assert.Throws<ParseException>(() => Apex.IfStatement.End().Parse("if (true) return null; else"));
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
        public void VariableDeclarationCanHaveExpressionsWithCommasInIt()
        {
            var variable = Apex.VariableDeclaration.Parse("int a = test(1,2,3);");
            Assert.AreEqual("int", variable.Type.Identifier);
            Assert.AreEqual(1, variable.Variables.Count);
            Assert.AreEqual("a", variable.Variables[0].Identifier);
            Assert.AreEqual("test(1,2,3)", variable.Variables[0].Expression);

            variable = Apex.VariableDeclaration.Parse("int x = new[] {1,2,3};");
            Assert.AreEqual("int", variable.Type.Identifier);
            Assert.AreEqual(1, variable.Variables.Count);
            Assert.AreEqual("x", variable.Variables[0].Identifier);
            Assert.AreEqual("new[]{1,2,3}", variable.Variables[0].Expression);
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
        public void RunAsMethodHasSpecialSyntaxHenceIsHandledAsAStatement()
        {
            var runAs = Apex.RunAsStatement.Parse(@"
            System.RunAs(new Version(1, 0)) {
                System.debug('wow!');
            }");
            Assert.AreEqual("new Version(1, 0)", runAs.Expression);
            var block = runAs.Statement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug('wow!')", block.Statements[0].Body);

            runAs = Apex.RunAsStatement.Parse(@"
            System.RunAs(new Version(1, 0))
                System.debug('wow!');");
            Assert.AreEqual("new Version(1, 0)", runAs.Expression);
            Assert.NotNull(runAs.Statement);
            Assert.AreEqual("System.debug('wow!')", runAs.Statement.Body);
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
            var ins = stmt as InsertStatementSyntax;
            Assert.NotNull(ins);
            Assert.AreEqual("something", ins.Expression);

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

            stmt = Apex.Statement.Parse(@"
            System.RunAs(new Version(1, 0)) {
                System.debug('wow!');
            }");
            var runAs = stmt as RunAsStatementSyntax;
            Assert.NotNull(runAs);
            Assert.AreEqual("new Version(1, 0)", runAs.Expression);
            var block = runAs.Statement as BlockSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(1, block.Statements.Count);
            Assert.AreEqual("System.debug('wow!')", block.Statements[0].Body);

            stmt = Apex.Statement.Parse("  insert contact; ");
            ins = stmt as InsertStatementSyntax;
            Assert.NotNull(ins);
            Assert.AreEqual("contact", ins.Expression);

            stmt = Apex.Statement.Parse("  delete\tuser;");
            var del = stmt as DeleteStatementSyntax;
            Assert.NotNull(del);
            Assert.AreEqual("user", del.Expression);

            stmt = Apex.Statement.Parse("  update \r\n items;");
            var upd = stmt as UpdateStatementSyntax;
            Assert.NotNull(upd);
            Assert.AreEqual("items", upd.Expression);
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
        public void CatchExpressionClauseIsATypeAndNameInBraces()
        {
            var cexpr = Apex.CatchExpressionTypeName.Parse(" ( exception )");
            Assert.AreEqual("exception", cexpr.Type.Identifier);
            Assert.IsNull(cexpr.Identifier);

            cexpr = Apex.CatchExpressionTypeName.Parse("(NullReferenceException ex   )");
            Assert.AreEqual("NullReferenceException", cexpr.Type.Identifier);
            Assert.AreEqual("ex", cexpr.Identifier);

            Assert.Throws<ParseException>(() => Apex.CatchExpressionTypeName.Parse("()"));
        }

        [Test]
        public void CatchExpressionClauseCanHaveTypeNameOrNothing()
        {
            var cexpr = Apex.CatchClause.Parse("catch ( exception ) { return; }");
            Assert.AreEqual("exception", cexpr.Type.Identifier);
            Assert.IsNull(cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);

            cexpr = Apex.CatchClause.Parse("catch (NullReferenceException ex   ) { break; }");
            Assert.AreEqual("NullReferenceException", cexpr.Type.Identifier);
            Assert.AreEqual("ex", cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);

            cexpr = Apex.CatchClause.Parse("catch { throw; }");
            Assert.IsNull(cexpr.Type);
            Assert.IsNull(cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);

            Assert.Throws<ParseException>(() => Apex.CatchClause.Parse("catch () {}"));
        }

        [Test]
        public void FinallyClauseIsParsed()
        {
            var fc = Apex.FinallyClause.Parse("   finally { /* nothing here */ return; } ");
            Assert.NotNull(fc.Block);
            Assert.AreEqual(1, fc.Block.Statements.Count);

            Assert.Throws<ParseException>(() => Apex.FinallyClause.Parse("finally () {}"));
        }

        [Test]
        public void TryCatchStatementCanHaveTypeNameOrNothing()
        {
            var ts = Apex.TryCatchFinallyStatement.Parse(" try { break; } catch ( exception ) { return; }");
            Assert.NotNull(ts.Block);
            Assert.AreEqual(1, ts.Block.Statements.Count);
            Assert.AreEqual(1, ts.Catches.Count);
            var cexpr = ts.Catches[0];
            Assert.AreEqual("exception", cexpr.Type.Identifier);
            Assert.IsNull(cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);

            ts = Apex.TryCatchFinallyStatement.Parse("try { /* nothing */ } catch (NullReferenceException ex) { break; } catch { throw; }");
            Assert.NotNull(ts.Block);
            Assert.AreEqual(0, ts.Block.Statements.Count);
            Assert.AreEqual(2, ts.Catches.Count);

            cexpr = ts.Catches[0];
            Assert.AreEqual("NullReferenceException", cexpr.Type.Identifier);
            Assert.AreEqual("ex", cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);

            cexpr = ts.Catches[1];
            Assert.IsNull(cexpr.Type);
            Assert.IsNull(cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);

            ts = Apex.TryCatchFinallyStatement.Parse(" try { break; } catch { return; } finally { throw; }");
            Assert.NotNull(ts.Block);
            Assert.AreEqual(1, ts.Block.Statements.Count);
            Assert.AreEqual(1, ts.Catches.Count);
            cexpr = ts.Catches[0];
            Assert.IsNull(cexpr.Type);
            Assert.IsNull(cexpr.Identifier);
            Assert.NotNull(cexpr.Block);
            Assert.AreEqual(1, cexpr.Block.Statements.Count);
            Assert.NotNull(ts.Finally);
            Assert.NotNull(ts.Finally.Block);
            Assert.AreEqual(1, ts.Finally.Block.Statements.Count);

            ts = Apex.TryCatchFinallyStatement.Parse(@"
            try // leading for block
            { // inside the block
            } // trailing for try
            // leading for catch
            catch // ignored
            // ignored
            (Exception e) // leading for block
            { // inside the block
            } // trailing for block
            // leading for finally
            finally // leading for block
            { // inside the block
            } // trailing for the block");

            Assert.Throws<ParseException>(() => Apex.TryCatchFinallyStatement.Parse("try {}"));
        }

        [Test]
        public void TryFinallyStatementCanBeWithoutAnyCatches()
        {
            var ts = Apex.TryCatchFinallyStatement.Parse("try {} finally {}");
            Assert.NotNull(ts.Block);
            Assert.False(ts.Block.Statements.Any());
            Assert.NotNull(ts.Finally);
            Assert.NotNull(ts.Finally.Block);
            Assert.False(ts.Finally.Block.Statements.Any());

            Assert.Throws<ParseException>(() => Apex.TryCatchFinallyStatement.Parse("try {} finally"));
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

        [Test]
        public void BlockStatementCanHaveLeadingComments()
        {
            var stmt = Apex.Block.Parse(@"/* This is the first comment.
            It has two lines. */
            // This is the second comment.
            /* And the third one. */
            {
                final string methodSig = 'Something'; // method contents might not be valid
                return new List<string>(); /* comments */
            }");

            Assert.AreEqual(3, stmt.LeadingComments.Count);
            Assert.AreEqual(2, stmt.Statements.Count);
            Assert.AreEqual("final string methodSig = 'Something'", stmt.Statements[0].Body);
            Assert.AreEqual(1, stmt.Statements[0].TrailingComments.Count);
            Assert.AreEqual("method contents might not be valid", stmt.Statements[0].TrailingComments[0].Trim());
            Assert.AreEqual("return new List<string>()", stmt.Statements[1].Body);
            Assert.AreEqual(1, stmt.Statements[1].TrailingComments.Count);
            Assert.AreEqual("comments", stmt.Statements[1].TrailingComments[0].Trim());
            Assert.AreEqual(0, stmt.TrailingComments.Count);
        }

        // [Test] // TODO: Try to improve the diagnostics
        public void TestParseExceptionImprovements()
        {
            var text = @"
            public with sharing class NavPackageController1 {

                private List<PackageDetails>  sort(List<PackageDetails> pkgList)
                {
                    class
                }
             }";

            try
            {
                Apex.ClassDeclaration.ParseEx(text);
                Assert.Fail("This snippet should not parse.");
            }
            catch (ParseExceptionCustom ex)
            {
                Assert.AreEqual(5, ex.LineNumber);
            }
        }
    }
}
