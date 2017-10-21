using System;
using System.IO;
using ApexSharpBase;
using NUnit.Framework;

namespace ApexSharpBaseTest
{

    [TestFixture]
    public class ApexSharpTest
    {
        //[Test, Ignore("Appveyor fails on this test")]
        public void ParseCSharpCodeTest()
        {
            ApesSharp apexSharp = new ApesSharp();
            //var cSharpFile = File.ReadAllText(@"C:\DevSharp\apexsharp\ApexSharpDemo\ApexCode\ForIfWhile.cs");
            //var classContainer = apexSharp.ParseCSharpCode(cSharpFile);

            //CSharpGenerator cSharpGenerator = new CSharpGenerator();
            //var cSharpCode = cSharpGenerator.Generate(classContainer);
            //ValidateLineByLine(cSharpCode, cSharpFile);
        }

        public void ValidateLineByLine(string convertedCode, string orginalCode)
        {
            var convertedCodeList = convertedCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var orginalCodeList = orginalCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < convertedCodeList.Length; i++)
            {
                Assert.AreEqual(orginalCodeList[i].Trim(), convertedCodeList[i].Trim(), "\n\n" + orginalCode + "\n" + convertedCode);
            }
        }

        public void ResourceFileTest()
        {
            var qaData = ApexSharpBaseTest.Properties.TestData.Demo;
            Console.WriteLine(qaData);

            Assert.AreEqual(5, 5);
        }


    }
}
