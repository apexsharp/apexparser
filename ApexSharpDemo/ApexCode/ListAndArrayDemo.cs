namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class ListAndArrayDemo
    {
        public List<string> StringList = new List<string> { "one", "two" };

        public int[] IntegerArray = new int[] { 1, 2, 3 };

        public void Method()
        {
            List<string> stringListLocal = new List<string> { "one", "two" };
            int[] integerArrayLocal = new int[] { 1, 2, 3 };
        }
    }
}
