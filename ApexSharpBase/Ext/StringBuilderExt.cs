using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSharpBase.Ext
{
    public static class StringBuilderExt
    {
        public static StringBuilder AppendSpace(this StringBuilder value)
        {
            return value.Append(" ");
        }

        public static StringBuilder AppendTab(this StringBuilder value)
        {
            return value.Append("\t");
        }


    }
}
