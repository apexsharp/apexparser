namespace ApexSharpBase.MetaClass
{
    public class ReturnStatement : BaseSyntax
    {
        public ReturnStatement()
        {
            Kind = SyntaxType.ReturnStatement.ToString();
        }

        public string Expression { get; set; }


    }
}