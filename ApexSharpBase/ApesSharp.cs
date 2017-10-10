namespace ApexSharpBase
{
    using Formatter;
    using MetaClass;
    using Parser.CSharp;

    public class ApesSharp
    {

        // Format APEX code so it looks nice
        public string GetFormatedApexCode(string apexCode)
        {
            var apexCodeFormated = FormatApexCode.GetFormattedApexCode(apexCode);
            return apexCodeFormated;
        }

        // Convert C# to APEX
        public string ConvertCSharpToApex(string cSharpCode)
        {
            return "";
        }

        // Convert APEX to C#
        public string ConvertApexToCSharp(string apexCode)
        {
            return "";
        }

        // Parse C# Code and get an AST
        public ClassContainer ParseCSharpCode(string cSharpCode)
        {
            CSharpParser parser = new CSharpParser();
            return parser.ParseCSharpFromText(cSharpCode);
        }

        // Parse APEX Code and get an AST
        public ClassContainer ParseApexCode(string apexCode)
        {
            return new ClassContainer();
        }

    }
}
