using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Host;
using NUnit.Framework;
using Syntax = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ApexSharpBaseTest.RoslynTests
{
    [TestFixture]
    public class CSharpFormattingTests
    {
        [Test, Ignore("Dependency issues in Roslyn workspaces")]
        public void AdhocWorkspaceLoads()
        {
            // doesn't seem to work, see this issue:
            // https://github.com/dotnet/roslyn/issues/17705
            var cw = new AdhocWorkspace();
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
            var formattedCode = Formatter.Format(@class, null);

            Console.WriteLine(formattedCode.ToFullString());
        }
    }
}
