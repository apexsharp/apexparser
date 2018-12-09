using System;
using System.Collections.Generic;
using System.Text;

namespace ApexSharp.ApexParser.Syntax
{
    public interface IAnnotatedSyntax
    {
        List<AnnotationSyntax> Annotations { get; }

        List<string> Modifiers { get; }
    }
}
