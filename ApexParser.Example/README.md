# Apex Parser Demos

Demos showing how to use the Apex Parser

## Syntax

apexparserdemo.exe -service ApexTestFind -dir c:\apex\src -apex demo.cls

apexparserdemo.exe -service ApexApiAnalyzer -dir c:\apex\src 

apexparserdemo.exe -service CaseClean -dir c:\apex\src 

apexparserdemo.exe -service NoAssert -dir c:\apex\src 

apexparserdemo.exe -service ApexCodeFormat -dir c:\apex\src 



## Detail

#### All the values are returnd as JSON so you can pipe these into other tools. 


### ApexTestFind

Given a apex classs, find all the unit test that calls the given class.

### ApexApiAnalyzer

Find all the Apex classes and methods used on your code

### CaseClean

Since apex is not case senstive you can write

* System.debug (The Correct way)
* system.debug 
* SYSTEM.DEBUG 

and all of the above are correct. CaseClean will clean up your code and make all the class and methods name to match what SF shows on the API docs

### No Assert

List all the Test Classes and the methods that miss Assert

### Apex Code Format

This will format all the settings based on Settings.cs (We will move this to JSON later)