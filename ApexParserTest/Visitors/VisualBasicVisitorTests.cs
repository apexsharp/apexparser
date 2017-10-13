using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using ApexParser.Visitors;
using NUnit.Framework;
using Sprache;

namespace ApexParserTest.Visitors
{
    [TestFixture, Ignore("TODO: Fix the newline comparison")]
    public class VisualBasicVisitorTests
    {
        private ApexGrammar Apex { get; } = new ApexGrammar();

        [Test]
        public void EmptyClassDeclarationIsFormatted()
        {
            var cd = Apex.ClassDeclaration.Parse("class Test {}");
            var result = VisualBasicCodeGenerator.Generate(cd);

            Assert.AreEqual(
@"Class Test
End Class
", result);
        }

        [Test]
        public void NonEmptyClassDeclarationIsFormatted()
        {
            var cd = Apex.ClassDeclaration.Parse("class Program{void Main(string arg){}}");
            var result = VisualBasicCodeGenerator.Generate(cd);

            Assert.AreEqual(
@"Class Program
    Sub Main(arg As string)
    End Sub
End Class
", result);
        }

    }
}
