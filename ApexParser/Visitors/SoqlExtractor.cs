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
    public class SoqlExtractor : ApexSyntaxVisitor
    {
        private static Regex SoqlRegex { get; } =
            new Regex(@"\[\s*(?i:select|find).*?\]", RegexOptions.Singleline);

        public static string[] ExtractAllQueries(string apexCode)
        {
            var apexAst = ApexParser.GetApexAst(apexCode);
            var visitor = new SoqlExtractor();
            apexAst.Accept(visitor);
            return visitor.SoqlQueries.ToArray();
        }

        private List<string> SoqlQueries { get; } = new List<string>();

        public override void VisitFieldDeclarator(FieldDeclaratorSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
        }

        public override void VisitStatement(StatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Body));
        }

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            if (node.Statement != null)
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            if (node.Statement != null)
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            if (node.Statement != null)
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitForStatement(ForStatementSyntax node)
        {
            foreach (var decl in node.Declaration?.Variables.EmptyIfNull())
            {
                decl.Accept(this);
            }

            SoqlQueries.AddRange(ExtractQueries(node.Condition));
            foreach (var inc in node.Incrementors.EmptyIfNull())
            {
                SoqlQueries.AddRange(ExtractQueries(inc));
            }

            if (node.Statement != null)
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitRunAsStatement(RunAsStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            if (node.Statement != null)
            {
                node.Statement.Accept(this);
            }
        }

        public override void VisitDeleteStatement(DeleteStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
        }

        public override void VisitUpdateStatement(UpdateStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
        }

        public override void VisitInsertStatement(InsertStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
        }

        private IEnumerable<string> ExtractQueries(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return new string[0];
            }

            return SoqlRegex.Matches(expression).OfType<Match>().Select(m => m.Value);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            foreach (var member in node.Members.EmptyIfNull())
            {
                member.Accept(this);
            }
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            foreach (var statement in node.Body.EmptyIfNull())
            {
                statement.Accept(this);
            }
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            foreach (var statement in node.Body.EmptyIfNull())
            {
                statement.Accept(this);
            }
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            foreach (var field in node.Fields.EmptyIfNull())
            {
                field.Accept(this);
            }
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            foreach (var var in node.Variables.EmptyIfNull())
            {
                var.Accept(this);
            }
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            foreach (var stmt in new[] { node.ThenStatement, node.ElseStatement }.Where(st => st != null))
            {
                stmt.Accept(this);
            }
        }

        public override void VisitTryStatement(TryStatementSyntax node)
        {
            if (node.Block != null)
            {
                node.Block.Accept(this);
            }

            foreach (var @catch in node.Catches.EmptyIfNull())
            {
                @catch?.Block?.Accept(this);
            }

            if (node.Finally != null)
            {
                node.Finally?.Block?.Accept(this);
            }
        }

        public override void VisitBlock(BlockSyntax node)
        {
            foreach (var stmt in node.Statements.EmptyIfNull())
            {
                stmt.Accept(this);
            }
        }
    }
}
