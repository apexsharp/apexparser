using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class VariableDeclarationSyntax : StatementSyntax
    {
        public override SyntaxType Kind => SyntaxType.VariableDeclaration;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitVariableDeclaration(this);

        public override IEnumerable<BaseSyntax> ChildNodes =>
            GetNodes(Type).Concat(Variables).Where(n => n != null);

        public TypeSyntax Type { get; set; }

        public List<VariableDeclaratorSyntax> Variables { get; set; } = new List<VariableDeclaratorSyntax>();
    }
}
