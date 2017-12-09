### A C# to APEX Converter AND a APEX to C# Converter

[![appveyor](https://ci.appveyor.com/api/projects/status/github/jayonsoftware/apexsharp?svg=true)](https://ci.appveyor.com/project/jayonsoftware/apexsharp)
[![tests](https://img.shields.io/appveyor/tests/jayonsoftware/apexsharp.svg)](https://ci.appveyor.com/project/jayonsoftware/apexsharp/build/tests)
[![codecov](https://codecov.io/gh/yallie/apexsharp/branch/master/graph/badge.svg)](https://codecov.io/gh/yallie/apexsharp)
[![NuGet](https://img.shields.io/nuget/v/ApexParser.svg)](https://nuget.org/packages/ApexParser)

![Logo](apexsharpLogo.jpg)


Apex <> C# - Two Way [Transpiler](https://en.wikipedia.org/wiki/Source-to-source_compiler). A Quick 10 Min Video Intro can be found at https://vimeo.com/224927838

I am working on a detail documentation, but for now I assume you are a C# developer who is working on Salesforce.

#### Setting up and Running 

1. Download the whole solution
2. The **ApexSharpDemo** project contains sample code.
3. Set the correct values on SimpleDemo.cs on the above project

 ```csharp
 var apexSharp = new ApexSharp();
// Setup connection info
apexSharp.SalesForceUrl("https://login.salesforce.com")
    .UseSalesForceApiVersion(40)
    .WithUserId("SalesForce User Id")
    .AndPassword("SalesForce Password")
    .AndToken("SalesForce Token")
    .SetApexFileLocation("Location Where you want your APEX Files to be saved")
    .SetLogLevel(LogLevle.Info)
    .SaveApexSharpConfig("login.json");
```



4. Run SimpleDemo.cs

This is very early stage Alpha Software use it at your own risk :-).

Jay
EMail: <Jay@JayOnSoftware.com>  
[LinkedIn](https://www.linkedin.com/in/jayonsoftware/) 
