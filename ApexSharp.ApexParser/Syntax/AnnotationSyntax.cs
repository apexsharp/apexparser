using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Parser;
using ApexSharp.ApexParser.Visitors;

namespace ApexSharp.ApexParser.Syntax
{
    public class AnnotationSyntax : BaseSyntax
    {
        public AnnotationSyntax(string identifier = null, string parameters = null)
        {
            Identifier = identifier;
            Parameters = parameters;
        }

        public override SyntaxType Kind => SyntaxType.Annotation;

        public string Identifier { get; set; }

        public string Parameters { get; set; }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitAnnotation(this);

        public override IEnumerable<BaseSyntax> ChildNodes => NoChildren;

        public bool IsTest => ApexKeywords.UnitTestKeywords.Contains(Identifier);
    }
}
