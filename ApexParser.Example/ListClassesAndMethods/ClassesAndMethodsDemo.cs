using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ApexSharpDemo.ListClassesAndMethods
{
    public class SalesForceClassInfo
    {
        public List<string> Variables = new List<string>();
        public string ClassName { get; }
        public List<SalesForceMethod> MethodList = new List<SalesForceMethod>();
        public bool IsSalesforceApi { get; }
        public SalesForceClassInfo(string className, bool isSalesforceApi)
        {
            ClassName = className;
            IsSalesforceApi = isSalesforceApi;
        }
    }
    public class SalesForceMethod
    {
        public string MethodName { get; }
        public SalesForceMethod(string methodName)
        {
            MethodName = methodName;
        }
    }


    public class ClassesAndMethodsDemo
    {
        // Class to keep Track of Usages
    

        public static void PrintDetails(List<SalesForceClassInfo> classNameList)
        {

            Console.WriteLine(JsonConvert.SerializeObject(classNameList, Formatting.Indented));
            foreach (var salesForceClassInfo in classNameList)
            {
                var methodEnumerable = salesForceClassInfo.MethodList.GroupBy(x => new
                {
                    x.MethodName,
                }).Select(x => new
                {
                    x.Key.MethodName,
                    Count = x.Count()
                });

                foreach (var salesForceMethod in methodEnumerable)
                {
                    //Console.WriteLine(salesForceClassInfo.ClassName + " " + salesForceMethod.MethodName + "  " + salesForceMethod.Count);
                }
            }

        }
        public static List<SalesForceClassInfo> GetClassMethodCount(string cSharpFile)
        {
            List<SalesForceClassInfo> classNameList = new List<SalesForceClassInfo>();


            SyntaxTree tree = CSharpSyntaxTree.ParseText(cSharpFile);
            var root = (CompilationUnitSyntax)tree.GetRoot();

            string currentMethod = String.Empty;

            foreach (SyntaxNode descendantNode in root.DescendantNodes())
            {
                //Console.WriteLine(">>: " + descendantNode.Kind() + "\n" + descendantNode.ToFullString().Trim());


                if (descendantNode.Kind() == SyntaxKind.MethodDeclaration)
                {
                    var methodDeclaration = (MethodDeclarationSyntax) descendantNode;
                    currentMethod = methodDeclaration.Identifier.Value.ToString();
                }
                else if (descendantNode.Kind() == SyntaxKind.VariableDeclaration)
                {
                    var variableDeclaration = (VariableDeclarationSyntax)descendantNode;
                    var className = variableDeclaration.Type.ToString();
                    var variableName = variableDeclaration.Variables[0].Identifier.ToString();

                    VariableDeclaration(className, variableName, classNameList, currentMethod);
                }

                else if (descendantNode.Kind() == SyntaxKind.SimpleMemberAccessExpression)
                {
                    var memberAccessExpression = (MemberAccessExpressionSyntax)descendantNode;
                    var expressionName = memberAccessExpression.Expression.ToString();
                    var methodName = memberAccessExpression.Name.ToString();

                    SimpleMemberAccess(expressionName, methodName, classNameList, currentMethod);
                }
            }

            return classNameList;
        }

        private static void SimpleMemberAccess(string expressionName, string methodName, List<SalesForceClassInfo> classNameList, string currentMethod)
        {
            // For Methods
            var salesforceClass = classNameList.FirstOrDefault(x =>
                x.Variables.Contains(currentMethod + ":" + expressionName) || x.ClassName.Equals(expressionName));
            if (salesforceClass != null)
            {
                salesforceClass.MethodList.Add(new SalesForceMethod(methodName));
            }
            else
            {
                salesforceClass = new SalesForceClassInfo(expressionName, IsSalesForceApi(expressionName));
                salesforceClass.MethodList.Add(new SalesForceMethod(methodName));
                classNameList.Add(salesforceClass);
            }
        }

        private static void VariableDeclaration(string className, string variableName, List<SalesForceClassInfo> classNameList, string currentMethod)
        {

            var salesforceClass =
                classNameList.FirstOrDefault(x => x.Variables.Contains(className) || x.ClassName.Equals(className));

            if (salesforceClass == null)
            {
                salesforceClass = new SalesForceClassInfo(className, IsSalesForceApi(className));
                salesforceClass.Variables.Add(currentMethod + ":" + variableName);
                classNameList.Add(salesforceClass);
            }
            else
            {
                salesforceClass.Variables.Add(currentMethod + ":" + variableName);
            }
        }


        // Look at SF DB and decide if the class is a Salesforce API Or Not
        public static bool IsSalesForceApi(string className)
        {
            if (className.Equals("HttpRequest") || className.Equals("Http"))
            {
                return true;
            }
            else return false;
        }
    }
}
