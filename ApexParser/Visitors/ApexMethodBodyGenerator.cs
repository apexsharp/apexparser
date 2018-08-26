using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Toolbox;

namespace ApexParser.Visitors
{
    public class ApexMethodBodyGenerator : ApexCodeGeneratorBase
    {
        public static string GenerateApex(MethodDeclarationSyntax ast, int tabSize = 4)
        {
            var generator = new ApexMethodBodyGenerator { IndentSize = tabSize };
            ast.Body.Accept(generator);
            return generator.Code.ToString();
        }

        private BlockSyntax CurrentBlock { get; set; }

        public override void VisitBlock(BlockSyntax node)
        {
            // don't generate the outermost braces
            var indented = default(IDisposable);
            if (CurrentBlock != null)
            {
                AppendLeadingComments(node);
                AppendIndentedLine("{{");
                indented = Indented();
            }

            // save the last block
            var oldCurrentBlock = CurrentBlock;
            CurrentBlock = node;
            EmptyLineIsRequired = false;

            // generate method body
            using (indented)
            {
                foreach (var st in node.Statements.AsSmart())
                {
                    if (EmptyLineIsRequired)
                    {
                        AppendLine();
                        EmptyLineIsRequired = false;
                    }
                    else if (!st.IsFirst && !st.Value.LeadingComments.IsNullOrEmpty())
                    {
                        AppendLine();
                    }

                    st.Value.Accept(this);
                }

                if (!node.Statements.IsNullOrEmpty() && !node.InnerComments.IsNullOrEmpty())
                {
                    AppendLine();
                }

                AppendComments(node.InnerComments);
            }

            if (oldCurrentBlock != null)
            {
                AppendIndented("}}");
                AppendTrailingComments(node);
            }

            CurrentBlock = oldCurrentBlock;
            EmptyLineIsRequired = true;
        }
    }
}
