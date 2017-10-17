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
            var syntaxTree = CSharpSyntaxTree.ParseText(cSharpCode);
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
            StringBuilder parameterString  = new StringBuilder();
            parameterString.Append(parameterListSyntax.OpenParenToken);
            foreach (var parameter in parameterListSyntax.Parameters)
            {
                parameterString.Append(FieldConverter.GetApexTypes(parameter.Type.ToString()));
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
                    var methodDeclaration = (MethodDeclarationSyntax) node;

                    sb.Append(GetAttributes(methodDeclaration.AttributeLists));
                    sb.AppendLine();
                    sb.Append($"{GetModifiers(methodDeclaration.Modifiers)} {FieldConverter.GetApexTypes(methodDeclaration.ReturnType.ToString())} {methodDeclaration.Identifier.Text}{GetParametr(methodDeclaration.ParameterList)}");
                    sb.AppendLine();

                    sb.AppendLine(methodDeclaration.Body.OpenBraceToken.Text);
                    base.Visit(node);
                    sb.AppendLine(methodDeclaration.Body.CloseBraceToken.Text);
                    break;
                }
                case SyntaxKind.LocalDeclarationStatement:
                {
                    sb.AppendLine("LocalDeclarationStatemen");
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
                    //  Console.WriteLine(node.Kind());

                    base.Visit(node);
                    break;
                }

            }
        }
    }
}