namespace ApexSharpDemo.ApexCode
{
    using Apex.System;

    public class ListAndArrayDemo
    {
        public List<string> StringList = new List<string> { "one", "two" };
        public int[] IntegerArray = new int[] { 1, 2, 3 };


        public void Method()
        {
            List<string> StringListLocal = new List<string> { "one", "two" };
            int[] IntegerArrayLocal = new int[] { 1, 2, 3 };
        }
    }
}
