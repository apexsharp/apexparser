using System;
using System.Collections.Generic;
using System.Text;

namespace ApexParser.MetaClass
{
    public interface IAnnotatedSyntax
    {
        List<AnnotationSyntax> Annotations { get; }

        List<string> Modifiers { get; }
    }
}
