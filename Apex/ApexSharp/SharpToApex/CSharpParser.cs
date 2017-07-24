using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apex.ApexSharp.MetaClass;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Apex.ApexSharp.SharpToApex
{
    public class CSharpParser
    {
        public ApexClassDeclarationSyntax ParseCSharpFromText(string apexFile)
        {
            CSharpParser cSharpParser = new CSharpParser();
            ApexClassDeclarationSyntax apexClassDeclarationSyntaxList = cSharpParser.ConvertRosyln(apexFile);

            return apexClassDeclarationSyntaxList;
        }

        public ApexClassDeclarationSyntax ParseCSharpFromFile(FileInfo apexFile)
        {
            var codeText = File.ReadAllText(apexFile.FullName);

            CSharpParser cSharpParser = new CSharpParser();
            ApexClassDeclarationSyntax apexClassDeclarationSyntaxList = cSharpParser.ConvertRosyln(codeText);

            return apexClassDeclarationSyntaxList;
        }

        private ApexClassDeclarationSyntax ConvertRosyln(string codeText)
        {
            CSharpParseOptions parseOption = new CSharpParseOptions();
            parseOption.WithDocumentationMode(DocumentationMode.Parse);

            var syntaxTree = CSharpSyntaxTree.ParseText(codeText, parseOption);
            var root = syntaxTree.GetRoot();

            var classNameSpace = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().First();
            var myClasss = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            ApexClassDeclarationSyntax apexClassDeclarationSyntax = GetApexClass(classNameSpace, myClasss);
            return apexClassDeclarationSyntax;
        }

        private ApexClassDeclarationSyntax GetApexClass(NamespaceDeclarationSyntax namespaceDeclarationSyntax,
            ClassDeclarationSyntax classDeclarationSyntax)
        {
            ApexClassDeclarationSyntax apexClass = new ApexClassDeclarationSyntax
            {
                CodeComments = GetComments(classDeclarationSyntax.GetLeadingTrivia()),
                Identifier = classDeclarationSyntax.Identifier.Text,
                NameSpace = namespaceDeclarationSyntax.Name.ToString()
            };

            apexClass.Modifiers.AddRange(GetModifiers(classDeclarationSyntax.Modifiers));

            foreach (var methodAttributeList in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in methodAttributeList.Attributes)
                {
                    if (attributeSyntax.ToString() == "ApexWithSharing")
                    {
                        apexClass.IsShareing = "YES";
                    }

                    else if (attributeSyntax.ToString() == "ApexWithOutSharing")
                    {
                        apexClass.IsShareing = "NO";
                    }

                    else if (attributeSyntax.ToString() == "TestFixture")
                    {
                        apexClass.AttributeLists.Add("@isTest");
                    }
                }
            }

            foreach (MemberDeclarationSyntax childNode in classDeclarationSyntax.Members)
            {
                SyntaxKindSwitch(apexClass, childNode);
            }
            return apexClass;
        }

        private void SyntaxKindSwitch(ApexSyntaxNode parentNode, SyntaxNode childNode)
        {
            switch (childNode.Kind())
            {
                case SyntaxKind.ExpressionStatement:
                    parentNode.ChildNodes.Add(GetApexExpressionStatementSyntax(childNode));
                    break;
                case SyntaxKind.LocalDeclarationStatement:
                    parentNode.ChildNodes.Add(GetApexLocalDeclarationStatement(childNode));
                    break;
                case SyntaxKind.ReturnStatement:
                    parentNode.ChildNodes.Add(ApexReturnStatementSyntax(childNode));
                    break;
                case SyntaxKind.ForEachStatement:
                    parentNode.ChildNodes.Add(GetApexForEachStatementSyntax(childNode));
                    break;
                case SyntaxKind.FieldDeclaration:
                    parentNode.ChildNodes.Add(GetFieldDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.PropertyDeclaration:
                    parentNode.ChildNodes.Add(GetApexPropertyDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.InvocationExpression:
                    parentNode.ChildNodes.Add(GetApexInvocationExpression(childNode));
                    break;
                case SyntaxKind.ConstructorDeclaration:
                    parentNode.ChildNodes.Add(GetConstructorDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.MethodDeclaration:
                    parentNode.ChildNodes.Add(GetMethodDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.ForStatement:
                    parentNode.ChildNodes.Add(GetApexForStatementSyntax(childNode));
                    break;
                case SyntaxKind.IfStatement:
                    parentNode.ChildNodes.Add(GetApexIfStatementSyntax(childNode));
                    break;
                case SyntaxKind.ElseClause:
                    parentNode.ChildNodes.Add(GetApexElseClauseSyntax(childNode));
                    break;
                case SyntaxKind.TryStatement:
                    parentNode.ChildNodes.Add(GetApexTryStatementSyntax(childNode));
                    break;
                case SyntaxKind.CatchClause:
                    parentNode.ChildNodes.Add(GetApexCatchClauseSyntax(childNode));
                    break;
                case SyntaxKind.FinallyClause:
                    parentNode.ChildNodes.Add(GetApexFinallyClauseSyntax(childNode));
                    break;
                case SyntaxKind.ThrowStatement:
                    parentNode.ChildNodes.Add(GetApexThrownStatementSyntax(childNode));
                    break;
                case SyntaxKind.DoStatement:
                    parentNode.ChildNodes.Add(GetApexDoStatementSyntax(childNode));
                    break;
                case SyntaxKind.WhileStatement:
                    parentNode.ChildNodes.Add(GetApexWhileStatementSyntax(childNode));
                    break;
                default:
                    Console.WriteLine("Could Not Support " + childNode.Kind());
                    Console.ReadLine();
                    break;
            }
        }

        public ApexDoStatementSyntax GetApexDoStatementSyntax(SyntaxNode node)
        {
            var syntax = (DoStatementSyntax)node;

            ApexDoStatementSyntax apex = new ApexDoStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Condition = syntax.Condition.ToString()
            };

            if (syntax.Statement.Kind() == SyntaxKind.Block)
            {
                var blockSyntax = (BlockSyntax)syntax.Statement;
                foreach (var childNode in blockSyntax.Statements)
                {
                    SyntaxKindSwitch(apex, childNode);
                }
            }
            else
            {
                SyntaxKindSwitch(apex, syntax.Statement);
            }

            return apex;
        }


        public ApexWhileStatementSyntax GetApexWhileStatementSyntax(SyntaxNode node)
        {
            var syntax = (WhileStatementSyntax)node;

            ApexWhileStatementSyntax apex = new ApexWhileStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Condition = syntax.Condition.ToString()
            };

            if (syntax.Statement.Kind() == SyntaxKind.Block)
            {
                var blockSyntax = (BlockSyntax)syntax.Statement;
                foreach (var childNode in blockSyntax.Statements)
                {
                    SyntaxKindSwitch(apex, childNode);
                }
            }
            else
            {
                SyntaxKindSwitch(apex, syntax.Statement);
            }

            return apex;
        }

        public ApexTryStatementSyntax GetApexTryStatementSyntax(SyntaxNode node)
        {
            var tryStatement = (TryStatementSyntax)node;

            ApexTryStatementSyntax apex = new ApexTryStatementSyntax
            {
                CodeComments = GetComments(tryStatement.GetLeadingTrivia())
            };

            foreach (var childNode in tryStatement.Block.Statements)
            {
                SyntaxKindSwitch(apex, childNode);
            }

            foreach (CatchClauseSyntax catchClauseSyntax in tryStatement.Catches)
            {
                SyntaxKindSwitch(apex, catchClauseSyntax);
            }

            if (tryStatement.Finally != null)
            {
                FinallyClauseSyntax finallyStatment = tryStatement.Finally;
                SyntaxKindSwitch(apex, finallyStatment);
            }

            return apex;
        }


        public ApexCatchClauseSyntax GetApexCatchClauseSyntax(SyntaxNode node)
        {
            var catchStatement = (CatchClauseSyntax)node;
            ApexCatchClauseSyntax apex = new ApexCatchClauseSyntax
            {
                CodeComments = GetComments(catchStatement.GetLeadingTrivia()),
                Type = catchStatement.Declaration.Type.ToString(),
                Identifier = catchStatement.Declaration.Identifier.ToString()
            };

            foreach (var childNode in catchStatement.Block.Statements)
            {
                SyntaxKindSwitch(apex, childNode);
            }
            return apex;
        }

        public ApexFinallyClauseSyntax GetApexFinallyClauseSyntax(SyntaxNode node)
        {
            var finallyStatement = (FinallyClauseSyntax)node;
            ApexFinallyClauseSyntax apex = new ApexFinallyClauseSyntax()
            {
                CodeComments = GetComments(finallyStatement.GetLeadingTrivia()),
            };

            foreach (var childNode in finallyStatement.Block.Statements)
            {
                SyntaxKindSwitch(apex, childNode);
            }
            return apex;
        }

        private ApexThrownStatementSyntax GetApexThrownStatementSyntax(SyntaxNode node)
        {
            var syntax = (ThrowStatementSyntax)node;
            var apex = new ApexThrownStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Expression = syntax.Expression.ToString()
            };
            return apex;
        }


        private ApexInvocationExpression GetApexInvocationExpression(SyntaxNode node)
        {
            var syntax = (InvocationExpressionSyntax)node;
            var apex = new ApexInvocationExpression
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Expression = syntax.Expression.ToString(),
                Argument = syntax.ArgumentList.ToString()
            };
            return apex;
        }

        private ApexConstructor GetConstructorDeclarationSyntax(SyntaxNode node)
        {
            var constructor = (ConstructorDeclarationSyntax)node;

            ApexConstructor apexConstructor = new ApexConstructor
            {
                CodeComments = GetComments(constructor.GetLeadingTrivia()),
                Identifier = constructor.Identifier.Text
            };
            apexConstructor.AttributeLists.AddRange(GetAttribute(constructor.AttributeLists));
            apexConstructor.Modifiers.AddRange(GetModifiers(constructor.Modifiers));

            apexConstructor.ApexParameters.AddRange(ApexParameterListSyntaxList(constructor.ParameterList));

            foreach (SyntaxNode childNode in constructor.Body.ChildNodes())
            {
                SyntaxKindSwitch(apexConstructor, childNode);
            }
            return apexConstructor;
        }

        private ApexMethodDeclarationSyntax GetMethodDeclarationSyntax(SyntaxNode node)
        {
            var method = (MethodDeclarationSyntax)node;

            ApexMethodDeclarationSyntax apexMethodDeclarationSyntax = new ApexMethodDeclarationSyntax
            {
                CodeComments = GetComments(method.GetLeadingTrivia()),
                ReturnType = method.ReturnType.ToString(),
                Identifier = method.Identifier.Text,
            };

            apexMethodDeclarationSyntax.AttributeLists.AddRange(GetAttribute(method.AttributeLists));
            apexMethodDeclarationSyntax.Modifiers.AddRange(GetModifiers(method.Modifiers));
            apexMethodDeclarationSyntax.ApexParameters.AddRange(ApexParameterListSyntaxList(method.ParameterList));

            foreach (SyntaxNode childNode in method.Body.ChildNodes())
            {
                SyntaxKindSwitch(apexMethodDeclarationSyntax, childNode);
            }
            return apexMethodDeclarationSyntax;
        }

        private ApexIfStatementSyntax GetApexIfStatementSyntax(SyntaxNode node)
        {
            var syntax = (IfStatementSyntax)node;

            var apexIfStatementSyntax = new ApexIfStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Condition = syntax.Condition.ToString()
            };

            // Code Block or Just One Line
            if (syntax.Statement.Kind() == SyntaxKind.Block)
            {
                var blockSyntax = (BlockSyntax)syntax.Statement;
                foreach (var childNode in blockSyntax.Statements)
                {
                    SyntaxKindSwitch(apexIfStatementSyntax, childNode);
                }
            }
            else
            {
                SyntaxKindSwitch(apexIfStatementSyntax, syntax.Statement);
            }

            if (syntax.Else != null) apexIfStatementSyntax.ElseStatementSyntax = GetApexElseClauseSyntax(syntax.Else);

            return apexIfStatementSyntax;
        }

        private ApexElseStatementSyntax GetApexElseClauseSyntax(SyntaxNode node)
        {
            var syntax = (ElseClauseSyntax)node;

            var apexElseIfStatementSyntax = new ApexElseStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
            };

            if (syntax.Statement.Kind() == SyntaxKind.Block)
            {
                var blockSyntax = (BlockSyntax)syntax.Statement;
                foreach (var childNode in blockSyntax.Statements)
                {
                    SyntaxKindSwitch(apexElseIfStatementSyntax, childNode);
                }
            }
            else
            {
                SyntaxKindSwitch(apexElseIfStatementSyntax, syntax.Statement);
            }
            return apexElseIfStatementSyntax;
        }


        private ApexForStatementSyntax GetApexForStatementSyntax(SyntaxNode node)
        {
            var syntax = (ForStatementSyntax)node;

            var apexForStatementSyntax = new ApexForStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Declaration = syntax.Declaration.ToString(),
                Condition = syntax.Condition.ToString(),
                Incrementors = syntax.Incrementors.ToString()
            };

            foreach (var childNode in syntax.Statement.ChildNodes())
            {
                SyntaxKindSwitch(apexForStatementSyntax, childNode);
            }
            return apexForStatementSyntax;
        }

        private ApexForEachStatementSyntax GetApexForEachStatementSyntax(SyntaxNode node)
        {
            var syntax = (ForEachStatementSyntax)node;

            var apexForEachStatementSyntax = new ApexForEachStatementSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Type = syntax.Type.ToString(),
                Identifier = syntax.Identifier.Text,
                Expression = syntax.Expression.ToString()
            };

            foreach (var childNode in syntax.Statement.ChildNodes())
            {
                SyntaxKindSwitch(apexForEachStatementSyntax, childNode);
            }

            return apexForEachStatementSyntax;
        }

        private ApexReturnStatementSyntax ApexReturnStatementSyntax(SyntaxNode node)
        {
            var expressionStatementSyntax = (ReturnStatementSyntax)node;
            var apexReturnStatementSyntax = new ApexReturnStatementSyntax
            {
                CodeComments = GetComments(expressionStatementSyntax.GetLeadingTrivia()),
                Expression = expressionStatementSyntax.Expression.ToString()
            };
            return apexReturnStatementSyntax;
        }

        private ApexExpressionStatementSyntax GetApexExpressionStatementSyntax(SyntaxNode node)
        {
            var expressionStatementSyntax = (ExpressionStatementSyntax)node;

            var apexExpressionStatementSyntax = new ApexExpressionStatementSyntax
            {
                CodeComments = GetComments(expressionStatementSyntax.GetLeadingTrivia()),
                Expression = expressionStatementSyntax.Expression.ToString()
            };

            if (apexExpressionStatementSyntax.Expression.Contains("Soql"))
            {
                var demo = apexExpressionStatementSyntax;
            }


            return apexExpressionStatementSyntax;
        }

        private ApexLocalDeclarationStatementSyntax GetApexLocalDeclarationStatement(SyntaxNode node)
        {
            var localDeclarationStatementSyntax = (LocalDeclarationStatementSyntax)node;

            var apexLocalDeclarationStatement = new ApexLocalDeclarationStatementSyntax
            {
                CodeComments = GetComments(localDeclarationStatementSyntax.GetLeadingTrivia()),
                Expression = localDeclarationStatementSyntax.Declaration.ToString(),
                LineNumber = localDeclarationStatementSyntax.GetLocation().GetLineSpan().StartLinePosition.Line
            };

            if (apexLocalDeclarationStatement.Expression.Contains("Soql"))
            {
                var nodes = localDeclarationStatementSyntax.DescendantNodes().ToList();
                var arg = localDeclarationStatementSyntax.DescendantNodes().OfType<ArgumentSyntax>().First();

                apexLocalDeclarationStatement.Soql = arg.ToFullString();
                apexLocalDeclarationStatement.Soql = apexLocalDeclarationStatement.Soql.Substring(1);
                apexLocalDeclarationStatement.Soql = apexLocalDeclarationStatement.Soql.TrimEnd('"');
            }
            return apexLocalDeclarationStatement;
        }

        private ApexPropertyDeclarationSyntax GetApexPropertyDeclarationSyntax(SyntaxNode node)
        {
            var syntax = (PropertyDeclarationSyntax)node;

            var apexProperty = new ApexPropertyDeclarationSyntax
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Type = syntax.Type.ToFullString().Trim(),
                Identifier = syntax.Identifier.ToString(),
            };

            apexProperty.AttributeLists.AddRange(GetAttribute(syntax.AttributeLists));
            apexProperty.Modifiers.AddRange(GetModifiers(syntax.Modifiers));

            foreach (var accessorDeclarationSyntax in syntax.AccessorList.Accessors)
            {
                var apexAccessor = new ApexAccessorDeclarationSyntax
                {
                    Accessor = accessorDeclarationSyntax.Keyword.ToString()
                };
                apexProperty.AttributeLists.AddRange(GetAttribute(accessorDeclarationSyntax.AttributeLists));
                apexAccessor.Modifiers.AddRange(GetModifiers(accessorDeclarationSyntax.Modifiers));

                if (accessorDeclarationSyntax.Body != null)
                {
                    SyntaxList<StatementSyntax> statementSyntaxList = accessorDeclarationSyntax.Body.Statements;
                    foreach (var statementSyntax in statementSyntaxList)
                    {
                        SyntaxKindSwitch(apexAccessor, statementSyntax);
                    }
                }

                apexProperty.ChildNodes.Add(apexAccessor);
            }
            return apexProperty;
        }

        private ApexFieldDeclarationSyntax GetFieldDeclarationSyntax(SyntaxNode node)
        {
            var fieldDeclarationSyntax = (FieldDeclarationSyntax)node;

            var apex = new ApexFieldDeclarationSyntax
            {
                CodeComments = GetComments(fieldDeclarationSyntax.GetLeadingTrivia()),
                Type = fieldDeclarationSyntax.Declaration.Type.ToFullString().Trim(),
            };

            apex.AttributeLists.AddRange(GetAttribute(fieldDeclarationSyntax.AttributeLists));
            apex.Modifiers.AddRange(GetModifiers(fieldDeclarationSyntax.Modifiers));

            foreach (var variable in fieldDeclarationSyntax.Declaration.Variables)
            {
                apex.IdentifierList.Add(variable.Identifier.Text);
                if (variable.Initializer != null)
                {
                    apex.Initializaer = variable.Initializer.Value.ToFullString();
                }
            }
            return apex;
        }


        private static List<ApexParameterListSyntax> ApexParameterListSyntaxList(ParameterListSyntax parameter)
        {
            List<ApexParameterListSyntax> apexParameters = new List<ApexParameterListSyntax>();

            foreach (var parameterSyntax in parameter.Parameters)
            {
                var apexParamter = new ApexParameterListSyntax
                {
                    Type = parameterSyntax.Type.ToString(),
                    Identifier = parameterSyntax.Identifier.Text
                };

                if (parameterSyntax.Type.Kind() == SyntaxKind.GenericName)
                {
                    var genericNameSyntax = (GenericNameSyntax)parameterSyntax.Type;
                    apexParamter.IsGneric = true;
                }

                apexParameters.Add(apexParamter);
            }
            return apexParameters;
        }

        private static List<string> GetAttribute(SyntaxList<AttributeListSyntax> attributeList)
        {
            List<string> attributes = new List<string>();
            foreach (var methodAttributeList in attributeList)
            {
                foreach (var attributeSyntax in methodAttributeList.Attributes)
                {
                    attributes.Add(attributeSyntax.ToString());
                }
            }
            return attributes;
        }

        private static List<string> GetModifiers(SyntaxTokenList syntaxToken)
        {
            List<string> modifiers = new List<string>();

            foreach (var token in syntaxToken)
            {
                modifiers.Add(token.Text);
            }
            return modifiers;
        }

        private static List<string> GetComments(SyntaxTriviaList commentList)
        {
            List<string> sb = new List<string>();
            foreach (var commentNode in commentList)
            {
                if (commentNode.Kind() == SyntaxKind.MultiLineCommentTrivia)
                {
                    var multiLineCommentList = commentNode.ToString()
                        .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var comment in multiLineCommentList)
                    {
                        sb.Add(comment.Trim());
                    }
                }
                else if (commentNode.Kind() == SyntaxKind.SingleLineCommentTrivia)
                {
                    sb.Add(commentNode.ToString().Trim());
                }
            }

            return sb;
        }
    }
}