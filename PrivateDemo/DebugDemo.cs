using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PrivateDemo
{

    public class DebugDemo
    {
        public System.String MyString { get; set; }

        public static void Main()
        {
            var str =
                @"
                namespace ClassLibrary31
                {
                public class Class1
                    {
                        public int x {get;set;}
                        public void Demo()
                        {
                            var demo = $""SELECT ID, Email, Name FROM Contact WHERE ID = { x } LIMIT 1"";
                        }
                    }
                }";



            MetadataReference[] metadataReferenceReferences = { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) };

            var syntaxTree = SyntaxFactory.ParseSyntaxTree(str);
            var compilation =
                CSharpCompilation
                    .Create("TraceFluent",
                        new[] { syntaxTree },
                        options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, warningLevel: 1),
                        references: metadataReferenceReferences
                    );


            SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree, true);

            List<IdentifierNameSyntax> propertySyntaxNode =
                syntaxTree.GetRoot()
                    .DescendantNodes()
                    .OfType<IdentifierNameSyntax>().ToList();
            //    .First();

            Console.WriteLine(propertySyntaxNode[2].Identifier);

            var symbolInfo = semanticModel.GetSymbolInfo(propertySyntaxNode[2]);
            Console.WriteLine(symbolInfo.Symbol.GetType().FullName);

        }
    }
}
