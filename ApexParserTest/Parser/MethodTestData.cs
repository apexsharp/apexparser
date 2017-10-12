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
            methodSyntax.CodeInsideMethod = "/* Comment */";

            var method = Apex.MethodDeclaration.Parse(methodSig);

            Assert.AreEqual(2, method.Modifiers.Count);
            Assert.AreEqual("public", method.Modifiers[0]);
            Assert.AreEqual("static", method.Modifiers[1]);
            Assert.AreEqual("void", method.ReturnType.Identifier);
            Assert.AreEqual("GetNumber", method.Identifier);

            Assert.AreEqual(1, method.MethodParameters.Count);
            Assert.AreEqual("string", method.MethodParameters[0].Type.Identifier);
            Assert.AreEqual("name", method.MethodParameters[0].Identifier);
            Assert.AreEqual("/* Comment */", method.CodeInsideMethod);
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
            Assert.AreEqual(@"final string methodSig = 'Something'; // method contents might not be valid
                return new List<string>(); /* comments */", method.CodeInsideMethod);
        }
    }
}
