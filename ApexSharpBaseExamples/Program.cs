using System;
using System.Collections.Generic;


namespace ApexSharpBaseExamples
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ApexSharpBase;
    using ApexSharpBase.Converter.CSharp;

    class Program
    {
        public static void Main(string[] args)
        {
            ApesSharp apexSharp = new ApesSharp();
            var cSharpFile = File.ReadAllText(@"..\..\ApexCode\Demo.cs");
            var classContainer = apexSharp.ParseCSharpCode(cSharpFile);

            Console.WriteLine(classContainer.ChildNodes[0].ChildNodes[0].Kind);
            Console.WriteLine(classContainer.ChildNodes[0].ChildNodes[0].CodeBlock);

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
