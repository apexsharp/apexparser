namespace ApexSharpBase.MetaClass
{
    using System.Collections.Generic;

    public class BaseSyntax
    {
        public List<BaseSyntax> ChildNodes = new List<BaseSyntax>();

        public List<string> CodeComments = new List<string>();

        public int LineNumber { get; set; }

        public string Kind { get; set; }

        // The code block that belongs to this module.
        public string CodeBlock { get; set; }
    }
}
