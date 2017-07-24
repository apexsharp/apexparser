namespace Apex.ApexSharp.ApexToSharp
{
    public class ApexTocken
    {
        public ApexTocken(TockenType tockenType)
        {
            TockenType = tockenType;
        }

        public ApexTocken(TockenType tockenType, string tocken)
        {
            TockenType = tockenType;
            Tocken = tocken;
        }

        public TockenType TockenType { set; get; }
        public string Tocken { get; set; }

        public override string ToString() => TockenType.ToString().PadRight(25, ' ') + Tocken.Trim();
    }
}