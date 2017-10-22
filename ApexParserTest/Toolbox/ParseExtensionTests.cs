using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Parser;
using ApexParser.Toolbox;
using NUnit.Framework;
using Sprache;
using static ApexParserTest.Properties.Resources;

namespace ApexParserTest.Toolbox
{
    [TestFixture]
    public class ParseExtensionTests : ICommentParserProvider
    {
        [Test]
        public void ParseExProducesMoreDetailedExceptionMessage()
        {
            // append the error line to the valid demo file
            var errorLine = "--===-- oops! --===--";
            var demo = Demo + Environment.NewLine + errorLine;

            try
            {
                new ApexGrammar().ClassDeclaration.End().ParseEx(demo);
                Assert.Fail("The code should have thrown ParseException.");
            }
            catch (ParseException ex)
            {
                // check that the error message contains the complete invalid code line
                var exc = ex as ParseExceptionCustom;
                Assert.NotNull(exc);
                Assert.True(exc.Apexcode.Contains(errorLine));
            }
        }

        private readonly Parser<string> Identifier1 =
            Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).TokenEx();

        [Test]
        public void ForStaticParserTokenExModifierWorksLikeOrdinaryToken()
        {
            var result = Identifier1.Parse("    \t hello123   \t\r\n  ");
            Assert.AreEqual("hello123", result);
        }

        private CommentParser CommentParser { get; } = new CommentParser();

        public Parser<string> Comment => CommentParser.AnyComment;

        private Parser<string> Identifier2 =>
            Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).TokenEx();

        // [Test] // Doesn't work: Parser.Target is not the current class
        public void ForParserOwnedByICommentParserProviderTokenExStripsOutComments()
        {
            // whitespace only
            var result = Identifier2.Parse("    \t hello123   \t\r\n  ");
            Assert.AreEqual("hello123", result);

            // trailing comments
            result = Identifier2.End().Parse("    \t hello123   // what's that? ");
            Assert.AreEqual("hello123", result);

            // leading and trailing comments
            result = Identifier2.Parse(@" // leading comments!
            hello_world
            // trailing comments! ");
            Assert.AreEqual("hello_world", result);
        }
    }
}
