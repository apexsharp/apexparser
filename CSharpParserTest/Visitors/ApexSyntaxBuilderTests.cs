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
    }
}
