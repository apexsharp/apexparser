using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharpBase;
using ApexSharpBase.Converter.CSharp;

namespace ApexSharpBaseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\"));


            ApesSharp apexSharp = new ApesSharp();
            var cSharpFile = File.ReadAllText(newPath + @"\ApexSharpDemo\ApexCode\DemoClass.cs");
            var classContainer = apexSharp.ParseCSharpCode(cSharpFile);

            CSharpGenerator cSharpGenerator = new CSharpGenerator();
            var cSharpCode = cSharpGenerator.Generate(classContainer);

            Console.WriteLine(cSharpCode);

            Console.ReadLine();
        }
    }
}
