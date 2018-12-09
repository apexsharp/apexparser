using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class VariableDeclaratorSyntax : BaseSyntax
    {
        public override SyntaxType Kind => SyntaxType.VariableDeclarator;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitVariableDeclarator(this);

        public override IEnumerable<BaseSyntax> ChildNodes => GetNodes(Expression);

        public string Identifier { get; set; }

        public ExpressionSyntax Expression { get; set; }
    }
}
