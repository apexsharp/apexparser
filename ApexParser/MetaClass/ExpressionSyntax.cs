using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;
using Sprache;

namespace ApexParser.MetaClass
{
    public class ExpressionSyntax : BaseSyntax
    {
        public ExpressionSyntax()
        {
        }

        public ExpressionSyntax(string expr) => ExpressionString = expr;

        public static ExpressionSyntax CreateOrDefault(IOption<string> expression)
        {
            if (expression.IsDefined)
            {
                return new ExpressionSyntax(expression.Get());
            }

            return null;
        }

        public override SyntaxType Kind => SyntaxType.Expression;

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitExpression(this);

        public string ExpressionString { get; set; }
    }
}
