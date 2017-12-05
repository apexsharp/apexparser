namespace PrivateDemo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using ApexParser;
    using ApexSharpApi;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;


    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Started");

            ConvertToCSharp();

            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
            watch.Restart();

            Console.WriteLine("Done, Press any key to exit");
            Console.ReadKey();
        }

        public static void OffLineSymbolTable()
        {
            Setup.Init();
            try
            {
                ModelGen modelGen = new ModelGen();
                modelGen.CreateOfflineSymbolTable("PrivateDemo.SObjects", modelGen.GetAllObjectNames());
            }
            catch (ApexSharpHttpException exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        public static string GetString()
        {
            return "using System";
        }

        public static void Rosyln()
        {
            SyntaxTree sourceTree = CSharpSyntaxTree.ParseText(GetString());

            var compilation = CSharpCompilation.Create("HelloWorld")
                .AddReferences(
                    MetadataReference.CreateFromFile(
                        typeof(object).Assembly.Location))
                .AddSyntaxTrees(sourceTree);

            var rootNode = (CompilationUnitSyntax)sourceTree.GetRoot();
            var semanticModel = compilation.GetSemanticModel(sourceTree);

            var variableDeclarations = rootNode
                .DescendantNodes()
            .OfType<LocalDeclarationStatementSyntax>();

            foreach (var variableDeclaration in variableDeclarations)
            {
                var symbolInfo = semanticModel.GetSymbolInfo(variableDeclaration.Declaration.Type);
                var typeSymbol = symbolInfo.Symbol; // the type symbol for the variable..
                Console.WriteLine(typeSymbol);
            }
        }

        public static void Copy()
        {
            List<FileInfo> orgApexFileList = new DirectoryInfo(@"C:\DevSharp\SalesForceApexSharp\src\classes\").GetFiles("*.cls").ToList();

            foreach (var apexFile in orgApexFileList)
            {
                if (apexFile.Name.Equals("RunAll.cls") || apexFile.Name.Equals("DemoTest.cls") || apexFile.Name.Equals("ClassRestTest.cls"))
                {

                    Console.WriteLine("Not Copying");
                }
                else
                {
                    var newApexFileName = @"C:\DevSharp\ApexSharp\ApexSharpDemo\ApexClasses\" + Path.ChangeExtension(apexFile.Name, ".apex");
                    File.Copy(apexFile.FullName, newApexFileName, true);
                }
            }
        }

        public static void ConvertToCSharp()
        {
            List<FileInfo> apexFileList = new DirectoryInfo(@"C:\DevSharp\SalesForceApexSharp\src\classes\").GetFiles("*.cls").ToList();

            foreach (var apexFile in apexFileList)
            {
                Console.WriteLine(apexFile.Name);
                var cSharpCode = File.ReadAllText(apexFile.FullName);
                var cSharpFile = ApexParser.ApexSharpParser.ConvertApexToCSharp(cSharpCode, "PrivateDemo.CSharpClasses");

                var cSharpFileName = Path.ChangeExtension(apexFile.Name, ".cs");
                File.WriteAllText(@"C:\DevSharp\ApexSharp\PrivateDemo\CSharpClasses\" + cSharpFileName, cSharpFile);
            }
        }


        public static void ConvertToApex()
        {
            List<FileInfo> apexFileList = new DirectoryInfo(@"C:\DevSharp\ApexSharp\ApexSharpDemo\ApexClasses\").GetFiles("*.apex").ToList();
            List<FileInfo> cSharpFileList = new DirectoryInfo(@"C:\DevSharp\ApexSharp\ApexSharpDemo\CSharpClasses\").GetFiles("*.cs").ToList();

            foreach (var cSharpFile in cSharpFileList)
            {
                var cSharpCode = File.ReadAllText(cSharpFile.FullName);
                var apexFilesDictionary = ApexSharpParser.ConvertToApex(cSharpCode);

                var cSharpFileName = Path.ChangeExtension(cSharpFile.Name, ".apex");

                Console.ReadLine();
                //File.WriteAllText(@"C:\DevSharp\ApexSharp\ApexSharpDemo\CSharpClasses\" + cSharpFileName, cSharpFile);
            }
        }

    }
}
