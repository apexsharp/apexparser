namespace Apex.ApexSharp.SharpToApex
{
    public class Type
    {
        public Type(string apexType, string cSharpType)
        {
            ApexType = apexType;
            CSharpType = cSharpType;
        }
        public string ApexType { get; set; }
        public string CSharpType { get; set; }
    }
}