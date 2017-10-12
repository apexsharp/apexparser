using ApexParser.MetaClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using NUnit.Framework;
using Sprache;

namespace ApexParserTest.Parser
{
    [TestFixture]
    public class MethodTestData
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        [Test]
        public void MethodSigTestOne()
        {
            var methodSig = "public static void GetNumber(string name) { /* Comment */ }";

            MethodSyntax methodSyntax = new MethodSyntax();
            methodSyntax.Modifiers.Add("public");
            methodSyntax.Modifiers.Add("static");
            methodSyntax.ReturnType = new TypeSyntax("void");
            methodSyntax.Identifier = "GetNumber";
            methodSyntax.MethodParameters.Add(new ParameterSyntax("string", "name"));

            var method = Apex.MethodDeclaration.Parse(methodSig);

            Assert.AreEqual(2, method.Modifiers.Count);
            Assert.AreEqual("public", method.Modifiers[0]);
            Assert.AreEqual("static", method.Modifiers[1]);
            Assert.AreEqual("void", method.ReturnType.Identifier);
            Assert.AreEqual("GetNumber", method.Identifier);

            Assert.AreEqual(1, method.MethodParameters.Count);
            Assert.AreEqual("string", method.MethodParameters[0].Type.Identifier);
            Assert.AreEqual("name", method.MethodParameters[0].Identifier);

            var block = method.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.False(block.Statements.Any());
            Assert.AreEqual(1, block.CodeComments.Count);
            Assert.AreEqual(" Comment ", block.CodeComments[0]);
        }

        [Test]
        public void MethodWithSomeDummyBody()
        {
            var methodSig = @"public testMethod void MethodWithSomeDummyBody()
            {
                final string methodSig = 'Something'; // method contents might not be valid
                return new List<string>(); /* comments */
            }";

            var method = Apex.MethodDeclaration.Parse(methodSig);

            Assert.AreEqual(2, method.Modifiers.Count);
            Assert.AreEqual("public", method.Modifiers[0]);
            Assert.AreEqual("testMethod", method.Modifiers[1]);
            Assert.AreEqual("void", method.ReturnType.Identifier);
            Assert.AreEqual("MethodWithSomeDummyBody", method.Identifier);

            Assert.False(method.MethodParameters.Any());

            var block = method.Statement as BlockStatementSyntax;
            Assert.NotNull(block);
            Assert.AreEqual(2, block.Statements.Count);
            Assert.AreEqual("final string methodSig = 'Something'", block.Statements[0].Body);
            Assert.AreEqual("return new List<string>()", block.Statements[1].Body);
        }
    }
}
