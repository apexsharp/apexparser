using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class AnnotationSyntax : BaseSyntax
    {
        public override SyntaxType Kind => SyntaxType.Annotation;

        public string Identifier { get; set; }

        public string Parameters { get; set; }

        public override void Accept(ApexSyntaxVisitor visitor) => visitor.VisitAnnotation(this);
    }
}
