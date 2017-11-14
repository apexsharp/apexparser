using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpParser;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace CSharpParserTest
{
    [TestFixture]
    public class CSharpParserTests
    {
        [Test]
        public void CSharpParserTest()
        {
            var tree = CSharpHelper.ParseText(
                @"using System;
                using System.Collections;
                using System.Linq;
                using System.Text;

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

            Assert.NotNull(tree);

            var txt = CSharpHelper.ToCSharp(tree);
            Assert.NotNull(txt);
        }
    }
}
