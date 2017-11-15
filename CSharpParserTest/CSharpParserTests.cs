using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpParser;
using CSharpParser.Visitors;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace CSharpParserTest
{
    [TestFixture]
    public class CSharpParserTests
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
    }
}
