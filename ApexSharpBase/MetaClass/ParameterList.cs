namespace ApexSharpBase.MetaClass
{
    public class ParameterList : BaseSyntax
    {
        public ParameterList()
        {
            IsGneric = false;
        }

        public bool IsGneric { get; set; }
        public string Type { set; get; }
        public string Identifier { get; set; }
    }
}