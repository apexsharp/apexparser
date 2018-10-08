using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParser
{
    public class ApexSharpParserOptions
    {
        public string Namespace { get; set; } = "ApexSharpDemo.ApexCode";

        public int TabSize { get; set; } = 4;

        public bool UseLocalSObjectsNamespace { get; set; } = true;
    }
}
