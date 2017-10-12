using System;
using System.Linq;
using ApexSharpBase.Ext;

namespace ApexSharpBase.Converter.CSharp
{
    using System.Text;
    using ApexSharpBase.MetaClass;

    public class CSharpGenerator
    {
        public string GenerateWithIndentation(ClassContainer classContainer)
        {
            return Generate(classContainer);
        }


        public string Generate(ClassContainer classContainer)
        {
            StringBuilder sb = new StringBuilder();

            GenerateCode(sb, classContainer);

            return sb.ToString();
        }


        public string GetAttributes(ClassSyntax cs)
        {
            if (cs.Attributes.Any()) return cs.Attributes[0];
            return "";
        }

        public string GetModifiers(ClassSyntax cs)
        {
            if (cs.Modifiers.Any()) return cs.Modifiers[0];
            return "";
        }

        public string GetModifiers(MethodSyntax cs)
        {
            if (cs.Modifiers.Any()) return cs.Modifiers[0];
            return "";
        }

        public string LangType { get; set; }
        public void GenerateCode(StringBuilder sb, BaseSyntax baseSyntax)
        {
            if (baseSyntax.Kind == SyntaxType.ClassContainer.ToString())
            {
                var classContainer = (ClassContainer)baseSyntax;

                LangType = classContainer.ContainerLang;

                sb.AppendLine("namespace ApexSharpDemo.ApexCode");
                sb.AppendLine("{");
                //sb.AppendLine("using SObjects;");
                //sb.AppendLine("using Apex.System;");
                //sb.AppendLine("using Apex.ApexSharp;");
                //sb.AppendLine("using Apex.ApexAttrbutes;");
                //sb.AppendLine("using SalesForceAPI.Apex;");
  
                foreach (var childNode in classContainer.ChildNodes) GenerateCode(sb, childNode);

                sb.Append("}");
            }

            else if (baseSyntax.Kind == SyntaxType.Class.ToString())
            {
                var classSyntex = (ClassSyntax)baseSyntax;
                if(GetAttributes(classSyntex) != String.Empty) sb.Append($"[{GetAttributes(classSyntex)}]").AppendLine(); ;
                sb.Append($"{GetModifiers(classSyntex)} class {classSyntex.Identifier}").AppendLine();
                sb.AppendLine("{");

                foreach (var childNode in baseSyntax.ChildNodes) GenerateCode(sb, childNode);

                sb.AppendLine("}");
            }
            else if (baseSyntax.Kind == SyntaxType.Method.ToString())
            {
                var methodSyntax = (MethodSyntax) baseSyntax;

                var returnType= methodSyntax.ReturnType;
                if (LangType == "APEX")
                {
                    returnType = FieldConverter.GetApexTypes(methodSyntax.ReturnType);
                }

                sb.Append($"{GetModifiers(methodSyntax)} {returnType} {methodSyntax.Identifier}()")
                    .AppendLine();
                sb.AppendLine("{");

                foreach (var childNode in baseSyntax.ChildNodes) GenerateCode(sb, childNode);

                sb.AppendLine().Append("}").AppendLine();
            }
            else if (baseSyntax.Kind == SyntaxType.ReturnStatement.ToString())
            {
                var returnStatement = (ReturnStatement) baseSyntax;
                sb.Append("return ").Append(returnStatement.Expression).Append(";");
            }
            else
            {
                sb.AppendLine("Could Not Generate Code For " + baseSyntax.Kind);
            }
        }
    }
}
