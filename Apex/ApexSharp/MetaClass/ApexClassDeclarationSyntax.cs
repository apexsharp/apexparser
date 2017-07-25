using System;
using System.Collections.Generic;
using System.Text;
using Apex.ApexSharp.ApexToSharp;
using Apex.ApexSharp.SharpToApex;
using Apex.ApexSharp.Util;
using Microsoft.CodeAnalysis;

namespace Apex.ApexSharp.MetaClass
{
    public enum ApexType
    {
        NotFound,

        Enum,
        Interface,

        ApexClassDeclarationSyntax,
        ApexConstructorDeclarationSyntax,
        ApexMethodDeclarationSyntax,
        ApexExpressionStatementSyntax,
        ApexLocalDeclarationStatementSyntax,
        ApexFieldDeclarationSyntax,
        ApexPropertyDeclarationSyntax,
        ApexAccessorDeclarationSyntax,
        ApexForStatementSyntax,
        ApexForEachStatementSyntax,
        ApexReturnStatementSyntax,

        ApexIfStatementSyntax,
        ApexElseStatementSyntax,

        ApexTryStatementSyntax,
        ApexCatchClauseSyntax,
        ApexFinallyClauseSyntax,
        ApexThrownStatementSyntax,
        ApexInvocationExpression,

        ApexWhileStatementSyntax,
        ApexDoStatementSyntax,


        Soql,
        Dml,
        CloseBrace,
    }

    public class ApexSyntaxNode
    {
        public readonly List<ApexTocken> ApexTockens = new List<ApexTocken>();
        public readonly List<ApexSyntaxNode> ChildNodes = new List<ApexSyntaxNode>();
        public List<string> CodeComments = new List<string>();
        public string CommentMustGo { get; set; }
        public int LineNumber { get; set; }

        protected ApexType ApexKind { private get; set; }

        public ApexType Kind()
        {
            return ApexKind;
        }

        public List<string> GetApexCode()
        {
            return new List<string>() { "Not Implemented" };
        }

        public List<string> GetCSharpCode()
        {
            return new List<string>() { "Not Implemented" };
        }
    }

    public class ApexClassDeclarationSyntax : ApexSyntaxNode
    {
        public List<ApexMethodDeclarationSyntax> ApexMethods = new List<ApexMethodDeclarationSyntax>();
        public List<string> AttributeLists = new List<string>();

        public List<string> CodeInsideClass = new List<string>();
        public List<string> Implementing = new List<string>();
        public List<string> Modifiers = new List<string>();


        public ApexClassDeclarationSyntax()
        {
            ApexKind = ApexType.ApexClassDeclarationSyntax;
        }

        public string NameSpace { get; set; }
        public string IsShareing { get; set; } // Yes No Null        
        public string Identifier { set; get; }
        public string Extending { set; get; }

        public List<string> GetApexCode()
        {
            List<string> apexCodeList = new List<string>();

            apexCodeList.AddRange(CodeComments);

            string apexCode = "";

            foreach (var attributeList in AttributeLists)
            {
                apexCode = apexCode + attributeList + " ";
            }

            if (IsShareing == "YES")
            {
                apexCode = apexCode + $"public with sharing class {Identifier}";
            }

            else if (IsShareing == "NO")
            {
                apexCode = apexCode + $"public without sharing class {Identifier}";
            }
            else
            {
                apexCode = apexCode + $"public class {Identifier}";
            }

            apexCodeList.Add(apexCode);
            apexCodeList.Add("{");

            foreach (var apexFieldDeclarationSyntax in ChildNodes)
            {
                if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexFieldDeclarationSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexFieldDeclarationSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetApexCode());
                }

                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexPropertyDeclarationSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexPropertyDeclarationSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetApexCode());
                }
                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexMethodDeclarationSyntax)
                {
                    var apexMethodDeclarationSyntax = (ApexMethodDeclarationSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexMethodDeclarationSyntax.GetApexCode());
                }
                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexConstructorDeclarationSyntax)
                {
                    var apexMethodDeclarationSyntax = (ApexConstructor)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexMethodDeclarationSyntax.GetApexCode());
                }
            }


            apexCodeList.Add("}");

            return apexCodeList;
        }


        public List<string> GetCSharpCode()
        {
            List<string> code = new List<string>();

            code.Add("namespace SalesForceApiDemo.Code");
            code.Add("{");


            code.AddRange(CodeComments);
            if (IsShareing == "YES")
            {
                code.Add("[ApexWithSharing]");
            }


            if (IsShareing == "NO")
            {
                code.Add("[ApexWithOutSharing]");
            }


            foreach (var attribute in AttributeLists)
            {
                code.Add(CSharpGenerator.AttributeCreater(attribute));
            }


            code.Add(CSharpGenerator.ModifierCreater(Modifiers) + " class " + Identifier);

            if (Extending != null)
                code.Add(" : " + Extending);

            if (Implementing.Count > 0)
            {
                foreach (var implementing in Implementing)
                {
                    code.Add(", implementing" + implementing);
                }
            }

            code.Add("{");

            foreach (var apexFieldDeclarationSyntax in ChildNodes)
            {
                code.AddRange(apexFieldDeclarationSyntax.GetCSharpCode());
            }

            code.Add("}");
            code.Add("}");

            return code;
        }
    }

    // ToDo : Fix class name
    public class ApexConstructor : ApexSyntaxNode
    {
        public readonly List<string> Modifiers = new List<string>();
        public List<ApexParameterListSyntax> ApexParameters = new List<ApexParameterListSyntax>();
        public List<string> AttributeLists = new List<string>();

        public ApexConstructor()
        {
            ApexKind = ApexType.ApexConstructorDeclarationSyntax;
        }

        public string Identifier { set; get; }

        public new List<string> GetApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexCodeList = new List<string>();
            apexCodeList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();

            apex.Append(ApexGeneratorUtil.ModifierAttributeCreater(AttributeLists)).AppendSpace()
                .Append(ApexGeneratorUtil.ModifierCreater(Modifiers)).AppendSpace()
                .Append(Identifier)
                .Append("(")
                .Append(gen.ParameterCreater(ApexParameters))
                .Append(")");
            apexCodeList.Add(apex.ToString());

            apexCodeList.Add("{");


            foreach (var apexFieldDeclarationSyntax in ChildNodes)
            {
                if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexLocalDeclarationStatementSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexLocalDeclarationStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetApexCode());
                }

                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexReturnStatementSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexReturnStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetCApexCode());
                }

                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexExpressionStatementSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexExpressionStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetApexCode());
                }

                else
                {
                    Console.WriteLine(">> " + apexFieldDeclarationSyntax.Kind());
                }
            }

            apexCodeList.Add("}");
            return apexCodeList;
        }
    }

    public class ApexMethodDeclarationSyntax : ApexSyntaxNode
    {
        public List<ApexParameterListSyntax> ApexParameters = new List<ApexParameterListSyntax>();
        public List<string> AttributeLists = new List<string>();

        public List<string> CodeInsideMethod = new List<string>();
        public List<string> Modifiers = new List<string>();

        public ApexMethodDeclarationSyntax()
        {
            ApexKind = ApexType.ApexMethodDeclarationSyntax;
        }

        public string ReturnType { get; set; }
        public string Identifier { get; set; }

        public new List<string> GetApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexCodeList = new List<string>();
            apexCodeList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();


            apex.Append(ApexGeneratorUtil.ModifierAttributeCreater(AttributeLists)).AppendSpace()
                .Append(ApexGeneratorUtil.ModifierCreater(Modifiers)).AppendSpace()
                .Append(gen.GetApexTypes(ReturnType)).AppendSpace()
                .Append(Identifier)
                .Append("(")
                .Append(gen.ParameterCreater(ApexParameters))
                .Append(")");
            apexCodeList.Add(apex.ToString());

            apexCodeList.Add("{");


            foreach (var apexFieldDeclarationSyntax in ChildNodes)
            {
                if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexLocalDeclarationStatementSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexLocalDeclarationStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetApexCode());
                }

                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexReturnStatementSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexReturnStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetCApexCode());
                }

                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexExpressionStatementSyntax)
                {
                    var apexFieldDeclarationSyntax1 = (ApexExpressionStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexFieldDeclarationSyntax1.GetApexCode());
                }
                else if (apexFieldDeclarationSyntax.Kind() == ApexType.ApexIfStatementSyntax)
                {
                    var apexIfStatementSyntax = (ApexIfStatementSyntax)apexFieldDeclarationSyntax;
                    apexCodeList.AddRange(apexIfStatementSyntax.GetApexCode());
                }

                else
                {
                    Console.WriteLine(">> Not Found " + apexFieldDeclarationSyntax.Kind());
                }
            }

            apexCodeList.Add("}");
            return apexCodeList;
        }
    }

    public class ApexFieldDeclarationSyntax : ApexSyntaxNode
    {
        public readonly List<string> AttributeLists = new List<string>();
        public readonly List<string> IdentifierList = new List<string>();
        public readonly List<string> Modifiers = new List<string>();

        public ApexFieldDeclarationSyntax()
        {
            Initializaer = String.Empty;
            ApexKind = ApexType.ApexFieldDeclarationSyntax;
        }

        public string Type { get; set; }
        public string Initializaer { get; set; }

        public new List<string> GetApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexList = new List<string>();
            apexList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();

            apex.Append(ApexGeneratorUtil.ModifierCreater(Modifiers)).AppendSpace()
                .Append(gen.GetApexTypes(Type)).AppendSpace()
                .Append(IdentifierList[0]);

            if (Initializaer != string.Empty)
            {
                apex.Append(" = ")
                    .Append(gen.GetApexLine(Initializaer));
            }

            apex.AppendLine(";");

            apexList.Add(apex.ToString());
            return apexList;
        }
    }

    public class ApexPropertyDeclarationSyntax : ApexSyntaxNode
    {
        public readonly List<string> Modifiers = new List<string>();
        public List<string> AttributeLists = new List<string>();

        public ApexPropertyDeclarationSyntax()
        {
            ApexKind = ApexType.ApexPropertyDeclarationSyntax;
        }

        public string Type { get; set; }
        public string Identifier { get; set; }

        public new List<string> GetApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexList = new List<string>();
            apexList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();

            apex.Append(ApexGeneratorUtil.ModifierCreater(Modifiers)).AppendSpace()
                .Append(gen.GetApexTypes(Type)).AppendSpace()
                .Append(Identifier);


            apex.AppendSpace().Append("{ get; set; }");


            apexList.Add(apex.ToString());
            return apexList;
        }
    }


    public class ApexAccessorDeclarationSyntax : ApexSyntaxNode
    {
        public readonly List<string> Modifiers = new List<string>();
        public List<string> AttributeLists = new List<string>();

        public ApexAccessorDeclarationSyntax()
        {
            ApexKind = ApexType.ApexAccessorDeclarationSyntax;
        }

        public string Accessor { get; set; }
        public bool ContainsChildren { get; set; }
    }

    public class ApexExpressionStatementSyntax : ApexSyntaxNode
    {
        public ApexExpressionStatementSyntax()
        {
            ApexKind = ApexType.ApexExpressionStatementSyntax;
        }

        public string Expression { get; set; }

        public new List<string> GetApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexList = new List<string>();
            apexList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();




            if (Expression.Contains("Soql"))
            {
                apex.Append(gen.GetApexLineSoql(Expression, Expression)).Append(';');

            }
            else
            {
                apex.Append(gen.GetApexLine(Expression)).Append(';');
            }



            apexList.Add(apex.ToString());
            return apexList;
        }
    }

    public class ApexLocalDeclarationStatementSyntax : ApexSyntaxNode
    {
        public List<SyntaxNode> DescendantNodes = new List<SyntaxNode>();

        public ApexLocalDeclarationStatementSyntax()
        {
            Soql = String.Empty;
            ApexKind = ApexType.ApexLocalDeclarationStatementSyntax;
        }

        public string Expression { get; set; }

        public string Soql { get; set; }

        public new List<string> GetApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexList = new List<string>();
            apexList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();

            if (Soql == String.Empty)
            {
                apex.Append(gen.GetApexLine(Expression)).Append(';');
            }
            else
            {
                apex.Append(gen.GetApexLineSoql(Expression, Soql)).Append(';');
            }


            apexList.Add(apex.ToString());
            return apexList;
        }
    }

    public class ApexReturnStatementSyntax : ApexSyntaxNode
    {
        public ApexReturnStatementSyntax()
        {
            ApexKind = ApexType.ApexReturnStatementSyntax;
        }

        public string Expression { get; set; }

        public new List<string> GetCApexCode()
        {
            ApexGenerator gen = new ApexGenerator();

            List<string> apexList = new List<string>();
            apexList.AddRange(CodeComments);

            StringBuilder apex = new StringBuilder();


            apex.Append("return").AppendSpace().Append(gen.GetApexLine(Expression)).Append(';');


            apexList.Add(apex.ToString());
            return apexList;
        }
    }

    public class ApexForEachStatementSyntax : ApexSyntaxNode
    {
        public ApexForEachStatementSyntax()
        {
            ApexKind = ApexType.ApexForEachStatementSyntax;
        }

        public string Type { get; set; }
        public string Identifier { get; set; }
        public string Expression { get; set; }
    }


    public class ApexIfStatementSyntax : ApexSyntaxNode
    {
        public ApexIfStatementSyntax()
        {
            ApexKind = ApexType.ApexIfStatementSyntax;
        }

        public string Condition { get; set; }
        public ApexElseStatementSyntax ElseStatementSyntax { get; set; }

        public List<string> GetApexCode()
        {
            List<string> apexList = new List<string>();
            apexList.AddRange(CodeComments);


            apexList.Add("if(" + Condition + "){");
            //   apexList.AddRange(ChildNodes)
            apexList.Add("}");

            if (ElseStatementSyntax != null)
            {
                apexList.AddRange(ElseStatementSyntax.GetApexCode());
            }

            return apexList;
        }
    }

    public class ApexElseStatementSyntax : ApexSyntaxNode
    {
        public ApexElseStatementSyntax()
        {
            ApexKind = ApexType.ApexElseStatementSyntax;
        }
    }

    public class ApexForStatementSyntax : ApexSyntaxNode
    {
        public ApexForStatementSyntax()
        {
            ApexKind = ApexType.ApexForStatementSyntax;
        }

        public string Declaration { get; set; }
        public string Condition { get; set; }
        public string Incrementors { get; set; }
    }

    public class ApexParameterListSyntax : ApexSyntaxNode
    {
        public ApexParameterListSyntax()
        {
            IsGneric = false;
        }

        public bool IsGneric { get; set; }
        public string Type { set; get; }
        public string Identifier { get; set; }
    }

    public class ApexDoStatementSyntax : ApexSyntaxNode
    {
        public ApexDoStatementSyntax()
        {
            ApexKind = ApexType.ApexDoStatementSyntax;
        }

        public string Condition { set; get; }
    }

    public class ApexWhileStatementSyntax : ApexSyntaxNode
    {
        public ApexWhileStatementSyntax()
        {
            ApexKind = ApexType.ApexWhileStatementSyntax;
        }

        public string Condition { set; get; }
    }

    public class ApexTryStatementSyntax : ApexSyntaxNode
    {
        public ApexTryStatementSyntax()
        {
            ApexKind = ApexType.ApexTryStatementSyntax;
        }
    }

    public class ApexCatchClauseSyntax : ApexSyntaxNode
    {
        public ApexCatchClauseSyntax()
        {
            ApexKind = ApexType.ApexCatchClauseSyntax;
        }

        public string Type { set; get; }
        public string Identifier { get; set; }
    }

    public class ApexFinallyClauseSyntax : ApexSyntaxNode
    {
        public ApexFinallyClauseSyntax()
        {
            ApexKind = ApexType.ApexFinallyClauseSyntax;
        }
    }

    public class ApexInvocationExpression : ApexSyntaxNode
    {
        public ApexInvocationExpression()
        {
            ApexKind = ApexType.ApexInvocationExpression;
        }

        public string Expression { get; set; }
        public string Argument { get; set; }
    }

    public class ApexThrownStatementSyntax : ApexSyntaxNode
    {
        public ApexThrownStatementSyntax()
        {
            ApexKind = ApexType.ApexThrownStatementSyntax;
        }

        public string Expression { get; set; }
    }


    public class SoqlExpression : ApexSyntaxNode
    {
        public SoqlExpression()
        {
            ApexKind = ApexType.Soql;
        }
    }

    public class DmlExpression : ApexSyntaxNode
    {
        public DmlExpression()
        {
            ApexKind = ApexType.Dml;
        }
    }

    public class ApexCloseCurlyBrackets : ApexSyntaxNode
    {
        public ApexCloseCurlyBrackets()
        {
            ApexKind = ApexType.CloseBrace;
        }
    }
}