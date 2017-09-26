using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apex.ApexSharp.ApexToSharp.Lexer;
using Apex.ApexSharp.MetaClass;

namespace Apex.ApexSharp.ApexToSharp
{
    public static class ApexTokenizer
    {
        public static ApexClassDeclarationSyntax Parse(FileInfo apexFile)
        {
            // ApexClassContainer contains all the metadata related to a given class
            ApexClassContainer apexClassContainer = new ApexClassContainer(apexFile);
            // Read the formated Apex class File into a List
            List<string> apexLines = File.ReadAllLines(apexClassContainer.ApexSource.FullName).ToList();
            return Parse(apexFile.Name, apexLines);
        }

        public static ApexClassDeclarationSyntax Parse(string apexFileName, List<string> apexLines)
        {
            ApexClassDeclarationSyntax apexClass = null;
            ApexMethodDeclarationSyntax apexMethod = null;

            string apexComments = "";
            foreach (var apexLine in apexLines)
            {
                List<ApexTocken> apexTokens = ParseApexLine(apexFileName, apexLine);

                // Remove Return, Space
                apexTokens = apexTokens.Where(x => x.TockenType != TockenType.Return).ToList();
                apexTokens = apexTokens.Where(x => x.TockenType != TockenType.Space).ToList();

                var returnLexar = Lexar(apexTokens);

                if (returnLexar == "CommentLine")
                {
                    apexComments = apexLine;
                }
                else if (returnLexar == "Class")
                {
                    apexClass = ApexClassCreator.CreateClass(apexTokens);
                    if (apexComments.Length != 0)
                    {
                        apexClass.CodeComments.Add(apexComments);
                        apexComments = "";
                    }
                }
                else if (returnLexar == "Method")
                {
                    apexMethod = ApexClassCreator.CreateMethod(apexTokens);
                    apexClass.ChildNodes.Add(apexMethod);

                    if (apexComments.Length != 0)
                    {
                        apexMethod.CodeComments.Add(apexComments);
                        apexComments = "";
                    }
                }
                else if ((returnLexar == "Open") || (returnLexar == "Close"))
                {
                }
                else if ((returnLexar == "Property") || (returnLexar == "Field"))
                {
                    apexClass.CodeInsideClass.Add(apexComments);
                    apexComments = "";
                    apexClass.CodeInsideClass.Add(apexLine);
                }
                else if (returnLexar == "")
                {
                    if ((apexComments.Length != 0) && (apexClass != null) && (apexMethod != null))
                    {
                        apexMethod.CodeInsideMethod.Add(apexComments);
                        apexComments = "";
                        apexMethod.CodeInsideMethod.Add(apexLine);
                    }

                    else if ((apexComments.Length != 0) && (apexClass != null) && (apexMethod == null))
                    {
                        apexClass.CodeInsideClass.Add(apexComments);
                        apexComments = "";
                        apexClass.CodeInsideClass.Add(apexLine);
                    }
                }
            }

            //PrintCode(apexClass);
            return apexClass;
        }

        public static void PrintCode(ApexClassDeclarationSyntax apexClass)
        {
            foreach (var code in apexClass.CodeInsideClass)
            {
                Console.WriteLine((string)code);
            }

            foreach (var apexMethod in apexClass.ApexMethods)
            {
                foreach (var apexLine in apexMethod.CodeInsideMethod)
                {
                    Console.WriteLine((string)apexLine);
                }
            }
        }

        private static List<ApexTocken> ParseApexLine(string fileName, string sourceCode)
        {
            List<ApexTocken> resultList = new List<ApexTocken>();
            Lexer.Lexer lexer = new Lexer.Lexer(fileName, sourceCode, ApexTokenList.GetTocken());

            while (true)
            {
                Result result = lexer.Next();
                if (result.IsGood)
                {
                    //if (result.TokenType != TockenType.Space) Console.WriteLine($"{result.TokenType}  {result.TokenContents}");
                    resultList.Add(new ApexTocken(result.TokenType, result.TokenContents));
                }
                else
                {
                    break;
                }
            }
            return resultList;
        }

        public static string Lexar(List<ApexTocken> apexTokens)
        {
            // Print the Token List for Debugging 
            foreach (ApexTocken apexToken in apexTokens)
            {
                Console.WriteLine($"{apexToken.TockenType}:{apexToken.Tocken}");
            }

            var index = apexTokens.FindIndex(x => x.TockenType == TockenType.CommentLine);
            if (index >= 0)
            {
                return "CommentLine";
            }

            index = apexTokens.FindIndex(x => x.TockenType == TockenType.KwClass);
            if (index >= 0)
            {
                return "Class";
            }

            index = apexTokens.FindIndex(x => x.TockenType == TockenType.OpenCurlyBrackets);
            if (index == 0)
            {
                return "Close";
            }


            index = apexTokens.FindIndex(x => x.TockenType == TockenType.CloseCurlyBrackets);
            if (index == 0)
            {
                return "Open";
            }

            var indexOne = apexTokens.FindIndex(x => x.TockenType == TockenType.AccessModifier);
            var indexTwo = apexTokens.FindIndex(x => x.TockenType == TockenType.OpenBrackets);
            var indexThree = apexTokens.FindIndex(x => x.TockenType == TockenType.CloseBrackets);
            if (indexOne == 0 && indexTwo > 0 && indexThree > 0)
            {
                return "Method";
            }

            if (apexTokens.Count > 2)
            {
                if ((apexTokens[0].TockenType == TockenType.AccessModifier) &&
                    (apexTokens[2].TockenType == TockenType.Word) &&
                    (apexTokens[apexTokens.Count - 1].TockenType == TockenType.StatmentTerminator))
                {
                    return "Field";
                }

                if ((apexTokens[0].TockenType == TockenType.AccessModifier) &&
                    (apexTokens[2].TockenType == TockenType.Word) &&
                    (apexTokens[apexTokens.Count - 1].TockenType == TockenType.CloseCurlyBrackets))
                {
                    return "Property";
                }
            }


            return "";
        }

        private static List<ApexTocken> GetFromTo(List<ApexTocken> apexTokenList, int startIndex, TockenType startType,
            TockenType endType)
        {
            var newTokenList = new List<ApexTocken>();

            bool tokenFound = false;

            for (int i = startIndex; i < apexTokenList.Count; i++)
            {
                if (apexTokenList[i].TockenType == startType) tokenFound = true;

                if (tokenFound)
                {
                    if (apexTokenList[i].TockenType == endType)
                    {
                        newTokenList.Add(apexTokenList[i]);
                        return newTokenList;
                    }
                    else
                    {
                        newTokenList.Add(apexTokenList[i]);
                    }
                }
            }

            // Dont send a large list if we have an error
            newTokenList.Clear();
            return newTokenList;
        }


        public static ApexList Parse(ApexList apexList)
        {
            List<ApexTocken> apexTokens = apexList.ApexTockens;

            // static testMethod void put() {
            if (apexTokens.Any(x => x.TockenType == TockenType.KwTestMethod))
            {
                apexList.ApexType = ApexType.ApexMethodDeclarationSyntax;
            }
            // for (Contact con : contactList)
            else if (apexTokens.Count > 4 &&
                     apexTokens[0].TockenType == TockenType.KwFor &&
                     apexTokens[1].TockenType == TockenType.OpenBrackets &&
                     apexTokens[2].TockenType == TockenType.ClassName &&
                     apexTokens[3].TockenType == TockenType.Word &&
                     apexTokens[4].TockenType == TockenType.Colon)
            {
                apexList.ApexType = ApexType.ApexForEachStatementSyntax;
            }
            // for (Integer i = 0; i < 10; i++)
            else if (apexTokens.Any(x => x.TockenType == TockenType.KwFor))
            {
                apexList.ApexType = ApexType.ApexForStatementSyntax;
            }
            // if(!contactList.isEmpty()) { / else if (!contactList.isEmpty())
            else if (apexTokens.Any(x => x.TockenType == TockenType.KwIf) ||
                     apexTokens.Any(x => x.TockenType == TockenType.KwElse))
            {
                apexList.ApexType = ApexType.ApexIfStatementSyntax;
            }
            //response.responseBody = 'OK';
            else if (apexTokens.Count > 4 &&
                     apexTokens[0].TockenType == TockenType.Word &&
                     apexTokens[1].TockenType == TockenType.Dot &&
                     apexTokens[2].TockenType == TockenType.Word &&
                     apexTokens[3].TockenType == TockenType.Equal)
            {
                apexList.ApexType = ApexType.ApexLocalDeclarationStatementSyntax;
            }
            //response.responseBody = 'OK'; Some time second word is a classname
            else if (apexTokens.Count > 4 &&
                     apexTokens[0].TockenType == TockenType.Word &&
                     apexTokens[1].TockenType == TockenType.Dot &&
                     apexTokens[2].TockenType == TockenType.ClassName &&
                     apexTokens[3].TockenType == TockenType.Equal)
            {
                apexList.ApexType = ApexType.ApexLocalDeclarationStatementSyntax;
            }
            // return
            else if (apexTokens[0].TockenType == TockenType.KwReturn)
            {
                apexList.ApexType = ApexType.ApexReturnStatementSyntax;
            }
            //  throw ((Exception) methodReturnValue.ReturnValue);
            else if (apexTokens[0].TockenType == TockenType.KwThrow)
            {
                apexList.ApexType = ApexType.ApexExpressionStatementSyntax;
            }
            // DML 
            else if (apexTokens[0].TockenType == TockenType.DML)
            {
                apexList.ApexType = ApexType.Dml;
            }
            // [SELECT ... ]
            else if (apexTokens.Any(x => x.TockenType == TockenType.Soql))
            {
                apexList.ApexType = ApexType.Soql;
            }
            // }
            else if (apexTokens[0].TockenType == TockenType.CloseCurlyBrackets)
            {
                apexList.ApexType = ApexType.CloseBrace;
            }
            // myName = "Jay"
            else if (apexTokens[0].TockenType == TockenType.Word && apexTokens[1].TockenType == TockenType.Equal)
            {
                apexList.ApexType = ApexType.ApexLocalDeclarationStatementSyntax;
            }
            // public DateTime dateTime {get;set;}
            else if (apexTokens[0].TockenType == TockenType.ClassName && apexTokens[1].TockenType == TockenType.Word &&
                     apexTokens[1].TockenType == TockenType.OpenCurlyBrackets)
            {
                apexList.ApexType = ApexType.ApexPropertyDeclarationSyntax;
            }
            // public DateTime dateTime {get;set;}
            else if (apexTokens[0].TockenType == TockenType.AccessModifier &&
                     apexTokens[1].TockenType == TockenType.ClassName && apexTokens[2].TockenType == TockenType.Word &&
                     apexTokens[3].TockenType == TockenType.OpenCurlyBrackets)
            {
                apexList.ApexType = ApexType.ApexPropertyDeclarationSyntax;
            }
            // DateTime dateTime {get;set;}
            else if (apexTokens[0].TockenType == TockenType.ClassName && apexTokens[1].TockenType == TockenType.Word &&
                     apexTokens[2].TockenType == TockenType.OpenCurlyBrackets)
            {
                apexList.ApexType = ApexType.ApexPropertyDeclarationSyntax;
            }
            // get; or set;
            else if (apexTokens[0].TockenType == TockenType.KwGetSet)
            {
                apexList.ApexType = ApexType.ApexAccessorDeclarationSyntax;
            }
            // public get; or private set;
            else if (apexTokens[0].TockenType == TockenType.AccessModifier &&
                     apexTokens[1].TockenType == TockenType.KwGetSet)
            {
                apexList.ApexType = ApexType.ApexAccessorDeclarationSyntax;
            }
            // methodCountRecorder.verifyMethodCall(qm , methodArg);
            else if (apexTokens.Count > 3 &&
                     apexTokens[0].TockenType == TockenType.Word &&
                     apexTokens[1].TockenType == TockenType.Dot &&
                     apexTokens[2].TockenType == TockenType.Word &&
                     apexTokens[3].TockenType == TockenType.OpenBrackets)
            {
                apexList.ApexType = ApexType.ApexExpressionStatementSyntax;
            }
            else if (apexTokens.Count > 3 &&
                     apexTokens[0].TockenType == TockenType.Word &&
                     apexTokens[1].TockenType == TockenType.OpenBrackets &&
                     apexTokens[2].TockenType == TockenType.Word)
            {
                apexList.ApexType = ApexType.ApexExpressionStatementSyntax;
            }
            // Contains ClassName, Generic or Arrary
            else if (apexTokens.FindIndex(x => x.TockenType == TockenType.ClassName ||
                                               x.TockenType == TockenType.ClassNameGeneric ||
                                               x.TockenType == TockenType.ClassNameArray) >= 0)
            {
                var classNameLocation =
                    apexTokens.FindIndex(x => x.TockenType == TockenType.ClassName ||
                                              x.TockenType == TockenType.ClassNameGeneric ||
                                              x.TockenType == TockenType.ClassNameArray);

                // Do we have a access modifier like public, private etc
                if ((apexTokens.FindIndex(x => x.TockenType == TockenType.AccessModifier) >= 0))
                {
                    // public class Aaa
                    if (apexTokens.Any(x => x.TockenType == TockenType.KwClass))
                    {
                        apexList.ApexType = ApexType.ApexClassDeclarationSyntax;
                    }
                    // public Aaa(); 
                    else if (apexTokens[classNameLocation + 1].TockenType == TockenType.OpenBrackets)
                    {
                        apexList.ApexType = ApexType.ApexConstructorDeclarationSyntax;
                    }

                    // public String MethodOneDemoDemo = 'This is a Test';
                    else if (apexTokens[classNameLocation + 1].TockenType == TockenType.Word &&
                             apexTokens[classNameLocation + 2].TockenType == TockenType.Equal)
                    {
                        apexList.ApexType = ApexType.ApexFieldDeclarationSyntax;
                    }
                    // public DateTime MethodOneDateTimeFour;
                    else if (apexTokens[classNameLocation + 1].TockenType == TockenType.Word &&
                             apexTokens[classNameLocation + 2].TockenType == TockenType.StatmentTerminator)
                    {
                        apexList.ApexType = ApexType.ApexFieldDeclarationSyntax;
                    }
                    // public void MethodOne()
                    else if (apexTokens[classNameLocation + 1].TockenType == TockenType.Word &&
                             apexTokens[classNameLocation + 2].TockenType == TockenType.OpenBrackets)
                    {
                        apexList.ApexType = ApexType.ApexMethodDeclarationSyntax;
                    }
                }
                // ExpressionStatement or LocalDeclaration
                else
                {
                    // System.debug(i)
                    if (apexTokens[classNameLocation + 1].TockenType == TockenType.Dot &&
                        apexTokens[classNameLocation + 2].TockenType == TockenType.Word)
                    {
                        apexList.ApexType = ApexType.ApexExpressionStatementSyntax;
                    }

                    // String MethodOneDemoDemo = 'This is a Test';
                    else if (apexTokens[classNameLocation + 1].TockenType == TockenType.Word &&
                             apexTokens[classNameLocation + 2].TockenType == TockenType.Equal)
                    {
                        apexList.ApexType = ApexType.ApexLocalDeclarationStatementSyntax;
                    }
                    // DateTime MethodOneDateTimeFour;
                    else if (apexTokens[classNameLocation + 1].TockenType == TockenType.Word &&
                             apexTokens[classNameLocation + 2].TockenType == TockenType.StatmentTerminator)
                    {
                        apexList.ApexType = ApexType.ApexLocalDeclarationStatementSyntax;
                    }
                }
            }
            else
            {
                foreach (var apexToken in apexList.ApexTockens)
                {
                    Console.WriteLine($"{apexToken.TockenType}:{apexToken.Tocken}");
                }
                apexList.ApexType = ApexType.NotFound;
            }

            return apexList;
        }

        // Simple Hack to Fix ForLoop
        public static List<string> ForLoopFix(List<string> apexTokenList)
        {
            List<string> apexCode = new List<string>();

            var count = 0;

            while (count < apexTokenList.Count)
            {
                if (apexTokenList[count].Trim().StartsWith("for", StringComparison.CurrentCultureIgnoreCase))
                {
                    var forloop = apexTokenList[count] + apexTokenList[count + 1] + apexTokenList[count + 2];
                    apexCode.Add(forloop);
                    count = count + 3;
                }
                else
                {
                    apexCode.Add(apexTokenList[count]);
                    count++;
                }
            }

            return apexCode;
        }

        public static List<string> GetApexList(List<ApexTocken> apexTokenList)
        {
            List<string> apexCode = new List<string>();

            String sb = "";

            foreach (var apexTocken in apexTokenList)
            {
                if (apexTocken.TockenType == TockenType.CloseCurlyBrackets)
                {
                    apexCode.Add(apexTocken.Tocken);
                    sb = "";
                }
                else if (apexTocken.TockenType == TockenType.CommentLine)
                {
                    apexCode.Add(" " + apexTocken.Tocken);
                    sb = "";
                }
                else if (apexTocken.TockenType == TockenType.OpenCurlyBrackets)
                {
                    apexCode.Add(sb);
                    apexCode.Add(apexTocken.Tocken);
                    sb = "";
                }
                else if (apexTocken.TockenType == TockenType.StatmentTerminator)
                {
                    sb = sb.TrimEnd(' ');
                    sb = sb + apexTocken.Tocken;
                    apexCode.Add(sb);
                    sb = "";
                }
                else if (apexTocken.TockenType == TockenType.Dot)
                {
                    sb = sb.TrimEnd(' ');
                    sb = sb + apexTocken.Tocken;
                }
                else
                {
                    sb = sb + apexTocken.Tocken;
                    sb = sb + " ";
                }
            }

            return apexCode;
        }
    }
}