using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace ApexParser.Toolbox
{
    public class ParseExceptionCustom : ParseException
    {
        public string[] Apexcode { get; set; }
        public int LineNumber { get; set; }
        public ParseExceptionCustom(string message, int lineNumber, string[] apexcode)
            : base(message)
        {
            LineNumber = lineNumber;
            Apexcode = apexcode;
        }
    }
}
