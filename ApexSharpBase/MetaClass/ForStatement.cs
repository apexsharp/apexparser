namespace ApexSharpBase.MetaClass
{
    public class ForStatement : BaseSyntax
    {
        public ForStatement()
        {

        }

        public string Declaration { get; set; }
        public string Condition { get; set; }
        public string Incrementors { get; set; }
    }
}