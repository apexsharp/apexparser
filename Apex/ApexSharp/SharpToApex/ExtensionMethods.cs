using System;
using System.Text;

namespace Apex.ApexSharp.SharpToApex
{
    public static class ExtensionMethods
    {
        public static StringBuilder AppendSpace(this StringBuilder value)
        {
            return value.Append(" ");
        }

        public static StringBuilder AppendTab(this StringBuilder value)
        {
            return value.Append("\t");
        }


        /// <summary>
        /// Get the string slice between the two indexes.
        /// Inclusive for start index, exclusive for end index.
        /// </summary>
        public static string Slice(this string source, int start, int end)
        {
            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }
            int len = end - start; // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}