using System.Text;
using ApexSharpBase.Ext;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApexSharpBase.Converter.Apex
{
    public class ApexCodeGenerator : CSharpSyntaxWalker
    {
        StringBuilder sb = new StringBuilder();

        public ApexCodeGenerator() : base(SyntaxWalkerDepth.Trivia)
        {
        }

        public string GetApexCode(string cSharpCode)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(cSharpCode);
            Visit(syntaxTree.GetRoot());
            return sb.ToString();
        }

        public string GetModifiers(SyntaxTokenList syntaxTokenList)
        {
            return syntaxTokenList[0].Text;
        }

        public string GetAttributes(SyntaxList<AttributeListSyntax> attributeList)
        {
            if (attributeList.Any()) return attributeList[0].ToString();
            return "";
        }

        public string GetParametr(ParameterListSyntax parameterListSyntax)
        {
            StringBuilder parameterString = new StringBuilder();
            parameterString.Append(parameterListSyntax.OpenParenToken);
            foreach (var parameter in parameterListSyntax.Parameters)
            {
                parameterString.Append(ExpressionConverter.TypeConverter(parameter.Type.ToString()));
                parameterString.AppendSpace();
                parameterString.Append(parameter.Identifier);
            }
            parameterString.Append(parameterListSyntax.CloseParenToken);
            return parameterString.ToString();
        }

        public override void Visit(SyntaxNode node)
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    {
                        sb.AppendLine("class {");
                        base.Visit(node);
                        sb.AppendLine("}");
                        break;
                    }
                case SyntaxKind.MethodDeclaration:
                    {
                        var syntax = (MethodDeclarationSyntax)node;

                        sb.Append(GetAttributes(syntax.AttributeLists));
                        sb.AppendLine();
                        sb.Append($"{GetModifiers(syntax.Modifiers)} {ExpressionConverter.TypeConverter(syntax.ReturnType.ToString())} {syntax.Identifier.Text}{GetParametr(syntax.ParameterList)}");
                        sb.AppendLine();

                        sb.AppendLine(syntax.Body.OpenBraceToken.Text);
                        base.Visit(node);
                        sb.AppendLine(syntax.Body.CloseBraceToken.Text);
                        break;
                    }
                case SyntaxKind.LocalDeclarationStatement:
                    {

                        var syntax = (LocalDeclarationStatementSyntax)node;
                        sb.AppendLine(ExpressionConverter.GetApexLine(syntax.GetText()));
                        base.Visit(node);
                        break;
                    }
                case SyntaxKind.ExpressionStatement:
                    {
                        var syntax = (ExpressionStatementSyntax)node;
                        sb.AppendLine(ExpressionConverter.GetApexLine(syntax.GetText()));
                        base.Visit(node);
                        break;
                    }
                case SyntaxKind.IfStatement:
                    {
                        sb.AppendLine("If {");
                        base.Visit(node);
                        sb.AppendLine("}");
                        break;
                    }
                default:
                    {
                        //Console.WriteLine(node.Kind());

                        base.Visit(node);
                        break;
                    }

            }
        }
    }
}