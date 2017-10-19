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
    public class ParseExtensionTests
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
    }
}
