using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;

namespace ApexParser.Visitors
{
    public class ApexCodeGenerator : CodeGeneratorBase
    {
        public static string Generate(ClassDeclarationSyntax cd)
        {
            var generator = new ApexCodeGenerator();
            cd.Accept(generator);
            return generator.Code.ToString();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax cd)
        {
            AppendIndentedLine("class {0}", cd.Identifier);
            AppendIndentedLine("{{");

            using (Indented())
            {
                foreach (var md in cd.Methods)
                {
                    md.Accept(this);
                }
            }

            AppendIndentedLine("}}");
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax md)
        {
            AppendIndented("{0} {1}(", md.ReturnType.Identifier, md.Identifier);

            var last = md.Parameters.LastOrDefault();
            foreach (var p in md.Parameters)
            {
                p.Accept(this);
                if (p != last)
                {
                    Append(", ");
                }
            }

            AppendLine(")");
            AppendIndentedLine("{{");
            AppendIndentedLine("}}");
        }

        public override void VisitParameter(ParameterSyntax pd)
        {
            Code.AppendFormat("{0} {1}", pd.Type.Identifier, pd.Identifier);
        }
    }
}
