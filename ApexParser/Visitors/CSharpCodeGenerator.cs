using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var optionalIndent = default(IDisposable);
            if (HasRootNamespace)
            {
                AppendIndentedLine("namespace {0}", Namespace);
                AppendIndentedLine("{{");
                optionalIndent = Indented();
            }

            using (optionalIndent)
            {
                foreach (var ns in Usings.AsSmart())
                {
                    AppendIndentedLine("using {0};", ns.Value);
                    if (ns.IsLast)
                    {
                        AppendLine();
                    }
                }

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

            if (HasRootNamespace)
            {
                AppendIndentedLine("}}");
            }
        }

        private void AppendCommentsAttributesAndModifiers(MemberDeclarationSyntax node)
        {
            foreach (var comment in node.LeadingComments.AsSmart())
            {
                var multiLine = comment.Value.Contains('\n');
                if (multiLine)
                {
                    var lines = comment.Value.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                    foreach (var line in lines.AsSmart())
                    {
                        var format = line.IsFirst ? "/*{0}" : line.IsLast ? "{0}*/" : "{0}";
                        var value = Regex.Replace(line.Value, @"^\s+|\s+$", " ");
                        AppendIndentedLine(format, value);
                    }
                }
                else
                {
                    AppendIndentedLine("//{0}", comment.Value.TrimEnd());
                }
            }

            foreach (var attribute in node.Annotations.AsSmart())
            {
                AppendIndentedLine("[{0}{1}]", attribute.Value.Identifier, attribute.Value.Parameters);
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
            AppendCommentsAttributesAndModifiers(node);
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

            if (node.Body != null)
            {
                node.Body.Accept(this);
            }
            else
            {
                AppendIndentedLine("{{");
                AppendIndentedLine("}}");
            }
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
                AppendStatementWithOptionalIndent(node.ThenStatement);
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
                    AppendStatementWithOptionalIndent(node.ElseStatement);
                }
            }
        }

        public override void VisitStatement(StatementSyntax node)
        {
            if (!string.IsNullOrWhiteSpace(node.Body))
            {
                AppendIndented(node.Body);
            }

            AppendLine(";");
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
            AppendIndented("for (");
            using (SkipNewLines())
            {
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
            AppendStatementWithOptionalIndent(node.Statement);
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            AppendIndented("foreach (");
            using (SkipNewLines())
            {
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
            AppendStatementWithOptionalIndent(node.Statement);
        }

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            AppendIndentedLine("do");
            AppendStatementWithOptionalIndent(node.Statement);
            AppendIndentedLine("while ({0});", node.Expression);
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            AppendIndentedLine("while ({0})", node.Expression);
            AppendStatementWithOptionalIndent(node.Statement);
        }

        private void AppendStatementWithOptionalIndent(StatementSyntax node)
        {
            var optionalIndent = default(IDisposable);
            if (!(node is BlockSyntax))
            {
                optionalIndent = Indented();
            }

            using (optionalIndent)
            {
                node.Accept(this);
            }
        }

        private bool EmptyLineIsRequired { get; set; }

        public override void VisitBlock(BlockSyntax node)
        {
            AppendIndentedLine("{{");
            EmptyLineIsRequired = false;

            using (Indented())
            {
                foreach (var st in node.Statements.AsSmart())
                {
                    if (EmptyLineIsRequired)
                    {
                        AppendLine();
                        EmptyLineIsRequired = false;
                    }

                    st.Value.Accept(this);
                }
            }

            AppendIndentedLine("}}");
            EmptyLineIsRequired = true;
        }

        public override void VisitInsertStatement(InsertStatementSyntax node)
        {
            AppendIndentedLine("SOQL.Insert({0});", node.Expression);
        }

        public override void VisitUpdateStatement(UpdateStatementSyntax node)
        {
            AppendIndentedLine("SOQL.Update({0});", node.Expression);
        }

        public override void VisitDeleteStatement(DeleteStatementSyntax node)
        {
            AppendIndentedLine("SOQL.Delete({0});", node.Expression);
        }
    }
}
