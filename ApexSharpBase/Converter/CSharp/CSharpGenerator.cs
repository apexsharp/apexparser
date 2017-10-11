using System;
using System.Linq;
using ApexSharpBase.Ext;

namespace ApexSharpBase.Converter.CSharp
{
    using System.Text;
    using ApexSharpBase.MetaClass;

    public class CSharpGenerator
    {
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

        public void GenerateCode(StringBuilder sb, BaseSyntax baseSyntax)
        {
            if (baseSyntax.Kind == SyntaxType.ClassContainer.ToString())
            {
                var classContainer = (ClassContainer)baseSyntax;

                sb.AppendLine("namespace ApexSharpDemo.ApexCode");
                sb.AppendLine("{");
                sb.AppendTab().AppendLine("using SObjects;");
                sb.AppendTab().AppendLine("using Apex.System;");
                sb.AppendTab().AppendLine("using Apex.ApexSharp;");
                sb.AppendTab().AppendLine("using Apex.ApexAttrbutes;");
                sb.AppendTab().AppendLine("using SalesForceAPI.Apex;");
                sb.AppendLine();

                foreach (var childNode in classContainer.ChildNodes) GenerateCode(sb, childNode);

                sb.Append("}");
            }

            else if (baseSyntax.Kind == SyntaxType.Class.ToString())
            {
                var classSyntex = (ClassSyntax)baseSyntax;
                sb.AppendTab().Append($"[{GetAttributes(classSyntex)}]").AppendLine(); ;
                sb.AppendTab().Append($"{GetModifiers(classSyntex)} class {classSyntex.Identifier}").AppendLine();
                sb.AppendTab().AppendLine("{");

                foreach (var childNode in baseSyntax.ChildNodes) GenerateCode(sb, childNode);

                sb.AppendTab().AppendLine("}");
            }
            else if (baseSyntax.Kind == SyntaxType.Method.ToString())
            {
                var methodSyntax = (MethodSyntax)baseSyntax;
                sb.AppendTab().AppendTab().Append($"{GetModifiers(methodSyntax)} {methodSyntax.ReturnType} {methodSyntax.Identifier}()").AppendLine();
                sb.AppendTab().AppendTab().AppendLine("{");

                foreach (var childNode in baseSyntax.ChildNodes) GenerateCode(sb, childNode);

                sb.AppendTab().AppendTab().AppendLine("}");
            }
        }
    }
}
