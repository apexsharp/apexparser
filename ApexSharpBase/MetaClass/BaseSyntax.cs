using System.Collections.Generic;

namespace ApexSharpBase.MetaClass
{
    public class BaseSyntax
    {
        public List<BaseSyntax> ChildNodes = new List<BaseSyntax>();

        public List<string> CodeComments = new List<string>();

        public int LineNumber { get; set; }

        public string Kind { get; set; }
    }
}
