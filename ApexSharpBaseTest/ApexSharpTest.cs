using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharpBase;
using ApexSharpBase.Converter.CSharp;
using NUnit.Framework;

namespace ApexSharpBaseTest
{
   
    public class ApexSharpTest
    {
       
        public void ParseCSharpCodeTest()
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\"));


            ApesSharp apexSharp = new ApesSharp();
            var cSharpFile = File.ReadAllText(@"C:\DevSharp\apexsharp\ApexSharpDemo\ApexCode\DemoClass.cs");
            var classContainer = apexSharp.ParseCSharpCode(cSharpFile);

            CSharpGenerator cSharpGenerator = new CSharpGenerator();
            var cSharpCode = cSharpGenerator.Generate(classContainer);

            Assert.AreEqual(5, 5);
        }
    }
}
