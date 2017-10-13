using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.ParserAst;

namespace ApexParser.Visitors
{
    public class VisualBasicCodeGenerator : BaseVisitor
    {
        private StringBuilder Code { get; } = new StringBuilder();

        public static string Generate(ClassDeclaration cd)
        {
            var generator = new VisualBasicCodeGenerator();
            cd.Accept(generator);
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

        public override void VisitClassDeclaration(ClassDeclaration cd)
        {
            AppendIndentedLine("Class {0}", cd.ClassName);

            IndentLevel++;
            foreach (var md in cd.Methods)
            {
                md.Accept(this);
            }

            IndentLevel--;
            AppendIndentedLine("End Class");
        }

        public override void VisitMethodDeclaration(MethodDeclaration md)
        {
            AppendIndented("Sub {0}", md.MethodName);
            md.Parameters.Accept(this);

            Code.AppendLine();
            AppendIndentedLine("End Sub");
        }

        public override void VisitMethodParameters(MethodParameters mp)
        {
            Code.Append("(");

            var last = mp.Parameters.LastOrDefault();
            foreach (var pd in mp.Parameters)
            {
                pd.Accept(this);
                if (pd != last)
                {
                    Code.Append(", ");
                }
            }

            Code.Append(")");
        }

        public override void VisitParameterDeclaration(ParameterDeclaration pd)
        {
            Code.AppendFormat("{1} As {0}", pd.ParameterType, pd.ParameterName);
        }
    }
}
