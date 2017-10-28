using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Toolbox;

namespace ApexParser.Visitors
{
    public class ApexCodeGenerator : CSharpCodeGenerator
    {
        public static string GenerateApex(BaseSyntax ast, int tabSize = 4)
        {
            var generator = new ApexCodeGenerator { IndentSize = tabSize };
            ast.Accept(generator);
            return generator.Code.ToString();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            AppendCommentsAttributesAndModifiers(node);
            AppendLine("class {0}", node.Identifier);
            AppendIndentedLine("{{");

            using (Indented())
            {
                foreach (var md in node.Members.AsSmart())
                {
                    md.Value.Accept(this);
                    if (!md.IsLast)
                    {
                        AppendLine();
                    }
                }
            }

            AppendIndentedLine("}}");
        }
    }
}
