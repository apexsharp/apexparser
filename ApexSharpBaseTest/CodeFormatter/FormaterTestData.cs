using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSharpBaseTest.CodeFormatter
{
    public class FormaterTestData
    {
        public static string DemoInput =
@"/*
* This  is a comment line one
* This is a comment // line two
*/
public with sharing class FormatDemoInput
{
public Integer
    dataOfBirth
        { get; set; }
public void ForLoopTest() {
    for (Integer i = 0; i < 10; i++) {
        List<Contact> contacts =
        [
                SELECT Name, Email // This is a middle line comment
                From Contact
                Where Name = 'Jay'
        ];
    }
}
}";

        public static string DemoOutPut =
@"/*
* This  is a comment line one
* This is a comment // line two
*/
public with sharing class FormatDemoInput
{
    public Integer dataOfBirth { get; set; }

    public void ForLoopTest()
    {
        for (Integer i = 0; i < 10; i++)
        {
            // This is a middle line comment
            List<Contact> contacts = [ SELECT Name, Email From Contact Where Name = 'Jay' ];
        }
    }
}";

    }
}
