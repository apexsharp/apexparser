using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Toolbox;

namespace ApexParser.Visitors
{
    public class CSharpCodeGenerator : CodeGeneratorBase
    {
        public string Namespace { get; set; } = "ApexSharpDemo.ApexCode";

        private bool HasRootNamespace => !string.IsNullOrWhiteSpace(Namespace);

        public List<string> Usings { get; set; } = new List<string>
        {
            "Apex.ApexSharp",
            "Apex.System",
            "SObjects",
        };

        public static string Generate(BaseSyntax ast)
        {
            var generator = new CSharpCodeGenerator();
            ast.Accept(generator);
            return generator.Code.ToString();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var unindent = default(IDisposable);
            if (HasRootNamespace)
            {
                AppendIndentedLine("namespace {0}", Namespace);
                AppendIndentedLine("{{");
                unindent = Indented();
            }

            foreach (var ns in Usings.AsSmart())
            {
                AppendIndentedLine("using {0};", ns.Value);
                if (ns.IsLast)
                {
                    AppendLine();
                }
            }

            AppendAttributesAndModifiers(node);
            AppendIndentedLine("class {0}", node.Identifier);
            AppendIndentedLine("{{");

            using (Indented())
            {
                foreach (var md in node.Methods.AsSmart())
                {
                    md.Value.Accept(this);
                    if (!md.IsLast)
                    {
                        AppendLine();
                    }
                }
            }

            AppendIndentedLine("}}");

            if (HasRootNamespace)
            {
                using (unindent)
                {
                    AppendIndentedLine("}}");
                }
            }
        }

        private void AppendAttributesAndModifiers(MemberDeclarationSyntax node)
        {
            foreach (var attribute in node.Attributes.AsSmart())
            {
                AppendIndentedLine("[{0}]", attribute.Value);
            }

            var indented = false;
            foreach (var modifier in node.Modifiers.AsSmart())
            {
                if (!indented)
                {
                    AppendIndent();
                    indented = true;
                }

                Append("{0} ", modifier.Value);
            }

            if (!indented)
            {
                AppendIndent();
                indented = true;
            }
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            AppendAttributesAndModifiers(node);
            Append("{0} {1}(", node.ReturnType.Identifier, node.Identifier);

            foreach (var p in node.Parameters.AsSmart())
            {
                p.Value.Accept(this);
                if (!p.IsLast)
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
            pd.Type.Accept(this);
            Append(" {0}", pd.Identifier);
        }

        public override void VisitType(TypeSyntax node)
        {
            foreach (var ns in node.Namespaces.AsSmart())
            {
                Append("{0}.", ns.Value);
            }

            Append(node.Identifier);

            foreach (var type in node.TypeParameters.AsSmart())
            {
                if (type.IsFirst)
                {
                    Append("<");
                }

                type.Value.Accept(this);

                if (type.IsFirst)
                {
                    Append(">");
                }
            }

            if (node.IsArray)
            {
                Append("[]");
            }
        }

        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            AppendIndentedLine("break;");
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            AppendIndentedLine("if ({0})", node.Expression);
            if (node.ThenStatement != null)
            {
                using (Indented())
                {
                    node.ThenStatement.Accept(this);
                }
            }

            if (node.ElseStatement != null)
            {
                AppendIndented("else");

                if (node.ElseStatement is IfStatementSyntax ifStmt)
                {
                    // support "else if" style formatting
                    Append(" ");
                    ifStmt.Accept(this);
                }
                else
                {
                    AppendLine();
                    using (Indented())
                    {
                        node.ElseStatement.Accept(this);
                    }
                }
            }
        }

        public override void VisitStatement(StatementSyntax node)
        {
            if (!string.IsNullOrWhiteSpace(node.Body))
            {
                AppendIndentedLine(node.Body);
            }
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            AppendIndent();

            node.Type.Accept(this);
            Append(" ");

            foreach (var var in node.Variables.AsSmart())
            {
                var.Value.Accept(this);
                if (!var.IsLast)
                {
                    Append(", ");
                }
            }

            AppendLine(";");
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            Append(node.Identifier);

            if (!string.IsNullOrWhiteSpace(node.Expression))
            {
                Append(" = {0}", node.Expression);
            }
        }

        public override void VisitForStatement(ForStatementSyntax node)
        {
            using (SkipNewLines())
            {
                AppendIndentedLine("for (");
                if (node.Declaration != null)
                {
                    node.Declaration.Accept(this);
                }
                else
                {
                    Append(";");
                }

                if (!string.IsNullOrWhiteSpace(node.Condition))
                {
                    Append(" {0};", node.Condition);
                }
                else
                {
                    Append(";");
                }

                foreach (var inc in node.Incrementors.AsSmart())
                {
                    Append(" {0}", inc.Value);
                    if (!inc.IsLast)
                    {
                        Append(",");
                    }
                }

                Append(")");
            }

            AppendLine();
            using (Indented())
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            using (SkipNewLines())
            {
                AppendIndentedLine("foreach (");
                if (node.Type != null)
                {
                    node.Type.Accept(this);
                }

                if (!string.IsNullOrWhiteSpace(node.Identifier))
                {
                    Append(" {0} in ", node.Identifier);
                }

                Append("{0})", node.Expression);
            }

            AppendLine();
            using (Indented())
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitBlock(BlockSyntax node)
        {
            AppendIndentedLine("{{");

            using (Indented())
            {
                foreach (var st in node.Statements.AsSmart())
                {
                    st.Value.Accept(this);
                }
            }

            AppendIndentedLine("}}");
        }
    }
}
