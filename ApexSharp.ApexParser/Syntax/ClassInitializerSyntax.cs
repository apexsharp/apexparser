using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Parser;
using ApexSharp.ApexParser.Toolbox;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class ClassInitializerSyntax : MemberDeclarationSyntax
    {
        public ClassInitializerSyntax(MemberDeclarationSyntax heading = null)
            : base(heading)
        {
        }

        public override SyntaxType Kind => SyntaxType.ClassInitializer;

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitClassInitializer(this);

        public override IEnumerable<BaseSyntax> ChildNodes =>
            base.ChildNodes.Concat(GetNodes(Body));

        public BlockSyntax Body { get; set; }

        public bool IsStatic => Modifiers.EmptyIfNull().Any(m => m == ApexKeywords.Static);
    }
}
