
namespace ApexSharpDemo.ApexCode
{
    using Apex.System;
    public class Expression
    {
        public void MethodOne()
        {
            List<string> newList = new List<string>();
            newList.Add("Hi");
            string reply = MethodTwo();
            System.Debug("This is " + reply);
            MethodOne();
            Expression exp = 
                new Expression();
            exp.MethodOne();
            string replyTwo = exp.MethodTwo();
            System.Debug(replyTwo);
        }

        public string MethodTwo()
        {
            return "Hello World";
        }
    }
}
