using System;
using System.Linq;
using ApexSharpBase.Ext;

namespace ApexSharpBase.Converter.CSharp
{
    using System.Text;
    using ApexSharpBase.MetaClass;

    public class CSharpGenerator //: BaseVisitor
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



            foreach (var childNode in classContainer.ChildNodes)
            {
                if (childNode.Kind == SyntaxType.Class.ToString())
                {
                    VisitClassDeclaration(sb, (ClassSyntax)childNode);
                }
            }

            sb.Append("}");

            return sb.ToString();
        }

        private int IndentLevel { get; set; }

        private int IndentSize { get; set; } = 4;

        private void Indent()
        {
            //      Code.Append(new string(' ', IndentLevel * IndentSize));
        }

        private void AppendIndented(string format, params string[] args)
        {
            Indent();
            //     Code.AppendFormat(format, args);
        }

        private void AppendIndentedLine(string format, params string[] args)
        {
            Indent();
            //     Code.AppendFormat(format, args);
            //     Code.AppendLine();
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

        public void VisitClassDeclaration(StringBuilder sb, ClassSyntax cs)
        {

            sb.AppendTab().AppendLine().AppendLine($"[{GetAttributes(cs)}]");
            sb.AppendTab().AppendLine().AppendLine($"{GetModifiers(cs)} class {cs.Identifier}");
            sb.AppendTab().AppendLine("{");

            foreach (var childNode in cs.ChildNodes)
            {

                sb.AppendTab().AppendTab().AppendLine(childNode.Kind);

            }

            sb.AppendTab().AppendLine("}");
        }

        //public override void VisitMethodDeclaration(MethodSyntax md)
        //{
        //    AppendIndented("{0} {1}", md.ReturnType, md.MethodName);
        //    md.Parameters.Accept(this);

        //    Code.AppendLine();
        //    AppendIndentedLine("{{");
        //    AppendIndentedLine("}}");
        //}

        //public override void VisitMethodParameters(Parameter mp)
        //{
        //    Code.Append("(");

        //    var last = mp.Parameters.LastOrDefault();
        //    foreach (var pd in mp.Parameters)
        //    {
        //        pd.Accept(this);
        //        if (pd != last)
        //        {
        //            Code.Append(", ");
        //        }
        //    }

        //    Code.Append(")");
        //}

        //public override void VisitParameterDeclaration(ParameterDeclaration pd)
        //{
        //    Code.AppendFormat("{0} {1}", pd.ParameterType, pd.ParameterName);
        //}
    }
}
