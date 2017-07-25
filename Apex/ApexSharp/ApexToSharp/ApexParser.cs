using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apex.ApexSharp.ApexToSharp.Lexer;
using Apex.ApexSharp.MetaClass;

namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexParser
    {
        public static void Parse(List<FileInfo> apexFileList)
        {
            List<ApexClassContainer> apexClassContainerList = new List<ApexClassContainer>();
            List<ApexClassDeclarationSyntax> apexClassDeclarationSyntaxList = new List<ApexClassDeclarationSyntax>();

            // Read all the classes and create a list of ApexClassContainer objects
            foreach (FileInfo apexFile in apexFileList)
            {
                ApexClassContainer apexClass = new ApexClassContainer(apexFile);
                apexClassContainerList.Add(apexClass);

                string lowerClassName = apexClass.GetClassName().ToLower();
                ClassNameManager.ClassNameList.Add(lowerClassName);
            }

            foreach (ApexClassContainer apexClassContainer in apexClassContainerList)
            {
                Console.WriteLine($"Loading {apexClassContainer.ApexSource.FullName}");

                // Read the file, Parase It
                string apexCode = File.ReadAllText(apexClassContainer.ApexSource.FullName);
                apexClassContainer.ApexTokens = Parse(apexClassContainer.ApexSource.FullName, apexCode);

                // Remove Mulit Lines Comments 
                apexClassContainer.ApexTokens = CommentClean.RemoveApexTokens(apexClassContainer.ApexTokens);

                // Remove Return, Space, ApexError Tokens
                apexClassContainer.ApexTokens = apexClassContainer.ApexTokens
                    .Where(x => x.TockenType != TockenType.Return).ToList();
                apexClassContainer.ApexTokens = apexClassContainer.ApexTokens
                    .Where(x => x.TockenType != TockenType.Space).ToList();
                apexClassContainer.ApexTokens = apexClassContainer.ApexTokens
                    .Where(x => x.TockenType != TockenType.ApexError).ToList();

                //    ApexTokenizer.Lexar(apexClassContainer.ApexTokens);


                // ToDo: If we have any classes on the parsed list then mark them. 


                //// mark all words that are class name
                //for (int i = 0; i < apexClassContainer.ApexTokens.Count - 1; i++)
                //{
                //    if (apexClassContainer.ApexTokens[i].TockenType == TockenType.Word)
                //    {
                //        string className = apexClassContainer.ApexTokens[i].Tocken;

                //        if (ClassNameManager.ClassNameList.Contains(className, StringComparer.CurrentCultureIgnoreCase))
                //        {
                //            apexClassContainer.ApexTokens[i].TockenType = TockenType.ClassName;
                //        }
                //    }
                //}

                //// Mark Methods
                //for (int i = 0; i < apexClassContainer.ApexTokens.Count - 2; i++)
                //{
                //    if (apexClassContainer.ApexTokens[i].TockenType == TockenType.ClassName &&
                //        apexClassContainer.ApexTokens[i + 1].TockenType == TockenType.Dot &&
                //        apexClassContainer.ApexTokens[i + 2].TockenType == TockenType.Word)
                //    {
                //        string methodName = apexClassContainer.ApexTokens[i + 2].Tocken;

                //        if (MethodNameManager.MethodNameList.Contains(methodName,
                //            StringComparer.CurrentCultureIgnoreCase))
                //        {
                //            apexClassContainer.ApexTokens[i + 2].TockenType = TockenType.MethodName;
                //        }
                //    }
                //}


                // ToDo: Better Error Management
                foreach (ApexList apexList in apexClassContainer.ApexListList)
                {
                    ApexList returnedApexList = ApexTokenizer.Parse(apexList);

                    if (returnedApexList.ApexType == ApexType.NotFound)
                    {
                        Console.WriteLine($"Error Found in, Enter to End {apexClassContainer.GetClassName()}");
                        Console.ReadLine();
                    }
                }



                // Build the Nested Syntex Tree
                Stack<ApexSyntaxNode> apexStack = new Stack<ApexSyntaxNode>();
                ApexClassDeclarationSyntax rootApexClass = null;

                foreach (ApexList apexList in apexClassContainer.ApexListList)
                {
                    //Console.WriteLine(apexList.GetTokenListForInsert(0));

                    switch (apexList.ApexType)
                    {
                        case ApexType.ApexClassDeclarationSyntax:
                            //     rootApexClass = ApexClassCreator.CreateClassDeclaration(apexList);
                            // Inner Class Support
                            if (apexStack.Count > 0)
                            {
                                apexStack.Peek().ChildNodes.Add(rootApexClass);
                            }
                            apexStack.Push(rootApexClass);
                            break;
                        case ApexType.ApexConstructorDeclarationSyntax:
                            ApexConstructor construtor = ApexClassCreator.CreateConstrutor(apexList);
                            apexStack.Peek().ChildNodes.Add(construtor);
                            apexStack.Push(construtor);
                            break;
                        case ApexType.ApexMethodDeclarationSyntax:
                            //   ApexMethodDeclarationSyntax apexMethod = ApexClassCreator.CreateMethod(apexList);
                            //    apexStack.Peek().ChildNodes.Add(apexMethod);
                            //   apexStack.Push(apexMethod);
                            break;
                        case ApexType.ApexForStatementSyntax:
                            ApexForStatementSyntax loop = ApexClassCreator.CreateForLoop(apexList);
                            apexStack.Peek().ChildNodes.Add(loop);
                            apexStack.Push(loop);
                            break;
                        case ApexType.ApexForEachStatementSyntax:
                            ApexForEachStatementSyntax forEach = ApexClassCreator.CreateForEach(apexList);
                            apexStack.Peek().ChildNodes.Add(forEach);
                            apexStack.Push(forEach);
                            break;
                        case ApexType.ApexPropertyDeclarationSyntax:
                            ApexPropertyDeclarationSyntax apexProperty =
                                ApexClassCreator.CreatePropertyDeclaration(apexList);
                            apexStack.Peek().ChildNodes.Add(apexProperty);
                            apexStack.Push(apexProperty);
                            break;
                        case ApexType.ApexAccessorDeclarationSyntax:
                            ApexAccessorDeclarationSyntax apexAccessor =
                                ApexClassCreator.CreateAccessorDeclaration(apexList);
                            apexStack.Peek().ChildNodes.Add(apexAccessor);

                            if (apexAccessor.ContainsChildren) apexStack.Push(apexAccessor);

                            break;
                        case ApexType.ApexIfStatementSyntax:
                            ApexIfStatementSyntax apexIf = ApexClassCreator.CreateIfStatementSyntax(apexList);
                            apexStack.Peek().ChildNodes.Add(apexIf);
                            apexStack.Push(apexIf);
                            break;

                        case ApexType.ApexFieldDeclarationSyntax:
                            ApexFieldDeclarationSyntax apexAssignment =
                                ApexClassCreator.CreateFieldDeclaration(apexList);
                            apexStack.Peek().ChildNodes.Add(apexAssignment);
                            break;
                        case ApexType.ApexExpressionStatementSyntax:
                            ApexExpressionStatementSyntax expression =
                                ApexClassCreator.CreateExpressionStatement(apexList);
                            apexStack.Peek().ChildNodes.Add(expression);
                            break;
                        case ApexType.ApexLocalDeclarationStatementSyntax:
                            ApexLocalDeclarationStatementSyntax local =
                                ApexClassCreator.CreateLocalDeclarationStatement(apexList);
                            apexStack.Peek().ChildNodes.Add(local);
                            break;
                        case ApexType.ApexReturnStatementSyntax:
                            ApexReturnStatementSyntax returnStateement =
                                ApexClassCreator.CreateReturnExpression(apexList);
                            apexStack.Peek().ChildNodes.Add(returnStateement);
                            break;
                        case ApexType.Soql:
                            SoqlExpression soql = ApexClassCreator.CreateSoqlExpression(apexList);
                            apexStack.Peek().ChildNodes.Add(soql);
                            break;
                        case ApexType.Dml:
                            DmlExpression dmlExpression = ApexClassCreator.CreateDmlExpressionn(apexList);
                            apexStack.Peek().ChildNodes.Add(dmlExpression);
                            break;

                        case ApexType.CloseBrace:
                            ApexCloseCurlyBrackets closeCurlyBrackets =
                                ApexClassCreator.CreateCloseCurlyBrackets(apexList);
                            apexStack.Peek().ChildNodes.Add(closeCurlyBrackets);
                            apexStack.Pop();
                            break;
                        default:
                            Console.WriteLine($"Could Not Support in ApexParser {apexList.ApexType}");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }

        public static void FormatApex(List<FileInfo> apexFileList)
        {
            List<ApexClassContainer> apexClassContainerList = new List<ApexClassContainer>();


            // Read all the classes and create a list of ApexClassContainer objects
            foreach (FileInfo apexFile in apexFileList)
            {
                ApexClassContainer apexClass = new ApexClassContainer(apexFile);
                apexClassContainerList.Add(apexClass);

                string lowerClassName = apexClass.GetClassName().ToLower();
                ClassNameManager.ClassNameList.Add(lowerClassName);
            }

            foreach (ApexClassContainer apexClassContainer in apexClassContainerList)
            {
                Console.WriteLine($"Loading {apexClassContainer.ApexSource.FullName}");

                // Read the file, Parase It
                string apexCode = File.ReadAllText(apexClassContainer.ApexSource.FullName);
                apexClassContainer.ApexTokens = Parse(apexClassContainer.ApexSource.FullName, apexCode);

                // Remove Mulit Lines Comments 
                apexClassContainer.ApexTokens = CommentClean.RemoveApexTokens(apexClassContainer.ApexTokens);

                // Remove Return, Space, ApexError Tokens
                // apexClassContainer.ApexTokens = apexClassContainer.ApexTokens.Where(x => x.TockenType != TockenType.CommentLine).ToList();
                apexClassContainer.ApexTokens = apexClassContainer.ApexTokens
                    .Where(x => x.TockenType != TockenType.Return).ToList();
                apexClassContainer.ApexTokens = apexClassContainer.ApexTokens
                    .Where(x => x.TockenType != TockenType.Space).ToList();
                apexClassContainer.ApexTokens = apexClassContainer.ApexTokens
                    .Where(x => x.TockenType != TockenType.ApexError).ToList();

                // Print the Token List for Debugging 
                foreach (ApexTocken apexToken in apexClassContainer.ApexTokens)
                {
                    Console.WriteLine($"{apexToken.TockenType}:{apexToken.Tocken}");
                }

                // Brake Code into lines 
                var apexCodeList = ApexTokenizer.GetApexList(apexClassContainer.ApexTokens);
                apexCodeList = ApexTokenizer.ForLoopFix(apexCodeList);

                foreach (var apexCodeString in apexCodeList)
                {
                    // Console.WriteLine(apexCodeString);
                }

                File.WriteAllLines(apexClassContainer.ApexSource.FullName, apexCodeList);
            }
        }

        private static List<ApexTocken> Parse(string fileName, string sourceCode)
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
    }
}