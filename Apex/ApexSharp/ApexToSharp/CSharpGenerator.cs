using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Apex.ApexSharp.MetaClass;
using Apex.ApexSharp.Util;

namespace Apex.ApexSharp.ApexToSharp
{
    // Generate CSharpCode
    public class CSharpGenerator
    {
        private readonly StringBuilder apex = new StringBuilder();

        public string GetCSharpCode(List<string> nameSpace, ApexClassDeclarationSyntax rootApexClass)
        {
            StringBuilder cSharp = new StringBuilder();

            //   if (rootApexClass.GetCSharpDirName(nameSpace).Length != 0) cSharp.AppendLine("namespace Demo.ApexCode." + rootApexClass.GetCSharpDirName(nameSpace));
            cSharp.AppendLine("namespace Demo.ApexCode");

            cSharp.AppendLine("{");
            cSharp.AppendLine("using Apex;");
            cSharp.AppendLine("using DbModel;");
            cSharp.AppendLine("using Apex.System;");
            cSharp.AppendLine("using NUnit.Framework;");

            cSharp.AppendLine();

            cSharp.Append(GetCSharpCodeInternal(rootApexClass));

            cSharp.AppendLine("}");


            return cSharp.ToString();
        }

        private string GetCSharpCodeInternal(ApexSyntaxNode syntax)
        {
            if (syntax.CommentMustGo != null) apex.AppendLine(syntax.CommentMustGo);

            apex.AppendLine("// " + syntax.Kind());

            switch (syntax.Kind())
            {
                case ApexType.ApexClassDeclarationSyntax:
                    var apexClass = (ApexClassDeclarationSyntax) syntax;

                    foreach (var attribute in apexClass.AttributeLists)
                    {
                        apex.AppendLine(AttributeCreater(attribute));
                    }
                    apex.Append(ModifierCreater(apexClass.Modifiers) + " class " + apexClass.Identifier);

                    if (apexClass.Extending != null) apex.Append(" : ").Append((string) apexClass.Extending);

                    if (apexClass.Implementing.Count > 0)
                    {
                        foreach (var implementing in apexClass.Implementing)
                        {
                            apex.Append(", ").Append((string) implementing);
                        }
                    }

                    apex.AppendLine("{");
                    AppendBody(apexClass.ChildNodes);
                    apex.AppendLine("}");

                    break;
                case ApexType.ApexConstructorDeclarationSyntax:
                    //var apexConstructor = (ApexConstructor)syntax;

                    //apex.Append(ModifierCreater(apexConstructor.Modifiers)).AppendSpace().Append(apexConstructor.Identifier);
                    //apex.Append("(" + ParameterCreater(apexConstructor.ApexParameters) + ")").AppendLine("{");

                    //AppendBody(apexConstructor.ChildNodes);

                    //apex.AppendLine("}");
                    break;
                case ApexType.ApexMethodDeclarationSyntax:
                    var apexMethod = (ApexMethodDeclarationSyntax) syntax;

                    foreach (var attribute in apexMethod.AttributeLists)
                    {
                        apex.AppendLine(AttributeCreater(attribute));
                    }

                    var mod = ModifierCreater(apexMethod.Modifiers);
                    apex.Append(mod).AppendSpace()
                        .Append(NameSpaceManger(DoTypeSwitch(apexMethod.ReturnType))).AppendSpace()
                        .Append(apexMethod.Identifier)
                        .Append("(")
                        .Append(ParameterCreater(apexMethod.ApexParameters))
                        .Append(")")
                        .AppendLine("{");

                    AppendBody(apexMethod.ChildNodes);

                    apex.AppendLine("}");
                    break;
                case ApexType.ApexPropertyDeclarationSyntax:
                    var apexProperty = (ApexPropertyDeclarationSyntax) syntax;
                    apex.Append(ModifierCreater(apexProperty.Modifiers)).AppendSpace();

                    var cSharpCodeProperty = GetCSharpLine(apexProperty.ApexTockens);
                    apex.AppendLine(cSharpCodeProperty);

                    AppendBody(apexProperty.ChildNodes);

                    apex.AppendLine("}");
                    break;
                // Get, Set
                case ApexType.ApexAccessorDeclarationSyntax:
                    var accessor = (ApexAccessorDeclarationSyntax) syntax;
                    apex.Append(ModifierCreater(accessor.Modifiers)).AppendSpace();
                    apex.Append((string) accessor.Accessor);

                    if (accessor.ContainsChildren)
                    {
                        apex.AppendLine("{");
                        AppendBody(accessor.ChildNodes);
                        apex.AppendLine("}");
                    }
                    else
                    {
                        apex.AppendLine(";");
                    }

                    break;
                case ApexType.ApexForEachStatementSyntax:
                    var apexEach = (ApexForEachStatementSyntax) syntax;
                    var forEachStatement = ForEach(apexEach.ApexTockens);
                    apex.AppendLine(forEachStatement);
                    AppendBody(apexEach.ChildNodes);
                    apex.AppendLine("}");
                    break;
                case ApexType.ApexForStatementSyntax:
                    var apexFor = (ApexForStatementSyntax) syntax;
                    var forStatement = GetCSharpLine(apexFor.ApexTockens);
                    apex.AppendLine(forStatement);
                    AppendBody(apexFor.ChildNodes);
                    apex.AppendLine("}");
                    break;
                case ApexType.ApexIfStatementSyntax:
                    var apexIf = (ApexIfStatementSyntax) syntax;
                    var ifStatement = GetCSharpLine(apexIf.ApexTockens);
                    apex.AppendLine(ifStatement);
                    AppendBody(apexIf.ChildNodes);
                    apex.AppendLine("}");
                    break;

                case ApexType.ApexFieldDeclarationSyntax:
                    var property = (ApexFieldDeclarationSyntax) syntax;
                    apex.Append(ModifierCreater(property.Modifiers)).AppendSpace();
                    var cSharpCodeField = GetCSharpLine(property.ApexTockens);
                    apex.AppendLine(cSharpCodeField);
                    break;
                case ApexType.ApexExpressionStatementSyntax:
                    var expression = (ApexExpressionStatementSyntax) syntax;
                    var cSharpCodeExpression = GetCSharpLine(expression.ApexTockens);
                    apex.AppendLine(cSharpCodeExpression);
                    break;
                case ApexType.ApexLocalDeclarationStatementSyntax:
                    var apexStatement = (ApexLocalDeclarationStatementSyntax) syntax;
                    var cSharpCodeStatement = GetCSharpLine(apexStatement.ApexTockens);
                    apex.AppendLine(cSharpCodeStatement);
                    break;
                case ApexType.ApexReturnStatementSyntax:
                    var returnExpression = (ApexReturnStatementSyntax) syntax;
                    var returnStatement = GetCSharpLine(returnExpression.ApexTockens);
                    apex.AppendLine(returnStatement);
                    break;
                case ApexType.Soql:
                    var apexSoql = (SoqlExpression) syntax;
                    var soqlStatement = GetCSharpLine(apexSoql.ApexTockens);
                    apex.AppendLine(soqlStatement);
                    break;
                case ApexType.Dml:
                    var apexDml = (DmlExpression) syntax;
                    var dmlStatement = GetCSharpLine(apexDml.ApexTockens);
                    apex.AppendLine(dmlStatement);
                    break;
            }

            return apex.ToString();
        }

        public void AppendBody(List<ApexSyntaxNode> syntaxNodes)
        {
            foreach (ApexSyntaxNode apexClassChildNode in syntaxNodes)
            {
                GetCSharpCodeInternal(apexClassChildNode);
            }
        }


        public static string AttributeCreater(string attribute)
        {
            // @RestResource(urlMapping='/api/v1/restdemo')
            if (attribute.ToLower() == "@istest") return "[TestFixture]";
            if (attribute.ToLower() == "@httpget") return "[ApexHttpPut]";
            if (attribute.ToLower().Contains("restresource"))
            {
                var startIndex = attribute.IndexOf('\'');
                var endIndex = attribute.LastIndexOf('\'');
                var url = attribute.Slice(startIndex + 1, endIndex);
                return "[ApexRest(\"" + url + "\")]";
            }

            return attribute;
        }

        public static string ModifierCreater(List<string> apexModifierList)
        {
            StringBuilder sb = new StringBuilder();

            if (apexModifierList.Count > 0)
            {
                if (apexModifierList[0] == "global")
                {
                    sb.AppendLine("[ApexGlobel]");
                    sb.Append("public");
                }
                else if (apexModifierList[0] == "global static")
                {
                    sb.AppendLine("[ApexGlobel]");
                    sb.Append("public static");
                }
                else
                {
                    sb.Append(apexModifierList[0]);
                }

                if (apexModifierList.Count() == 2)
                {
                    if (apexModifierList[1] == "final")
                    {
                        sb.Append(" readonly");
                    }
                    else
                    {
                        sb.AppendSpace().Append(apexModifierList[1]);
                    }
                }
            }
            return sb.ToString();
        }

        public static string ParameterCreater(List<ApexParameterListSyntax> apexParameters)
        {
            var sb = new StringBuilder();
            var parameters = new List<string>();

            foreach (var apexParameter in apexParameters)
            {
                parameters.Add((apexParameter.Type) + " " + apexParameter.Identifier);
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                if (i == 0) sb.Append(parameters[i]);
                else sb.Append(", ").Append(parameters[i]);
            }
            return sb.ToString().Trim();
        }

        public string GetCSharpLine(List<ApexTocken> apexTokenList)
        {
            // Remove any Accesmodifers and Attrubutes
            var apexTokens = apexTokenList
                .Where(x => x.TockenType != TockenType.AccessModifier && x.TockenType != TockenType.Attrubute).ToList();

            var apexTokenClassNameList =
                apexTokens.FindAll(x => x.TockenType == TockenType.ClassName ||
                                        x.TockenType == TockenType.ClassNameGeneric ||
                                        x.TockenType == TockenType.ClassNameArray ||
                                        x.TockenType == TockenType.ClassNameArraySize);
            foreach (var apexTokenClassName in apexTokenClassNameList)
            {
                apexTokenClassName.Tocken = DoTypeSwitch(apexTokenClassName.Tocken);
            }

            var apexQuotedStrings = apexTokenList.FindAll(x => x.TockenType == TockenType.QuotedString);
            foreach (var apexQuotedString in apexQuotedStrings)
            {
                apexQuotedString.Tocken = apexQuotedString.Tocken.Replace('\'', '"');
            }

            if (apexTokenList.FindIndex(x => x.TockenType == TockenType.Soql) >= 0)
                return NameSpaceManger(Soql(apexTokenList));
            else if (apexTokenList.FindIndex(x => x.TockenType == TockenType.DML) >= 0)
                return NameSpaceManger(Update(apexTokenList));
            else if (apexTokenList.FindIndex(x => x.TockenType == TockenType.JsonDeserialize) >= 0)
                return NameSpaceManger(JsonDeSerialize(apexTokenList));


            var apexCode = ApexFormater.PrintCleanLine(apexTokens);


            return NameSpaceManger(apexCode);
        }

        public string NameSpaceManger(string apexCode)
        {
            List<string> nameSpaceList = new List<string> {"RestDto"};
            foreach (var nameSpace in nameSpaceList)
            {
                return apexCode.Replace(nameSpace, nameSpace + ".");
            }
            return String.Empty;
        }

        public string DoTypeSwitch(string cSharpType)
        {
            cSharpType = cSharpType.Replace("Integer", "int");
            cSharpType = cSharpType.Replace("String", "string");
            cSharpType = cSharpType.Replace("final", "readonly");
            return cSharpType;
        }

        private static string ForEach(List<ApexTocken> apexTockens)
        {
            var sb = new StringBuilder();
            sb.Append("foreach(")
                .Append(GetToken(TockenType.ClassName, apexTockens)).AppendSpace()
                .Append(GetToken(TockenType.Word, apexTockens))
                .Append(" in ")
                .Append(GetSecondToken(TockenType.Word, apexTockens))
                .AppendLine("){");
            return sb.ToString();
        }

        private static string Soql(List<ApexTocken> apexTockens)
        {
            var sb = new StringBuilder();

            sb.Append(GetToken(TockenType.ClassName, apexTockens));
            sb.Append(GetToken(TockenType.ClassNameGeneric, apexTockens));
            sb.AppendSpace().Append(GetToken(TockenType.Word, apexTockens));
            sb.AppendSpace().Append(GetToken(TockenType.Equal, apexTockens));
            sb.AppendSpace().Append("Soql.Select");

            var className = GetToken(TockenType.ClassNameGeneric, apexTockens);
            className = className?.Replace("List", "");
            sb.Append(className);

            var soql = GetToken(TockenType.Soql, apexTockens);

            Regex regex = new Regex(@":[A-Za-z.]*");
            foreach (global::System.Text.RegularExpressions.Match match in regex.Matches(soql))
            {
                var newValue = match.Value.Substring(1);
                newValue = "\" + " + newValue;

                soql = soql.Replace(match.Value, newValue);
            }

            soql = soql.Replace("[", "(\"");
            soql = soql.Replace("]", ")");

            sb.Append(soql).Append(";");

            return sb.ToString();
        }

        // RestDtoUpdateUserNameRequest dto =(RestDtoUpdateUserNameRequest) JSON.deserialize(requestBody , RestDtoUpdateUserNameRequest .class);
        // RestDtoUpdateUserNameRequest dto = JSON.deserialize<RestDtoUpdateUserNameRequest>(requestBody);
        public static string JsonDeSerialize(List<ApexTocken> apexTockens)
        {
            var sb = new StringBuilder();
            sb.Append(GetToken(TockenType.ClassName, apexTockens)).AppendSpace();
            sb.Append(GetToken(TockenType.Word, apexTockens));
            sb.Append(GetToken(TockenType.Equal, apexTockens));
            sb.Append("JSON.deserialize<");
            sb.Append(GetToken(TockenType.ClassName, apexTockens));
            sb.Append(">(");
            sb.Append(GetSecondToken(TockenType.Word, apexTockens));
            sb.Append(");");
            return sb.ToString();
        }


        private static string Update(List<ApexTocken> apexTockens)
        {
            var sb = new StringBuilder();
            sb.Append("Soql.Update(").Append(GetToken(TockenType.Word, apexTockens)).Append(");");
            return sb.ToString();
        }

        private static string GetToken(TockenType tokenType, List<ApexTocken> apexTockens)
        {
            var token = apexTockens.FirstOrDefault(x => x.TockenType == tokenType);
            return token?.Tocken;
        }

        private static string GetSecondToken(TockenType tokenType, List<ApexTocken> apexTockens)
        {
            var tokenList = apexTockens.Where(x => x.TockenType == tokenType).ToList();
            return tokenList.Count > 1 ? tokenList[1].Tocken : null;
        }

        private static bool IsExists(TockenType tokenType, List<ApexTocken> apexTockens)
        {
            var apexTocken = apexTockens.Find(x => x.TockenType == tokenType);
            return apexTocken != null;
        }
    }
}