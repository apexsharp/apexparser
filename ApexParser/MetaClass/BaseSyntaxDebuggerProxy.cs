using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApexParser.Visitors;

namespace ApexParser.MetaClass
{
    public class BaseSyntaxDebuggerProxy
    {
        public BaseSyntaxDebuggerProxy(BaseSyntax content) => Content = content;

        private BaseSyntax Content { get; }

        public string NodeType => Content.GetType().Name;

        public string ApexCode => Content.ToApex();

        public string CSharpCode => Content.ToCSharp();
    }
}
