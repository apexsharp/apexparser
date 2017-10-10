namespace ApexSharpBase
{
    using Formatter;
    using MetaClass;
    using Parser.CSharp;

    public class ApesSharp
    {
        public string GetFormatedApexCode(string apexCode)
        {
            var apexCodeFormated = FormatApexCode.GetFormattedApexCode(apexCode);
            return apexCodeFormated;
        }

        public string ConvertCSharpToApex(string cSharpCode)
        {
            return "";
        }

        public string ConvertApexToCSharp(string apexCode)
        {
            return "";
        }

        public ClassContainer ParseCSharpFromText(string cSharpCode)
        {
            CSharpParser parser = new CSharpParser();
            return parser.ParseCSharpFromText(cSharpCode);
        }

        public ClassContainer ParseApexFromText(string apexCode)
        {
            return new ClassContainer();
        }

    }
}
