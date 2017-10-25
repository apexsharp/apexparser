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
            from id in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).Token(null)
            select id.Value;

        [Test]
        public void ForStaticParserTokenNullModifierWorksLikeOrdinaryToken()
        {
            var result = Identifier1.Parse("    \t hello123   \t\r\n  ");
            Assert.AreEqual("hello123", result);
        }

        private CommentParser CommentParser { get; } = new CommentParser();

        public Parser<string> Comment => CommentParser.AnyComment;

        private Parser<string> Identifier2 =>
            from id in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).Token(this)
            select id.Value;

        [Test]
        public void ForParserOwnedByICommentParserProviderTokenThisStripsOutComments()
        {
            // whitespace only
            var result = Identifier2.Parse("    \t hello123   \t\r\n  ");
            Assert.AreEqual("hello123", result);

            // trailing comments
            result = Identifier2.End().Parse("    \t hello123   // what's that? ");
            Assert.AreEqual("hello123", result);

            // leading and trailing comments
            result = Identifier2.Parse(@" // leading comments!
            helloWorld
            // trailing comments! ");
            Assert.AreEqual("helloWorld", result);

            // multiple leading and trailing comments
            result = Identifier2.Parse(@" // leading comments!

            /* multiline leading comments
            this is the second line */

            test321

            // trailing comments!
            /* --==-- */");
            Assert.AreEqual("test321", result);
        }

        private Parser<BreakStatementSyntax> BreakStatement1 =>
            from @break in Parse.IgnoreCase(ApexKeywords.Break).Token(this)
            from semicolon in Parse.Char(';').Token(this)
            select new BreakStatementSyntax();

        [Test]
        public void TokenThisModifierDoesntSaveCommentsContentAutomatically()
        {
            // whitespace only
            var @break = BreakStatement1.Parse("    \t break;   \t\r\n  ");
            Assert.AreEqual(0, @break.LeadingComments.Count);
            Assert.AreEqual(0, @break.TrailingComments.Count);

            // leading comments
            @break = BreakStatement1.Parse(@"
            // this is a break statement
            break;");
            Assert.AreEqual(0, @break.LeadingComments.Count);
            Assert.AreEqual(0, @break.TrailingComments.Count);

            // trailing comments
            @break = BreakStatement1.Parse(@"
            break /* a comment before the semicolon */; // this is ignored");
            Assert.AreEqual(0, @break.LeadingComments.Count);
            Assert.AreEqual(0, @break.TrailingComments.Count);
        }

        private Parser<BreakStatementSyntax> BreakStatement2 =>
            from @break in Parse.IgnoreCase(ApexKeywords.Break).Token(this)
            from semicolon in Parse.Char(';').Token(this)
            select new BreakStatementSyntax
            {
                LeadingComments = @break.LeadingComments.ToList(),
                TrailingComments = semicolon.TrailingComments.ToList(),
            };

        [Test]
        public void TokenThisModifierAllowsSavingCommentsAsNeeded()
        {
            // whitespace only
            var @break = BreakStatement2.Parse("    \t break;   \t\r\n  ");
            Assert.AreEqual(0, @break.LeadingComments.Count);
            Assert.AreEqual(0, @break.TrailingComments.Count);

            // leading comments
            @break = BreakStatement2.Parse(@"

            // this is a break statement
            break;
            ");
            Assert.AreEqual(1, @break.LeadingComments.Count);
            Assert.AreEqual("this is a break statement", @break.LeadingComments[0].Trim());
            Assert.AreEqual(0, @break.TrailingComments.Count);

            // trailing comments
            @break = BreakStatement2.Parse(@"
            break /* this is ignored */; // a comment after the semicolon");
            Assert.AreEqual(0, @break.LeadingComments.Count);
            Assert.AreEqual(1, @break.TrailingComments.Count);
            Assert.AreEqual("a comment after the semicolon", @break.TrailingComments[0].Trim());

            // both leading trailing comments
            @break = BreakStatement2.Parse(@"
            // leading1
            /* leading2 */
            break /* this is ignored */; // a comment after the semicolon");
            Assert.AreEqual(2, @break.LeadingComments.Count);
            Assert.AreEqual("leading1", @break.LeadingComments[0].Trim());
            Assert.AreEqual("leading2", @break.LeadingComments[1].Trim());
            Assert.AreEqual(1, @break.TrailingComments.Count);
            Assert.AreEqual("a comment after the semicolon", @break.TrailingComments[0].Trim());
        }

        private Parser<ISourceSpan<string>> IdentifierSpan =>
            from id in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).Span().Token(this)
            select id.Value;

        [Test]
        public void SourceSpanOfIdentifierReturnsProperValues()
        {
            var id = IdentifierSpan.Parse("  HelloThere  ");
            Assert.AreEqual(1, id.Start.Line);
            Assert.AreEqual(3, id.Start.Column);
            Assert.AreEqual(2, id.Start.Pos);
            Assert.AreEqual(1, id.End.Line);
            Assert.AreEqual(13, id.End.Column);
            Assert.AreEqual(12, id.End.Pos);
            Assert.AreEqual(10, id.Length);
            Assert.AreEqual("HelloThere", id.Value);

            id = IdentifierSpan.Parse(@" // comment
            /* another comment */
            MyIdentifier // test");
            Assert.AreEqual(3, id.Start.Line);
            Assert.AreEqual(13, id.Start.Column);
            Assert.AreEqual(3, id.End.Line);
            Assert.AreEqual(25, id.End.Column);
            Assert.AreEqual(12, id.Length);
            Assert.AreEqual("MyIdentifier", id.Value);
        }

        private Parser<string> PreviewParserDemo =>
            from test in Parse.String("test").Token().Preview()
            from testMethod in Parse.String("testMethod").Token().Text()
            select testMethod;

        [Test]
        public void PreviewVersionOfAParserDoesntConsumeAnyInput()
        {
            var testMethod = PreviewParserDemo.Parse("   testMethod  ");
            Assert.AreEqual("testMethod", testMethod);
        }

        private Parser<ICommented<string>> Identifier4 =>
            from id in Parse.Identifier(Parse.Letter, Parse.LetterOrDigit).Commented(this)
            select id;

        [Test]
        public void CommentedParserStripsOutLeadingCommentsAndSingleTrailingCommentThatStartsOnTheSameLine()
        {
            // whitespace only
            var result = Identifier4.Parse("    \t hello123   \t\r\n  ");
            Assert.AreEqual("hello123", result.Value);
            Assert.False(result.LeadingComments.Any());
            Assert.False(result.TrailingComments.Any());

            // trailing comments
            result = Identifier4.End().Parse("    \t hello123   // what's that? ");
            Assert.AreEqual("hello123", result.Value);
            Assert.False(result.LeadingComments.Any());
            Assert.True(result.TrailingComments.Any());
            Assert.AreEqual("what's that?", result.TrailingComments.First().Trim());

            // leading and trailing comments
            result = Identifier4.Parse(@" // leading comments!
            /* more leading comments! */
            helloWorld // trailing comments!
            // more trailing comments! (that don't belong to the parsed value)");
            Assert.AreEqual("helloWorld", result.Value);
            Assert.AreEqual(2, result.LeadingComments.Count());
            Assert.AreEqual("leading comments!", result.LeadingComments.First().Trim());
            Assert.AreEqual("more leading comments!", result.LeadingComments.Last().Trim());
            Assert.AreEqual(1, result.TrailingComments.Count());
            Assert.AreEqual("trailing comments!", result.TrailingComments.First().Trim());

            // multiple leading and trailing comments
            result = Identifier4.Parse(@" // leading comments!

            /* multiline leading comments
            this is the second line */

            test321

            // trailing comments!
            /* --==-- */");
            Assert.AreEqual("test321", result.Value);
            Assert.AreEqual(2, result.LeadingComments.Count());
            Assert.AreEqual("leading comments!", result.LeadingComments.First().Trim());
            Assert.True(result.LeadingComments.Last().Trim().StartsWith("multiline leading comments"));
            Assert.True(result.LeadingComments.Last().Trim().EndsWith("this is the second line"));
            Assert.False(result.TrailingComments.Any());
        }

        private Parser<string> OptionalTestParser =>
            from test in Parse.String("test").Text().Optional()
            from t in Parse.String("t").Text().Optional()
            from method in Parse.String("method").Text()
            select test.GetOrElse(string.Empty) + t.GetOrElse(string.Empty) + method;

        private Parser<string> XOptionalTestParser =>
            from test in Parse.String("test").Text().XOptional()
            from t in Parse.String("t").Text().Optional() // this part never succeeds
            from method in Parse.String("method").Text()
            select test.GetOrElse(string.Empty) + t.GetOrElse(string.Empty) + method;

        [Test]
        public void XOptionalParserFailsIfSomeInputIsConsumed()
        {
            var result = OptionalTestParser.Parse("testmethod");
            Assert.AreEqual("testmethod", result);

            XOptionalTestParser.Parse("testmethod");
            Assert.AreEqual("testmethod", result);

            result = OptionalTestParser.Parse("method");
            Assert.AreEqual("method", result);

            result = XOptionalTestParser.Parse("method");
            Assert.AreEqual("method", result);

            result = OptionalTestParser.Parse("tmethod");
            Assert.AreEqual("tmethod", result);

            Assert.Throws<ParseException>(() => XOptionalTestParser.Parse("tmethod"));
        }
    }
}
