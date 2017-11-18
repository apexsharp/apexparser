using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Visitors;
using NUnit.Framework;
using static System.Math;

namespace ApexParserTest
{
    public class TestFixtureBase
    {
        protected void CompareLineByLine(string actual, string expected)
        {
            var actualList = actual.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var expectedList = expected.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < Min(expectedList.Length, actualList.Length); i++)
            {
                Assert.AreEqual(expectedList[i].Trim(), actualList[i].Trim());
            }

            if (Abs(expectedList.Length - actualList.Length) > 1)
            {
                Assert.Fail("Too many difference in lines: expected {0}, actual {1}", expectedList.Length, actualList.Length);
            }
        }
    }
}
