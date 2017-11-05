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

        private IEnumerable<string> ExtractQueries(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return new string[0];
            }

            return SoqlRegex.Matches(expression).OfType<Match>().Select(m => m.Value);
        }

        public override void DefaultVisit(BaseSyntax node)
        {
            foreach (var child in node.ChildNodes)
            {
                child.Accept(this);
            }
        }

        public override void VisitFieldDeclarator(FieldDeclaratorSyntax node) =>
            SoqlQueries.AddRange(ExtractQueries(node.Expression));

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node) =>
            SoqlQueries.AddRange(ExtractQueries(node.Expression));

        public override void VisitStatement(StatementSyntax node) =>
            SoqlQueries.AddRange(ExtractQueries(node.Body));

        public override void VisitDeleteStatement(DeleteStatementSyntax node) =>
            SoqlQueries.AddRange(ExtractQueries(node.Expression));

        public override void VisitUpdateStatement(UpdateStatementSyntax node) =>
            SoqlQueries.AddRange(ExtractQueries(node.Expression));

        public override void VisitInsertStatement(InsertStatementSyntax node) =>
            SoqlQueries.AddRange(ExtractQueries(node.Expression));

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            base.VisitDoStatement(node);
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            base.VisitWhileStatement(node);
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            base.VisitForEachStatement(node);
        }

        public override void VisitRunAsStatement(RunAsStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Expression));
            base.VisitRunAsStatement(node);
        }

        public override void VisitForStatement(ForStatementSyntax node)
        {
            SoqlQueries.AddRange(ExtractQueries(node.Condition));
            foreach (var inc in node.Incrementors.EmptyIfNull())
            {
                SoqlQueries.AddRange(ExtractQueries(inc));
            }

            base.VisitForStatement(node);
        }
    }
}
