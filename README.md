### A C# to APEX Converter AND a APEX to C# Converter

[![appveyor](https://ci.appveyor.com/api/projects/status/github/apexsharp/apexparser?svg=true)](https://ci.appveyor.com/project/yallie/apexparser)
[![tests](https://img.shields.io/appveyor/tests/yallie/apexparser.svg)](https://ci.appveyor.com/project/yallie/apexparser/build/tests)
[![codecov](https://codecov.io/gh/apexsharp/apexparser/branch/master/graph/badge.svg)](https://codecov.io/gh/apexsharp/apexparser)
[![NuGet](https://img.shields.io/nuget/v/ApexParser.svg)](https://nuget.org/packages/ApexParser)

Apex ←→ C# - Two Way [Transpiler](https://en.wikipedia.org/wiki/Source-to-source_compiler).


ApexSharp.ApexParser

var ast = ApexSharpParser.GetApexAst(string apexCode) var apexCode = ApexSharpParser.GetApex(ast)

ApexSharp.ApexToCSharp

ApexToCSharp.convertToCSharp(apexparser ast)

CSharpToApex

SyntaxTree tree = CSharpSyntaxTree.ParseText(cSharpFile); var root = (CompilationUnitSyntax)tree.GetRoot(); var ast CSharpToApex.ConvertToApexAst(root)

TApexSharp.CSharpToApex

Var apexCode = ApexSharpParser.GetApex(ast)



This is very early stage Beta Software use it at your own risk :-).

Jay
EMail: <Jay@JayOnSoftware.com>
[LinkedIn](https://www.linkedin.com/in/jayonsoftware/)
