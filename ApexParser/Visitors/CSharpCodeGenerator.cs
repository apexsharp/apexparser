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
    public class CSharpCodeGenerator : ApexCodeGeneratorBase
    {
        public string Namespace { get; set; } = "ApexSharpDemo.ApexCode";

        private bool HasRootNamespace => !string.IsNullOrWhiteSpace(Namespace);

        public const string Soql = "Soql";

        public List<string> Usings { get; set; } = new List<string>
        {
            "Apex.ApexSharp",
            "Apex.System",
            "SObjects",
        };

        public static string GenerateCSharp(BaseSyntax ast, int tabSize = 4)
        {
            var generator = new CSharpCodeGenerator { IndentSize = tabSize };
            ast.Accept(generator);
            return generator.Code.ToString();
        }

        protected override void AppendClassDeclaration(ClassDeclarationSyntax node, string classOrInterface = "class")
        {
            var optionalIndent = default(IDisposable);
            var appendNamespace = IsTopLevelDeclaration && HasRootNamespace;
            if (appendNamespace)
            {
                AppendIndentedLine("namespace {0}", Namespace);
                AppendIndentedLine("{{");
                optionalIndent = Indented();
            }

            using (optionalIndent)
            {
                if (IsTopLevelDeclaration)
                {
                    foreach (var ns in Usings.AsSmart())
                    {
                        AppendIndentedLine("using {0};", ns.Value);
                        if (ns.IsLast)
                        {
                            AppendLine();
                        }
                    }
                }

                AppendCommentsAttributesAndModifiers(node);
                Append("{0} {1}", classOrInterface, node.Identifier);

                // base type and all interfaces are merged into one comma-separated list
                var baseTypes = Enumerable.Repeat(node.BaseType, 1).Concat(node.Interfaces.EmptyIfNull());
                foreach (var baseType in baseTypes.Where(t => t != null).AsSmart())
                {
                    if (baseType.IsFirst)
                    {
                        Append(" : ");
                    }

                    baseType.Value.Accept(this);
                    if (!baseType.IsLast)
                    {
                        Append(", ");
                    }
                }

                AppendLine();
                AppendClassMembers(node);
            }

            if (appendNamespace)
            {
                AppendIndentedLine("}}");
            }
        }

        protected override string ConvertModifier(string modifier, BaseSyntax ownerNode)
        {
            if (modifier == ApexKeywords.Final)
            {
                if (ownerNode is ClassDeclarationSyntax || ownerNode is MethodDeclarationSyntax)
                {
                    return "sealed";
                }
                else if (ownerNode is FieldDeclarationSyntax)
                {
                    return "readonly";
                }
                else
                {
                    return "/* final */";
                }
            }
            else if (modifier == ApexKeywords.Global ||
                modifier.EndsWith(ApexKeywords.Sharing) ||
                modifier == ApexKeywords.TestMethod ||
                modifier == ApexKeywords.Transient ||
                modifier == ApexKeywords.WebService)
            {
                return $"/* {modifier} */";
            }

            return modifier;
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            AppendCommentsAttributesAndModifiers(node);
            Append("{0}(", node.ReturnType?.Identifier ?? node.Identifier);
            AppendMethodParametersAndBody(node);
        }

        private HashSet<ClassDeclarationSyntax> GeneratedInitializers { get; } =
            new HashSet<ClassDeclarationSyntax>();

        public override void VisitClassInitializer(ClassInitializerSyntax node)
        {
            // generate class initializers once per class
            var currentClass = CurrentMember as ClassDeclarationSyntax;
            if (!GeneratedInitializers.Add(currentClass))
            {
                return;
            }

            // TODO: generate instance initializer
            var initializers = currentClass?.Members?.EmptyIfNull().OfType<ClassInitializerSyntax>();
            GenerateStaticConstructor(currentClass, initializers.Where(init => init.IsStatic));
        }

        private void GenerateStaticConstructor(ClassDeclarationSyntax @class, IEnumerable<ClassInitializerSyntax> initializers)
        {
            if (initializers.IsNullOrEmpty())
            {
                return;
            }

            var nodes = initializers.ToArray();
            var node = initializers.First();
            AppendCommentsAttributesAndModifiers(node);
            AppendLine("{0}()", @class.Identifier);
            if (nodes.Length == 1)
            {
                node.Body.Accept(this);
                return;
            }

            // merge all bodies into one block
            var statements =
                from n in nodes
                let block = new BlockSyntax
                {
                    LeadingComments = n.LeadingComments,
                    Statements = n.Body.Statements,
                    TrailingComments = n.Body.TrailingComments,
                }
                select block as StatementSyntax;

            new BlockSyntax(statements).Accept(this);
        }

        public override void VisitAnnotation(AnnotationSyntax node)
        {
            AppendIndentedLine("[{0}{1}]", node.Identifier, node.Parameters);
        }

        protected override void AppendMethodParametersAndBody(MethodDeclarationSyntax node)
        {
            // C# version skips the parameter modifiers
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

                node.Expression.Accept(this);
                Append(")");
            }

            AppendLine();
            AppendStatementWithOptionalIndent(node.Statement);
        }

        public override void VisitInsertStatement(InsertStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.Insert(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        public override void VisitUpdateStatement(UpdateStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.Update(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        public override void VisitDeleteStatement(DeleteStatementSyntax node)
        {
            AppendLeadingComments(node);
            AppendIndented("{0}.Delete(", Soql);
            node.Expression.Accept(this);
            Append(");");
            AppendTrailingComments(node);
        }

        protected override void AppendStringLiteral(string literal) =>
            Append("\"{0}\"", literal.Substring(1, literal.Length - 2));

        protected override void AppendSoqlQuery(string soqlQuery) =>
            Append("Soql.Query<T>(\"{0}\")", soqlQuery.Substring(1, soqlQuery.Length - 2));
    }
}
