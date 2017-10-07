namespace ApexSharpBase.MetaClass
{
    public class ApexParameterListSyntax : BaseSyntax
    {
        public ApexParameterListSyntax()
        {
            IsGneric = false;
        }

        public bool IsGneric { get; set; }
        public string Type { set; get; }
        public string Identifier { get; set; }
    }
}