namespace ApexSharpBase.MetaClass
{
    public class IfStatement : BaseSyntax
    {
        public IfStatement()
        {

        }

        public string Condition { get; set; }
        public ElseStatement ElseStatement { get; set; }


    }
}