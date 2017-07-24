using System;
using System.Collections.Generic;
using System.Text;
using Apex.ApexSharp.MetaClass;
using Apex.ApexSharp.Util;

namespace Apex.ApexSharp.SharpToApex
{
    public class ApexGenerator
    {
        public StringBuilder apex = new StringBuilder();

        public void GetApexCode(ApexSyntaxNode syntax)
        {
            //apex.AppendLine("//:: " + syntax.Kind());

            switch (syntax.Kind())
            {
                case ApexType.ApexConstructorDeclarationSyntax:
                    var apexConstructor = (ApexConstructor) syntax;

                    apex.Append(ApexGeneratorUtil.ModifierAttributeCreater(apexConstructor.AttributeLists))
                        .AppendSpace().Append(apexConstructor.Identifier);
                    apex.Append("(" + ParameterCreater(apexConstructor.ApexParameters) + ")").AppendLine("{");

                    AppendBody(apexConstructor.ChildNodes);

                    apex.AppendLine("}");
                    break;


                // get; set;
                case ApexType.ApexAccessorDeclarationSyntax:
                    var accessorDeclaration = (ApexAccessorDeclarationSyntax) syntax;

                    if (accessorDeclaration.ChildNodes.Count > 0)
                    {
                        apex.Append(accessorDeclaration.Accessor).AppendLine("{");
                        AppendBody(accessorDeclaration.ChildNodes);
                        apex.AppendLine("}");
                    }
                    else apex.Append(accessorDeclaration.Accessor).AppendLine(";");
                    break;
                case ApexType.ApexExpressionStatementSyntax:
                    var expression = (ApexExpressionStatementSyntax) syntax;
                    apex.Append(GetApexLine(expression.Expression)).AppendLine(";");
                    break;
                case ApexType.ApexIfStatementSyntax:
                    var apexIf = (ApexIfStatementSyntax) syntax;
                    apex.Append("if(")
                        .Append(GetApexLine(apexIf.Condition))
                        .AppendLine("){");
                    AppendBody(apexIf.ChildNodes);
                    apex.AppendLine("}");

                    if (apexIf.ElseStatementSyntax != null)
                    {
                        var elseStatment = new List<ApexSyntaxNode> {apexIf.ElseStatementSyntax};
                        AppendBody(elseStatment);
                    }

                    break;
                case ApexType.ApexElseStatementSyntax:
                    var apexElse = (ApexElseStatementSyntax) syntax;

                    if (apexElse.ChildNodes[0].Kind() == ApexType.ApexIfStatementSyntax)
                    {
                        apex.Append(" else ");
                        AppendBody(apexElse.ChildNodes);
                    }
                    else
                    {
                        apex.Append(" else {");
                        AppendBody(apexElse.ChildNodes);
                        apex.AppendLine("}");
                    }
                    break;
                case ApexType.ApexForEachStatementSyntax:
                    var apexEach = (ApexForEachStatementSyntax) syntax;
                    apex.Append("for(")
                        .Append(GetApexLine(apexEach.Type))
                        .AppendSpace()
                        .Append(apexEach.Identifier)
                        .Append(" : ")
                        .Append(apexEach.Expression)
                        .AppendLine("){");

                    AppendBody(apexEach.ChildNodes);
                    apex.AppendLine("}");
                    break;
                case ApexType.ApexForStatementSyntax:
                    var apexFor = (ApexForStatementSyntax) syntax;
                    apex.Append("for(")
                        .Append(GetApexLine(apexFor.Declaration))
                        .Append(";")
                        .Append(apexFor.Condition)
                        .Append(";")
                        .Append(apexFor.Incrementors)
                        .AppendLine("){");

                    AppendBody(apexFor.ChildNodes);
                    apex.AppendLine("}");
                    break;
                default:
                    Console.WriteLine("Could Not Generate " + syntax.Kind());
                    Console.ReadLine();
                    break;
            }
        }

        private void AppendBody(List<ApexSyntaxNode> syntaxNodes)
        {
            foreach (ApexSyntaxNode apexClassChildNode in syntaxNodes)
            {
                GetApexCode(apexClassChildNode);
            }
        }


        public string ParameterCreater(List<ApexParameterListSyntax> apexParameters)
        {
            var sb = new StringBuilder();
            var parameters = new List<string>();

            foreach (var apexParameter in apexParameters)
            {
                if (apexParameter.Type.Contains("RestDto."))
                    parameters.Add(apexParameter.Type.Replace("RestDto.", "RestDto") + " " + apexParameter.Identifier);
                else parameters.Add(apexParameter.Type + " " + apexParameter.Identifier);
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                if (i == 0) sb.Append(parameters[i]);
                else sb.Append(", ").Append(parameters[i]);
            }
            return sb.ToString().Trim();
        }

        public string GetApexTypes(string cSharpType)
        {
            //cSharpType = cSharpType.Replace("string", "String");
            cSharpType = cSharpType.Replace("int", "Integer");
            return cSharpType;
        }

        public string GetApexLineSoql(string cSharpLine, string soql)
        {
            if (cSharpLine.StartsWith("NoApex"))
            {
                cSharpLine = "//" + cSharpLine;
                return cSharpLine;
            }

            if (cSharpLine.Contains("Soql.Query")) cSharpLine = SoqlSelect(cSharpLine, soql);
            else if (cSharpLine.Contains("Soql.Update")) cSharpLine = SoqlUpdate(cSharpLine);
            else if (cSharpLine.Contains("Soql.Upsert")) cSharpLine = SoqlUpdate(cSharpLine);
            else if (cSharpLine.Contains("Soql.Insert")) cSharpLine = SoqlInsert(cSharpLine);
            else if (cSharpLine.Contains("Soql.Delete")) cSharpLine = SoqlDelete(cSharpLine);
            else if (cSharpLine.Contains("Soql.UnDelete")) cSharpLine = SoqlUnDelete(cSharpLine);
            else if (cSharpLine.Contains("JSON.deserialize")) cSharpLine = JsonDeSerialize(cSharpLine);

            return cSharpLine;
        }

        public string GetApexLine(string cSharpLine)
        {
            if (cSharpLine.StartsWith("NoApex"))
            {
                cSharpLine = "//" + cSharpLine;
                return cSharpLine;
            }
            cSharpLine = cSharpLine.Replace('\"', '\'');


            return cSharpLine;
        }

        public string SoqlSelect(string cSharpLine, string soql)
        {
            var left = cSharpLine.Substring(0, cSharpLine.IndexOf("=", StringComparison.Ordinal) + 1).Trim();
            var newSoql = left + " [" + soql + "]";

            return newSoql;
        }

        // Soql.Update(accountList);
        // update accountList;
        public string SoqlUpdate(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "update " + value;
        }

        // Soql.Upsert(accountList);
        // upsert accountList;
        public string SoqlUpsert(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "upsert " + value;
        }

        // Soql.Insert(accountList);
        // insert accountList
        public string SoqlInsert(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "insert " + value;
        }

        // Soql.Delete(accountList);
        // delete accountList
        public string SoqlDelete(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "delete " + value;
        }

        // Soql.UnDelete(accountList);
        // undelete accountList
        public string SoqlUnDelete(string cSharpLine)
        {
            var value = cSharpLine.Split('(', ')')[1];
            return "undelete " + value;
        }

        // List<Account> newnewDateTime = JSON.deserialize<List<Account>>(newDateTimeJson);
        // List<Account> newnewDateTime = (List<Account>)JSON.deserialize(newDateTimeJson, List<Account>.class);
        public string JsonDeSerialize(string cSharpLine)
        {
            var left = cSharpLine.Substring(0, cSharpLine.IndexOf("=", StringComparison.Ordinal) + 1).Trim();
            var right = cSharpLine.Substring(cSharpLine.IndexOf("=", StringComparison.Ordinal) + 1).Trim();

            var index = right.IndexOf("<", StringComparison.Ordinal);
            var lastIndex = right.LastIndexOf(">", StringComparison.Ordinal);
            var jsonType = right.Substring(index + 1, lastIndex - (index + 1));

            var value = right.Split('(', ')')[1];

            var returnString = left + " (" + jsonType + ")JSON.deserialize(" + value + "," + jsonType + ".class)";

            return returnString;
        }
    }
}