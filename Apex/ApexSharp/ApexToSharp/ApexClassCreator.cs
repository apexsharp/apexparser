using System;
using System.Collections.Generic;
using System.Linq;
using Apex.ApexSharp.MetaClass;

namespace Apex.ApexSharp.ApexToSharp
{
    public static class ApexClassCreator
    {
        private static List<string> GetTokens(List<ApexTocken> apexTokenList, TockenType tokenType)
        {
            List<ApexTocken> classIndex = apexTokenList.FindAll(x => x.TockenType == tokenType);
            return classIndex.Select(x => x.Tocken).ToList();
        }

        public static ApexClassDeclarationSyntax CreateClass(List<ApexTocken> apexTokenList)
        {
            ApexClassDeclarationSyntax apex = new ApexClassDeclarationSyntax();


            if (GetTokens(apexTokenList, TockenType.IsTestAttrubute).Count > 0)
            {
                apex.AttributeLists.Add("@isTest");
            }

            if (GetTokens(apexTokenList, TockenType.KwWithSharing).Count > 0)
            {
                apex.IsShareing = "YES";
            }
            else if (GetTokens(apexTokenList, TockenType.KwWithoutSharing).Count > 0)
            {
                apex.IsShareing = "NO";
            }

            apex.Modifiers.AddRange(GetTokens(apexTokenList, TockenType.AccessModifier));
            apex.Identifier = apexTokenList.Find(x => x.TockenType == TockenType.Word).Tocken;

            //var extendKeywordIndex = apexTokenList.FindIndex(x => x.TockenType == TockenType.KwExtends);
            //if (extendKeywordIndex > 0) apex.Extending = apexTokenList[extendKeywordIndex + 1].Tocken;

            //var implementsKewordIndex = apexTokenList.FindIndex(x => x.TockenType == TockenType.KwImplements);
            //if (implementsKewordIndex > 0)
            //{
            //    implementsKewordIndex++;
            //    while (apexTokenList[implementsKewordIndex].TockenType != TockenType.OpenCurlyBrackets)
            //    {
            //        if (apexTokenList[implementsKewordIndex].TockenType != TockenType.Comma)
            //        {
            //            apex.Implementing.Add(apexTokenList[implementsKewordIndex].Tocken);
            //        }
            //        implementsKewordIndex++;
            //    }
            //}
            return apex;
        }


        public static ApexConstructor CreateConstrutor(ApexList apexList)
        {
            ApexConstructor apex = new ApexConstructor {CommentMustGo = ApexComments(apexList)};


            var index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.AccessModifier);
            if (index.Count > 0) apex.Modifiers.AddRange(index.Select(x => x.Tocken));

            index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.ClassName ||
                                                      x.TockenType == TockenType.ClassNameGeneric);
            if (index.Count > 0) apex.Identifier = index.First().Tocken;

            apex.ApexParameters = CreateParameters(apexList.ApexTockens);

            return apex;
        }

        public static ApexMethodDeclarationSyntax CreateMethod(List<ApexTocken> apexTokenList)
        {
            ApexMethodDeclarationSyntax apex = new ApexMethodDeclarationSyntax();

            // Print the Token List for Debugging 
            // PrintToken.Print(apexTokenList);

            apex.AttributeLists.AddRange(GetTokens(apexTokenList, TockenType.Attrubute));
            apex.Modifiers.AddRange(GetTokens(apexTokenList, TockenType.AccessModifier));

            if (GetTokens(apexTokenList, TockenType.KwTestMethod).Count > 0)
            {
                apex.AttributeLists.Add("[Test]");
            }


            if (GetTokens(apexTokenList, TockenType.KwVoid).Count > 0)
            {
                apex.ReturnType = "void";
            }

            if (GetTokens(apexTokenList, TockenType.Word).Count > 0)
            {
                apex.Identifier = GetTokens(apexTokenList, TockenType.Word)[0];
            }


            apex.ApexParameters = CreateParameters(apexTokenList);

            return apex;
        }

        public static ApexPropertyDeclarationSyntax CreatePropertyDeclaration(ApexList apexList)
        {
            ApexPropertyDeclarationSyntax apex =
                new ApexPropertyDeclarationSyntax {CommentMustGo = ApexComments(apexList)};

            var index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.AccessModifier);
            if (index.Count > 0) apex.Modifiers.AddRange(index.Select(x => x.Tocken));

            //index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.ClassName || x.TockenType == TockenType.ClassNameGeneric || x.TockenType == TockenType.ClassNameArray);
            //if (index.Count > 0) apex.Type = index.First().Tocken;

            //apex.Identifier = apexList.ApexTockens.Find(x => x.TockenType == TockenType.Word).Tocken;

            //apex.AccessorList = "{get;set;}";

            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        // Get Set
        public static ApexAccessorDeclarationSyntax CreateAccessorDeclaration(ApexList apexList)
        {
            ApexAccessorDeclarationSyntax apex = new ApexAccessorDeclarationSyntax
            {
                CommentMustGo = ApexComments(apexList),
                Accessor = apexList.ApexTockens.Find(x => x.TockenType == TockenType.KwGetSet).Tocken
            };

            var modifiers = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.AccessModifier);
            if (modifiers.Count > 0) apex.Modifiers.AddRange(modifiers.Select(x => x.Tocken));

            var index = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.StatmentTerminator);
            if (index > 0) apex.ContainsChildren = false;
            else apex.ContainsChildren = true;

            return apex;
        }

        public static ApexFieldDeclarationSyntax CreateFieldDeclaration(ApexList apexList)
        {
            ApexFieldDeclarationSyntax apex = new ApexFieldDeclarationSyntax {CommentMustGo = ApexComments(apexList)};

            var index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.AccessModifier);
            if (index.Count > 0) apex.Modifiers.AddRange(index.Select(x => x.Tocken));

            //index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.ClassName || x.TockenType == TockenType.ClassNameGeneric || x.TockenType == TockenType.ClassNameArray);
            //if (index.Count > 0) apex.Type = index.First().Tocken;

            //index = apexList.ApexTockens.FindAll(x => x.TockenType == TockenType.Word);
            //if (index.Count > 0) apex.IdentifierList.Add(index.First().Tocken);

            //var indexStart = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.Equal);
            //if (indexStart > 0)
            //{
            //    apex.Initializaer = "=";
            //    apex.ApexTockens.AddRange(apexList.ApexTockens);
            //}

            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexLocalDeclarationStatementSyntax CreateLocalDeclarationStatement(ApexList apexList)
        {
            ApexLocalDeclarationStatementSyntax apex =
                new ApexLocalDeclarationStatementSyntax {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexExpressionStatementSyntax CreateExpressionStatement(ApexList apexList)
        {
            ApexExpressionStatementSyntax apex =
                new ApexExpressionStatementSyntax {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexReturnStatementSyntax CreateReturnExpression(ApexList apexList)
        {
            ApexReturnStatementSyntax apex = new ApexReturnStatementSyntax {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexIfStatementSyntax CreateIfStatementSyntax(ApexList apexList)
        {
            ApexIfStatementSyntax apex = new ApexIfStatementSyntax {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexForStatementSyntax CreateForLoop(ApexList apexList)
        {
            ApexForStatementSyntax apex = new ApexForStatementSyntax {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexForEachStatementSyntax CreateForEach(ApexList apexList)
        {
            ApexForEachStatementSyntax apex = new ApexForEachStatementSyntax {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static SoqlExpression CreateSoqlExpression(ApexList apexList)
        {
            SoqlExpression apex = new SoqlExpression {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static DmlExpression CreateDmlExpressionn(ApexList apexList)
        {
            DmlExpression apex = new DmlExpression {CommentMustGo = ApexComments(apexList)};
            apex.ApexTockens.AddRange(apexList.ApexTockens);
            return apex;
        }

        public static ApexCloseCurlyBrackets CreateCloseCurlyBrackets(ApexList apexList)
        {
            ApexCloseCurlyBrackets apex = new ApexCloseCurlyBrackets {CommentMustGo = ApexComments(apexList)};
            return apex;
        }

        private static string ApexComments(ApexList apexList)
        {
            if (apexList.ApexComments.Count > 0)
            {
                return string.Join(Environment.NewLine, apexList.ApexComments);
            }
            else
            {
                return null;
            }
        }

        private static List<ApexParameterListSyntax> CreateParameters(List<ApexTocken> apexTokens)
        {
            var apexParameterList = new List<ApexParameterListSyntax>();
            var parameterStart = apexTokens.FindIndex(x => x.TockenType == TockenType.OpenBrackets);

            for (int i = parameterStart; i < apexTokens.Count; i++)
            {
                if (apexTokens[i].TockenType == TockenType.ClassName ||
                    apexTokens[i].TockenType == TockenType.ClassNameGeneric ||
                    apexTokens[i].TockenType == TockenType.ClassNameArray)
                {
                    apexParameterList.Add(new ApexParameterListSyntax()
                    {
                        Type = apexTokens[i].Tocken,
                        Identifier = apexTokens[i + 1].Tocken
                    });
                    i++;
                    i++;
                }
            }
            return apexParameterList;
        }
        //    ApexInterface apexClass = new ApexInterface();
        //{

        //public static ApexInterface CreateInterface(ApexList apexList)

        //}

        //    return apexClass;
        //    }
        //        apexClass.EnumList.Add(apexList.ApexTockens[i].Tocken);
        //    {

        //    for (int i = openBracketIndex + 1; i < closeBracketIndex; i++)
        //    var closeBracketIndex = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.CloseCurlyBrackets);

        //    var openBracketIndex = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.OpenCurlyBrackets);
        //    apexClass.Identifier = apexList.ApexTockens[classIndex + 1].Tocken;

        //    classIndex = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.KwEnum);
        //    if (classIndex >= 0) apexClass.Modifiers = apexList.ApexTockens[classIndex].Tocken;

        //    var classIndex = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.AccessModifier);
        //    ApexEnum apexClass = new ApexEnum();
        //{


        //public static ApexEnum CreateEnum(ApexList apexList)

        //    var classIndex = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.AccessModifier);
        //    if (classIndex >= 0) apexClass.Modifiers = apexList.ApexTockens[classIndex].Tocken;

        //    classIndex = apexList.ApexTockens.FindIndex(x => x.TockenType == TockenType.KwInterface);
        //    apexClass.Identifier = apexList.ApexTockens[classIndex + 1].Tocken;

        //    return apexClass;

        //}
    }
}