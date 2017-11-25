using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser;
using ApexParser.Visitors;
using NUnit.Framework;

namespace ApexParserTest
{
    [TestFixture]
    public class CSharpParserTests : TestFixtureBase
    {
        [Test]
        public void CSharpHelperParsesTheCSharpCodeAndReturnsTheSyntaxTree()
        {
            var unit = CSharpHelper.ParseText(
                @"using System;
                using System.Collections;
                using System.Linq.Think;
                using System.Text;
                using system.debug;

                namespace HelloWorld
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine(""Hello, World!"");
                        }
                    }
                }");

            Assert.NotNull(unit);

            var txt = CSharpHelper.ToCSharp(unit);
            Assert.NotNull(txt);
        }

        [Test]
        public void SampleWalkerDisplaysTheSyntaxTreeStructure()
        {
            var unit = CSharpHelper.ParseText(
                @"using System;
                using System.Collections;
                using System.Linq.Think;
                using System.Text;
                using system.debug;

                namespace Demo
                {
                    struct Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine(""Hello, World!"");
                        }
                    }
                }");

            var walker = new SampleWalker();
            unit.Accept(walker);

            var tree = walker.ToString();
            Assert.False(string.IsNullOrWhiteSpace(tree));
        }

        [Test]
        public void CSharpHelperConvertsCSharpTextsToApex()
        {
            var csharp = "class Test1 { public Test1(int x) { } } class Test2 : Test1 { private int x = 10; }";
            var apexClasses = CSharpHelper.ToApex(csharp);
            Assert.AreEqual(2, apexClasses.Length);

            CompareLineByLine(
                @"class Test1
                {
                    public Test1(int x)
                    {
                    }
                }", apexClasses[0]);

            CompareLineByLine(
                @"class Test2 extends Test1
                {
                    private int x = 10;
                }", apexClasses[1]);
        }
    }
}
