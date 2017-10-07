using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharpBase.MetaClass;
using ApexSharpBase.Parser.CSharp;
using Newtonsoft.Json;

namespace ApexSharpBase
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CSharpParser parser = new CSharpParser();
            NamespaceSyntax reply = parser.ParseCSharpFromFile(new FileInfo(@"C:\DevSharp\ApexSharp\ApexSharpDemo\ApexCode\Demo.cs"));
            string jsonString = JsonConvert.SerializeObject(reply, Formatting.Indented);
            Console.WriteLine(jsonString);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
