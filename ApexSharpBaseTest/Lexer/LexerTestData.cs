using System.Collections.Generic;
using ApexParser.Lexer;

namespace ApexParserTest.Lexer
{
    public class LexerTestData
    {
        public static List<LexerTestElement> GetMethods()
        {
            List<LexerTestElement> dataList = new List<LexerTestElement>
            {
                new LexerTestElement
                {
                    ApexLine = "void method1()",
                    TokenList = new List<TokenType>() {TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "string method2(string firstName)",
                    TokenList = new List<TokenType>() { TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.ReturnType, TokenType.Word, TokenType.CloseBrackets}
                },
                new LexerTestElement
                {
                    ApexLine = "void method3(List<string> stringArray, List<string> stringArray)",
                    TokenList = new List<TokenType>() { TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.ClassNameGeneric, TokenType.Word, TokenType.Comma, TokenType.ClassNameGeneric, TokenType.Word, TokenType.CloseBrackets}
                },
                new LexerTestElement
                {
                    ApexLine = "void method4(List<string> stringArray, List<string> stringArray, List<string> stringArray)",
                    TokenList = new List<TokenType>() {TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.ClassNameGeneric, TokenType.Word, TokenType.Comma, TokenType.ClassNameGeneric, TokenType.Word, TokenType.Comma, TokenType.ClassNameGeneric, TokenType.Word, TokenType.CloseBrackets}
                },
                new LexerTestElement
                {
                    ApexLine = "public void method5()",
                    TokenList = new List<TokenType>() {TokenType.AccessModifier, TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "public static void method6()",
                    TokenList = new List<TokenType>() {TokenType.AccessModifier, TokenType.KwStatic, TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "public static string method7()",
                    TokenList = new List<TokenType>() {TokenType.AccessModifier, TokenType.KwStatic, TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "public static List<string> method8()",
                    TokenList = new List<TokenType>() {TokenType.AccessModifier, TokenType.KwStatic, TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "List<string> method9()",
                    TokenList = new List<TokenType>() {TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "@isTest static void method10()",
                    TokenList = new List<TokenType>() {TokenType.IsTestAttrubute, TokenType.KwStatic, TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
                new LexerTestElement
                {
                    ApexLine = "static testMethod void method11()",
                    TokenList = new List<TokenType>() {TokenType.KwStatic, TokenType.KwTestMethod, TokenType.ReturnType, TokenType.MethodName, TokenType.OpenBrackets, TokenType.CloseBrackets,}
                },
            };

            return dataList;
        }

    }
}