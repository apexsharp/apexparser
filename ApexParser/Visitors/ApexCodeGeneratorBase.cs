using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser.MetaClass;
using ApexParser.Parser;
using ApexParser.Toolbox;

namespace ApexParser.Visitors
{
    public class ApexCodeGeneratorBase : CodeGeneratorBase
    {
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

            AppendIndented("}}");
            AppendTrailingComments(node);
        }

        public override void VisitAnnotation(AnnotationSyntax node)
        {
            if (node.Parameters != null)
            {
                AppendIndentedLine("@{0}({1})", node.Identifier, node.Parameters);
            }
            else
            {
                AppendIndentedLine("@{0}", node.Identifier);
            }
        }

        protected virtual void AppendLeadingComments(BaseSyntax node) =>
            AppendComments(node?.LeadingComments);

        protected virtual void AppendTrailingComments(BaseSyntax node) =>
            AppendComments(node?.TrailingComments, indentFirst: false);

        protected virtual void AppendComments(IEnumerable<string> comments, bool indentFirst = true)
        {
            if (comments.IsNullOrEmpty())
            {
                if (!indentFirst)
                {
                    AppendLine();
                }

                return;
            }

            foreach (var comment in comments.AsSmart())
            {
                void EmitLine(string format, params string[] args)
                {
                    if (indentFirst || !comment.IsFirst)
                    {
                        AppendIndentedLine(format, args);
                    }
                    else
                    {
                        AppendLine(" " + format, args);
                    }
                }

                var multiLine = comment.Value.Contains('\n');
                if (multiLine)
                {
                    var lines = comment.Value.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                    foreach (var line in lines.AsSmart())
                    {
                        var format = line.IsFirst ? "/*{0}" : line.IsLast ? "{0}*/" : "{0}";
                        var value = Regex.Replace(line.Value, @"^\s+|\s+$", " ");
                        EmitLine(format, value);
                    }
                }
                else
                {
                    EmitLine("//{0}", comment.Value.TrimEnd());
                }
            }
        }

        protected virtual void AppendCommentsAttributesAndModifiers(MemberDeclarationSyntax node)
        {
            AppendLeadingComments(node);
            foreach (var annotation in node.Annotations.AsSmart())
            {
                annotation.Value.Accept(this);
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
            AppendMethodParametersAndBody(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            AppendCommentsAttributesAndModifiers(node);
            Append("{0}(", node.ReturnType?.Identifier ?? node.Identifier);
            AppendMethodParametersAndBody(node);
        }

        protected virtual void AppendMethodParametersAndBody(MethodDeclarationSyntax node)
        {
            foreach (var p in node.Parameters.AsSmart())
            {
                p.Value.Accept(this);
                if (!p.IsLast)
                {
                    Append(", ");
                }
            }

            Append(")");

            if (node.Body != null)
            {
                // non-abstract method
                AppendLine();
                node.Body.Accept(this);
            }
            else
            {
                // abstract method
                Append(";");
                AppendTrailingComments(node);
            }
        }

        public override void VisitParameter(ParameterSyntax pd)
        {
            pd.Type.Accept(this);
            Append(" {0}", pd.Identifier);
        }

        protected virtual HashSet<string> SystemClasses =>
            new HashSet<string> { ApexKeywords.Map, ApexKeywords.List, ApexKeywords.Set };

        public virtual string NormalizeTypeName(string id) =>
            SystemClasses.Contains(id) ?
                id.Substring(0, 1).ToUpper() + id.Substring(1) : id;

        public override void VisitType(TypeSyntax node)
        {
            foreach (var ns in node.Namespaces.AsSmart())
            {
                Append("{0}.", ns.Value);
            }

            // normalize type names like Map<...>, List<...> and Set<...>
            Append(NormalizeTypeName(node.Identifier));

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
            AppendLeadingComments(node);
            AppendIndented("break;");
            AppendTrailingComments(node);
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            AppendLeadingComments(node);
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
            AppendLeadingComments(node);
            if (!string.IsNullOrWhiteSpace(node.Body))
            {
                AppendIndented(node.Body);
            }

            Append(";");
            AppendTrailingComments(node);
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            AppendLeadingComments(node);
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

            Append(";");
            AppendTrailingComments(node);
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
            AppendLeadingComments(node);
            AppendIndented("for (");
            using (SkipNewLines(replaceWithSpace: false))
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
            AppendLeadingComments(node);
            AppendIndented("for (");
            using (SkipNewLines())
            {
                if (node.Type != null)
                {
                    node.Type.Accept(this);
                }

                if (!string.IsNullOrWhiteSpace(node.Identifier))
                {
                    Append(" {0} : ", node.Identifier);
                }

                Append("{0})", node.Expression);
            }

            AppendLine();
            AppendStatementWithOptionalIndent(node.Statement);
        }

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndentedLine("do");
            AppendStatementWithOptionalIndent(node.Statement);
            AppendIndented("while ({0});", node.Expression);
            AppendTrailingComments(node);
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndentedLine("while ({0})", node.Expression);
            AppendStatementWithOptionalIndent(node.Statement);
        }

        protected virtual void AppendStatementWithOptionalIndent(StatementSyntax node)
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
            AppendLeadingComments(node);
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

            AppendIndented("}}");
            AppendTrailingComments(node);
            EmptyLineIsRequired = true;
        }

        public override void VisitInsertStatement(InsertStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("insert {0};", node.Expression);
            AppendTrailingComments(node);
        }

        public override void VisitUpdateStatement(UpdateStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("update {0};", node.Expression);
            AppendTrailingComments(node);
        }

        public override void VisitDeleteStatement(DeleteStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("delete {0};", node.Expression);
            AppendTrailingComments(node);
        }

        public override void VisitAccessor(AccessorDeclarationSyntax node)
        {
            AppendIndent();
            foreach (var mod in node.Modifiers)
            {
                Append("{0} ", mod);
            }

            Append(node.IsGetter ? "get" : "set");
            if (node.IsEmpty)
            {
                Append(";");
                AppendTrailingComments(node);
            }
            else
            {
                AppendLine();
                node.Body.Accept(this);
            }
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            AppendCommentsAttributesAndModifiers(node);
            node.Type?.Accept(this);
            Append(" {0}", node.Identifier);

            // put empty accessors on the same line
            var noNewLines = node.Accessors.All(acc => acc.IsEmpty);
            using (noNewLines ? SkipNewLines() : null)
            {
                AppendLine();
                AppendIndentedLine("{{");

                using (Indented())
                {
                    node.Getter?.Accept(this);
                    node.Setter?.Accept(this);
                }

                AppendIndented("}}");
            }

            AppendTrailingComments(node);
        }
    }
}
