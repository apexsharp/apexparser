namespace ApexSharpBase.Converter.CSharp
{
    using System.Text;
    using ApexSharpBase.MetaClass;

    public class CSharpGenerator : BaseVisitor
    {
        private StringBuilder Code { get; } = new StringBuilder();

        public static string Generate(ClassSyntax classSyntax)
        {
            var generator = new CSharpGenerator();
            classSyntax.Accept(generator);
            return generator.Code.ToString();
        }

        private int IndentLevel { get; set; }

        private int IndentSize { get; set; } = 4;

        private void Indent()
        {
            Code.Append(new string(' ', IndentLevel * IndentSize));
        }

        private void AppendIndented(string format, params string[] args)
        {
            Indent();
            Code.AppendFormat(format, args);
        }

        private void AppendIndentedLine(string format, params string[] args)
        {
            Indent();
            Code.AppendFormat(format, args);
            Code.AppendLine();
        }

        public override void VisitClassDeclaration(ClassSyntax cd)
        {
            //    AppendIndentedLine("class {0}", cd.ClassName);
            //    AppendIndentedLine("{{");

            //    IndentLevel++;
            //    foreach (var md in cd.Methods)
            //    {
            //        md.Accept(this);
            //    }

            //    IndentLevel--;
            //    AppendIndentedLine("}}");
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
