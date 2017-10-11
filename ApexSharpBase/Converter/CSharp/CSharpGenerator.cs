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

            sb.AppendLine("namespace ApexSharpDemo.ApexCode");
            sb.AppendLine("{");
            sb.AppendTab().AppendLine("using SObjects;");
            sb.AppendTab().AppendLine("using Apex.System;");
            sb.AppendTab().AppendLine("using Apex.ApexSharp;");
            sb.AppendTab().AppendLine("using Apex.ApexAttrbutes;");
            sb.AppendTab().AppendLine("using SalesForceAPI.Apex;");
            sb.AppendLine();

            foreach (var childNode in classContainer.ChildNodes)
            {
                GenerateCode(sb, childNode);
            }

            sb.Append("}");

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

        public void GenerateCode(StringBuilder sb, BaseSyntax cs)
        {
            if(cs.Kind == SyntaxType.Class.ToString())
            {
                var classSyntex = (ClassSyntax)cs;
                sb.AppendTab().Append($"[{GetAttributes(classSyntex)}]").AppendLine(); ;
                sb.AppendTab().Append($"{GetModifiers(classSyntex)} class {classSyntex.Identifier}").AppendLine(); 
                sb.AppendTab().AppendLine("{");

                foreach (var childNode in cs.ChildNodes) GenerateCode(sb, childNode);

                sb.AppendTab().AppendLine("}");
            }
            else if (cs.Kind == SyntaxType.Method.ToString())
            {
                var classSyntex = (MethodSyntax)cs;
                sb.AppendTab().AppendTab().Append($"{GetModifiers(classSyntex)} {classSyntex.ReturnType} {classSyntex.Identifier}()").AppendLine();
                sb.AppendTab().AppendTab().AppendLine("{");

                foreach (var childNode in cs.ChildNodes) GenerateCode(sb, childNode);

                sb.AppendTab().AppendTab().AppendLine("}");
            }
        }
    }
}
