using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Host.Mef;
using NUnit.Framework;
using Syntax = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ApexSharpBaseTest.RoslynTests
{
    [TestFixture]
    public class CSharpFormattingTests
    {
        private AdhocWorkspace CreateAdhocWorkspace()
        {
            // using the workaround described here:
            // https://github.com/dotnet/roslyn/issues/15603
            var parts = new List<Type>();
            foreach (var a in MefHostServices.DefaultAssemblies)
            {
                try
                {
                    parts.AddRange(a.GetTypes());
                }
                catch (ReflectionTypeLoadException thatsWhyWeCantHaveNiceThings)
                {
                    parts.AddRange(thatsWhyWeCantHaveNiceThings.Types);
                }
            }
            parts.RemoveAll(x => x == null);

            var container = new ContainerConfiguration().WithParts(parts).CreateContainer();
            var host = MefHostServices.Create(container);
            return new AdhocWorkspace(host);
        }

        [Test]
        public void AdhocWorkspaceLoads()
        {
            // doesn't seem to work, see this issue:
            // https://github.com/dotnet/roslyn/issues/17705
            //var cw = new AdhocWorkspace();

            Assert.DoesNotThrow(() => CreateAdhocWorkspace());
        }

        [Test, Ignore("Dependency issues in Roslyn workspaces")]
        public void CSharpFormatterCanFormatAstWithoutTrivia()
        {
            var consoleWriteLine = Syntax.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                Syntax.IdentifierName("Console"),
                name: Syntax.IdentifierName("WriteLine"));

            var arguments = Syntax.ArgumentList(
                Syntax.SeparatedList(new[]
                {
                    Syntax.Argument(
                        Syntax.LiteralExpression (
                            SyntaxKind.StringLiteralExpression,
                                Syntax.Literal(@"""Hello World!""", "Hello World!")))
                }));

            var consoleWriteLineStatement = Syntax.ExpressionStatement(
                Syntax.InvocationExpression(consoleWriteLine, arguments));

            var voidType = Syntax.ParseTypeName("void");
            var method = Syntax.MethodDeclaration(voidType, "Method")
                .WithBody(Syntax.Block(consoleWriteLineStatement));

            var intType = Syntax.ParseTypeName("int");
            var getterBody = Syntax.ReturnStatement(Syntax.DefaultExpression(intType));
            var getter = Syntax.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration,
                Syntax.Block(getterBody));

            var property = Syntax.PropertyDeclaration(intType, "Property")
                .WithAccessorList(Syntax.AccessorList(Syntax.SingletonList(getter)));

            var @class = Syntax.ClassDeclaration("MyClass")
                .WithMembers(Syntax.List(new MemberDeclarationSyntax[] { method, property }));

            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
            var formattedCode = Formatter.Format(@class, cw);

            Console.WriteLine(formattedCode.ToFullString());
        }
    }
}
