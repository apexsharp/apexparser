namespace ApexSharpBase.Parser.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ApexSharpBase.MetaClass;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class CSharpParser
    {
        public ClassContainer ParseCSharpFromText(string cSharpCode)
        {
            ClassContainer apexClassDeclarationList = this.ConvertRosyln(cSharpCode);
            return apexClassDeclarationList;
        }

        private ClassContainer ConvertRosyln(string codeText)
        {
            CSharpParseOptions parseOption = new CSharpParseOptions();
            parseOption.WithDocumentationMode(DocumentationMode.Parse);

            var syntaxTree = CSharpSyntaxTree.ParseText(codeText, parseOption);
            var root = syntaxTree.GetRoot();

            var namespaceDeclarationSyntaxSpace = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().First();

            var classContainer = new ClassContainer();
            classContainer.ContainerLang = "C#";

            var syntaxNodes = namespaceDeclarationSyntaxSpace.Members;
            foreach (MemberDeclarationSyntax childNode in syntaxNodes)
            {
                this.SyntaxKindSwitch(classContainer, childNode);
            }
            return classContainer;
        }

        private void SyntaxKindSwitch(BaseSyntax parentNode, SyntaxNode childNode)
        {
            // Console.WriteLine(childNode.Kind());

            switch (childNode.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    parentNode.ChildNodes.Add(this.GetClassDeclaration(childNode));
                    break;
                case SyntaxKind.ConstructorDeclaration:
                    parentNode.ChildNodes.Add(this.GetConstructorDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.MethodDeclaration:
                    parentNode.ChildNodes.Add(this.GetMethodDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.FieldDeclaration:
                    parentNode.ChildNodes.Add(this.GetFieldDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.PropertyDeclaration:
                    parentNode.ChildNodes.Add(this.GetApexPropertyDeclarationSyntax(childNode));
                    break;
                case SyntaxKind.ExpressionStatement:
                    parentNode.ChildNodes.Add(this.GetApexExpressionStatementSyntax(childNode));
                    break;
                case SyntaxKind.LocalDeclarationStatement:
                    parentNode.ChildNodes.Add(this.GetLocalDeclarationStatement(childNode));
                    break;
                case SyntaxKind.ReturnStatement:
                    parentNode.ChildNodes.Add(this.ReturnStatementSyntax(childNode));
                    break;
                case SyntaxKind.IfStatement:
                    parentNode.ChildNodes.Add(this.GetIfElseStatementSyntax(childNode));
                    break;
                case SyntaxKind.ForEachStatement:
                    parentNode.ChildNodes.Add(this.GetForEachStatement(childNode));
                    break;
                case SyntaxKind.ForStatement:
                    parentNode.ChildNodes.Add(this.GetForStatement(childNode));
                    break;

                case SyntaxKind.TryStatement:
                    parentNode.ChildNodes.Add(this.GetApexTryStatementSyntax(childNode));
                    break;

                case SyntaxKind.ThrowStatement:
                    parentNode.ChildNodes.Add(this.GetApexThrownStatementSyntax(childNode));
                    break;
                //        case SyntaxKind.DoStatement:
                //            parentNode.ChildNodes.Add(GetApexDoStatementSyntax(childNode));
                //            break;
                //        case SyntaxKind.WhileStatement:
                //            parentNode.ChildNodes.Add(GetApexWhileStatementSyntax(childNode));
                //            break;
                default:
                    Console.WriteLine("Could Not Support " + childNode.Kind());
                    Console.ReadLine();
                    break;
            }
        }


        private ClassSyntax GetClassDeclaration(SyntaxNode node)
        {
            ClassDeclarationSyntax classDeclarationSyntax = (ClassDeclarationSyntax)node;

            ClassSyntax classSyntax = new ClassSyntax
            {
                CodeComments = GetComments(classDeclarationSyntax.GetLeadingTrivia()),
                Identifier = classDeclarationSyntax.Identifier.Text,
                CodeBlock = classDeclarationSyntax.GetText().ToString()
            };

            classSyntax.Modifiers.AddRange(GetModifiers(classDeclarationSyntax.Modifiers));
            classSyntax.Attributes.AddRange(GetAttribute(classDeclarationSyntax.AttributeLists));

            foreach (MemberDeclarationSyntax childNode in classDeclarationSyntax.Members)
            {
                this.SyntaxKindSwitch(classSyntax, childNode);
            }
            return classSyntax;
        }

        private MethodSyntax GetMethodDeclarationSyntax(SyntaxNode node)
        {
            var method = (MethodDeclarationSyntax)node;

            MethodSyntax methodSyntax = new MethodSyntax
            {
                CodeComments = GetComments(method.GetLeadingTrivia()),
                ReturnType = method.ReturnType.ToString(),
                Identifier = method.Identifier.Text,
                CodeBlock = method.Body.ToString()
            };

            methodSyntax.Attributes.AddRange(GetAttribute(method.AttributeLists));
            methodSyntax.Modifiers.AddRange(GetModifiers(method.Modifiers));
            methodSyntax.Parameters.AddRange(GetParameterSyntaxs(method.ParameterList));

            foreach (SyntaxNode childNode in method.Body.ChildNodes())
            {
                this.SyntaxKindSwitch(methodSyntax, childNode);
            }
            return methodSyntax;
        }

        private IfStatement GetIfElseStatementSyntax(SyntaxNode node)
        {
            var syntax = (IfStatementSyntax)node;

            var apexIfStatementSyntax = new IfStatement
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Condition = syntax.Condition.ToString(),
            };


            foreach (var childNode in syntax.Statement.ChildNodes())
            {
                Console.WriteLine(childNode.Kind());
                this.SyntaxKindSwitch(apexIfStatementSyntax, childNode);
            }



            if (syntax.Else != null)
            {
                var elseSyntax = (ElseClauseSyntax) syntax.Else;

                var apexElseIfStatementSyntax = new ElseStatement
                {
                    CodeComments = GetComments(elseSyntax.GetLeadingTrivia()),
                    Condition = syntax.Statement.ToFullString(),
                };

                foreach (var childNode in syntax.Statement.ChildNodes())
                {
                    Console.WriteLine(childNode.Kind());
                    this.SyntaxKindSwitch(apexIfStatementSyntax, childNode);
                }
                apexIfStatementSyntax.ElseStatement = apexElseIfStatementSyntax;
            }

            return apexIfStatementSyntax;
        }

        private Constructor GetConstructorDeclarationSyntax(SyntaxNode node)
        {
            var constructor = (ConstructorDeclarationSyntax)node;

            Constructor apexConstructor = new Constructor
            {
                CodeComments = GetComments(constructor.GetLeadingTrivia()),
                Identifier = constructor.Identifier.Text,
                CodeBlock = constructor.Body.ToString()
            };

            apexConstructor.Attributes.AddRange(GetAttribute(constructor.AttributeLists));
            apexConstructor.Modifiers.AddRange(GetModifiers(constructor.Modifiers));
            apexConstructor.Parameters.AddRange(GetParameterSyntaxs(constructor.ParameterList));

            foreach (SyntaxNode childNode in constructor.Body.ChildNodes())
            {
                this.SyntaxKindSwitch(apexConstructor, childNode);
            }

            return apexConstructor;
        }

        private Property GetApexPropertyDeclarationSyntax(SyntaxNode node)
        {
            var propertyDeclarationSyntax = (PropertyDeclarationSyntax)node;

            var propertySyntax = new Property
            {
                CodeComments = GetComments(propertyDeclarationSyntax.GetLeadingTrivia()),
                Type = propertyDeclarationSyntax.Type.ToFullString().Trim(),
                Identifier = propertyDeclarationSyntax.Identifier.ToString(),

            };

            propertySyntax.AttributeLists.AddRange(GetAttribute(propertyDeclarationSyntax.AttributeLists));
            propertySyntax.Modifiers.AddRange(GetModifiers(propertyDeclarationSyntax.Modifiers));

            foreach (var accessorDeclarationSyntax in propertyDeclarationSyntax.AccessorList.Accessors)
            {
                var apexAccessor = new AccessorDeclaration
                {
                    Accessor = accessorDeclarationSyntax.Keyword.ToString(),
                };
                propertySyntax.AttributeLists.AddRange(GetAttribute(accessorDeclarationSyntax.AttributeLists));
                apexAccessor.Modifiers.AddRange(GetModifiers(accessorDeclarationSyntax.Modifiers));

                if (accessorDeclarationSyntax.Body != null)
                {
                    SyntaxList<StatementSyntax> statementSyntaxList = accessorDeclarationSyntax.Body.Statements;
                    foreach (var statementSyntax in statementSyntaxList)
                    {
                        this.SyntaxKindSwitch(apexAccessor, statementSyntax);
                    }
                }

                propertySyntax.ChildNodes.Add(apexAccessor);
            }

            return propertySyntax;
        }

        private FieldDeclaration GetFieldDeclarationSyntax(SyntaxNode node)
        {
            var fieldDeclarationSyntax = (FieldDeclarationSyntax)node;
            var fieldDeclaration = new FieldDeclaration
            {
                CodeComments = GetComments(fieldDeclarationSyntax.GetLeadingTrivia()),
                Type = fieldDeclarationSyntax.Declaration.Type.ToFullString().Trim(),
            };

            fieldDeclaration.AttributeLists.AddRange(GetAttribute(fieldDeclarationSyntax.AttributeLists));
            fieldDeclaration.Modifiers.AddRange(GetModifiers(fieldDeclarationSyntax.Modifiers));

            foreach (var variable in fieldDeclarationSyntax.Declaration.Variables)
            {
                fieldDeclaration.IdentifierList.Add(variable.Identifier.Text);
                if (variable.Initializer != null)
                {
                    fieldDeclaration.Initializaer = variable.Initializer.Value.ToFullString();
                }
            }
            return fieldDeclaration;
        }

        private LocalDeclaration GetLocalDeclarationStatement(SyntaxNode node)
        {
            var localDeclarationStatementSyntax = (LocalDeclarationStatementSyntax)node;

            var apexLocalDeclarationStatement = new LocalDeclaration
            {
                CodeComments = GetComments(localDeclarationStatementSyntax.GetLeadingTrivia()),
                Expression = localDeclarationStatementSyntax.Declaration.ToString(),
                LineNumber = localDeclarationStatementSyntax.GetLocation().GetLineSpan().StartLinePosition.Line,
            };
            return apexLocalDeclarationStatement;
        }

        private ExpressionStatement GetApexExpressionStatementSyntax(SyntaxNode node)
        {
            var expressionStatementSyntax = (ExpressionStatementSyntax)node;

            var expressionStatement = new ExpressionStatement
            {
                CodeComments = GetComments(expressionStatementSyntax.GetLeadingTrivia()),
                Expression = expressionStatementSyntax.Expression.ToString(),
                LineNumber = expressionStatementSyntax.GetLocation().GetLineSpan().StartLinePosition.Line,
            };

            return expressionStatement;
        }

        private ReturnStatement ReturnStatementSyntax(SyntaxNode node)
        {
            var expressionStatementSyntax = (ReturnStatementSyntax)node;
            var apexReturnStatementSyntax = new ReturnStatement
            {
                CodeComments = GetComments(expressionStatementSyntax.GetLeadingTrivia()),
                Expression = expressionStatementSyntax.Expression.ToString(),
            };
            return apexReturnStatementSyntax;
        }



        private ForEachStatement GetForEachStatement(SyntaxNode node)
        {
            var syntax = (ForEachStatementSyntax)node;

            var forEachStatement = new ForEachStatement
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Type = syntax.Type.ToString(),
                Identifier = syntax.Identifier.Text,
                Expression = syntax.Expression.ToString(),
            };

            foreach (var childNode in syntax.Statement.ChildNodes())
            {
                this.SyntaxKindSwitch(forEachStatement, childNode);
            }

            return forEachStatement;
        }

        private ForStatement GetForStatement(SyntaxNode node)
        {
            var syntax = (ForStatementSyntax)node;

            var forStatement = new ForStatement
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Declaration = syntax.Declaration.ToString(),
                Condition = syntax.Condition.ToString(),
                Incrementors = syntax.Incrementors.ToString(),
            };

            foreach (var childNode in syntax.Statement.ChildNodes())
            {
                this.SyntaxKindSwitch(forStatement, childNode);
            }
            return forStatement;
        }

        private TryStatement GetApexTryStatementSyntax(SyntaxNode node)
        {
            var tryStatement = (TryStatementSyntax)node;

            TryStatement apex = new TryStatement
            {
                CodeComments = GetComments(tryStatement.GetLeadingTrivia()),
            };

            foreach (var childNode in tryStatement.Block.Statements)
            {
                this.SyntaxKindSwitch(apex, childNode);
            }

            foreach (CatchClauseSyntax catchClauseSyntax in tryStatement.Catches)
            {

                var catchStatement = (CatchClauseSyntax)catchClauseSyntax;
                CatchClause apexCatch = new CatchClause
                {
                    CodeComments = GetComments(catchStatement.GetLeadingTrivia()),
                    Type = catchStatement.Declaration.Type.ToString(),
                    Identifier = catchStatement.Declaration.Identifier.ToString(),
                };

                apex.ChildNodes.Add(apexCatch);
            }

            if (tryStatement.Finally != null)
            {
                FinallyClauseSyntax finallyStatment = tryStatement.Finally;

                var finallyStatement = (FinallyClauseSyntax)finallyStatment;
                FinallyClause apexFinally = new FinallyClause()
                {
                    CodeComments = GetComments(finallyStatement.GetLeadingTrivia()),
                };

                foreach (var childNode in finallyStatement.Block.Statements)
                {
                    this.SyntaxKindSwitch(apex, childNode);
                }

                apex.ChildNodes.Add(apexFinally);
            }

            return apex;
        }

        private ThrownStatement GetApexThrownStatementSyntax(SyntaxNode node)
        {
            var syntax = (ThrowStatementSyntax)node;
            var apex = new ThrownStatement
            {
                CodeComments = GetComments(syntax.GetLeadingTrivia()),
                Expression = syntax.Expression.ToString(),
            };
            return apex;
        }



        //public WhileStatement GetApexWhileStatementSyntax(SyntaxNode node)
        //{
        //    var syntax = (WhileStatementSyntax)node;

        //    WhileStatement apex = new WhileStatement
        //    {
        //        CodeComments = GetComments(syntax.GetLeadingTrivia()),
        //        Condition = syntax.Condition.ToString()
        //    };

        //    if (syntax.Statement.Kind() == SyntaxKind.Block)
        //    {
        //        var blockSyntax = (BlockSyntax)syntax.Statement;
        //        foreach (var childNode in blockSyntax.Statements)
        //        {
        //            SyntaxKindSwitch(apex, childNode);
        //        }
        //    }
        //    else
        //    {
        //        SyntaxKindSwitch(apex, syntax.Statement);
        //    }

        //    return apex;
        //}
        //public DoStatement GetApexDoStatementSyntax(SyntaxNode node)
        //{
        //    var syntax = (DoStatementSyntax)node;

        //    DoStatement apex = new DoStatement
        //    {
        //        CodeComments = GetComments(syntax.GetLeadingTrivia()),
        //        Condition = syntax.Condition.ToString()
        //    };

        //    if (syntax.Statement.Kind() == SyntaxKind.Block)
        //    {
        //        var blockSyntax = (BlockSyntax)syntax.Statement;
        //        foreach (var childNode in blockSyntax.Statements)
        //        {
        //            SyntaxKindSwitch(apex, childNode);
        //        }
        //    }
        //    else
        //    {
        //        SyntaxKindSwitch(apex, syntax.Statement);
        //    }

        //    return apex;
        //}



        private static List<MetaClass.Parameter> GetParameterSyntaxs(ParameterListSyntax parameter)
        {
            List<MetaClass.Parameter> parameterSyntaxs = new List<MetaClass.Parameter>();

            foreach (var parameterSyntax in parameter.Parameters)
            {
                var apexParamter = new MetaClass.Parameter
                {
                    Type = parameterSyntax.Type.ToString(),
                    Identifier = parameterSyntax.Identifier.Text,
                };


                parameterSyntaxs.Add(apexParamter);
            }
            return parameterSyntaxs;
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
