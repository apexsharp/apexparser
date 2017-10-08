namespace ApexSharpBase.MetaClass
{
    public class CatchClause : BaseSyntax
    {
        public CatchClause()
        {
            Kind = SyntaxType.CatchClauseSyntax.ToString();
        }

        public string Type { set; get; }

        public string Identifier { get; set; }

    }
}