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

        private void AddQueries(string expr) => SoqlQueries.AddRange(ExtractQueries(expr));

        private void AddQueries(ExpressionSyntax expr) => AddQueries(expr?.Expression);

        public override void DefaultVisit(BaseSyntax node)
        {
            foreach (var child in node.ChildNodes)
            {
                child.Accept(this);
            }
        }

        public override void VisitExpression(ExpressionSyntax node) => AddQueries(node);

        public override void VisitStatement(StatementSyntax node) => AddQueries(node.Body);
    }
}
