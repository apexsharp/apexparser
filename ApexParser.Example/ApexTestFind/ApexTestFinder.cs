using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ApexSharp.ApexParser;
using ApexSharp.ApexParser.Syntax;

namespace ApexTestFind
{
    public class ApexTestFinder
    {

        public static List<string> GetAllTestClasses(string apexClassName)
        {
            var executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return GetAllTestClasses(executableLocation + @"\apex\", apexClassName);
        }
        public static List<string> GetAllTestClasses(string location, string apexClassName)
        {
            List<string> apexFileNames = Directory.GetFiles(location, "*.cls").ToList();

            // Console.WriteLine(apexFileNames.Count);


            //foreach (var apexFileName in apexFileNames)
            //{
            //        var apexFile = File.ReadAllText(apexFileName);
            //        var ast = ApexSharpParser.GetApexAst(apexFile);

            //}

            var apexTexts = apexFileNames.Select(File.ReadAllText).ToArray();
            var classes = GetApexClassesReferencingAGivenClass(apexTexts, apexClassName);
            return classes.ToList();
        }

        public static readonly string[] emptyArray = new string[0];

        public static string[] GetApexClassesReferencingAGivenClass(string[] apexTexts, string className)
        {
            if (apexTexts == null || apexTexts.All(string.IsNullOrWhiteSpace))
            {
                return emptyArray;
            }



            var apexClasses = apexTexts.Select(ApexSharpParser.GetApexAst).ToArray();
            return GetApexClassesReferencingAGivenClass(apexClasses, className);
        }

        public static string[] GetApexClassesReferencingAGivenClass(MemberDeclarationSyntax[] apexClasses, string className)
        {
            if (apexClasses == null)
            {
                return emptyArray;
            }

            return
                apexClasses.OfType<ClassDeclarationSyntax>()
                    .Where(IsTestClass)
                    .Where(c => HasReferencesTo(c, className))
                    .Select(c => c.Identifier)
                    .ToArray();
        }

        public static bool IsTestClass(ClassDeclarationSyntax node)
        {
            if (node == null)
            {
                return false;
            }

            var attributes =
                from ann in node.ChildNodes.OfType<AnnotationSyntax>()
                select ann.Identifier;

            var knownAttributes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "TestFixture", "Test", "isTest"
            };

            return attributes.Any(a => knownAttributes.Contains(a));
        }

        public static bool TextReferencesAClass(string text, string className)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(className))
            {
                return false;
            }

            return Regex.IsMatch(text, @"\b" + className + @"\b", RegexOptions.IgnoreCase);
        }

        public static bool HasReferencesTo(ClassDeclarationSyntax node, string className)
        {
            if (node == null)
            {
                return false;
            }

            var comparer = StringComparer.OrdinalIgnoreCase;

            // field declaration expressions
            foreach (var fieldDecl in node.DescendantNodes().OfType<FieldDeclarationSyntax>())
            {
                if (comparer.Compare(fieldDecl.Type?.Identifier, className) == 0)
                {
                    return true;
                }
            }

            // property declaration expressions
            foreach (var propDecl in node.DescendantNodes().OfType<PropertyDeclarationSyntax>())
            {
                if (comparer.Compare(propDecl.Type?.Identifier, className) == 0)
                {
                    return true;
                }
            }

            // method declaration expressions
            foreach (var methodDecl in node.DescendantNodes().OfType<MethodDeclarationSyntax>())
            {
                if (comparer.Compare(methodDecl.ReturnType?.Identifier, className) == 0)
                {
                    return true;
                }
            }

            // variable declaration expressions
            foreach (var varDecl in node.DescendantNodes().OfType<VariableDeclarationSyntax>())
            {
                if (comparer.Compare(varDecl.Type?.Identifier, className) == 0)
                {
                    return true;
                }
            }

            // variable initialization expressions
            foreach (var varInit in node.DescendantNodes().OfType<VariableDeclaratorSyntax>())
            {
                if (TextReferencesAClass(varInit?.Expression?.ExpressionString, className))
                {
                    return true;
                }
            }

            // expressions
            foreach (var expression in node.DescendantNodes().OfType<ExpressionSyntax>())
            {
                if (TextReferencesAClass(expression.ExpressionString, className))
                {
                    return true;
                }
            }

            // generic-type statements, like expressions
            foreach (var statement in node.DescendantNodes().OfType<StatementSyntax>())
            {
                if (statement.GetType() == typeof(StatementSyntax) &&
                    TextReferencesAClass(statement.Body, className))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
